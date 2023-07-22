using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace GeKtviWpfToolkit.Controls.ElementSelection
{
    public class ElementSelectionTriggerInfo : FrameworkElement
    {
        public string VisualStateName { get; set; }
        public DoubleAnimation EnterAnimation { get; set; } = new DoubleAnimation();
        public DoubleAnimation ExitAnimation { get; set; } = new DoubleAnimation();
        //public string VisualStateName { get; set; }

        //public double? ToOnEnter { get; set; }

        //public double? FromOnEnter { get; set; }

        //public double? ToOnExit { get; set; }

        //public double

        //public TimeSpan DurationEnter { get; set; }

        //public TimeSpan DurationExit { get; set; }
    }
}

