using MongoDB.Bson;

namespace game.Models
{
    public class HighScore
    {
        public ObjectId Id { get; set; }
        public int Score { get; set; }
        public string Name { get; set; }

    }
}
