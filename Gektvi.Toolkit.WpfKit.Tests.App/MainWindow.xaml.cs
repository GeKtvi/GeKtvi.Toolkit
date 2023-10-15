using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GeKtvi.Toolkit.WpfKit.Tests.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Wpf.Ui.Controls.UiWindow
    {
        public ObservableCollection<TestData0> TestData0 { get; set; }
        public ObservableCollection<TestData1> TestData1 { get; set; }

        public MainWindow()
        {
            Wpf.Ui.Appearance.Watcher.Watch(this);


            TestData0 = new ObservableCollection<TestData0>()
            {
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"},
               new TestData0(){Value1 = 123, Value2 = Visibility.Visible, Value3 = "asdfg"}
            };

            TestData1 = new ObservableCollection<TestData1>()
            {
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"},
               new TestData1(){Value1 = "asdfg", Value2 = "asdfg"}
            };

            InitializeComponent();
            //DataGridGK1.ItemsSource = testData;
            //DataGridGK2.ItemsSource = testData;
        }

        private void DataGridGK1_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IInputElement asdf = FocusManager.GetFocusedElement(this);
        }
    }

    public class TestData0
    {
        public int Value1 { get; set; }
        public Visibility Value2 { get; set; }
        public string Value3 { get; set; }
        public int Value4 { get; set; }
        public bool Value5 { get; set; }
    }

    public class TestData1
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }

}
