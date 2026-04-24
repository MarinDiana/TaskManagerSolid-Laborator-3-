using TaskManagerSolid.Models;
using System.IO;

namespace TaskManagerSolid.Notifications
{
    public class FileLogNotifier : ITaskNotifier
    {
        public void Notify(TaskItem task)
        {
            File.AppendAllText("log.txt", "Task: " + task.Title + "\n");
        }
    }
}