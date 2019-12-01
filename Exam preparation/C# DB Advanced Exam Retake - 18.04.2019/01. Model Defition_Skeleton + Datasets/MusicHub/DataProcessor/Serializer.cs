namespace MusicHub.DataProcessor
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Data;
    using AutoMapper.QueryableExtensions;
    using MusicHub.DataProcessor.ExportDtos;
    using Newtonsoft.Json;
    using System.Xml.Serialization;
    using System.IO;

    public class Serializer
    {
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albumsToExport = context
                .Albums
                .Where(a => a.ProducerId == producerId)
                //.OrderByDescending(a => a.Songs.Sum(s => s.Price))
                .OrderByDescending(a => a.Price)
                .ProjectTo<ExportAlbumByProducerDTO>()
                //albumprice is string i am so stupid the code below is wrong
                //!!!!.OrderByDescending(ep=>ep.AlbumPrice)
                .ToList();

            foreach (var album in albumsToExport)
            {
                album.Songs = album.Songs.OrderByDescending(s => s.SongName).ThenBy(s => s.Writer).ToList();
            }

            var output = JsonConvert.SerializeObject(albumsToExport, Formatting.Indented);

            return output;
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var serializer = new XmlSerializer(typeof(List<ExportSongDTO>), new XmlRootAttribute("Songs"));

            var songsToExport = context
                .Songs
                .Where(s => s.Duration.TotalSeconds > duration)
                .ProjectTo<ExportSongDTO>()
                .OrderBy(s => s.SongName)
                .ThenBy(s => s.Writer)
                .ThenBy(s => s.Performer)
                .ToList();

            StringBuilder sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, songsToExport, namespaces);
            }


            return sb.ToString().TrimEnd();
        }
    }
}