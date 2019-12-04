namespace SoftJail
{
    using AutoMapper;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Globalization;

    public class SoftJailProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public SoftJailProfile()
        {
            //import
            this.CreateMap<ImportCellDTO, Cell>();
            this.CreateMap<ImportDepartmentDTO, Department>();

            this.CreateMap<ImportMailDTO, Mail>();
            this.CreateMap<ImportPrisonerDTO, Prisoner>()
                .ForMember(x => x.IncarcerationDate, y => 
                            y.MapFrom(z => DateTime.ParseExact(z.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(x=>x.ReleaseDate, y=>
                            y.MapFrom(z => DateTime.ParseExact(z.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

        }
    }
}
