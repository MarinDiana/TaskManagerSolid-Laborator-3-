using Microsoft.Extensions.DependencyInjection;
using TaskManagerSolid.Interfaces;
using TaskManagerSolid.Models;
using TaskManagerSolid.Repositories;
using TaskManagerSolid.Services;
using TaskManagerSolid.Notifications;

var services = new ServiceCollection();

services.AddSingleton<ITaskRepository, SQLiteTaskRepository>();
services.AddSingleton<ITaskReader>(provider => provider.GetRequiredService<ITaskRepository>());
services.AddSingleton<ITaskWriter>(provider => provider.GetRequiredService<ITaskRepository>());

services.AddTransient<TaskValidator>();
services.AddTransient<ReportService>();

services.AddSingleton<Dictionary<NotificationType, ITaskNotifier>>(provider =>
    new Dictionary<NotificationType, ITaskNotifier>
    {
        { NotificationType.Console, new ConsoleNotifier() },
        { NotificationType.Email, new EmailNotifier() },
        { NotificationType.FileLog, new FileLogNotifier() },
        { NotificationType.Slack, new SlackNotifier() }
    });

services.AddTransient<TaskService>();

var provider = services.BuildServiceProvider();

var taskService = provider.GetRequiredService<TaskService>();
var reportService = provider.GetRequiredService<ReportService>();

taskService.AddTask(new TaskItem(
    10,
    "Laborator 4 ISP si DIP",
    "Refactorizare proiect Task Manager",
    TaskPriority.High,
    NotificationType.Console));

taskService.AddTask(new DeadlineTask(
    11,
    "Predare laborator 4",
    "Incarcare proiect pe GitHub",
    TaskPriority.High,
    NotificationType.Email,
    DateTime.Now.AddDays(2)));

taskService.CompleteTask(10);

foreach (var task in taskService.GetTasks())
{
    Console.WriteLine($"{task.Id} - {task.Title} - {task.TaskType} - {task.Priority} - {task.Status}");
}

Console.WriteLine(reportService.GenerateSummary());