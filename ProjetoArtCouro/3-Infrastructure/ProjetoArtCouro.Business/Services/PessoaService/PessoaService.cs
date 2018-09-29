using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Contracts.IService.IPessoa;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Pessoa;

namespace ProjetoArtCouro.Business.Services.PessoaService
{
    public class PessoaService : IPessoaService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IEstadoCivilRepository _estadoCivilRepository;
        private readonly IEstadoRepository _estadoRepository;
        private readonly IMeioComunicacaoRepository _meioComunicacaoRepository;
        private readonly IPapelRepository _papelRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IPessoaFisicaRepository _pessoaFisicaRepository;
        private readonly IPessoaJuridicaRepository _pessoaJuridicaRepository;

        public PessoaService(IEnderecoRepository enderecoRepository, IEstadoCivilRepository estadoCivilRepository,
            IEstadoRepository estadoRepository, IMeioComunicacaoRepository meioComunicacaoRepository,
            IPessoaRepository pessoaRepository, IPessoaFisicaRepository pessoaFisicaRepository,
            IPessoaJuridicaRepository pessoaJuridicaRepository, IPapelRepository papelRepository)
        {
            _enderecoRepository = enderecoRepository;
            _estadoCivilRepository = estadoCivilRepository;
            _estadoRepository = estadoRepository;
            _meioComunicacaoRepository = meioComunicacaoRepository;
            _papelRepository = papelRepository;
            _pessoaRepository = pessoaRepository;
            _pessoaFisicaRepository = pessoaFisicaRepository;
            _pessoaJuridicaRepository = pessoaJuridicaRepository;
        }

        public void Dispose()
        {
            _enderecoRepository.Dispose();
            _estadoCivilRepository.Dispose();
            _estadoRepository.Dispose();
            _meioComunicacaoRepository.Dispose();
            _papelRepository.Dispose();
            _pessoaRepository.Dispose();
            _pessoaFisicaRepository.Dispose();
            _pessoaJuridicaRepository.Dispose();
        }

        public void CriarPessoa(PessoaModel model)
        {
            var pessoa = Mapper.Map<Pessoa>(model);
            ValidarPessoa(pessoa);

            pessoa.Papeis = new List<Papel>
            {
                _papelRepository.ObterPorCodigo(pessoa.Papeis.First().PapelCodigo)
            };

            if (model.EPessoaFisica)
            {
                pessoa.PessoaFisica = Mapper.Map<PessoaFisica>(model);
                CriarPessoaFisica(pessoa);
            }
            else
            {
                pessoa.PessoaJuridica = Mapper.Map<PessoaJuridica>(model);
                CriarPessoaJuridica(pessoa);
            }
        }

        public void CriarPessoaFisica(Pessoa pessoa)
        {
            pessoa.PessoaFisica.Pessoa = pessoa;
            pessoa.PessoaFisica.Validar();

            var existePessoaFisica = _pessoaRepository.ObterPorCPFComPessoaCompleta(pessoa.PessoaFisica.CPF);
            if (existePessoaFisica != null)
            {
                existePessoaFisica.Papeis.Add(pessoa.Papeis.First());
                _pessoaRepository.Atualizar(existePessoaFisica);
            }
            else
            {
                pessoa.Enderecos.First().Estado = _estadoRepository.ObterPorCodigo(pessoa.Enderecos.First().Estado.EstadoCodigo);
                pessoa.PessoaFisica.EstadoCivil =
                    _estadoCivilRepository.ObterPorCodigo(pessoa.PessoaFisica.EstadoCivil.EstadoCivilCodigo);
                _pessoaRepository.Criar(pessoa);
            }
        }

        public void CriarPessoaJuridica(Pessoa pessoa)
        {
            pessoa.PessoaJuridica.Pessoa = pessoa;
            pessoa.PessoaJuridica.Validar();

            var existePessoaJuridica = _pessoaRepository.ObterPorCNPJComPessoaCompleta(pessoa.PessoaJuridica.CNPJ);
            if (existePessoaJuridica != null)
            {
                existePessoaJuridica.Papeis.Add(pessoa.Papeis.First());
                _pessoaRepository.Atualizar(existePessoaJuridica);
            }
            else
            {
                pessoa.Enderecos.First().Estado = _estadoRepository.ObterPorCodigo(pessoa.Enderecos.First().Estado.EstadoCodigo);
                _pessoaRepository.Criar(pessoa);
            }
        }

        public void AtualizarPessoa(PessoaModel model)
        {
            var pessoa = Mapper.Map<Pessoa>(model);
            if (model.EPessoaFisica)
            {
                pessoa.PessoaFisica = Mapper.Map<PessoaFisica>(model);
            }
            else
            {
                pessoa.PessoaJuridica = Mapper.Map<PessoaJuridica>(model);
            }

            var pessoaAtual = _pessoaRepository.ObterPorCodigoComPessoaCompleta(pessoa.PessoaCodigo);
            if (pessoaAtual == null)
            {
                throw new BusinessException(Erros.PersonDoesNotExist);
            }

            pessoaAtual.Nome = pessoa.Nome;
            if (pessoaAtual.PessoaFisica != null)
            {
                pessoaAtual.PessoaFisica.RG = pessoa.PessoaFisica.RG;
                pessoaAtual.PessoaFisica.Sexo = pessoa.PessoaFisica.Sexo;
                pessoaAtual.PessoaFisica.EstadoCivil = _estadoCivilRepository.ObterPorCodigo(pessoa.PessoaFisica.EstadoCivil.EstadoCivilCodigo);
                pessoaAtual.Validar();
                pessoaAtual.PessoaFisica.Validar();
            }
            else
            {
                pessoaAtual.PessoaJuridica.Contato = pessoa.PessoaJuridica.Contato;
                pessoaAtual.Validar();
                pessoaAtual.PessoaJuridica.Validar();
            }

            //Adiciona um novo endereço ou modifica o exitente para principal
            AtualizarEnderecoPessoa(pessoa, pessoaAtual);
            //Adiciona um meio de cominicação ou modifica o exitente para principal
            AtualizarMeioCominicacaoPessoa(pessoa, pessoaAtual, TipoComunicacaoEnum.Telefone);
            AtualizarMeioCominicacaoPessoa(pessoa, pessoaAtual, TipoComunicacaoEnum.Celular);
            AtualizarMeioCominicacaoPessoa(pessoa, pessoaAtual, TipoComunicacaoEnum.Email);
            _pessoaRepository.Atualizar(pessoaAtual);
        }

        public void ExcluirPessoa(int pessoaCodigo)
        {
            if (pessoaCodigo == 0)
            {
                throw new BusinessException(Erros.InvalidCode);
            }

            var pessoa = _pessoaRepository.ObterPorCodigoComPessoaCompleta(pessoaCodigo);

            if (pessoa == null)
            {
                throw new BusinessException(Erros.PersonDoesNotExist);
            }

            _pessoaRepository.Deletar(pessoa);
        }

        public List<PessoaModel> PesquisarPessoa(PesquisaPessoaModel filtro)
        {
            if (filtro.Codigo == 0 &&
                string.IsNullOrEmpty(filtro.Nome) &&
                string.IsNullOrEmpty(filtro.CPFCNPJ) &&
                string.IsNullOrEmpty(filtro.Email) &&
                filtro.TipoPapelPessoa == TipoPapelPessoaEnum.Nenhum)
            {
                throw new BusinessException(Erros.EmptyParameters);
            };

            if (filtro.EPessoaFisica)
            {
                var pessoasFisicas = _pessoaFisicaRepository
                    .ObterLista(filtro.Codigo ?? 0, filtro.Nome, filtro.CPFCNPJ, filtro.Email, filtro.TipoPapelPessoa);
                return Mapper.Map<List<PessoaModel>>(pessoasFisicas);
            }
            var pessoasJuridicas = _pessoaJuridicaRepository
                .ObterLista(filtro.Codigo ?? 0, filtro.Nome, filtro.CPFCNPJ, filtro.Email, filtro.TipoPapelPessoa);
            return Mapper.Map<List<PessoaModel>>(pessoasJuridicas);
        }

        public PessoaModel ObterPessoaPorCodigo(int codigo)
        {
            var pessoa = _pessoaRepository.ObterPorCodigoComPessoaCompleta(codigo);
            if (pessoa.PessoaJuridica == null)
            {
                return Mapper.Map<PessoaModel>(pessoa.PessoaFisica);
            }
            return Mapper.Map<PessoaModel>(pessoa.PessoaJuridica);
        }

        public List<Estado> ObterEstados()
        {
            return _estadoRepository.ObterLista();
        }

        public List<EstadoCivil> ObterEstadosCivis()
        {
            return _estadoCivilRepository.ObterLista();
        }

        public List<Pessoa> ObterListaPessoa()
        {
            return _pessoaRepository.ObterListaComPessoaFisicaEJuridica();
        }

        public List<Pessoa> ObterListaPessoaFisicaEJuridicaPorPapel(TipoPapelPessoaEnum papelCodigo)
        {
            return _pessoaRepository.ObterListaComPessoaFisicaEJuridicaPorPapel(papelCodigo);
        }

        public List<PessoaFisica> ObterListaPessoaFisicaPorPapel(TipoPapelPessoaEnum papelCodigo)
        {
            return _pessoaFisicaRepository.ObterLista(0, null, null, null, papelCodigo);
        }

        public List<PessoaJuridica> ObterListaPessoaJuridicaPorPapel(TipoPapelPessoaEnum papelCodigo)
        {
            return _pessoaJuridicaRepository.ObterLista(0, null, null, null, papelCodigo);
        }

        public List<PessoaModel> TesteProjecao()
        {
            return _pessoaRepository.TesteProjecao();
        }

        private void ValidarPessoa(Pessoa pessoa)
        {
            pessoa.Validar();
            pessoa.Enderecos.First().Validar();

            if (pessoa.MeiosComunicacao.All(x => x.TipoComunicacao != TipoComunicacaoEnum.Telefone))
            {
                throw new BusinessException(Erros.EmptyPhone);
            }

            foreach (var meioComunicacao in pessoa.MeiosComunicacao)
            {
                meioComunicacao.Validar();
            }

            if (!pessoa.Papeis.Any())
            {
                throw new BusinessException(string.Format(Erros.NullParameter, "Papeis"));
            }
        }

        private void AtualizarEnderecoPessoa(Pessoa pessoa, Pessoa pessoaAtual)
        {
            //Adiciona um novo endereço ou modifica o exitente para principal
            var enderecoAtualizar = pessoa.Enderecos.FirstOrDefault();
            AssertionConcern.AssertArgumentNotEquals(enderecoAtualizar, null, Erros.EmptyAddress);
            if (enderecoAtualizar != null && enderecoAtualizar.EnderecoCodigo == -1 &&
                !pessoaAtual.Enderecos.Any(x =>
                    x.Bairro.Equals(enderecoAtualizar.Bairro) &&
                    x.CEP.Equals(enderecoAtualizar.CEP) &&
                    x.Cidade.Equals(enderecoAtualizar.Cidade) &&
                    x.Logradouro.Equals(enderecoAtualizar.Logradouro) &&
                    x.Numero.Equals(enderecoAtualizar.Numero)))
            {
                enderecoAtualizar.Pessoa = pessoaAtual;
                enderecoAtualizar.Estado = _estadoRepository.ObterPorCodigo(enderecoAtualizar.Estado.EstadoCodigo);
                var novoEndereco = _enderecoRepository.Criar(enderecoAtualizar);
                pessoaAtual.Enderecos.Add(novoEndereco);
            }
            else if (enderecoAtualizar != null && enderecoAtualizar.EnderecoCodigo != -1)
            {
                //Encotra o endereço que sera o principal
                foreach (var item in pessoaAtual.Enderecos)
                {
                    item.Principal = item.EnderecoCodigo == enderecoAtualizar.EnderecoCodigo;
                }
            }
        }

        private void AtualizarMeioCominicacaoPessoa(Pessoa pessoa, Pessoa pessoaAtual, TipoComunicacaoEnum tipoComunicacao)
        {
            //Adiciona um meio de cominicação ou modifica o exitente para principal
            var meioComunicacaoAtualizar = pessoa.MeiosComunicacao.FirstOrDefault(x => x.TipoComunicacao == tipoComunicacao);
            if (meioComunicacaoAtualizar == null && (tipoComunicacao == TipoComunicacaoEnum.Email || tipoComunicacao == TipoComunicacaoEnum.Celular))
            {
                return;
            }
            AssertionConcern.AssertArgumentNotEquals(meioComunicacaoAtualizar, null, Erros.EmptyPhone);
            if (meioComunicacaoAtualizar != null && meioComunicacaoAtualizar.MeioComunicacaoCodigo == -1 &&
                !pessoaAtual.MeiosComunicacao.Any(x => x.MeioComunicacaoNome.Equals(meioComunicacaoAtualizar.MeioComunicacaoNome)))
            {
                meioComunicacaoAtualizar.Pessoa = pessoaAtual;
                var novoMeioComunicacao = _meioComunicacaoRepository.Criar(meioComunicacaoAtualizar);
                pessoaAtual.MeiosComunicacao.Add(novoMeioComunicacao);
            }
            else if (meioComunicacaoAtualizar != null && meioComunicacaoAtualizar.MeioComunicacaoCodigo != -1)
            {
                //Encotra o endereço que sera o principal
                foreach (var item in pessoaAtual.MeiosComunicacao)
                {
                    item.Principal = item.MeioComunicacaoCodigo.Equals(meioComunicacaoAtualizar.MeioComunicacaoCodigo);
                }
            }
        }
    }
}
