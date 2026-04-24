using System;

namespace TaskManagerSolid.Models
{
    public enum TaskStatus
    {
        Todo,
        InProgress,
        Done,
        Overdue
    }

    public enum TaskPriority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }

    public enum TaskType
    {
        Standard,
        Recurring,
        Deadline
    }

    public enum NotificationType
    {
        Email,
        Console,
        FileLog,
        Slack
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; protected set; }
        public TaskPriority Priority { get; set; }
        public TaskType TaskType { get; protected set; }
        public NotificationType NotificationType { get; set; }
        public DateTime? DueDate { get; protected set; }
        public int? RecurrenceInterval { get; protected set; }
        public DateTime CreatedAt { get; set; }

        public TaskItem(
            int id,
            string title,
            string description,
            TaskPriority priority,
            NotificationType notificationType)
        {
            Id = id;
            Title = title;
            Description = description;
            Priority = priority;
            NotificationType = notificationType;
            Status = TaskStatus.Todo;
            TaskType = TaskType.Standard;
            CreatedAt = DateTime.Now;
        }

        public void Complete()
        {
            if (Status == TaskStatus.Done)
                throw new InvalidOperationException("Sarcina este deja finalizata.");

            CompleteCore();

            if (Status != TaskStatus.Done)
                throw new InvalidOperationException("Postconditia nu este respectata.");
        }

        protected virtual void CompleteCore()
        {
            Status = TaskStatus.Done;
        }
    }
}