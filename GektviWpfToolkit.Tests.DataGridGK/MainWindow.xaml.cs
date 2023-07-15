using System.Collections.Generic;
using System.Windows;

namespace GektviWpfToolkit.Tests.DataGridGK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataGridGK.ItemsSource = new List<object>()
            {
               new {Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new {Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new {Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new {Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new {Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new {Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new {Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"}
            };
        }
    }
}
