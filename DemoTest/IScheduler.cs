using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTest
{
    public interface IScheduler
    {
        void Add(Task t);
        void Remove(Task t);
        IEnumerable<Task> GetTasks();
    }
}
