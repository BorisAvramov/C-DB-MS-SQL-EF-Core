namespace SoftJail
{
    using AutoMapper;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ExportDto;
    using SoftJail.DataProcessor.ImportDto;
    using System.Linq;

    public class SoftJailProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public SoftJailProfile()
        {

            CreateMap<ImportDepartmentCellDto, Cell>();
            CreateMap<ImportPrisonerMailDto, Mail>();
            CreateMap<ImportOfficerPrisonerDto, OfficerPrisoner>()
                .ForMember(d => d.PrisonerId, opt => opt.MapFrom(s => s.Id));

            CreateMap<Prisoner, ExportPrisonerWithMails>()
                .ForMember(d => d.IncarcerationDate, opt => opt.MapFrom(s =>  s.IncarcerationDate.ToString("yyyy-MM-dd")))
                .ForMember(d => d.Mails, opt => opt.MapFrom(s => s.Mails));
            CreateMap<Mail, ExportPrisonerMailcs>()
                .ForMember(d => d.Description, opt => opt.MapFrom(s => string.Join("", s.Description.Reverse()) ));
        }
    }
}
