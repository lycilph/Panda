using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Panda.Utilities.Extensions
{
    public static class ObservableExtensions
    {
        public static IObservable<T> Regulate<T>(this IObservable<T> observable, TimeSpan duration)
        {
            return Regulate(observable, duration, TaskPoolScheduler.Default);
        }

        public static IObservable<T> Regulate<T>(this IObservable<T> observable, TimeSpan duration, IScheduler scheduler)
        {
            var regulator = new ObservableRegulator<T>(duration, scheduler);

            return Observable.Create<T>(observer => observable.Subscribe(obj => regulator.ProcessItem(obj, observer)));
        }

        private class ObservableRegulator<T>
        {
            private DateTimeOffset last_entry = DateTimeOffset.MinValue;
            private readonly object last_entry_lock = new object();

            private readonly TimeSpan duration;
            private readonly IScheduler scheduler;

            public ObservableRegulator(TimeSpan duration, IScheduler scheduler)
            {
                this.duration = duration;
                this.scheduler = scheduler;
            }

            public void ProcessItem(T val, IObserver<T> observer)
            {
                var can_broadcast_now = false;
                var next_entry_time = DateTimeOffset.MaxValue;

                lock (last_entry_lock)
                {
                    var now = DateTimeOffset.Now;
                    if (now.Subtract(last_entry) > duration)
                    {
                        last_entry = now;
                        can_broadcast_now = true;
                    }
                    else
                    {
                        last_entry = last_entry.Add(duration);
                        next_entry_time = last_entry;
                    }
                }

                if (can_broadcast_now)
                {
                    observer.OnNext(val);
                }
                else
                {
                    scheduler.Schedule(next_entry_time, () => observer.OnNext(val));
                }

            }
        }
    }
}
