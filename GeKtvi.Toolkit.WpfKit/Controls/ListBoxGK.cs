using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GeKtvi.Toolkit.WpfKit.Controls
{
    public class ListBoxGK : ListBox
    {
        internal bool processSelectionChanges = false;

        public static readonly DependencyProperty BindableSelectedItemsProperty =
            DependencyProperty.Register(nameof(BindableSelectedItems),
                typeof(object), typeof(ListBoxGK),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnBindableSelectedItemsChanged)));

        public IList BindableSelectedItems
        {
            get => (IList)GetValue(BindableSelectedItemsProperty);
            set => SetValue(BindableSelectedItemsProperty, value);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (BindableSelectedItems == null || !IsInitialized) return; //Handle pre initilized calls

            IList selectedItems = BindableSelectedItems;

            if (e.AddedItems.Count > 0)
                if (!string.IsNullOrWhiteSpace(SelectedValuePath))
                {
                    foreach (object item in e.AddedItems)
                        if (!selectedItems.Contains(item.GetType().GetProperty(SelectedValuePath).GetValue(item, null)))
                            selectedItems.Add(item.GetType().GetProperty(SelectedValuePath).GetValue(item, null));
                }
                else
                {
                    foreach (object item in e.AddedItems)
                        if (!selectedItems.Contains(item))
                            selectedItems.Add(item);
                }

            if (e.RemovedItems.Count > 0)
                if (!string.IsNullOrWhiteSpace(SelectedValuePath))
                {
                    foreach (object item in e.RemovedItems)
                        if (selectedItems.Contains(item.GetType().GetProperty(SelectedValuePath).GetValue(item, null)))
                            selectedItems.Remove(item.GetType().GetProperty(SelectedValuePath).GetValue(item, null));
                }
                else
                {
                    foreach (object item in e.RemovedItems)
                        if (selectedItems.Contains(item))
                            selectedItems.Remove(item);
                }
        }

        private static void OnBindableSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListBoxGK listBox)
            {
                List<dynamic> newSelection = new List<dynamic>();
                if (!string.IsNullOrWhiteSpace(listBox.SelectedValuePath))
                    foreach (object item in listBox.BindableSelectedItems)
                    {
                        foreach (object lbItem in listBox.Items)
                        {
                            object lbItemValue = lbItem.GetType().GetProperty(listBox.SelectedValuePath).GetValue(lbItem, null);
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
