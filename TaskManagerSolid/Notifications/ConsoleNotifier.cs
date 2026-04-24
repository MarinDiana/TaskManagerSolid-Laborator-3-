using TaskManagerSolid.Models;
using System;

namespace TaskManagerSolid.Notifications
{
    public class ConsoleNotifier : ITaskNotifier
    {
        public void Notify(TaskItem task)
        {
            Console.WriteLine("Task nou adaugat: " + task.Title);
        }
    }
}