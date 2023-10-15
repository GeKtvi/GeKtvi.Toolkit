using System;
using System.Timers;

namespace GeKtvi.Toolkit
{
    public class DeferredOperation
    {
        public int Delay { get; set; }

        private Timer _timer;
        private Action _operation;

        public DeferredOperation(Action operation, int delay)
        {
            _operation = operation;

            Delay = delay;

            _timer = new Timer(delay);
            _timer.AutoReset = false;
            _timer.Elapsed += Elapsed;
        }

        public void DoOperation()
        {
            _timer.Stop();
            _timer.Start();
        }

        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            _operation.Invoke();
        }
    }
}
