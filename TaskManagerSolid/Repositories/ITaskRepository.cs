using TaskManagerSolid.Models;
using System.Collections.Generic;

namespace TaskManagerSolid.Repositories
{
    public interface ITaskRepository
    {
        List<TaskItem> GetAll();
        TaskItem? GetById(int id);
        void Add(TaskItem task);
        void Update(TaskItem task);
        void Delete(int id);
    }
}