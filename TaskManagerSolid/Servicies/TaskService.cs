using TaskManagerSolid.Models;
using TaskManagerSolid.Repositories;
using TaskManagerSolid.Notifications;
using System;
using System.Collections.Generic;

namespace TaskManagerSolid.Services
{
    public class TaskService
    {
        private readonly ITaskRepository repository;
        private readonly TaskValidator validator;
        private readonly Dictionary<NotificationType, ITaskNotifier> notifiers;

        public TaskService(
            ITaskRepository repository,
            TaskValidator validator,
            Dictionary<NotificationType, ITaskNotifier> notifiers)
        {
            this.repository = repository;
            this.validator = validator;
            this.notifiers = notifiers;
        }

        public void AddTask(TaskItem task)
        {
            if (!validator.IsValid(task))
                throw new ArgumentException("Task invalid.");

            repository.Add(task);

            if (notifiers.ContainsKey(task.NotificationType))
                notifiers[task.NotificationType].Notify(task);
        }

        public List<TaskItem> GetTasks()
        {
            return repository.GetAll();
        }

        public TaskItem? GetTaskById(int id)
        {
            return repository.GetById(id);
        }

        public void CompleteTask(int id)
        {
            var task = repository.GetById(id);

            if (task == null)
                throw new ArgumentException("Task-ul nu exista.");

            task.Complete();
            repository.Update(task);
        }

        public void DeleteTask(int id)
        {
            repository.Delete(id);
        }
    }
}