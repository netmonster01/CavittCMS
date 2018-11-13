namespace cavitt.net.Dtos
{
    public class VoteDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }

        public int UserVote { get; set; }
        public string UserId { get; set; }
    }
}
