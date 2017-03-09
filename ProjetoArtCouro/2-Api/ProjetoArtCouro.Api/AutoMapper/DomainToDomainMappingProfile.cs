using AutoMapper;
using ProjetoArtCouro.Domain.Entities.Pessoas;

namespace ProjetoArtCouro.Api.AutoMapper
{
    public class DomainToDomainMappingProfile : Profile
    {
        //Configuração de mapeamento do domain para domain
        public DomainToDomainMappingProfile()
        {
            MapperPerson();
        }

        private void MapperPerson()
        {
            CreateMap<Pessoa, Pessoa>();
        }
    }
}