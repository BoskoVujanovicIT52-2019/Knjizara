using AutoMapper;
using Knjizara.Model;
using Knjizara.Model.DTO;

namespace Knjizara.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<Korisnik, KorisnikRegDTO>();
            CreateMap<KorisnikRegDTO, Korisnik>();

            CreateMap<KorisnikLogDTO, Korisnik>();
            CreateMap<Korisnik,KorisnikLogDTO>();

        }
    }
}
