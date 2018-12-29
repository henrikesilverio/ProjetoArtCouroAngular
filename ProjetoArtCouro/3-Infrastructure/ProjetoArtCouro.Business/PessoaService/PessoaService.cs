using System.Collections.Generic;
using System.Linq;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Contracts.IService.IPessoa;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Pessoa;
using ProjetoArtCouro.Mapping;

namespace ProjetoArtCouro.Business.PessoaService
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
            IPapelRepository papelRepository, IPessoaRepository pessoaRepository,
            IPessoaFisicaRepository pessoaFisicaRepository, IPessoaJuridicaRepository pessoaJuridicaRepository)
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
            var pessoa = Map<Pessoa>.MapperTo(model);
            ValidarPessoa(pessoa);

            pessoa.Papeis = new List<Papel>
            {
                _papelRepository.ObterPorCodigo(pessoa.Papeis.First().PapelCodigo)
            };

            if (model.EPessoaFisica)
            {
                pessoa.PessoaFisica = Map<PessoaFisica>.MapperTo(model);
                CriarPessoaFisica(pessoa);
            }
            else
            {
                pessoa.PessoaJuridica = Map<PessoaJuridica>.MapperTo(model);
                CriarPessoaJuridica(pessoa);
            }
        }

        public List<PessoaModel> PesquisarPessoa(PesquisaPessoaModel pessoaFiltro)
        {
            if (!pessoaFiltro.Codigo.HasValue &&
                string.IsNullOrEmpty(pessoaFiltro.Nome) &&
                string.IsNullOrEmpty(pessoaFiltro.CPFCNPJ) &&
                string.IsNullOrEmpty(pessoaFiltro.Email) &&
                pessoaFiltro.TipoPapelPessoa == TipoPapelPessoaEnum.Nenhum)
            {
                throw new BusinessException(Erros.EmptyParameters);
            };

            if (pessoaFiltro.EPessoaFisica)
            {
                var filtroFisica = Map<PesquisaPessoaFisica>.MapperTo(pessoaFiltro);
                var pessoasFisicas = _pessoaFisicaRepository.ObterListaPorFiltro(filtroFisica);
                return Map<List<PessoaModel>>.MapperTo(pessoasFisicas);
            }

            var filtroJuridica = Map<PesquisaPessoaJuridica>.MapperTo(pessoaFiltro);
            var pessoasJuridicas = _pessoaJuridicaRepository.ObterListaPorFiltro(filtroJuridica);
            return Map<List<PessoaModel>>.MapperTo(pessoasJuridicas);
        }

        public PessoaModel ObterPessoaPorCodigo(int codigo)
        {
            var pessoa = _pessoaRepository.ObterPorCodigoComPessoaCompleta(codigo);
            if (pessoa.PessoaJuridica == null)
            {
                return Map<PessoaModel>.MapperTo(pessoa.PessoaFisica);
            }
            return Map<PessoaModel>.MapperTo(pessoa.PessoaJuridica);
        }

        public List<LookupModel> ObterEstados()
        {
            var estados = _estadoRepository.ObterLista();
            return Map<List<LookupModel>>.MapperTo(estados);
        }

        public List<LookupModel> ObterEstadosCivis()
        {
            var estadosCivis = _estadoCivilRepository.ObterLista();
            return Map<List<LookupModel>>.MapperTo(estadosCivis);
        }

        public List<PessoaModel> ObterListaPessoa()
        {
            var pessoas = _pessoaRepository.ObterListaComPessoaFisicaEJuridica();
            return Map<List<PessoaModel>>.MapperTo(pessoas);
        }

        public List<Pessoa> ObterListaPessoaFisicaEJuridicaPorPapel(TipoPapelPessoaEnum papelCodigo)
        {
            return _pessoaRepository.ObterListaComPessoaFisicaEJuridicaPorPapel(papelCodigo);
        }

        public List<PessoaFisica> ObterListaPessoaFisicaPorPapel(TipoPapelPessoaEnum papelCodigo)
        {
            var filtro = new PesquisaPessoaFisica { TipoPapelPessoa = papelCodigo };
            return _pessoaFisicaRepository.ObterListaPorFiltro(filtro);
        }

        public List<PessoaJuridica> ObterListaPessoaJuridicaPorPapel(TipoPapelPessoaEnum papelCodigo)
        {
            var filtro = new PesquisaPessoaJuridica { TipoPapelPessoa = papelCodigo };
            return _pessoaJuridicaRepository.ObterListaPorFiltro(filtro);
        }

        public void AtualizarPessoa(PessoaModel model)
        {
            var pessoa = Map<Pessoa>.MapperTo(model);
            pessoa.Validar();

            if (model.EPessoaFisica)
            {
                pessoa.PessoaFisica = Map<PessoaFisica>.MapperTo(model);
                pessoa.PessoaFisica.Pessoa = pessoa;
                pessoa.PessoaFisica.Validar();
            }
            else
            {
                pessoa.PessoaJuridica = Map<PessoaJuridica>.MapperTo(model);
                pessoa.PessoaJuridica.Pessoa = pessoa;
                pessoa.PessoaJuridica.Validar();
            }

            var pessoaAtual = _pessoaRepository
                .ObterPorCodigoComPessoaCompleta(pessoa.PessoaCodigo);
            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(pessoaAtual, Erros.PersonDoesNotExist);

            pessoaAtual.Nome = pessoa.Nome;
            if (pessoaAtual.PessoaFisica != null)
            {
                pessoaAtual.PessoaFisica.RG = pessoa.PessoaFisica.RG;
                pessoaAtual.PessoaFisica.Sexo = pessoa.PessoaFisica.Sexo;

                var estadoCivil = _estadoCivilRepository
                    .ObterPorCodigo(pessoa.PessoaFisica.EstadoCivil.EstadoCivilCodigo);

                AssertionConcern<BusinessException>
                    .AssertArgumentNotNull(estadoCivil, Erros.EmptyMaritalStatus);

                pessoaAtual.PessoaFisica.EstadoCivil = estadoCivil;
            }
            else
            {
                pessoaAtual.PessoaJuridica.Contato = pessoa.PessoaJuridica.Contato;
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
            var pessoa = _pessoaRepository.ObterPorCodigoComPessoaCompleta(pessoaCodigo);
            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(pessoa, Erros.PersonDoesNotExist);

            _pessoaRepository.Deletar(pessoa);
        }

        private void CriarPessoaFisica(Pessoa pessoa)
        {
            pessoa.PessoaFisica.Pessoa = pessoa;
            pessoa.PessoaFisica.Validar();

            var existePessoaFisica = _pessoaRepository
                .ObterPorCPFComPessoaCompleta(pessoa.PessoaFisica.CPF);

            if (existePessoaFisica != null)
            {
                existePessoaFisica.Papeis.Add(pessoa.Papeis.First());
                _pessoaRepository.Atualizar(existePessoaFisica);
            }
            else
            {
                pessoa.Enderecos.First().Estado = _estadoRepository
                    .ObterPorCodigo(pessoa.Enderecos.First().Estado.EstadoCodigo);

                pessoa.PessoaFisica.EstadoCivil = _estadoCivilRepository
                    .ObterPorCodigo(pessoa.PessoaFisica.EstadoCivil.EstadoCivilCodigo);

                _pessoaRepository.Criar(pessoa);
            }
        }

        private void CriarPessoaJuridica(Pessoa pessoa)
        {
            pessoa.PessoaJuridica.Pessoa = pessoa;
            pessoa.PessoaJuridica.Validar();

            var existePessoaJuridica = _pessoaRepository
                .ObterPorCNPJComPessoaCompleta(pessoa.PessoaJuridica.CNPJ);

            if (existePessoaJuridica != null)
            {
                existePessoaJuridica.Papeis.Add(pessoa.Papeis.First());
                _pessoaRepository.Atualizar(existePessoaJuridica);
            }
            else
            {
                pessoa.Enderecos.First().Estado = _estadoRepository
                    .ObterPorCodigo(pessoa.Enderecos.First().Estado.EstadoCodigo);

                _pessoaRepository.Criar(pessoa);
            }
        }

        private void ValidarPessoa(Pessoa pessoa)
        {
            pessoa.Validar();

            foreach (var endereco in pessoa.Enderecos)
            {
                endereco.Validar();
            }

            AssertionConcern<BusinessException>
                .AssertArgumentTrue(pessoa.MeiosComunicacao.Any(), 
                Erros.MeansOfCommunicationEmpty);

            foreach (var meioComunicacao in pessoa.MeiosComunicacao)
            {
                meioComunicacao.Validar();
            }

            AssertionConcern<BusinessException>
                .AssertArgumentFalse(pessoa.MeiosComunicacao
                .All(x => x.TipoComunicacao != TipoComunicacaoEnum.Telefone), Erros.EmptyPhone);
        }

        private void AtualizarEnderecoPessoa(Pessoa pessoa, Pessoa pessoaAtual)
        {
            //Adiciona um novo endereço ou modifica o exitente para principal
            var enderecoAtualizar = pessoa.Enderecos.First();
            enderecoAtualizar.Validar();

            if (enderecoAtualizar.EnderecoCodigo == -1 &&
                !pessoaAtual.Enderecos.Any(x =>
                    x.Bairro == enderecoAtualizar.Bairro &&
                    x.CEP == enderecoAtualizar.CEP &&
                    x.Cidade == enderecoAtualizar.Cidade &&
                    x.Logradouro == enderecoAtualizar.Logradouro &&
                    x.Numero == enderecoAtualizar.Numero))
            {
                enderecoAtualizar.Pessoa = pessoaAtual;
                enderecoAtualizar.Estado = _estadoRepository.ObterPorCodigo(enderecoAtualizar.Estado.EstadoCodigo);
                var novoEndereco = _enderecoRepository.Criar(enderecoAtualizar);
                pessoaAtual.Enderecos.Add(novoEndereco);
            }
            else if (enderecoAtualizar.EnderecoCodigo != -1)
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

            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(meioComunicacaoAtualizar, Erros.EmptyPhone);

            if (meioComunicacaoAtualizar.MeioComunicacaoCodigo == -1 &&
                !pessoaAtual.MeiosComunicacao.Any(x => x.MeioComunicacaoNome == meioComunicacaoAtualizar.MeioComunicacaoNome))
            {
                meioComunicacaoAtualizar.Pessoa = pessoaAtual;
                var novoMeioComunicacao = _meioComunicacaoRepository.Criar(meioComunicacaoAtualizar);
                pessoaAtual.MeiosComunicacao.Add(novoMeioComunicacao);
            }
            else if (meioComunicacaoAtualizar.MeioComunicacaoCodigo != -1)
            {
                //Encotra o meio de comunicação que sera o principal
                foreach (var item in pessoaAtual.MeiosComunicacao)
                {
                    item.Principal = item.MeioComunicacaoCodigo == meioComunicacaoAtualizar.MeioComunicacaoCodigo;
                }
            }
        }
    }
}
