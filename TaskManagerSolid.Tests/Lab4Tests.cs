using NUnit.Framework;
using TaskManagerSolid.Models;
using TaskManagerSolid.Repositories;
using TaskManagerSolid.Services;
using TaskManagerSolid.Notifications;
using TaskManagerSolid.Interfaces;
using System;
using System.Collections.Generic;

using TaskStatus = TaskManagerSolid.Models.TaskStatus;

namespace TaskManagerSolid.Tests
{
    public class Lab4Tests
    {
        [Test]
        public void ReportService_CanBeConstructedWithInMemoryRepository()
        {
            ITaskReader reader = new InMemoryTaskRepository();

            var reportService = new ReportService(reader);

            Assert.That(reportService, Is.Not.Null);
        }

        [Test]
        public void GenerateSummary_ReturnsCorrectValues()
        {
            var repository = new InMemoryTaskRepository();

            var task1 = new TaskItem(1, "Task 1", "Desc", TaskPriority.High, NotificationType.Console);
            var task2 = new TaskItem(2, "Task 2", "Desc", TaskPriority.Low, NotificationType.Console);

            repository.Add(task1);
            repository.Add(task2);

            task1.Complete();
            repository.Update(task1);

            var reportService = new ReportService(repository);

            var result = reportService.GenerateSummary();

            Assert.That(result, Is.EqualTo("Total: 2, Done: 1"));
        }

        [Test]
        public void TaskService_CanBeConstructedWithRepositoryInterface()
        {
            ITaskRepository repository = new InMemoryTaskRepository();
            var validator = new TaskValidator();

            var notifiers = new Dictionary<NotificationType, ITaskNotifier>
            {
                { NotificationType.Console, new ConsoleNotifier() }
            };

            var service = new TaskService(repository, validator, notifiers);

            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void TaskRepository_CanBeUsedAsReaderAndWriter()
        {
            ITaskRepository repository = new InMemoryTaskRepository();

            ITaskReader reader = repository;
            ITaskWriter writer = repository;

            var task = new TaskItem(1, "Task ISP", "Desc", TaskPriority.Medium, NotificationType.Console);

            writer.Add(task);

            Assert.That(reader.GetAll().Count, Is.EqualTo(1));
        }
    }
}