using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerForm
{
    public class TaskInfo
    {
        public string EventName { get; set; }
        public string TargetApplication { get; set; }
        public DateTime TargetDateTime { get; set; }
        public TaskType Type { get; set; }
    }

    public enum TaskType
    {
        OneTime,
        Daily
    }

}
