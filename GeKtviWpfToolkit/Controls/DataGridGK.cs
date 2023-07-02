using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GeKtviWpfToolkit.ValueConverters;
using System.Linq;
using System;
using System.Data.Common;
using System.Data;
using System.Dynamic;
using System.Windows.Markup;

namespace GeKtviWpfToolkit.Controls
{
    public class DataGridGK : DataGrid
    {
        /// <summary>
        /// Some solution for fix performance issue
        /// For work must be set ItemsSource
        /// </summary>
        public static readonly DependencyProperty UseDirectPasteProperty =
                DependencyProperty.Register(
                      name: nameof(UseDirectPaste),
                      propertyType: typeof(bool),
                      ownerType: typeof(DataGridGK),
                      typeMetadata: new FrameworkPropertyMetadata(
                          defaultValue: false,
                          flags: FrameworkPropertyMetadataOptions.AffectsRender
                          )
                    );

        public bool UseDirectPaste
        {
            get => (bool)GetValue(UseDirectPasteProperty);
            set => SetValue(UseDirectPasteProperty, value);
        }



        public event ExecutedRoutedEventHandler ExecutePasteEvent;
        public event CanExecuteRoutedEventHandler CanExecutePasteEvent;
        private HoldButtonPress _HoldPressForDrag;

        public DataGridGK()
        {
            RegisterCommands();
            CreateContextMenu();
            _HoldPressForDrag = new HoldButtonPress(this, HoldForDragTime);
            _HoldPressForDrag.HoldPressSuccess += StartDrag;
        }

        #region On create

        protected virtual void RegisterCommands()
        {
            CommandManager.RegisterClassCommandBinding(
                typeof(DataGridGK),
                new CommandBinding(ApplicationCommands.SelectAll,
                    new ExecutedRoutedEventHandler(OnExecutedSelectAllInternal),
                    new CanExecuteRoutedEventHandler(OnCanExecuteSelectAllInternal)));

            CommandManager.RegisterClassCommandBinding(
                typeof(DataGridGK),
                new CommandBinding(ApplicationCommands.Paste,
                    new ExecutedRoutedEventHandler(OnExecutedPasteInternal),
                    new CanExecuteRoutedEventHandler(OnCanExecutePasteInternal)));

            CommandManager.RegisterClassCommandBinding(
                typeof(DataGridGK),
                new CommandBinding(ApplicationCommands.Delete,
                    new ExecutedRoutedEventHandler(OnExecutedDeleteInternal),
                    new CanExecuteRoutedEventHandler(OnCanExecuteDeleteInternal)));
        }

        protected virtual void CreateContextMenu()
        {
            ContextMenu = new ContextMenu();

            MenuItem copy = new MenuItem();
            copy.Command = ApplicationCommands.Copy;
            ContextMenu.Items.Add(copy);

            MenuItem paste = new MenuItem();
            paste.Command = ApplicationCommands.Paste;
            ContextMenu.Items.Add(paste);

            MenuItem selectAll = new MenuItem();
            selectAll.Command = ApplicationCommands.SelectAll;
            ContextMenu.Items.Add(selectAll);

            MenuItem delete = new MenuItem();
            delete.Command = ApplicationCommands.Delete;
            ContextMenu.Items.Add(delete);

            Binding pasteBinding = new Binding(nameof(IsReadOnly));
            pasteBinding.Source = this;
            pasteBinding.Mode = BindingMode.OneWay;
            pasteBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            pasteBinding.Converter = new BooleanToVisibilityConverterReverse();
            paste.SetBinding(VisibilityProperty, pasteBinding);

            Binding deleteBinding = new Binding(nameof(CanUserDeleteRows));
            deleteBinding.Source = this;
            deleteBinding.Mode = BindingMode.OneWay;
            deleteBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            deleteBinding.Converter = new BooleanToVisibilityConverter();
            delete.SetBinding(VisibilityProperty, deleteBinding);
        }

        #endregion

        #region Behaivior fixes

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e) //If ui have more then one datagrid provides context menu opening for mouse selected 
        {
            base.OnMouseRightButtonDown(e);
            this.Focus();
        }

        #endregion

        #region GeneratingColumn

        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            base.OnAutoGeneratingColumn(e);

            System.ComponentModel.PropertyDescriptor propertyDescriptor = e.PropertyDescriptor as System.ComponentModel.PropertyDescriptor;
            System.ComponentModel.DataAnnotations.DisplayAttribute displayAttribute =
                propertyDescriptor.Attributes[typeof(System.ComponentModel.DataAnnotations.DisplayAttribute)] as System.ComponentModel.DataAnnotations.DisplayAttribute;

            if (displayAttribute == null)
                return;

            if (displayAttribute.Name != null)
                e.Column.Header = displayAttribute.Name;


            if (displayAttribute.GetAutoGenerateField() == false)
                e.Cancel = true;
        }

        #endregion

        #region SelectAll

        private static void OnCanExecuteSelectAllInternal(object target, CanExecuteRoutedEventArgs args)
        {
            ((DataGridGK)target).OnCanExecuteSelectAll(target, args);
        }

        protected virtual void OnCanExecuteSelectAll(object target, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = Items != null && Items.Count != 0;
            args.Handled = true;
        }

        private static void OnExecutedSelectAllInternal(object target, ExecutedRoutedEventArgs args)
        {
            ((DataGridGK)target).OnExecutedSelectAll(target, args);
        }

        protected virtual void OnExecutedSelectAll(object target, ExecutedRoutedEventArgs args)
        {
            Focus();
            SelectAll();
        }

        #endregion

        #region Clipboard Paste

        private static void OnCanExecutePasteInternal(object target, CanExecuteRoutedEventArgs args)
        {
            ((DataGridGK)target).OnCanExecutePaste(target, args);
        }

        protected virtual void OnCanExecutePaste(object target, CanExecuteRoutedEventArgs args)
        {
            if (CanExecutePasteEvent != null)
            {
                CanExecutePasteEvent(target, args);
                if (args.Handled)
                {
                    return;
                }
            }

            args.CanExecute = CurrentCell.Column != null && !IsReadOnly;
            args.Handled = true;
        }

        private static void OnExecutedPasteInternal(object target, ExecutedRoutedEventArgs args)
        {
            ((DataGridGK)target).OnExecutedPaste(target, args);
        }

        protected virtual void OnExecutedPaste(object target, ExecutedRoutedEventArgs args)
        {
            if (ExecutePasteEvent != null)
            {
                ExecutePasteEvent(target, args);
                if (args.Handled)
                {
                    return;
                }
            }

            List<string[]> clipboardData = ClipboardHelper.ParseClipboardData();
            PasteInSelection(clipboardData);
        }

        private void PasteInSelection(List<string[]> clipboardData)
        {
            if (clipboardData.Count == 1 && clipboardData.First().Count() == 1 && Items.Count != 1)
            {
                InsertValue(clipboardData[0][0], true);
                return;
            }

            UnselectAll();
            UnselectAllCells();

            int minRowIndex = Items.IndexOf(CurrentItem);
            int maxRowIndex = Items.Count - 1;
            InsertValues(clipboardData, minRowIndex, maxRowIndex);
        }

        private void InsertValue(string clipboardData, bool createNewRows)
        {
            foreach (var cell in SelectedCells)
            {
                if ((ItemsSource == null && UseDirectPaste == false))
                {
                    cell.Column.OnPastingCellClipboardContent(cell.Item, clipboardData);
                }
                else
                {
                    var cellProp = cell.Item.GetType().GetProperty(cell.Column.SortMemberPath);

                    if (cellProp is null)
                    {
                        if (createNewRows == true && cell.Item == Items[Items.Count - 1]) // Не создавать последнюю строку если выбрана временная строка
                            cell.Column.OnPastingCellClipboardContent(cell.Item, clipboardData);
                    }
                    else
                    {
                        cellProp.SetValue(cell.Item, clipboardData);

                    }
                }
            }
            CommitEditCommand.Execute(null, this);
        }

        private void InsertValues(List<string[]> values, int minRowInGridIndex, int maxRowInGridIndex)
        {
            int startIndexOfDisplayCol = (SelectionUnit != DataGridSelectionUnit.FullRow && CurrentColumn is null == false) ? CurrentColumn.DisplayIndex : 0;
            int clipboardRowIndex = 0;

            ///TODO
            ///Реализовать отмену добавления для при свойстве
            ///false CanUserPasteToNewRows
            ///
            for (int i = 0; i < values.Count() - (maxRowInGridIndex - minRowInGridIndex); i++)
                (ItemsSource as IList).Add(Activator.CreateInstance(ItemsSource.GetType().GenericTypeArguments[0]));

            for (int i = minRowInGridIndex; i <= maxRowInGridIndex && clipboardRowIndex < values.Count; i++, clipboardRowIndex++)
            {
                if (i < Items.Count)
                {
                    int clipboardColumnIndex = 0;
                    for (int j = startIndexOfDisplayCol; clipboardColumnIndex < values[clipboardRowIndex].Length; j++, clipboardColumnIndex++)
                    {
                        DataGridColumn column = null;
                        foreach (DataGridColumn columnItem in Columns)
                        {
                            if (columnItem.DisplayIndex == j)
                            {
                                column = columnItem;
                                break;
                            }
                        }

                        if (column != null)
                        {
                            if (ItemsSource == null && UseDirectPaste == false)
                            {
                                column.OnPastingCellClipboardContent(Items[i], values[clipboardRowIndex][clipboardColumnIndex]);
                            }
                            else
                            {
                                var cellProp = Items[i].GetType().GetProperty(column.SortMemberPath);
                                cellProp.SetValue(Items[i], values[clipboardRowIndex][clipboardColumnIndex]);
                            }
                            SelectedCells.Add(new DataGridCellInfo(Items[i], column));
                        }
                    }

                    if (i == maxRowInGridIndex)
                    {
                        maxRowInGridIndex++;
                    }
                }
            }
            CommitEditCommand.Execute(null, this);
        }

        /// <summary>
        ///     Whether the end-user can add new rows to the ItemsSource.
        /// </summary>
        public bool CanUserPasteToNewRows
        {
            get { return (bool)GetValue(CanUserPasteToNewRowsProperty); }
            set { SetValue(CanUserPasteToNewRowsProperty, value); }
        }

        /// <summary>
        ///     DependencyProperty for CanUserAddRows.
        /// </summary>
        public static readonly DependencyProperty CanUserPasteToNewRowsProperty =
            DependencyProperty.Register("CanUserPasteToNewRows",
                                        typeof(bool), typeof(DataGridGK),
                                        new FrameworkPropertyMetadata(true, null, null));

        #endregion Clipboard Paste

        #region Delete

        private static void OnCanExecuteDeleteInternal(object target, CanExecuteRoutedEventArgs args)
        {
            ((DataGridGK)target).OnCanExecuteDelete(target, args);
        }

        protected virtual void OnCanExecuteDelete(object target, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = CurrentCell != null && CanUserDeleteRows;
            args.Handled = true;
        }

        private static void OnExecutedDeleteInternal(object target, ExecutedRoutedEventArgs args)
        {
            ((DataGridGK)target).OnExecutedDelete(target, args);
        }

        protected virtual void OnExecutedDelete(object target, ExecutedRoutedEventArgs args)
        {
            if (SelectedItems.Count > 0)
            {
                object[] items = new object[SelectedItems.Count];
                SelectedItems.CopyTo(items, 0);
                foreach (var item in items)
                    ((IList)ItemsSource).Remove(item);
                return;
            }

            InsertValue("", false);
        }

        #endregion Clipboard Paste

        #region DragDrop

        /// <summary>
        ///     DependencyProperty for AllowDrag.
        /// </summary>
        public static readonly DependencyProperty AllowDragProperty =
            DependencyProperty.Register(nameof(AllowDrag),
                                        typeof(bool), typeof(DataGridGK),
                                        new FrameworkPropertyMetadata(true, null, null));

        public bool AllowDrag
        {
            get => (bool)GetValue(AllowDragProperty);
            set => SetValue(AllowDragProperty, value);
        }

        /// <summary>
        ///     DependencyProperty for HoldForDragTime.
        /// </summary>
        public static readonly DependencyProperty HoldForDragTimeProperty =
            DependencyProperty.Register(nameof(HoldForDragTime),
                                        typeof(double), typeof(DataGridGK),
                                        new FrameworkPropertyMetadata(500.0, null, null));

        public double HoldForDragTime
        {
            get => (double)GetValue(HoldForDragTimeProperty);
            set
            {
                _HoldPressForDrag.HoldTime = value;
                SetValue(HoldForDragTimeProperty, value);
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);

            if (ItemsSource != null)
                (ItemsSource as IList).Clear();
            else
                Items.Clear();

            var data = ClipboardHelper.ParseClipboardData(e.Data);
            if (data is null == false)
            {
                InsertValues(data, 0, Items.Count - 1);
            }
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            
            if (AllowDrag == false)
                return;

            _HoldPressForDrag.OnPress();
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            
            if (AllowDrag == false)
                return;

            _HoldPressForDrag.OnUnPress();
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            if (AllowDrag == false)
                return;

            _HoldPressForDrag.OnMove();
        }

        private void StartDrag(object sender, EventArgs e)
        {
            bool allowDropChanged = false;
            if (AllowDrop)
            {
                AllowDrop = false;
                allowDropChanged = true;
            }

            Cursor = Cursors.No;
            var dataObject = ClipboardHelper.ToDataObject(GetGridValues());

            DragDrop.DoDragDrop(this, dataObject, DragDropEffects.Copy);

            Cursor = Cursors.Arrow;

            if (allowDropChanged)
                AllowDrop = true;
        }

        public List<List<string>> GetGridValues()
        {
            List<List<string>> values = new();

            foreach (var row in Items)
            {
                values.Add(new List<string>());
                foreach (var column in Columns)
                {
                    string value = string.Empty;
                    if (ItemsSource is null == false)
                    {
                        var cellProp = row.GetType().GetProperty(column.SortMemberPath);
                        if (cellProp is null == false)
                            value = cellProp.GetValue(row).ToString();
                    }
                    else
                    {
                        throw new NotImplementedException("Реализовано только для заданного ItemsSource");
                    }
                    values.Last().Add(value);
                }
            }

            return values;
        }

        #endregion DragDrop
    }
}
