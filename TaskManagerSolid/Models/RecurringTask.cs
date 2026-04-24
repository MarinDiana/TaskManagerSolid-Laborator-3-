using System;

namespace TaskManagerSolid.Models
{
    public class RecurringTask : TaskItem
    {
        public RecurringTask(
            int id,
            string title,
            string description,
            TaskPriority priority,
            NotificationType notificationType,
            DateTime dueDate,
            int recurrenceInterval)
            : base(id, title, description, priority, notificationType)
        {
            TaskType = TaskType.Recurring;
            DueDate = dueDate;
            RecurrenceInterval = recurrenceInterval;
        }

        protected override void CompleteCore()
        {
            Status = TaskStatus.Done;

            if (DueDate.HasValue && RecurrenceInterval.HasValue)
            {
                DueDate = DueDate.Value.AddDays(RecurrenceInterval.Value);
            }
        }
    }
}