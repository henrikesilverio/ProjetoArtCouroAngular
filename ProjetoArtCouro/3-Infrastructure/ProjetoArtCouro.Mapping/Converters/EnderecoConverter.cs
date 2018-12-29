using AutoMapper;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Common;
using System.Collections.Generic;

namespace ProjetoArtCouro.Mapping.Converters
{
    public class EnderecoConverter : ITypeConverter<EnderecoModel, ICollection<Endereco>>
    {
        public ICollection<Endereco> Convert(
            EnderecoModel modelo, 
            ICollection<Endereco> entidades, 
            ResolutionContext contexto)
        {
            if (modelo == null)
            {
                return entidades;
            }

            return new List<Endereco>
            {
                new Endereco
                {
                    EnderecoCodigo = modelo.EnderecoId ?? 0,
                    Logradouro = modelo.Logradouro,
                    Numero = modelo.Numero,
                    Bairro = modelo.Bairro,
                    Complemento = modelo.Complemento,
                    Cidade = modelo.Cidade,
                    CEP = modelo.Cep,
                    Estado = new Estado { EstadoCodigo = modelo.UFId ?? 0 },
                    Principal = true
                }
            };
        }
    }
}
