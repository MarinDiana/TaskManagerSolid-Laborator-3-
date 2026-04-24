using TaskManagerSolid.Models;
using TaskManagerSolid.Repositories;
using TaskManagerSolid.Services;
using TaskManagerSolid.Notifications;

var repository = new SQLiteTaskRepository();
var validator = new TaskValidator();

var notifiers = new Dictionary<NotificationType, ITaskNotifier>
{
    { NotificationType.Console, new ConsoleNotifier() },
    { NotificationType.Email, new EmailNotifier() },
    { NotificationType.FileLog, new FileLogNotifier() },
    { NotificationType.Slack, new SlackNotifier() }
};

var service = new TaskService(repository, validator, notifiers);

service.AddTask(new TaskItem(
    1,
    "Invat SOLID",
    "Recapitulez principiile SRP, OCP si LSP",
    TaskPriority.High,
    NotificationType.Console));

service.AddTask(new DeadlineTask(
    2,
    "Predau tema",
    "Incarc proiectul pe GitHub",
    TaskPriority.High,
    NotificationType.Email,
    DateTime.Now.AddDays(2)));

service.AddTask(new RecurringTask(
    3,
    "Backup proiect",
    "Salvez proiectul periodic",
    TaskPriority.Medium,
    NotificationType.FileLog,
    DateTime.Now.AddDays(1),
    7));

service.AddTask(new TaskItem(
    4,
    "Anunt pe Slack",
    "Testez SlackNotifier",
    TaskPriority.Low,
    NotificationType.Slack));

service.CompleteTask(1);

foreach (var task in service.GetTasks())
{
    Console.WriteLine(
        $"{task.Id} - {task.Title} - {task.TaskType} - {task.Priority} - {task.Status}");
}