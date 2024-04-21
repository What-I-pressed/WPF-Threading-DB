using Infrastraction.Events;
using Infrastraction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.MVVM
{
    internal class View
    {
        Action _DGUpdater;
        static Action<string> _messageBoxEvent;
        Action<int> _PBMaximumEvent;
        Stopwatch _stopWatch;

        public View() { _stopWatch = new Stopwatch(); }
        public void BindDGUpdateEvent(Action del) =>
            _DGUpdater= del;
        public Task InvokeDGUpdateEventAsync() =>
            Task.Run(_DGUpdater);
        public void InvokeDGUpdateEvent() =>
            _DGUpdater();

        public void StartTimer() => _stopWatch.Start();

        public void BindPBMaximumEvent(Action<int> del) =>
            _PBMaximumEvent= del;

        public void BindMessageBoxEvent(Action<string> del) =>
            _messageBoxEvent = del;

        public void InvokePBMaximumEvent(int count) => _PBMaximumEvent(count);

        public void ShowTimerMeasurements()
        {
            _stopWatch.Stop();
            TimeSpan ts = _stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            InvokeMessageBoxEvent(elapsedTime);
        }

        public void InvokeMessageBoxEvent(string text)
        {
            _messageBoxEvent(text);
        }
    }
}
