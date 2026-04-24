using TaskManagerSolid.Models;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagerSolid.Repositories
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> tasks = new List<TaskItem>();

        public List<TaskItem> GetAll()
        {
            return tasks;
        }

        public TaskItem? GetById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public void Add(TaskItem task)
        {
            tasks.Add(task);
        }

        public void Update(TaskItem task)
        {
            var existingTask = GetById(task.Id);

            if (existingTask != null)
            {
                Delete(task.Id);
                tasks.Add(task);
            }
        }

        public void Delete(int id)
        {
            var task = GetById(id);

            if (task != null)
            {
                tasks.Remove(task);
            }
        }
    }
}