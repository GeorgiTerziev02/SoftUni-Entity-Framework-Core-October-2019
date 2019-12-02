namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enumerations;
    using VaporStore.DataProcessor.Dtos;

    public static class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";
        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var gameDtos = JsonConvert.DeserializeObject<List<ImportGameDTO>>(jsonString);

            StringBuilder sb = new StringBuilder();

            var genres = new List<Genre>();
            var developers = new List<Developer>();
            var tags = new List<Tag>();

            foreach (var gameDto in gameDtos)
            {
                if (IsValid(gameDto) == false || gameDto.Tags == null || gameDto.Tags.Count == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var game = new Game()
                {
                    Name = gameDto.Name,
                    Price = gameDto.Price,
                    ReleaseDate = DateTime.ParseExact(gameDto.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                };

                var developer = context.Developers.Where(d => d.Name == gameDto.Developer).FirstOrDefault();

                if (developer == null)
                {
                    developer = new Developer()
                    {
                        Name = gameDto.Developer
                    };

                    context.Developers.Add(developer);
                }

                var genre = context.Genres.Where(g => g.Name == gameDto.Genre).FirstOrDefault();

                if (genre == null)
                {
                    genre = new Genre()
                    {
                        Name = gameDto.Genre
                    };

                    context.Genres.Add(genre);
                }

                genre.Games.Add(game);

                developer.Games.Add(game);

                game.Genre = genre;
                game.Developer = developer;

                foreach (var tagName in gameDto.Tags)
                {
                    var tag = context.Tags.Where(t => t.Name == tagName).FirstOrDefault();

                    if (tag == null)
                    {
                        tag = new Tag()
                        {
                            Name = tagName
                        };

                        context.Tags.Add(tag);
                    }

                    var gameTag = new GameTag()
                    {
                        Game = game,
                        Tag = tag
                    };

                    game.GameTags.Add(gameTag);
                    context.GameTags.Add(gameTag);
                }

                context.Games.Add(game);
                context.SaveChanges();

                sb.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");
            }

            return sb.ToString().TrimEnd();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var userDtos = JsonConvert.DeserializeObject<List<ImportUserDTO>>(jsonString);

            StringBuilder sb = new StringBuilder();

            var users = new List<User>();
            var cards = new List<Card>();

            foreach (var userDto in userDtos)
            {
                //TODO: NO cards check
                if (IsValid(userDto) == false || userDto.Cards.All(IsValid) == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var user = new User()
                {
                    Username = userDto.Username,
                    FullName = userDto.FullName,
                    Age = userDto.Age,
                    Email = userDto.Email
                };

                //TODO: IS defined enum
                foreach (var cardDto in userDto.Cards)
                {
                    var card = new Card()
                    {
                        Cvc = cardDto.CVC,
                        Number = cardDto.Number,
                        Type = Enum.Parse<CardType>(cardDto.Type),
                        User = user,
                    };

                    user.Cards.Add(card);
                    cards.Add(card);
                }

                sb.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");
            }

            context.AddRange(users);
            context.AddRange(cards);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(List<ImportPurchaseDTO>), new XmlRootAttribute("Purchases"));

            List<ImportPurchaseDTO> purchaseDTOs;
            using (var reader = new StringReader(xmlString))
            {
                purchaseDTOs = (List<ImportPurchaseDTO>)serializer.Deserialize(reader);
            }

            StringBuilder sb = new StringBuilder();

            var purchases = new List<Purchase>();

            foreach (var purchaseDto in purchaseDTOs)
            {
                if (IsValid(purchaseDto) == false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var purchase = new Purchase()
                {
                    Game = context.Games.Where(g => g.Name == purchaseDto.GameName).FirstOrDefault(),
                    ProductKey = purchaseDto.ProductKey,
                    Card = context.Cards.Where(c => c.Number == purchaseDto.CardNumber).FirstOrDefault(),
                    Date = DateTime.ParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    Type = Enum.Parse<PurchaseType>(purchaseDto.Type)
                };

                purchases.Add(purchase);

                sb.AppendLine($"Imported {purchase.Game.Name} for {purchase.Card.User.Username}");
            }

            context.AddRange(purchases);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResults = new List<ValidationResult>();

            bool result = Validator.TryValidateObject(entity, validationContext, validationResults, true);

            return result;
        }
    }
}