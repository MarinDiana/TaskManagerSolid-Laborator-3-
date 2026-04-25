using TaskManagerSolid.Models;
using System.Collections.Generic;

namespace TaskManagerSolid.Interfaces
{
    public interface ITaskReader
    {
        List<TaskItem> GetAll();
        TaskItem? GetById(int id);
    }
}