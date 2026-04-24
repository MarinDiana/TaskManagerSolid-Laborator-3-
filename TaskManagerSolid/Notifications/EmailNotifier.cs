using TaskManagerSolid.Models;
using System;

namespace TaskManagerSolid.Notifications
{
    public class EmailNotifier : ITaskNotifier
    {
        public void Notify(TaskItem task)
        {
            Console.WriteLine("Email trimis pentru task: " + task.Title);
        }
    }
}