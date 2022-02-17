using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTest
{
    public class CustomerScheduler : IScheduler
    {
        protected List<Task> existTasks = new List<Task>();

        public void Add(Task t)
        {
            if (Contains(t, out Task oldTask))
            {
                existTasks.Remove(oldTask);
            }
            existTasks.Add(t);
        }

        public IEnumerable<Task> GetTasks()
        {
            return existTasks;
        }

        public void Remove(Task t)
        {
            existTasks.Remove(t);
        }

        public virtual bool Contains(Task t, out Task oldTask)
        {
            bool result = false;
            oldTask = null;
            foreach (var task in existTasks)
            {
                //调度逻辑：如果当前Task列表中存在与新建Task的AsyncState相同的Task，则删除列表中对应Task
                if (t.AsyncState != null && t.AsyncState.Equals(task.AsyncState))
                {
                    oldTask = task;
                    return true;
                }
            }
            return result;
        }
    }
}
