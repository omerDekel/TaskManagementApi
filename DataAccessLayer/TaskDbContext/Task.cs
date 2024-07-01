using System;
using System.Collections.Generic;

namespace DataAccessLayer.TaskDbContext
{
    public partial class Task
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool? IsDone { get; set; }
    }
}
