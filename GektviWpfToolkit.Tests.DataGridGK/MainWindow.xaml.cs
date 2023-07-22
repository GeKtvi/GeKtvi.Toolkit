using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui;

namespace GektviWpfToolkit.Tests.DataGridGK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            Wpf.Ui.Appearance.Watcher.Watch(this);

            InitializeComponent();
            var testData = new ObservableCollection<object>()
            {
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"}
            };

            DataContext = new { Value = testData };
            //DataGridGK1.ItemsSource = testData;
            //DataGridGK2.ItemsSource = testData;
        }

        private void DataGridGK1_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var asdf = FocusManager.GetFocusedElement(this);
        }
    }

    public class TestData
    {
        public int Value1 { get; set; }
        public Visibility Value2 { get; set; }
        public string Value3 { get; set; }
        public int Value4 { get; set; }
    }

}
