using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Common;

namespace ProjetoArtCouro.Api.AutoMapper.Converters
{
    public class EnderecoModelConverter : ITypeConverter<ICollection<Endereco>, EnderecoModel>
    {
        public EnderecoModel Convert(ICollection<Endereco> source, EnderecoModel destination, ResolutionContext context)
        {
            var listaEndereco = source;
            return Mapper.Map<EnderecoModel>(listaEndereco.FirstOrDefault(x => x.Principal));
        }
    }
}