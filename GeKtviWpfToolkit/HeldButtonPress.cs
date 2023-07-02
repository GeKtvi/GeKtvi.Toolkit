using System;
using System.Timers;
using System.Windows.Controls;

namespace GeKtviWpfToolkit
{
    internal class HoldButtonPress
    {
        public event EventHandler HoldPressSuccess;
        public Control Parent { get; set; }
        public double HoldTime 
        {
            get => _timer.Interval;
            set => _timer.Interval = value;
        }

        private Timer _timer;

        public HoldButtonPress(Control parent, double holdTime)
        {
            Parent = parent;
            _timer = new Timer();
            _timer.AutoReset = false;
            _timer.Interval = holdTime;
            _timer.Elapsed += OnHoldPressSuccess;
        }

        private void OnHoldPressSuccess(object sender, ElapsedEventArgs e)
        {
            Parent.Dispatcher.Invoke(() => HoldPressSuccess?.Invoke(this, e));
        }

        public void OnPress()
        {
            _timer.Start();
        }

        public void OnUnPress()
        {
            _timer.Stop();
        }

        public void OnMove()
        {
            _timer.Stop();
        }
    }
}
