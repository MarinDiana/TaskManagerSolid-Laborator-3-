using TaskManagerSolid.Interfaces;
using TaskManagerSolid.Models;
using System.Linq;
using TaskStatus = TaskManagerSolid.Models.TaskStatus;
namespace TaskManagerSolid.Services
{
    public class ReportService
    {
        private readonly ITaskReader reader;

        public ReportService(ITaskReader reader)
        {
            this.reader = reader;
        }

        public string GenerateSummary()
        {
            var tasks = reader.GetAll();

            int total = tasks.Count;
            int done = tasks.Count(t => t.Status == TaskStatus.Done);

            return "Total: " + total + ", Done: " + done;
        }
    }
}