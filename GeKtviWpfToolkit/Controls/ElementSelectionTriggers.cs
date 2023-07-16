//using Microsoft.VisualBasic;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media.Animation;

//namespace GeKtviWpfToolkit.Controls
//{
//    public class ElementSelectionTriggers : TriggerBase
//    {

//        public ElementSelectionTriggers()
//        {

//        }
//        //public ElementSelectionTriggers()  
//        //{
//        //    CreateSelectedTrigger();
//        //    new Trigger
//        //}

//        #region EnterActionsDurationProperty
//        static int Main(string[] args)
//        {

//            return 0;
//        }

//        public double EnterActionsDuration
//        {
//            get { return (double)GetValue(EnterActionsDurationProperty); }
//            set { SetValue(EnterActionsDurationProperty, value); }
//        }

//        public static readonly DependencyProperty EnterActionsDurationProperty =
//            DependencyProperty.Register("EnterActionsDuration", typeof(double), typeof(ElementSelectionTriggers), new PropertyMetadata(0.25));

//        #endregion

//        #region ExitActionsDurationPropertyProperty

//        public static double ExitActionsDuration { get; set; } = 0.25;

//        public double ExitActionsDurationProperty
//        {
//            get { return (double)GetValue(ExitActionsDurationPropertyProperty); }
//            set { SetValue(ExitActionsDurationPropertyProperty, value); }
//        }

//        public static readonly DependencyProperty ExitActionsDurationPropertyProperty =
//            DependencyProperty.Register("ExitActionsDurationProperty", typeof(double), typeof(ElementSelectionTriggers), new PropertyMetadata(0.25));

//        #endregion

//        #region SelectedTriggerProperty

//        public Trigger SelectedTrigger
//        {
//            get { return (Trigger)GetValue(SelectedTriggerProperty); }
//            set { SetValue(SelectedTriggerProperty, value); }
//        }

//        public static readonly DependencyProperty SelectedTriggerProperty =
//            DependencyProperty.Register("SelectedTrigger", typeof(Trigger), typeof(ElementSelectionTriggers), new PropertyMetadata(null));


//        private Trigger CreateSelectedTrigger()
//        {

//            Property = DataGridRow.IsSelectedProperty;
//                Value = true;


//            var beginStoryboard = new BeginStoryboard();
//            var storyboard = new Storyboard();
//            beginStoryboard.Storyboard = storyboard;

//            var animationEnterActions = new DoubleAnimation()
//                                        {
//                                            To = 0.5,
//                                            Duration = 
//                                               new Duration(new System.TimeSpan(0, 0, 0, 0, (int)EnterActionsDuration)),
//                                            AccelerationRatio = 0.5
//                                        };

//            var animationExitActions = new DoubleAnimation() 
//                                        {
//                                            Duration = 
//                                                new Duration(new System.TimeSpan(0, 0, 0, 0, (int)ExitActionsDuration)),
//                                        };

//            Storyboard.SetTargetName(animationEnterActions, "DGR_Border");

//            storyboard.Children.Add(animationEnterActions);

//            this.EnterActions.Add(new BeginStoryboard()
//            {
//                Storyboard = new Storyboard()
//            });

//            return this;
//        }


//        #endregion

//        #region tmplt


//        #endregion

//        #region tmplt


//        #endregion

//        #region tmplt


//        #endregion

//        #region tmplt


//        #endregion
//    }
//}
