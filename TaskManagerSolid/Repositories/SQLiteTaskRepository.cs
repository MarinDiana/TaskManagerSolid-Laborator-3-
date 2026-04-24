using Microsoft.Data.Sqlite;
using TaskManagerSolid.Models;
using System;
using System.Collections.Generic;

namespace TaskManagerSolid.Repositories
{
    public class SQLiteTaskRepository : ITaskRepository
    {
        private readonly string connectionString = "Data Source=tasks.db";

        public SQLiteTaskRepository()
        {
            CreateTable();
        }

        private void CreateTable()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS Tasks (
                    Id INTEGER PRIMARY KEY,
                    Title TEXT NOT NULL,
                    Description TEXT NOT NULL,
                    Status TEXT NOT NULL,
                    Priority INTEGER NOT NULL,
                    TaskType TEXT NOT NULL,
                    NotificationType TEXT NOT NULL,
                    DueDate TEXT,
                    RecurrenceInterval INTEGER,
                    CreatedAt TEXT NOT NULL
                );";

            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public List<TaskItem> GetAll()
        {
            var tasks = new List<TaskItem>();

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = "SELECT * FROM Tasks";
            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                tasks.Add(ReadTask(reader));
            }

            return tasks;
        }

        public TaskItem? GetById(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = "SELECT * FROM Tasks WHERE Id = @Id";
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
                return ReadTask(reader);

            return null;
        }

        public void Add(TaskItem task)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = @"
                INSERT OR REPLACE INTO Tasks
                (Id, Title, Description, Status, Priority, TaskType, NotificationType, DueDate, RecurrenceInterval, CreatedAt)
                VALUES
                (@Id, @Title, @Description, @Status, @Priority, @TaskType, @NotificationType, @DueDate, @RecurrenceInterval, @CreatedAt);";

            using var command = new SqliteCommand(sql, connection);
            AddParameters(command, task);
            command.ExecuteNonQuery();
        }

        public void Update(TaskItem task)
        {
            Add(task);
        }

        public void Delete(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            string sql = "DELETE FROM Tasks WHERE Id = @Id";
            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }

        private void AddParameters(SqliteCommand command, TaskItem task)
        {
            command.Parameters.AddWithValue("@Id", task.Id);
            command.Parameters.AddWithValue("@Title", task.Title);
            command.Parameters.AddWithValue("@Description", task.Description);
            command.Parameters.AddWithValue("@Status", task.Status.ToString());
            command.Parameters.AddWithValue("@Priority", (int)task.Priority);
            command.Parameters.AddWithValue("@TaskType", task.TaskType.ToString());
            command.Parameters.AddWithValue("@NotificationType", task.NotificationType.ToString());
            command.Parameters.AddWithValue("@DueDate", task.DueDate?.ToString("o") ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@RecurrenceInterval", task.RecurrenceInterval ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@CreatedAt", task.CreatedAt.ToString("o"));
        }

        private TaskItem ReadTask(SqliteDataReader reader)
        {
            int id = Convert.ToInt32(reader["Id"]);
            string title = reader["Title"].ToString()!;
            string description = reader["Description"].ToString()!;
            var priority = (TaskPriority)Convert.ToInt32(reader["Priority"]);
            var notificationType = Enum.Parse<NotificationType>(reader["NotificationType"].ToString()!);
            var taskType = Enum.Parse<TaskType>(reader["TaskType"].ToString()!);

            DateTime? dueDate = reader["DueDate"] == DBNull.Value
                ? null
                : DateTime.Parse(reader["DueDate"].ToString()!);

            int? recurrenceInterval = reader["RecurrenceInterval"] == DBNull.Value
                ? null
                : Convert.ToInt32(reader["RecurrenceInterval"]);

            TaskItem task;

            if (taskType == TaskType.Recurring)
            {
                task = new RecurringTask(id, title, description, priority, notificationType, dueDate!.Value, recurrenceInterval!.Value);
            }
            else if (taskType == TaskType.Deadline)
            {
                task = new DeadlineTask(id, title, description, priority, notificationType, dueDate!.Value);
            }
            else
            {
                task = new TaskItem(id, title, description, priority, notificationType);
            }

            return task;
        }
    }
}