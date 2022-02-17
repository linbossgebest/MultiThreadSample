using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoTest
{
    public class TaskSchedulerBase<T> : TaskScheduler where T : IScheduler, new()
    {
        private readonly Thread _processThread;
        private readonly object _lock = new object();

        public TaskSchedulerBase()
        {
            _processThread = new Thread(this.Process);
            _processThread.Start();
            UnobservedTaskException += TaskSchedulerBase_UnobservedTaskException;
        }

        private void Process()
        {
            while (true)
            {
                var firstTask = GetScheduledTasks().FirstOrDefault();
                if (firstTask != null)
                {
                    try
                    {
                        base.TryExecuteTask(firstTask);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    finally 
                    {
                        TryDequeue(firstTask);
                    }
                }
            }
        }

        private void TaskSchedulerBase_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            if (e.Exception != null)
            {
                if (e.Exception.InnerExceptions != null)
                {
                    foreach (var exception in e.Exception.InnerExceptions)
                    {
                        Console.WriteLine($"TaskSchedulerBase_UnobservedTaskException {exception}");
                    }
                }
            }
            e.SetObserved();
        }

      

        private T _scheduler = new T();
        public T Scheduler
        {
            get { return _scheduler; }
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            lock (_lock)
            {
                return Scheduler.GetTasks();
            }
        }

        protected override void QueueTask(Task task)
        {
            lock (_lock)
            {
                Scheduler.Add(task);
            }
        }

        protected override bool TryDequeue(Task task)
        {
            lock (_lock)
            {
                Scheduler.Remove(task);
            }
            return true;
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (taskWasPreviouslyQueued)
            {
                if (TryDequeue(task))
                {
                    return base.TryExecuteTask(task);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return base.TryExecuteTask(task);
            }
        }
    }
}
