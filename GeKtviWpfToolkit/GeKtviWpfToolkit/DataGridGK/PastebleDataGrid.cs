using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GeKtviWpfToolkit.DataGridGK;
using GeKtviWpfToolkit.ValueConverters;

namespace GeKtviWpfToolkit.DataGridGK
{
    public class PastebleDataGrid : DataGrid
    {
        public event ExecutedRoutedEventHandler ExecutePasteEvent;
        public event CanExecuteRoutedEventHandler CanExecutePasteEvent;

        public PastebleDataGrid()
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
            paste.SetBinding(MenuItem.VisibilityProperty, pasteBinding);

            Binding deleteBinding = new Binding(nameof(CanUserDeleteRows));
            deleteBinding.Source = this;
            deleteBinding.Mode = BindingMode.OneWay;
            deleteBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            deleteBinding.Converter = new BooleanToVisibilityConverter();
            delete.SetBinding(MenuItem.VisibilityProperty, deleteBinding);

            CommandManager.RegisterClassCommandBinding(
                typeof(PastebleDataGrid),
                new CommandBinding(ApplicationCommands.Paste,
                    new ExecutedRoutedEventHandler(OnExecutedPasteInternal),
                    new CanExecuteRoutedEventHandler(OnCanExecutePasteInternal)));

            CommandManager.RegisterClassCommandBinding(
                typeof(PastebleDataGrid),
                new CommandBinding(ApplicationCommands.Delete,
                    new ExecutedRoutedEventHandler(OnExecutedDeleteInternal),
                    new CanExecuteRoutedEventHandler(OnCanExecuteDeleteInternal)));
        }

        #region Clipboard Paste

        private static void OnCanExecutePasteInternal(object target, CanExecuteRoutedEventArgs args)
        {
            ((PastebleDataGrid)target).OnCanExecutePaste(target, args);
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

            args.CanExecute = CurrentCell != null;
            args.Handled = true;
        }

        private static void OnExecutedPasteInternal(object target, ExecutedRoutedEventArgs args)
        {
            ((PastebleDataGrid)target).OnExecutedPaste(target, args);
        }

        protected virtual void OnExecutedPaste(object target, ExecutedRoutedEventArgs args)
        {

            UnselectAll();
            UnselectAllCells();
            if (ExecutePasteEvent != null)
            {
                ExecutePasteEvent(target, args);
                if (args.Handled)
                {
                    return;
                }
            }

            List<string[]> clipboardData = ClipboardHelper.ParseClipboardData();

            int minRowIndex = Items.IndexOf(CurrentItem);
            int maxRowIndex = Items.Count - 1;
            int startIndexOfDisplayCol = (SelectionUnit != DataGridSelectionUnit.FullRow) ? CurrentColumn.DisplayIndex : 0;
            int clipboardRowIndex = 0;

            BeginEditCommand.Execute(null, this);

            for (int i = minRowIndex; i <= maxRowIndex && clipboardRowIndex < clipboardData.Count; i++, clipboardRowIndex++)
            {
                if (i < this.Items.Count)
                {
                    CurrentItem = Items[i];


                    int clipboardColumnIndex = 0;
                    for (int j = startIndexOfDisplayCol; clipboardColumnIndex < clipboardData[clipboardRowIndex].Length; j++, clipboardColumnIndex++)
                    {
                        DataGridColumn column = null;
                        foreach (DataGridColumn columnIter in this.Columns)
                        {
                            if (columnIter.DisplayIndex == j)
                            {
                                column = columnIter;
                                break;
                            }
                        }

                        if (column != null)
                        {
                            column.OnPastingCellClipboardContent(Items[i], clipboardData[clipboardRowIndex][clipboardColumnIndex]);

                            SelectedCells.Add(new DataGridCellInfo(Items[i], column));
                        }
                    }

                    CommitEditCommand.Execute(this, this);
                    if (i == maxRowIndex)
                    {
                        maxRowIndex++;
                    }
                }
            }
        }

        // ******************************************************************
        /// <summary>
        ///     Whether the end-user can add new rows to the ItemsSource.
        /// </summary>
        public bool CanUserPasteToNewRows
        {
            get { return (bool)GetValue(CanUserPasteToNewRowsProperty); }
            set { SetValue(CanUserPasteToNewRowsProperty, value); }
        }

        // ******************************************************************
        /// <summary>
        ///     DependencyProperty for CanUserAddRows.
        /// </summary>
        public static readonly DependencyProperty CanUserPasteToNewRowsProperty =
            DependencyProperty.Register("CanUserPasteToNewRows",
                                        typeof(bool), typeof(PastebleDataGrid),
                                        new FrameworkPropertyMetadata(true, null, null));

        // ******************************************************************
        #endregion Clipboard Paste

        #region Delete

        private static void OnCanExecuteDeleteInternal(object target, CanExecuteRoutedEventArgs args)
        {
            ((PastebleDataGrid)target).OnCanExecuteDelete(target, args);
        }

        protected virtual void OnCanExecuteDelete(object target, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = CurrentCell != null && CanUserDeleteRows;
            args.Handled = true;
        }

        private static void OnExecutedDeleteInternal(object target, ExecutedRoutedEventArgs args)
        {
            ((PastebleDataGrid)target).OnExecutedDelete(target, args);
        }

        protected virtual void OnExecutedDelete(object target, ExecutedRoutedEventArgs args)
        {
            if (SelectedItems.Count > 0)
            {
                object[] items = new object[SelectedItems.Count];
                SelectedItems.CopyTo(items, 0);
                foreach (var item in items)
                    ((IList)ItemsSource).Remove(item);
                //Items.Remove(item);
                return;
            }

            BeginEditCommand.Execute(null, this);

            foreach (DataGridCellInfo cellInfo in SelectedCells)
            {
                if (cellInfo.IsValid)
                {
                    // element will be your DataGridCell Content
                    var element = cellInfo.Column.GetCellContent(cellInfo.Item);

                    if (element != null)
                    {
                        var myCell = element.Parent as DataGridCell;
                        cellInfo.Column.OnPastingCellClipboardContent(cellInfo.Item, "");
                        CommitEditCommand.Execute(this, this);
                    }
                }
            }
        }

        #endregion Clipboard Paste

    }
}
