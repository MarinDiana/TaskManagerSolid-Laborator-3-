using TaskManagerSolid.Interfaces;

namespace TaskManagerSolid.Repositories
{
    public interface ITaskRepository : ITaskReader, ITaskWriter
    {
    }
}