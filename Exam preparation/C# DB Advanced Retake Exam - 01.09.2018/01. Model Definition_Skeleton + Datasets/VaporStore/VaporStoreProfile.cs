namespace VaporStore
{
    using AutoMapper;
    using System.Linq;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.ExportDtos;

    public class VaporStoreProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public VaporStoreProfile()
        {
            this.CreateMap<Game, ExportGameDTO>()
                .ForMember(x => x.Genre, y => y.MapFrom(z => z.Genre.Name))
                .ForMember(x => x.Title, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price));

            this.CreateMap<Purchase, ExportPurchaseDTO>()
                .ForMember(x => x.Card, y => y.MapFrom(z => z.Card.Number))
                .ForMember(x => x.Cvc, y => y.MapFrom(z => z.Card.Cvc))
                .ForMember(x => x.Date, y => y.MapFrom(z => z.Date))
                .ForMember(x => x.Game, y => y.MapFrom(z => z.Game));

            this.CreateMap<User, ExportUserDTO>()
                .ForMember(x => x.Purchases, y => y.MapFrom(z => z.Cards.Select(c => c.Purchases)))
                .ForMember(x => x.TotalSpent, y => y.MapFrom(z => z.Cards.Sum(c => c.Purchases.Sum(p => p.Game.Price))));
        }
    }
}