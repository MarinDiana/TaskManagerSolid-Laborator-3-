using System;

namespace TaskManagerSolid.Models
{
    public class DeadlineTask : TaskItem
    {
        public DeadlineTask(
            int id,
            string title,
            string description,
            TaskPriority priority,
            NotificationType notificationType,
            DateTime dueDate)
            : base(id, title, description, priority, notificationType)
        {
            TaskType = TaskType.Deadline;
            DueDate = dueDate;
        }

        protected override void CompleteCore()
        {
            Status = TaskStatus.Done;
        }
    }
}