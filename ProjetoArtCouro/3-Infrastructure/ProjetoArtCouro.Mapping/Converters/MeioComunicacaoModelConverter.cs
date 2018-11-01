using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Common;

namespace ProjetoArtCouro.Mapping.Converters
{
    public class MeioComunicacaoModelConverter : ITypeConverter<ICollection<MeioComunicacao>, MeioComunicacaoModel>
    {
        public MeioComunicacaoModel Convert(
            ICollection<MeioComunicacao> entidades, 
            MeioComunicacaoModel modelo, 
            ResolutionContext context)
        {
            var meioComunicacaoModel = new MeioComunicacaoModel();
            foreach (var entidade in entidades.Where(x => x.Principal).ToList())
            {
                MapearPorTipoComunicacao(entidades, entidade, meioComunicacaoModel, entidade.TipoComunicacao);
            }
            return meioComunicacaoModel;
        }

        private void MapearPorTipoComunicacao(
            ICollection<MeioComunicacao> entidades,
            MeioComunicacao entidade,
            MeioComunicacaoModel modelo,
            TipoComunicacaoEnum tipo)
        {
            var propriedades = new[]
            {
                new { Tipo = TipoComunicacaoEnum.Telefone, Propriedades = new [] { "TelefoneId", "Telefone", "Telefones" } },
                new { Tipo = TipoComunicacaoEnum.Celular, Propriedades = new [] { "CelularId", "Celular", "Celulares" } },
                new { Tipo = TipoComunicacaoEnum.Email, Propriedades = new [] { "EmailId", "Email", "Emails" } }
            }.FirstOrDefault(x => x.Tipo == tipo)?.Propriedades;

            if (propriedades != null)
            {
                var id = modelo.GetType().GetProperty(propriedades[0]);
                id.SetValue(modelo, entidade.MeioComunicacaoCodigo);

                var nome = modelo.GetType().GetProperty(propriedades[1]);
                nome.SetValue(modelo, entidade.MeioComunicacaoNome);

                var lista = modelo.GetType().GetProperty(propriedades[2]);
                lista.SetValue(modelo, entidades
                    .Where(x => x.TipoComunicacao == tipo)
                    .Select(x => new LookupModel
                    {
                        Codigo = x.MeioComunicacaoCodigo,
                        Nome = x.MeioComunicacaoNome
                    })
                    .ToList());
            }
        }
    }
}