using TaskManagerSolid.Models;

namespace TaskManagerSolid.Notifications
{
    public interface ITaskNotifier
    {
        void Notify(TaskItem task);
    }
}