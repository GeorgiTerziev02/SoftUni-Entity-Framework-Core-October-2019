namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.DataProcessor.ExportDtos;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var genre = context
                .Genres
                .Where(g => genreNames.Contains(g.Name))
                .Where(g => g.Games.Any(game => game.Purchases.Count >= 1))
                .Select(g => new
                {
                    Id = g.Id,
                    Genre = g.Name,
                    Games = g.Games.Where(game => game.Purchases.Count() >= 1).Select(game => new
                    {
                        Id = game.Id,
                        Title = game.Name,
                        Developer = game.Developer.Name,
                        Tags = string.Join(", ", game.GameTags.Select(gt => gt.Tag.Name)),
                        Players = game.Purchases.Count()
                    })
                    .OrderByDescending(game => game.Players)
                    .ThenBy(game => game.Id),
                    TotalPlayers = g.Games.Where(tp => tp.Purchases.Any())
                                            .Sum(game => game.Purchases.Count)
                })
                .OrderByDescending(g => g.TotalPlayers)
                .ThenBy(g => g.Id)
                .ToList();

            var output = JsonConvert.SerializeObject(genre, Formatting.Indented);

            return output;
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var serializer = new XmlSerializer(typeof(List<ExportUserDTO>), new XmlRootAttribute("Users"));

            var usersToExport = context
                .Users
                .Select(u => new ExportUserDTO
                {
                    Username = u.Username,
                    Purchases = u.Cards.SelectMany(c => c.Purchases)
                                    .Where(c => c.Type.ToString() == storeType)
                                    .Select(p => new ExportPurchaseDTO
                                    {
                                        Card = p.Card.Number,
                                        Cvc = p.Card.Cvc,
                                        Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                                        Game = new ExportGameDTO
                                        {
                                            Genre = p.Game.Genre.Name,
                                            Price = p.Game.Price,
                                            Title = p.Game.Name
                                        }
                                    })
                                    .OrderBy(p => p.Date)
                                    .ToList(),
                    TotalSpent = u.Cards.Sum(c => c.Purchases.Where(p => p.Type.ToString() == storeType).Sum(p => p.Game.Price))
                })
                .Where(u => u.Purchases.Any())
                .OrderByDescending(u => u.TotalSpent)
                .ThenBy(u => u.Username)
                .ToList();

            StringBuilder sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, usersToExport, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}