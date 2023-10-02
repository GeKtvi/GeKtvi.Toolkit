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
