namespace DTOs.Entities
{
    public class ToDoTaskDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool IsDone { get; set; }
    }
}