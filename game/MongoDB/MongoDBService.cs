using System.Runtime.CompilerServices;
using game.Models;
using MongoDB.Driver;

namespace game.MongoDB
{
    public class MongoDBService
    {
        private readonly IMongoCollection<HighScore> _highScores;

        public MongoDBService(string database)
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGO_DB_CONNECTION");
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(database);
            _highScores = db.GetCollection<HighScore>("HighScores");
        }

        public async Task<List<HighScore>> GetScoresAsync()
        {
            var allHighScores = await _highScores.Find(_ => true).ToListAsync();

            if (allHighScores.Count == 0)
            {
                var newHighScore = new HighScore
                {
                    Score = 0,
                    Name = "Start"
                };
                await _highScores.InsertOneAsync(newHighScore);

                allHighScores = await _highScores.Find(_ => true).ToListAsync();
            }

            var sortedHighScores = allHighScores.OrderByDescending(h => h.Score).ToList();
            return sortedHighScores;

        }

        public async Task AddHighScoreAsync(HighScore newHighScore)
        {
            await _highScores.InsertOneAsync(newHighScore);
        }
    }
}
