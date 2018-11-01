using AutoMapper;

namespace ProjetoArtCouro.Mapping
{
    public static class Map<From> where From : class
    {
        public static From MapperTo<To>(To to) where To : class
        {
            return Mapper.Map<From>(to);
        }
    }
}
