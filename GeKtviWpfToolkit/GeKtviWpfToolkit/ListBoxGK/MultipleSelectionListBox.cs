using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Collections;

namespace GeKtviWpfToolkit.ListBoxGK
{
    public class MultipleSelectionListBox : ListBox
    {
        internal bool processSelectionChanges = false;

        public static readonly DependencyProperty BindableSelectedItemsProperty =
            DependencyProperty.Register(nameof(BindableSelectedItems),
                typeof(object), typeof(MultipleSelectionListBox),
                new FrameworkPropertyMetadata((object)null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new System.Windows.PropertyChangedCallback(OnBindableSelectedItemsChanged)));

        public IList BindableSelectedItems
        {
            get => (IList)GetValue(BindableSelectedItemsProperty);
            set => SetValue(BindableSelectedItemsProperty, value);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (BindableSelectedItems == null || !this.IsInitialized) return; //Handle pre initilized calls

            IList selectedItems = BindableSelectedItems;

            if (e.AddedItems.Count > 0)
                if (!string.IsNullOrWhiteSpace(SelectedValuePath))
                {
                    foreach (var item in e.AddedItems)
                        if (!selectedItems.Contains(item.GetType().GetProperty(SelectedValuePath).GetValue(item, null)))
                            selectedItems.Add(item.GetType().GetProperty(SelectedValuePath).GetValue(item, null));
                }
                else
                {
                    foreach (var item in e.AddedItems)
                        if (!selectedItems.Contains(item))
                            selectedItems.Add(item);
                }

            if (e.RemovedItems.Count > 0)
                if (!string.IsNullOrWhiteSpace(SelectedValuePath))
                {
                    foreach (var item in e.RemovedItems)
                        if (selectedItems.Contains(item.GetType().GetProperty(SelectedValuePath).GetValue(item, null)))
                            selectedItems.Remove(item.GetType().GetProperty(SelectedValuePath).GetValue(item, null));
                }
                else
                {
                    foreach (var item in e.RemovedItems)
                        if (selectedItems.Contains(item))
                            selectedItems.Remove(item);
                }
        }

        private static void OnBindableSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MultipleSelectionListBox listBox)
            {
                List<dynamic> newSelection = new List<dynamic>();
                if (!string.IsNullOrWhiteSpace(listBox.SelectedValuePath))
                    foreach (var item in listBox.BindableSelectedItems)
                    {
                        foreach (var lbItem in listBox.Items)
                        {
                            var lbItemValue = lbItem.GetType().GetProperty(listBox.SelectedValuePath).GetValue(lbItem, null);
                            if (lbItemValue == item)
                                newSelection.Add(lbItem);
                        }
                    }
                else
                    newSelection = listBox.BindableSelectedItems as List<dynamic>;

                listBox.SetSelectedItems(newSelection);
            }
        }
    }
}
