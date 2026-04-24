using System.Collections.Generic;
using TaskManagerSolid.Models;

namespace TaskManagerSolid.Notifications
{
    public class CompositeNotifier : ITaskNotifier
    {
        private readonly List<ITaskNotifier> notifiers;

        public CompositeNotifier(List<ITaskNotifier> notifiers)
        {
            this.notifiers = notifiers;
        }

        public void Notify(TaskItem task)
        {
            foreach (var notifier in notifiers)
            {
                notifier.Notify(task);
            }
        }
    }
}