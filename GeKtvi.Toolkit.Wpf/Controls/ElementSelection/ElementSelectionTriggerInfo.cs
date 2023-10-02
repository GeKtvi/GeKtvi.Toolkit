using System.Windows;
using System.Windows.Media.Animation;

namespace GeKtvi.Toolkit.Wpf.Controls.ElementSelection
{
    public class ElementSelectionTriggerInfo : FrameworkElement
    {
        public string VisualStateName { get; set; }
        public DoubleAnimation EnterAnimation { get; set; } = new DoubleAnimation();
        public DoubleAnimation ExitAnimation { get; set; } = new DoubleAnimation();
    }
}

