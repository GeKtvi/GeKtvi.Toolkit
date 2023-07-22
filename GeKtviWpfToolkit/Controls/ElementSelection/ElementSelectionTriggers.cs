using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;

namespace GeKtviWpfToolkit.Controls.ElementSelection
{
    public class ElementSelectionTrigger : Trigger
    {
        //public string TargetName { get; set; } = "DGR_Border";
       // public string TargetProperty { get; set; } = "(Control.Background).(SolidColorBrush.Opacity)";



        public string TargetName
        {
            get { return (string)GetValue(TargetNameProperty); }
            set { SetValue(TargetNameProperty, value); }
        }

        public static readonly DependencyProperty TargetNameProperty =
            DependencyProperty.Register(nameof(TargetName), typeof(string), typeof(ElementSelectionTrigger), new PropertyMetadata(null));



        public string TargetProperty
        {
            get { return (string)GetValue(TargetPropertyProperty); }
            set { SetValue(TargetPropertyProperty, value); }
        }

        public static readonly DependencyProperty TargetPropertyProperty =
            DependencyProperty.Register(nameof(TargetPropertyProperty), typeof(string), typeof(ElementSelectionTrigger), new PropertyMetadata(null));

        public string ElementVisualState
        {
            get { return (string)GetValue(ElementVisualStateProperty); }
            set { SetValue(ElementVisualStateProperty, value); }
        }

        public static readonly DependencyProperty ElementVisualStateProperty =
            DependencyProperty.Register(nameof(ElementVisualState), typeof(string), typeof(ElementSelectionTrigger), new PropertyMetadata(string.Empty));

        public IList<ElementSelectionTriggerInfo> ElementSelectionTriggerInfoCollection
        {
            get { return (IList<ElementSelectionTriggerInfo>)GetValue(ElementSelectionTriggerInfoCollectionProperty); }
            set { SetValue(ElementSelectionTriggerInfoCollectionProperty, value); }
        }

        public static readonly DependencyProperty ElementSelectionTriggerInfoCollectionProperty =
            DependencyProperty.Register(nameof(ElementSelectionTriggerInfoCollectionProperty), typeof(IList), typeof(ElementSelectionTrigger), new PropertyMetadata(null));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            InitializeSelectedTrigger();
        }

        private void InitializeSelectedTrigger()
        {
            if (ElementSelectionTriggerInfoCollection is null)
                return;

            ElementSelectionTriggerInfo Info = ElementSelectionTriggerInfoCollection.Where(x => x.VisualStateName == ElementVisualState).FirstOrDefault();

            if (Info is null)
                return;

            try
            {
                
                BeginStoryboard beginStoryboardEnter = CreateStoryboard(Info.EnterAnimation.Clone());
                BeginStoryboard beginStoryboardExit = CreateStoryboard(Info.ExitAnimation.Clone());

                EnterActions.Add(beginStoryboardEnter);
                ExitActions.Add(beginStoryboardExit);
            }
            catch (InvalidOperationException)
            {

            }



        }

        private BeginStoryboard CreateStoryboard(DoubleAnimation animation)
        {
            BeginStoryboard beginStoryboard = new BeginStoryboard();
            Storyboard storyboard = new Storyboard();
            beginStoryboard.Storyboard = storyboard;

            if (TargetName == null)
                throw new InvalidOperationException("");

            if (TargetProperty == null)
                throw new InvalidOperationException("");

            if (Storyboard.GetTargetName(animation) == null)
                    Storyboard.SetTargetName(animation, TargetName);
            if (Storyboard.GetTargetProperty(animation) == null)
                    Storyboard.SetTargetProperty(animation, new PropertyPath(TargetProperty));

            storyboard.Children.Add(animation);
            beginStoryboard.Storyboard = storyboard;
            return beginStoryboard;
        }
    }
}
