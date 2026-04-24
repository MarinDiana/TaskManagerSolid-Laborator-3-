using NUnit.Framework;
using TaskManagerSolid.Models;
using TaskManagerSolid.Repositories;
using TaskManagerSolid.Services;
using TaskManagerSolid.Notifications;
using System;
using System.Collections.Generic;

using TaskStatus = TaskManagerSolid.Models.TaskStatus;

namespace TaskManagerSolid.Tests
{
    public class Tests
    {
        private TaskService service;

        [SetUp]
        public void Setup()
        {
            var repository = new InMemoryTaskRepository();
            var validator = new TaskValidator();

            var notifiers = new Dictionary<NotificationType, ITaskNotifier>
            {
                { NotificationType.Console, new ConsoleNotifier() }
            };

            service = new TaskService(repository, validator, notifiers);
        }

        [Test]
        public void AddTask_ValidTask_ShouldBeAdded()
        {
            var task = new TaskItem(1, "Test", "Desc", TaskPriority.High, NotificationType.Console);

            service.AddTask(task);

            Assert.That(service.GetTasks().Count, Is.EqualTo(1));
        }

        [Test]
        public void AddTask_InvalidTask_ShouldThrowException()
        {
            var task = new TaskItem(1, "", "", TaskPriority.High, NotificationType.Console);

            Assert.Throws<ArgumentException>(() => service.AddTask(task));
        }

        [Test]
        public void CompleteTask_ShouldSetStatusDone()
        {
            var task = new TaskItem(1, "Test", "Desc", TaskPriority.High, NotificationType.Console);

            service.AddTask(task);
            service.CompleteTask(1);

            Assert.That(service.GetTaskById(1)?.Status, Is.EqualTo(TaskStatus.Done));
        }

        [Test]
        public void CompleteTask_AlreadyDone_ShouldThrow()
        {
            var task = new TaskItem(1, "Test", "Desc", TaskPriority.High, NotificationType.Console);

            service.AddTask(task);
            service.CompleteTask(1);

            Assert.Throws<InvalidOperationException>(() => service.CompleteTask(1));
        }

        [Test]
        public void DeleteTask_ShouldRemoveTask()
        {
            var task = new TaskItem(1, "Test", "Desc", TaskPriority.High, NotificationType.Console);

            service.AddTask(task);
            service.DeleteTask(1);

            Assert.That(service.GetTasks().Count, Is.EqualTo(0));
        }

        [Test]
        public void RecurringTask_ShouldUpdateDueDate()
        {
            var task = new RecurringTask(
                1,
                "Recurring",
                "Desc",
                TaskPriority.Medium,
                NotificationType.Console,
                DateTime.Now,
                7);

            service.AddTask(task);
            var oldDate = task.DueDate;

            service.CompleteTask(1);

            Assert.That(task.DueDate, Is.Not.EqualTo(oldDate));
        }

        [Test]
        public void DeadlineTask_ShouldBeDone()
        {
            var task = new DeadlineTask(
                1,
                "Deadline",
                "Desc",
                TaskPriority.High,
                NotificationType.Console,
                DateTime.Now.AddDays(1));

            service.AddTask(task);
            service.CompleteTask(1);

            Assert.That(task.Status, Is.EqualTo(TaskStatus.Done));
        }

        [Test]
        public void Validator_ShouldRejectEmptyTitle()
        {
            var validator = new TaskValidator();

            var task = new TaskItem(1, "", "Desc", TaskPriority.Low, NotificationType.Console);

            Assert.That(validator.IsValid(task), Is.False);
        }
    }
}