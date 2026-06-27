namespace BGS.Shared.Dtos
{
    public class JoinGameResponseDto
    {
        public Guid GameId { get; set; }
        public Guid PlayerToken { get; set; }
        public object InitialState { get; set; }
    }
}