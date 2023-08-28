using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerForm
{
    public interface ITaskDAO
    {
        void SaveTasks(List<TaskInfo> taskInfos);
        List<TaskInfo> LoadTaskData(string selectedFolderPath);
    }
}
