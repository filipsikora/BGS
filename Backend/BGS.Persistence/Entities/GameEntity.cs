using System.ComponentModel.DataAnnotations;

namespace BGS.Persistence.Entities
{
    public class GameEntity
    {
        [Key]
        public Guid GameId { get; set; }
        public string GameType { get; set; }
        public string StateJson { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}