namespace MusicHub
{
    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.DataProcessor.ExportDtos;
    using MusicHub.DataProcessor.ImportDtos;
    using System;
    using System.Globalization;
    using System.Linq;

    public class MusicHubProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public MusicHubProfile()
        {
            this.CreateMap<ImportWriterDTO, Writer>();

            this.CreateMap<ImportAlbumDTO, Album>();

            this.CreateMap<ImportProducerDTO, Producer>();

            this.CreateMap<ImportSongDTO, Song>()
                .ForMember(x => x.CreatedOn, y => y.MapFrom(x => DateTime.ParseExact(x.CreatedOn, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(x => x.Duration, y => y.MapFrom(x => TimeSpan.ParseExact(x.Duration, "c", CultureInfo.InvariantCulture)))
                .ForMember(x => x.Genre, y => y.MapFrom(x => Enum.Parse(typeof(Genre), x.Genre)));

            this.CreateMap<ImportPerformer, Performer>();
            this.CreateMap<ImportSongPerformerDTO, SongPerformer>()
                .ForMember(t => t.SongId, y => y.MapFrom(x => x.Id));

            //Export

            this.CreateMap<Song, ExportSongFromAlbumDTO>()
                .ForMember(x => x.SongName, y => y.MapFrom(x => x.Name))
                .ForMember(x => x.Writer, y => y.MapFrom(x => x.Writer.Name))
                .ForMember(x => x.Price, y => y.MapFrom(x => x.Price.ToString("f2")));

            this.CreateMap<Album, ExportAlbumByProducerDTO>()
                .ForMember(x => x.AlbumName, y => y.MapFrom(x => x.Name))
                .ForMember(x => x.ReleaseDate, y => y.MapFrom(x => x.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(x => x.AlbumPrice, y => y.MapFrom(x => x.Price.ToString("f2")));

            this.CreateMap<Song, ExportSongDTO>()
                .ForMember(x => x.Writer, y => y.MapFrom(x => x.Writer.Name))
                .ForMember(x => x.SongName, y => y.MapFrom(x => x.Name))
                .ForMember(x => x.Performer, y => y.MapFrom(x => x.SongPerformers.Select(z => z.Performer.FirstName + " " + z.Performer.LastName).FirstOrDefault()))
                .ForMember(x => x.AlbumProducer, y => y.MapFrom(x => x.Album.Producer.Name))
                .ForMember(x => x.Duration, y => y.MapFrom(x => x.Duration.ToString("c")));
        }
    }
}
