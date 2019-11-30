namespace MusicHub.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Data;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.DataProcessor.ImportDtos;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter
            = "Imported {0}";
        private const string SuccessfullyImportedProducerWithPhone
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";
        private const string SuccessfullyImportedSong
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            var writersToImport = JsonConvert.DeserializeObject<List<ImportWriterDTO>>(jsonString);

            StringBuilder sb = new StringBuilder();

            var writers = new List<Writer>();

            foreach (var writer in writersToImport)
            {
                var newWriter = Mapper.Map<Writer>(writer);

                if (!IsValid(newWriter))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                else
                {
                    writers.Add(newWriter);

                    sb.AppendLine(string.Format(SuccessfullyImportedWriter, newWriter.Name));
                }
            }

            context.Writers.AddRange(writers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            var producerDtos = JsonConvert.DeserializeObject<List<ImportProducerDTO>>(jsonString);

            StringBuilder sb = new StringBuilder();

            var producers = new List<Producer>();

            foreach (var producerDto in producerDtos)
            {
                if (!IsValid(producerDto) || !producerDto.Albums.All(IsValid))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var producer = Mapper.Map<Producer>(producerDto);

                var isValidProducer = IsValid(producer);

                var areAllAlbumsValid = producer.Albums.All(a => IsValid(a));

                if (areAllAlbumsValid == false || isValidProducer == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                producers.Add(producer);

                if (producer.PhoneNumber == null)
                {
                    sb.AppendLine(string.Format(SuccessfullyImportedProducerWithNoPhone, producer.Name, producer.Albums.Count));
                }
                else
                {
                    sb.AppendLine(string.Format(SuccessfullyImportedProducerWithPhone, producer.Name, producer.PhoneNumber, producer.Albums.Count));
                }
            }

            context.Producers.AddRange(producers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ImportSongDTO>), new XmlRootAttribute("Songs"));

            List<ImportSongDTO> songDTOs;

            using (var reader = new StringReader(xmlString))
            {
                songDTOs = (List<ImportSongDTO>)xmlSerializer.Deserialize(reader);
            }

            StringBuilder sb = new StringBuilder();

            var songs = new List<Song>();

            foreach (var songDto in songDTOs)
            {
                var isValidGenre = Enum.IsDefined(typeof(Genre), songDto.Genre);
                var isValidAlbum = context.Albums.Any(a => a.Id == songDto.AlbumId);
                var isValidWriter = context.Writers.Any(w => w.Id == songDto.WriterId);

                if (isValidGenre == false || isValidAlbum == false || isValidWriter == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var song = Mapper.Map<Song>(songDto);

                if (IsValid(song) == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                sb.AppendFormat(SuccessfullyImportedSong, song.Name, song.Genre.ToString(), song.Duration.ToString());
                sb.AppendLine();
                songs.Add(song);
            }

            context.Songs.AddRange(songs);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(List<ImportPerformer>), new XmlRootAttribute("Performers"));

            var performerDtos = new List<ImportPerformer>();

            using (var reader = new StringReader(xmlString))
            {
                performerDtos = (List<ImportPerformer>)serializer.Deserialize(reader);
            }

            var validPerformers = new List<Performer>();

            StringBuilder sb = new StringBuilder();

            foreach (var performerDto in performerDtos)
            {

                if (IsValid(performerDto) == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                sb.AppendLine("-");
            }

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(entity);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);

            return isValid;
        }
    }
}