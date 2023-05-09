using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GeKtviWpfToolkit
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

        private void Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _operation.Invoke();
        }
    }
}
