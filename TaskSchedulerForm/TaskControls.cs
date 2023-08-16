using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerForm
{
    public class TaskControls
    {
        public Label Label { get; set; }
        public Button Button { get; set; }

        public System.Timers.Timer Timer { get; set; }

        public string TargetPath { get; set; }
        public TaskInfo TaskInfo { get; set; }
    }
}
