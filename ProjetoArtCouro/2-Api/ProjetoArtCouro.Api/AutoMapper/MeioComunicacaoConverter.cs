using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Common;

namespace ProjetoArtCouro.Api.AutoMapper
{
    public class MeioComunicacaoConverter : ITypeConverter<ICollection<MeioComunicacao>, MeioComunicacaoModel>
    {
        public MeioComunicacaoModel Convert(ICollection<MeioComunicacao> source, MeioComunicacaoModel destination, ResolutionContext context)
        {
            var listaMeioComunicacao = source;
            var meioComunicacaoModel = new MeioComunicacaoModel();
            foreach (var item in listaMeioComunicacao.Where(x => x.Principal).ToList())
            {
                if (item.TipoComunicacao.Equals(TipoComunicacaoEnum.Telefone))
                {
                    meioComunicacaoModel.TelefoneId = item.MeioComunicacaoCodigo;
                    meioComunicacaoModel.Telefone = item.MeioComunicacaoNome;
                    meioComunicacaoModel.Telefones =
                        listaMeioComunicacao.Where(x => x.TipoComunicacao.Equals(TipoComunicacaoEnum.Telefone))
                            .Select(x => new LookupModel
                            {
                                Codigo = x.MeioComunicacaoCodigo,
                                Nome = x.MeioComunicacaoNome
                            }).ToList();
                }
                else if (item.TipoComunicacao.Equals(TipoComunicacaoEnum.Celular))
                {
                    meioComunicacaoModel.CelularId = item.MeioComunicacaoCodigo;
                    meioComunicacaoModel.Celular = item.MeioComunicacaoNome;
                    meioComunicacaoModel.Celulares =
                        listaMeioComunicacao.Where(x => x.TipoComunicacao.Equals(TipoComunicacaoEnum.Celular))
                            .Select(x => new LookupModel
                            {
                                Codigo = x.MeioComunicacaoCodigo,
                                Nome = x.MeioComunicacaoNome
                            }).ToList();
                }
                else if (item.TipoComunicacao.Equals(TipoComunicacaoEnum.Email))
                {
                    meioComunicacaoModel.EmailId = item.MeioComunicacaoCodigo;
                    meioComunicacaoModel.Email = item.MeioComunicacaoNome;
                    meioComunicacaoModel.Emalis =
                        listaMeioComunicacao.Where(x => x.TipoComunicacao.Equals(TipoComunicacaoEnum.Celular))
                            .Select(x => new LookupModel
                            {
                                Codigo = x.MeioComunicacaoCodigo,
                                Nome = x.MeioComunicacaoNome
                            }).ToList();
                }
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