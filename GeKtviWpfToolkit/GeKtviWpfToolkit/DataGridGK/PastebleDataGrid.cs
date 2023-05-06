using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace GeKtviWpfToolkit.DataGridGK
{
    public class PastebleDataGrid : DataGrid
    {
        public event ExecutedRoutedEventHandler ExecutePasteEvent;
        public event CanExecuteRoutedEventHandler CanExecutePasteEvent;

        // ******************************************************************
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

            CommandManager.RegisterClassCommandBinding(
                typeof(PastebleDataGrid),
                new CommandBinding(ApplicationCommands.Paste,
                    new ExecutedRoutedEventHandler(OnExecutedPasteInternal),
                    new CanExecuteRoutedEventHandler(OnCanExecutePasteInternal)));

            CommandManager.RegisterClassCommandBinding(
                typeof(PastebleDataGrid),
                new CommandBinding(ApplicationCommands.Delete,
                    new ExecutedRoutedEventHandler(OnExecutedDeleteInternal),
                    new CanExecuteRoutedEventHandler(OnCanExecuteDelete)));
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


#if DEBUG
            Debug.Print(">>> DataGrid Paste: >>>");
            StringBuilder sb = new StringBuilder();
#endif 
            int minRowIndex = Items.IndexOf(CurrentItem);
            int maxRowIndex = Items.Count - 1;
            int startIndexOfDisplayCol = SelectionUnit != DataGridSelectionUnit.FullRow ? CurrentColumn.DisplayIndex : 0;
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


#if DEBUG
                        sb.AppendFormat("{0,-10}", clipboardData[clipboardRowIndex][clipboardColumnIndex]);
                        sb.Append(" - ");
#endif
                    }

                    CommitEditCommand.Execute(this, this);
                    if (i == maxRowIndex)
                    {
                        maxRowIndex++;
                    }
                }

                Debug.Print(sb.ToString());
#if DEBUG
                sb.Clear();
#endif
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

        //private static void OnCanExecutePasteInternal(object target, CanExecuteRoutedEventArgs args)
        //{
        //    ((PastebleDataGrid)target).OnCanExecutePaste(target, args);
        //}

        protected virtual void OnCanExecuteDelete(object target, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = true;
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

            //BeginEditCommand.Execute(null, this);


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


        private void SetGridToSupportManyEditEitherWhenValidationErrorExists()
        {
            this.Items.CurrentChanged += Items_CurrentChanged;


            //Type DatagridType = this.GetType().BaseType;
            //PropertyInfo HasCellValidationProperty = DatagridType.GetProperty("HasCellValidationError", BindingFlags.NonPublic | BindingFlags.Instance);
            //HasCellValidationProperty.
        }

        void Items_CurrentChanged(object sender, EventArgs e)
        {
            //this.Items[0].
            //throw new NotImplementedException();
        }

        // ******************************************************************
        private void SetGridWritable()
        {
            Type DatagridType = GetType().BaseType;
            PropertyInfo HasCellValidationProperty = DatagridType.GetProperty("HasCellValidationError", BindingFlags.NonPublic | BindingFlags.Instance);
            if (HasCellValidationProperty != null)
            {
                HasCellValidationProperty.SetValue(this, false, null);
            }
        }

        // ******************************************************************
        public void SetGridWritableEx()
        {
            BindingFlags bindingFlags = BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Instance;
            PropertyInfo cellErrorInfo = GetType().BaseType.GetProperty("HasCellValidationError", bindingFlags);
            PropertyInfo rowErrorInfo = GetType().BaseType.GetProperty("HasRowValidationError", bindingFlags);
            cellErrorInfo.SetValue(this, false, null);
            rowErrorInfo.SetValue(this, false, null);
        }

        //public PastebleDataGrid():base() 
        //{
        //    DelegateCommand Paste = new DelegateCommand(TextBoxPaste, () => true);
        //    InputGesture gst = new KeyGesture(Key.V, ModifierKeys.Control);
        //    InputBinding ib = new InputBinding(Paste, gst);
        //    this.InputBindings.Add(ib);
        //    //DataFormats.CommaSeparatedValue
        //    ContextMenu = new ContextMenu();
        //    MenuItem paste = new MenuItem();
        //    paste.Command = Paste;
        //    paste.Header = "Paste";
        //    paste.InputGestureText = "Ctl + V";
        //    ContextMenu.Items.Add(paste);
        //}


        //private void TextBoxPaste()
        //{
        //    //Clipboard.SetData(DataFormats.CommaSeparatedValue, SelectedCells);


        //    var cellInfos = this.SelectedCells;

        //    var list1 = new List<string>();

        //    //Columns



        //    foreach (DataGridCellInfo cellInfo in cellInfos)
        //    {
        //        if (cellInfo.IsValid)
        //        {
        //            //GetCellContent returns FrameworkElement
        //            var content = cellInfo.Column.GetCellContent(cellInfo.Item);

        //            //Need to add the extra lines of code below to get desired output

        //            //get the datacontext from FrameworkElement and typecast to DataRowView
        //            var row = (string)content.GetValue(TextBlock.TextProperty);


        //            list1.Add(row);
        //            ////ItemArray returns an object array with single element
        //            //object[] obj = row.Row.ItemArray;

        //            ////store the obj array in a list or Arraylist for later use
        //            //list1.Add(obj[0].ToString());
        //        }
        //    }
        //    //DataObject d = this.GetClipboardContent();
        //    // Clipboard.SetDataObject(d);
        //    //string clipboard = args.DataObject.GetData(typeof(string)) as string;

        //    //Regex nonNumeric = new System.Text.RegularExpressions.Regex(@"\D");
        //    //string result = nonNumeric.Replace(clipboard, String.Empty);

        //    //int start = uiTextBox.SelectionStart;
        //    //int length = uiTextBox.SelectionLength;
        //    //int caret = uiTextBox.CaretIndex;

        //    //string text = uiTextBox.Text.Substring(0, start);
        //    //text += uiTextBox.Text.Substring(start + length);

        //    //string newText = text.Substring(0, uiTextBox.CaretIndex) + result;
        //    //newText += text.Substring(caret);
        //    //uiTextBox.Text = newText;
        //    //uiTextBox.CaretIndex = caret + result.Length;

        //    //args.CancelCommand();
        //}

        //private void TextBoxCopy(object sender, DataObjectCopyingEventArgs args)
        //{
        //    //string clipboard = args.DataObject.GetData(typeof(string)) as string;

        //    //Regex nonNumeric = new System.Text.RegularExpressions.Regex(@"\D");
        //    //string result = nonNumeric.Replace(clipboard, String.Empty);

        //    //int start = uiTextBox.SelectionStart;
        //    //int length = uiTextBox.SelectionLength;
        //    //int caret = uiTextBox.CaretIndex;

        //    //string text = uiTextBox.Text.Substring(0, start);
        //    //text += uiTextBox.Text.Substring(start + length);

        //    //string newText = text.Substring(0, uiTextBox.CaretIndex) + result;
        //    //newText += text.Substring(caret);
        //    //uiTextBox.Text = newText;
        //    //uiTextBox.CaretIndex = caret + result.Length;

        //    //args.CancelCommand();
        //}

        //private void PasteClipboardValue()
        //{
        //    ////Show Error if no cell is selected
        //    //if (dataGridView1.SelectedCells.Count == 0)
        //    //{
        //    //    MessageBox.Show("Please select a cell", "Paste",
        //    //    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    //    return;
        //    //}

        //    ////Get the starting Cell
        //    //DataGridViewCell startCell = GetStartCell(dataGridView1);
        //    ////Get the clipboard value in a dictionary
        //    //Dictionary<int, Dictionary<int, string>> cbValue =
        //    //        ClipBoardValues(Clipboard.GetText());

        //    //int iRowIndex = startCell.RowIndex;
        //    //foreach (int rowKey in cbValue.Keys)
        //    //{
        //    //    int iColIndex = startCell.ColumnIndex;
        //    //    foreach (int cellKey in cbValue[rowKey].Keys)
        //    //    {
        //    //        //Check if the index is within the limit
        //    //        if (iColIndex <= dataGridView1.Columns.Count - 1
        //    //        && iRowIndex <= dataGridView1.Rows.Count - 1)
        //    //        {
        //    //            DataGridViewCell cell = dataGridView1[iColIndex, iRowIndex];

        //    //            //Copy to selected cells if 'chkPasteToSelectedCells' is checked
        //    //            if ((chkPasteToSelectedCells.Checked && cell.Selected) ||
        //    //                (!chkPasteToSelectedCells.Checked))
        //    //                cell.Value = cbValue[rowKey][cellKey];
        //    //        }
        //    //        iColIndex++;
        //    //    }
        //    //    iRowIndex++;
        //    //}
        //}
    }
}
