using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DTOs.Entities
{
    public class ToDoTaskDto
    {
        [BsonId]
        public ObjectId? PersistentId { get; set; } // MongoDB's ObjectId
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool IsDone { get; set; }
    }
}