using TaskManagerSolid.Models;
using System;

namespace TaskManagerSolid.Notifications
{
    public class SlackNotifier : ITaskNotifier
    {
        public void Notify(TaskItem task)
        {
            Console.WriteLine("Slack notification pentru task: " + task.Title);
        }
    }
}