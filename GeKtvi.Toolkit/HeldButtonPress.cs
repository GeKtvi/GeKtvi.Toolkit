using System;
using System.Timers;

namespace GeKtvi.Toolkit
{
    public class HoldButtonPress
    {
        public event EventHandler HoldPressSuccess;
        public Action<Action> _invokeInDispatcher { get; set; }
        public double HoldTime
        {
            get => _timer.Interval;
            set => _timer.Interval = value;
        }

        private Timer _timer;

        public HoldButtonPress(Action<Action> invokeInDispatcher, double holdTime)
        {
            _invokeInDispatcher = invokeInDispatcher;
            _timer = new Timer();
            _timer.AutoReset = false;
            _timer.Interval = holdTime;
            _timer.Elapsed += OnHoldPressSuccess;
        }

        private void OnHoldPressSuccess(object sender, ElapsedEventArgs e)
        {
            _invokeInDispatcher.Invoke(() => HoldPressSuccess?.Invoke(this, e));
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
