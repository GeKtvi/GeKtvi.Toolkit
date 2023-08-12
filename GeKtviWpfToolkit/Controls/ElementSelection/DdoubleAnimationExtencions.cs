using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace GeKtviWpfToolkit.Controls.ElementSelection
{
    public static class DoubleAnimationExtensions
    {
        public static DoubleAnimation Copy(this DoubleAnimation doubleAnimation)
        {
            return new DoubleAnimation(doubleAnimation.From.GetValueOrDefault(), doubleAnimation.To.GetValueOrDefault(), doubleAnimation.Duration, doubleAnimation.FillBehavior);
        }
    }
}
