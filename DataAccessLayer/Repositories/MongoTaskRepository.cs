using AutoMapper;
using DTOs.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccessLayer.Contracts;
using TaskManagement.DataAccessLayer.TaskDbContext;

namespace TaskManagement.DataAccessLayer.Repositories
{
    public class MongoTaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<MongoToDoTask> _collection;
        private readonly IMapper _mapper;


        public MongoTaskRepository(string connectionString, string databaseName, string collectionName, IMapper mapper)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<MongoToDoTask>(collectionName);
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToDoTaskDto>> GetAllAsync()
        {
            var mongoTasks = await _collection.Find(FilterDefinition<MongoToDoTask>.Empty).ToListAsync();
            var dtoTasks = _mapper.Map<IEnumerable<ToDoTaskDto>>(mongoTasks);
            return dtoTasks;
        }

        public async Task<ToDoTaskDto> GetByIdAsync(int id)
        {
            var mongoTask = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ToDoTaskDto>(mongoTask);
        }

        public async Task<bool> AddAsync(ToDoTaskDto task)
        {
            var mongoId = ObjectId.GenerateNewId().ToString();
            task.Id = ConvertObjectIdToInt(mongoId);

            var mongoTask = _mapper.Map<MongoToDoTask>(task);
            mongoTask._id = mongoId;

            await _collection.InsertOneAsync(mongoTask);
            return true;
        }

        public async Task<bool> UpdateAsync(ToDoTaskDto task)
        {
            var mongoTask = _mapper.Map<MongoToDoTask>(task);
            if (mongoTask._id == null)
            {
                //Get the MongoId from db
                var taskDto = await GetByIdAsync(task.Id);
                if (taskDto != null && taskDto.PersistentId != null)
                {
                    mongoTask._id = taskDto.PersistentId.ToString();
                }
            }
            var result = await _collection.ReplaceOneAsync(x => x.Id == mongoTask.Id, mongoTask);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _collection.DeleteOneAsync(x => x.Id == id);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }
        private int ConvertObjectIdToInt(string objectId)
        {
            return int.Parse(objectId.Substring(0, 8), System.Globalization.NumberStyles.HexNumber);
        }
    }
}
