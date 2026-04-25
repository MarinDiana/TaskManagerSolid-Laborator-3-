using TaskManagerSolid.Models;

namespace TaskManagerSolid.Interfaces
{
    public interface ITaskWriter
    {
        void Add(TaskItem task);
        void Update(TaskItem task);
        void Delete(int id);
    }
}