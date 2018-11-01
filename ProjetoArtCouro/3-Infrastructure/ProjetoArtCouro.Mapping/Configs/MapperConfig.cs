using AutoMapper;
using ProjetoArtCouro.Mapping.Profiles;

namespace ProjetoArtCouro.Mapping.Configs
{
    public static class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Reset();
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToModelMappingProfile>();
                x.AddProfile<DomainToDomainMappingProfile>();
                x.AddProfile<ModelToDomainMappingProfile>();
                x.AddProfile<ModelToModelMappingProfile>();
            });
        }
    }
}