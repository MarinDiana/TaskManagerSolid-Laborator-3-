using TaskManagerSolid.Models;
using System;

namespace TaskManagerSolid.Services
{
    public class TaskValidator
    {
        public bool IsValid(TaskItem task)
        {
            if (task == null)
                return false;

            if (string.IsNullOrWhiteSpace(task.Title))
                return false;

            if (string.IsNullOrWhiteSpace(task.Description))
                return false;

            if (task.TaskType == TaskType.Deadline && task.DueDate == null)
                return false;

            if (task.TaskType == TaskType.Recurring)
            {
                if (task.DueDate == null)
                    return false;

                if (task.RecurrenceInterval == null || task.RecurrenceInterval <= 0)
                    return false;
            }

            return true;
        }
    }
}