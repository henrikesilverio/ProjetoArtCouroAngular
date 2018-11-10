using System;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Domain.Entities.Pessoas
{
    public class MeioComunicacao : Notifiable
    {
        public Guid MeioComunicacaoId { get; set; }
        public int MeioComunicacaoCodigo { get; set; }
        public string MeioComunicacaoNome { get; set; }
        public TipoComunicacaoEnum TipoComunicacao { get; set; }
        public bool Principal { get; set; }
        public Guid PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public void Validar()
        {
            new ValidationContract<MeioComunicacao>(this)
                .IsRequired(x => x.MeioComunicacaoNome)
                .HasMaxLenght(x => x.MeioComunicacaoNome, 250)
                .IsNotEquals(x => x.TipoComunicacao, TipoComunicacaoEnum.None)
                .IsRequired(x => x.Principal);

            if (!IsValid())
            {
                throw new DomainException(GetMergeNotifications());
            }
        }
    }
}
