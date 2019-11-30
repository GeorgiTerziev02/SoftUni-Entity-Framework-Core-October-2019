namespace MusicHub.Data.Models
{
    public class SongPerformer
    {
        public int PerformerId { get; set; }

        public Performer Performer { get; set; }

        public int SongId { get; set; }

        public Song Song { get; set; }
    }
}
