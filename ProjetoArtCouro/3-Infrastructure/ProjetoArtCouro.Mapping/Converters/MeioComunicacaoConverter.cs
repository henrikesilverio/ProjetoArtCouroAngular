using AutoMapper;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Domain.Models.Enums;
using System.Collections.Generic;

namespace ProjetoArtCouro.Mapping.Converters
{
    public class MeioComunicacaoConverter : ITypeConverter<MeioComunicacaoModel, ICollection<MeioComunicacao>>
    {
        public ICollection<MeioComunicacao> Convert(
            MeioComunicacaoModel modelo, 
            ICollection<MeioComunicacao> entidades, 
            ResolutionContext contexto)
        {
            if (modelo == null)
            {
                return entidades;
            }

            return MapearMeioComunicacao(modelo);
        }

        private List<MeioComunicacao> MapearMeioComunicacao(MeioComunicacaoModel modelo)
        {
            var retorno = new List<MeioComunicacao>();

            var propriedades = new[]
            {
                new { Tipo = TipoComunicacaoEnum.Telefone, Id = "TelefoneId", Nome = "Telefone" },
                new { Tipo = TipoComunicacaoEnum.Celular, Id = "CelularId", Nome = "Celular" },
                new { Tipo = TipoComunicacaoEnum.Email, Id = "EmailId", Nome = "Email"  }
            };

            foreach (var propriedade in propriedades)
            {
                var nome = (string)modelo.GetType().GetProperty(propriedade.Nome).GetValue(modelo);

                if (!string.IsNullOrWhiteSpace(nome))
                {
                    var id = (int?)modelo.GetType().GetProperty(propriedade.Id).GetValue(modelo);

                    retorno.Add(new MeioComunicacao
                    {
                        MeioComunicacaoCodigo = id ?? 0,
                        MeioComunicacaoNome = nome,
                        TipoComunicacao = propriedade.Tipo,
                        Principal = true
                    });
                }
            }

            return retorno;
        }
    }
}