using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjetoArtCouro.Business.PessoaService;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Entities.Pessoas;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Domain.Models.Common;
using ProjetoArtCouro.Domain.Models.Enums;
using ProjetoArtCouro.Domain.Models.Pessoa;
using ProjetoArtCouro.Mapping.Configs;
using System.Collections.Generic;

namespace ProjetoArtCouro.Test.Business.Pessoas
{
    [TestClass]
    public class PessoaServiceUnitTest
    {
        private PessoaService _pessoaService;
        private Mock<IEnderecoRepository> _enderecoRepositoryMock;
        private Mock<IEstadoCivilRepository> _estadoCivilRepositoryMock;
        private Mock<IEstadoRepository> _estadoRepositoryMock;
        private Mock<IMeioComunicacaoRepository> _meioComunicacaoRepositoryMock;
        private Mock<IPapelRepository> _papelRepositoryMock;
        private Mock<IPessoaRepository> _pessoaRepositoryMock;
        private Mock<IPessoaFisicaRepository> _pessoaFisicaRepositoryMock;
        private Mock<IPessoaJuridicaRepository> _pessoaJuridicaRepositoryMock;

        [TestInitialize]
        public void Inicializacao()
        {
            _enderecoRepositoryMock = new Mock<IEnderecoRepository>();
            _estadoCivilRepositoryMock = new Mock<IEstadoCivilRepository>();
            _estadoRepositoryMock = new Mock<IEstadoRepository>();
            _meioComunicacaoRepositoryMock = new Mock<IMeioComunicacaoRepository>();
            _papelRepositoryMock = new Mock<IPapelRepository>();
            _pessoaRepositoryMock = new Mock<IPessoaRepository>();
            _pessoaFisicaRepositoryMock = new Mock<IPessoaFisicaRepository>();
            _pessoaJuridicaRepositoryMock = new Mock<IPessoaJuridicaRepository>();

            _pessoaService = new PessoaService(
                _enderecoRepositoryMock.Object,
                _estadoCivilRepositoryMock.Object,
                _estadoRepositoryMock.Object,
                _meioComunicacaoRepositoryMock.Object,
                _papelRepositoryMock.Object,
                _pessoaRepositoryMock.Object,
                _pessoaFisicaRepositoryMock.Object,
                _pessoaJuridicaRepositoryMock.Object);

            MapperConfig.RegisterMappings();
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarPessoa_DadosInvalidos_Excecao()
        {
            _pessoaService.CriarPessoa(new PessoaModel());
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarPessoa_DadosDeEnderecoInvalidos_Excecao()
        {
            _pessoaService.CriarPessoa(new PessoaModel
            {
                CPF = "12345678909",
                EPessoaFisica = true,
                Nome = "Henrique",
                RG = "203304409",
                Sexo = "M",
                Endereco = new EnderecoModel(),
                MeioComunicacao = new MeioComunicacaoModel(),
                PapelPessoa = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Meio de comunicação preenchido")]
        public void CriarPessoa_MeioComunicacaoVazio_Excecao()
        {
            _pessoaService.CriarPessoa(new PessoaModel
            {
                CPF = "12345678909",
                EPessoaFisica = true,
                Nome = "Henrique",
                RG = "203304409",
                Sexo = "M",
                Endereco = new EnderecoModel
                {
                    Bairro = "Teste",
                    Cep = "52125980",
                    Cidade = "Sarandi",
                    Complemento = "Casa",
                    Logradouro = "Rua vida 1",
                    Numero = "125B",
                    UFId = 1
                },
                MeioComunicacao = new MeioComunicacaoModel(),
                PapelPessoa = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarPessoa_DadosDeMeioComunicacaoInvalidos_Excecao()
        {
            _pessoaService.CriarPessoa(new PessoaModel
            {
                CPF = "12345678909",
                EPessoaFisica = true,
                Nome = "Henrique",
                RG = "203304409",
                Sexo = "M",
                Endereco = new EnderecoModel
                {
                    Bairro = "Teste",
                    Cep = "52125980",
                    Cidade = "Sarandi",
                    Complemento = "Casa",
                    Logradouro = "Rua vida 1",
                    Numero = "125B",
                    UFId = 1
                },
                MeioComunicacao = new MeioComunicacaoModel
                {
                    Celular = new string('A', 251)
                },
                PapelPessoa = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Telefone preenchido")]
        public void CriarPessoa_SemTelefone_Excecao()
        {
            _pessoaService.CriarPessoa(new PessoaModel
            {
                CPF = "12345678909",
                EPessoaFisica = true,
                Nome = "Henrique",
                RG = "203304409",
                Sexo = "M",
                Endereco = new EnderecoModel
                {
                    Bairro = "Teste",
                    Cep = "52125980",
                    Cidade = "Sarandi",
                    Complemento = "Casa",
                    Logradouro = "Rua vida 1",
                    Numero = "125B",
                    UFId = 1
                },
                MeioComunicacao = new MeioComunicacaoModel
                {
                    Celular = "2145679999",
                    Email = "teste@gmail.com"
                },
                PapelPessoa = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarPessoa_PessoaFisicaInvalida_Excecao()
        {
            _papelRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Papel
                {
                    PapelCodigo = 1,
                    PapelNome = "Cliente"
                });

            _pessoaService.CriarPessoa(new PessoaModel
            {
                EPessoaFisica = true,
                Nome = "Henrique",
                Endereco = new EnderecoModel
                {
                    Bairro = "Teste",
                    Cep = "52125980",
                    Cidade = "Sarandi",
                    Complemento = "Casa",
                    Logradouro = "Rua vida 1",
                    Numero = "125B",
                    UFId = 1
                },
                MeioComunicacao = new MeioComunicacaoModel
                {
                    Telefone = "2145679999",
                },
                PapelPessoa = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void CriarPessoa_PessoaJuridicaInvalida_Excecao()
        {
            _papelRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Papel
                {
                    PapelCodigo = 1,
                    PapelNome = "Cliente"
                });

            _pessoaService.CriarPessoa(new PessoaModel
            {
                EPessoaFisica = false,
                Nome = "Henrique",
                Endereco = new EnderecoModel
                {
                    Bairro = "Teste",
                    Cep = "52125980",
                    Cidade = "Sarandi",
                    Complemento = "Casa",
                    Logradouro = "Rua vida 1",
                    Numero = "125B",
                    UFId = 1
                },
                MeioComunicacao = new MeioComunicacaoModel
                {
                    Telefone = "2145679999",
                },
                PapelPessoa = 1
            });
        }

        [TestMethod]
        public void CriarPessoa_DadosValidos_CriarPessoaFisica()
        {
            _papelRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Papel
                {
                    PapelCodigo = 1,
                    PapelNome = "Cliente"
                });

            _estadoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Estado
                {
                    EstadoCodigo = 1,
                    EstadoNome = "PR"
                });

            _estadoCivilRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new EstadoCivil
                {
                    EstadoCivilCodigo = 1,
                    EstadoCivilNome = "Solteiro"
                });

            _pessoaService.CriarPessoa(new PessoaModel
            {
                EPessoaFisica = true,
                Nome = "Henrique",
                CPF = "12345678909",
                RG = "203004009",
                Sexo = "M",
                EstadoCivilId = 1,
                Endereco = new EnderecoModel
                {
                    Bairro = "Teste",
                    Cep = "52125980",
                    Cidade = "Sarandi",
                    Complemento = "Casa",
                    Logradouro = "Rua vida 1",
                    Numero = "125B",
                    UFId = 1
                },
                MeioComunicacao = new MeioComunicacaoModel
                {
                    Telefone = "2145679999",
                },
                PapelPessoa = 1
            });
        }

        [TestMethod]
        public void CriarPessoa_DadosValidos_CriarPessoaJuridica()
        {
            _papelRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Papel
                {
                    PapelCodigo = 1,
                    PapelNome = "Cliente"
                });

            _estadoRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Estado
                {
                    EstadoCodigo = 1,
                    EstadoNome = "PR"
                });

            _pessoaService.CriarPessoa(new PessoaModel
            {
                EPessoaFisica = false,
                Nome = "Henrique",
                CNPJ = "12456789000156",
                Contato = "Henrique",
                RazaoSocial = "Henrique",
                Endereco = new EnderecoModel
                {
                    Bairro = "Teste",
                    Cep = "52125980",
                    Cidade = "Sarandi",
                    Complemento = "Casa",
                    Logradouro = "Rua vida 1",
                    Numero = "125B",
                    UFId = 1
                },
                MeioComunicacao = new MeioComunicacaoModel
                {
                    Telefone = "2145679999",
                },
                PapelPessoa = 1
            });
        }

        [TestMethod]
        public void CriarPessoa_DadosValidos_AtualizarPapelPessoaFisica()
        {
            _papelRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Papel
                {
                    PapelCodigo = 1,
                    PapelNome = "Cliente"
                });

            _pessoaRepositoryMock
                .Setup(x => x.ObterPorCPFComPessoaCompleta("12345678909"))
                .Returns(new Pessoa
                {
                    Papeis = new List<Papel>
                    {
                        new Papel
                        {
                            PapelCodigo = 1,
                            PapelNome = "Funcionario"
                        }
                    }
                });

            _pessoaService.CriarPessoa(new PessoaModel
            {
                EPessoaFisica = true,
                Nome = "Henrique",
                CPF = "12345678909",
                RG = "203004009",
                Sexo = "M",
                EstadoCivilId = 1,
                Endereco = new EnderecoModel
                {
                    Bairro = "Teste",
                    Cep = "52125980",
                    Cidade = "Sarandi",
                    Complemento = "Casa",
                    Logradouro = "Rua vida 1",
                    Numero = "125B",
                    UFId = 1
                },
                MeioComunicacao = new MeioComunicacaoModel
                {
                    Telefone = "2145679999",
                },
                PapelPessoa = 1
            });
        }

        [TestMethod]
        public void CriarPessoa_DadosValidos_AtualizarPapelPessoaJudica()
        {
            _papelRepositoryMock
                .Setup(x => x.ObterPorCodigo(1))
                .Returns(new Papel
                {
                    PapelCodigo = 1,
                    PapelNome = "Cliente"
                });

            _pessoaRepositoryMock
                .Setup(x => x.ObterPorCNPJComPessoaCompleta("12456789000156"))
                .Returns(new Pessoa
                {
                    Papeis = new List<Papel>
                    {
                        new Papel
                        {
                            PapelCodigo = 1,
                            PapelNome = "Funcionario"
                        }
                    }
                });

            _pessoaService.CriarPessoa(new PessoaModel
            {
                EPessoaFisica = false,
                Nome = "Henrique",
                CNPJ = "12456789000156",
                Contato = "Henrique",
                RazaoSocial = "Henrique",
                Endereco = new EnderecoModel
                {
                    Bairro = "Teste",
                    Cep = "52125980",
                    Cidade = "Sarandi",
                    Complemento = "Casa",
                    Logradouro = "Rua vida 1",
                    Numero = "125B",
                    UFId = 1
                },
                MeioComunicacao = new MeioComunicacaoModel
                {
                    Telefone = "2145679999",
                },
                PapelPessoa = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Filtros preenchidos")]
        public void PesquisarPessoa_FiltroInvalido_Excecao()
        {
            _pessoaService.PesquisarPessoa(new PesquisaPessoaModel());
        }

        [TestMethod]
        public void PesquisarPessoa_DadosValidos_PessoaFisica()
        {
            _pessoaFisicaRepositoryMock
                .Setup(x => x.ObterListaPorFiltro(It.IsAny<PesquisaPessoaFisica>()))
                .Returns(new List<PessoaFisica>
                {
                    new PessoaFisica
                    {
                        CPF = "12345678909",
                        RG = "12345689",
                        Sexo = "M",
                        PessoaFisicaCodigo = 1,
                        EstadoCivil = new EstadoCivil
                        {
                            EstadoCivilCodigo = 1,
                            EstadoCivilNome = "Solteiro"
                        },
                        Pessoa = new Pessoa
                        {
                            Enderecos = new List<Endereco>(),
                            MeiosComunicacao = new List<MeioComunicacao>()
                        }
                    }
                });

            var pessoas = _pessoaService.PesquisarPessoa(new PesquisaPessoaModel
            {
                Codigo = 1,
                EPessoaFisica = true
            });

            Assert.IsNotNull(pessoas, "Pessoas não encontradas");
            Assert.AreEqual(pessoas.Count, 1, "Quantidade de pessoas invalidas");
        }

        [TestMethod]
        public void PesquisarPessoa_DadosValidos_PessoaJuridica()
        {
            _pessoaJuridicaRepositoryMock
                .Setup(x => x.ObterListaPorFiltro(It.IsAny<PesquisaPessoaJuridica>()))
                .Returns(new List<PessoaJuridica>
                {
                    new PessoaJuridica
                    {
                        CNPJ = "1213215465",
                        Contato = "Henrique",
                        PessoaJuridicaCodigo = 1,
                        Pessoa = new Pessoa
                        {
                            Enderecos = new List<Endereco>(),
                            MeiosComunicacao = new List<MeioComunicacao>()
                        }
                    }
                });

            var pessoas = _pessoaService.PesquisarPessoa(new PesquisaPessoaModel
            {
                Codigo = 1,
                EPessoaFisica = false
            });

            Assert.IsNotNull(pessoas, "Pessoas não encontradas");
            Assert.AreEqual(pessoas.Count, 1, "Quantidade de pessoas invalidas");
        }

        [TestMethod]
        public void ObterPessoaPorCodigo_DadosValidos_PessoaFisica()
        {
            _pessoaRepositoryMock
                .Setup(x => x.ObterPorCodigoComPessoaCompleta(1))
                .Returns(new Pessoa
                {
                    PessoaFisica = new PessoaFisica
                    {
                        CPF = "12345678909",
                        Pessoa = new Pessoa
                        {
                            Enderecos = new List<Endereco>(),
                            MeiosComunicacao = new List<MeioComunicacao>(),
                        }
                    }
                });

            var pessoa = _pessoaService.ObterPessoaPorCodigo(1);

            Assert.IsNotNull(pessoa, "Pessoas não encontradas");
            Assert.IsTrue(pessoa.EPessoaFisica, "Não é pessoa fisica");
        }

        [TestMethod]
        public void ObterPessoaPorCodigo_DadosValidos_PessoaJuridica()
        {
            _pessoaRepositoryMock
                .Setup(x => x.ObterPorCodigoComPessoaCompleta(1))
                .Returns(new Pessoa
                {
                    PessoaJuridica = new PessoaJuridica
                    {
                        CNPJ = "12345678999901",
                        Pessoa = new Pessoa
                        {
                            Enderecos = new List<Endereco>(),
                            MeiosComunicacao = new List<MeioComunicacao>(),
                        }
                    }
                });

            var pessoa = _pessoaService.ObterPessoaPorCodigo(1);

            Assert.IsNotNull(pessoa, "Pessoas não encontradas");
            Assert.IsFalse(pessoa.EPessoaFisica, "Não é pessoa juridica");
        }

        [TestMethod]
        public void ObterEstados_DadosValidos_RetornarLista()
        {
            _estadoRepositoryMock
                .Setup(x => x.ObterLista())
                .Returns(new List<Estado>
                {
                    new Estado
                    {
                        EstadoCodigo = 1,
                        EstadoNome = "Parana"
                    }
                });

            var estados = _pessoaService.ObterEstados();

            Assert.IsNotNull(estados, "Estados não encontrados");
            Assert.AreEqual(estados.Count, 1, "Quantidade de estados invalidas");
        }

        [TestMethod]
        public void ObterEstadosCivis_DadosValidos_RetornarLista()
        {
            _estadoCivilRepositoryMock
                .Setup(x => x.ObterLista())
                .Returns(new List<EstadoCivil>
                {
                    new EstadoCivil
                    {
                        EstadoCivilCodigo = 1,
                        EstadoCivilNome = "Parana"
                    }
                });

            var estadosCivis = _pessoaService.ObterEstadosCivis();

            Assert.IsNotNull(estadosCivis, "Estados civis não encontrados");
            Assert.AreEqual(estadosCivis.Count, 1, "Quantidade de estados civis invalidas");
        }

        [TestMethod]
        public void ObterListaPessoa_DadosValidos_RetornarLista()
        {
            _pessoaRepositoryMock
                .Setup(x => x.ObterListaComPessoaFisicaEJuridica())
                .Returns(new List<Pessoa>
                {
                    new Pessoa
                    {
                        Nome = "Henrique",
                        PessoaCodigo = 1
                    }
                });

            var pessoas = _pessoaService.ObterListaPessoa();

            Assert.IsNotNull(pessoas, "Pessoas não encontrados");
            Assert.AreEqual(pessoas.Count, 1, "Quantidade de pessoas invalidas");
        }

        [TestMethod]
        public void ObterListaPessoaFisicaEJuridicaPorPapel_DadosValidos_RetornarLista()
        {
            _pessoaRepositoryMock
                .Setup(x => x.ObterListaComPessoaFisicaEJuridicaPorPapel(TipoPapelPessoaEnum.Cliente))
                .Returns(new List<Pessoa>
                {
                    new Pessoa
                    {
                        Nome = "Henrique",
                        PessoaCodigo = 1
                    }
                });

            var pessoas = _pessoaService
                .ObterListaPessoaFisicaEJuridicaPorPapel(TipoPapelPessoaEnum.Cliente);

            Assert.IsNotNull(pessoas, "Pessoas não encontrados");
            Assert.AreEqual(pessoas.Count, 1, "Quantidade de pessoas invalidas");
        }

        [TestMethod]
        public void ObterListaPessoaFisicaPorPapel_DadosValidos_RetornarLista()
        {
            _pessoaFisicaRepositoryMock
                .Setup(x => x.ObterListaPorFiltro(It.IsAny<PesquisaPessoaFisica>()))
                .Returns(new List<PessoaFisica>
                {
                    new PessoaFisica
                    {
                        CPF = "12345678909",
                        RG = "1233221",
                        Sexo = "M",
                        PessoaFisicaCodigo = 1
                    }
                });

            var pessoas = _pessoaService
                .ObterListaPessoaFisicaPorPapel(TipoPapelPessoaEnum.Cliente);

            Assert.IsNotNull(pessoas, "Pessoas não encontrados");
            Assert.AreEqual(pessoas.Count, 1, "Quantidade de pessoas invalidas");
        }

        [TestMethod]
        public void ObterListaPessoaJuridicaPorPapel_DadosValidos_RetornarLista()
        {
            _pessoaJuridicaRepositoryMock
                .Setup(x => x.ObterListaPorFiltro(It.IsAny<PesquisaPessoaJuridica>()))
                .Returns(new List<PessoaJuridica>
                {
                    new PessoaJuridica
                    {
                        CNPJ = "122121",
                        Contato = "sdasd",
                        PessoaJuridicaCodigo = 1
                    }
                });

            var pessoas = _pessoaService
                .ObterListaPessoaJuridicaPorPapel(TipoPapelPessoaEnum.Cliente);

            Assert.IsNotNull(pessoas, "Pessoas não encontrados");
            Assert.AreEqual(pessoas.Count, 1, "Quantidade de pessoas invalidas");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void AtualizarPessoa_PessoaInvalida_Excecao()
        {
            _pessoaService.AtualizarPessoa(new PessoaModel());
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void AtualizarPessoa_PessoaFisicaInvalida_Excecao()
        {
            _pessoaService.AtualizarPessoa(new PessoaModel
            {
                EPessoaFisica = true,
                Nome = "Henrique",
                Endereco = new EnderecoModel(),
                MeioComunicacao = new MeioComunicacaoModel(),
                PapelPessoa = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Entidade valida")]
        public void AtualizarPessoa_PessoaJuridicaInvalida_Excecao()
        {
            _pessoaService.AtualizarPessoa(new PessoaModel
            {
                EPessoaFisica = false,
                Nome = "Henrique",
                Endereco = new EnderecoModel(),
                MeioComunicacao = new MeioComunicacaoModel(),
                PapelPessoa = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Pessoa encontrada")]
        public void AtualizarPessoa_PessoaNaoEncontrada_Excecao()
        {
            _pessoaService.AtualizarPessoa(new PessoaModel
            {
                EPessoaFisica = false,
                Nome = "Henrique",
                Endereco = new EnderecoModel(),
                MeioComunicacao = new MeioComunicacaoModel(),
                PapelPessoa = 1,
                CNPJ = "12132565465",
                Contato = "sdasdasd",
                Codigo = 1,
                RazaoSocial = "sdasdasd"
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Estado civil encontrado")]
        public void AtualizarPessoa_EstadoCivilNaoEncontrada_Excecao()
        {
            _pessoaRepositoryMock
                .Setup(x => x.ObterPorCodigoComPessoaCompleta(1))
                .Returns(new Pessoa
                {
                    Nome = "Henrique",
                    PessoaFisica = new PessoaFisica
                    {
                        CPF = "12345678909",
                        RG = "231321323",
                        Sexo = "M",
                        EstadoCivil = new EstadoCivil
                        {
                            EstadoCivilCodigo = 1,
                            EstadoCivilNome = "sdasd"
                        },
                        PessoaFisicaCodigo = 1
                    }
                });

            _pessoaService.AtualizarPessoa(new PessoaModel
            {
                EPessoaFisica = true,
                Nome = "Henrique",
                Endereco = new EnderecoModel(),
                MeioComunicacao = new MeioComunicacaoModel(),
                PapelPessoa = 1,
                EstadoCivilId = 1,
                CPF = "12132565465",
                RG = "sdasdasd",
                Sexo = "M",
                Codigo = 1
            });
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Endereço valido")]
        public void AtualizarPessoa_EnderecoInvalido_Excecao()
        {
            _pessoaRepositoryMock
                .Setup(x => x.ObterPorCodigoComPessoaCompleta(1))
                .Returns(new Pessoa
                {
                    Nome = "Henrique",
                    PessoaJuridica = new PessoaJuridica
                    {
                        CNPJ = "1213213",
                        Contato = "HEren",
                        PessoaJuridicaCodigo = 1
                    }
                });

            _pessoaService.AtualizarPessoa(new PessoaModel
            {
                EPessoaFisica = false,
                Nome = "Henrique",
                Endereco = new EnderecoModel(),
                MeioComunicacao = new MeioComunicacaoModel(),
                PapelPessoa = 1,
                CNPJ = "12132565465",
                Contato = "sdasdasd",
                Codigo = 1,
                RazaoSocial = "sdasdasd"
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Telefone encontrado")]
        public void AtualizarPessoa_TelefoneNaoEncontrado_Excecao()
        {
            _pessoaRepositoryMock
                .Setup(x => x.ObterPorCodigoComPessoaCompleta(1))
                .Returns(new Pessoa
                {
                    Nome = "Henrique",
                    Enderecos = new List<Endereco>
                    {
                        new Endereco
                        {
                            EnderecoCodigo = 1,
                            Bairro = "A",
                            CEP = "dadasdasd",
                            Cidade = "sdasdas",
                            Logradouro = "sdasdas",
                            Numero = "12345",
                            Estado = new Estado
                            {
                                EstadoCodigo = 1,
                                EstadoNome = "sdasda"
                            }
                        }
                    },
                    PessoaJuridica = new PessoaJuridica
                    {
                        CNPJ = "1213213",
                        Contato = "HEren",
                        PessoaJuridicaCodigo = 1
                    }
                });

            _pessoaService.AtualizarPessoa(new PessoaModel
            {
                EPessoaFisica = false,
                Nome = "Henrique",
                Endereco = new EnderecoModel
                {
                    Bairro = "A",
                    Cep = "dadasdasd",
                    Cidade = "sdasdas",
                    Logradouro = "sdasdas",
                    EnderecoId = 1,
                    Numero = "12345",
                    UFId = 1
                },
                MeioComunicacao = new MeioComunicacaoModel(),
                PapelPessoa = 1,
                CNPJ = "12132565465",
                Contato = "sdasdasd",
                Codigo = 1,
                RazaoSocial = "sdasdasd"
            });
        }

        [TestMethod]
        public void AtualizarPessoa_DadosValidos_PessoaAtualizada()
        {
            _pessoaRepositoryMock
                .Setup(x => x.ObterPorCodigoComPessoaCompleta(1))
                .Returns(new Pessoa
                {
                    Nome = "Henrique",
                    Enderecos = new List<Endereco>
                    {
                        new Endereco
                        {
                            EnderecoCodigo = 1,
                            Bairro = "A",
                            CEP = "dadasdasd",
                            Cidade = "sdasdas",
                            Logradouro = "sdasdas",
                            Numero = "12345",
                            Estado = new Estado
                            {
                                EstadoCodigo = 1,
                                EstadoNome = "sdasda"
                            }
                        }
                    },
                    MeiosComunicacao = new List<MeioComunicacao>
                    {
                        new MeioComunicacao
                        {
                            MeioComunicacaoCodigo = 1,
                            MeioComunicacaoNome = "SAdasdads",
                            Principal = true,
                            TipoComunicacao = TipoComunicacaoEnum.Telefone
                        }
                    },
                    PessoaJuridica = new PessoaJuridica
                    {
                        CNPJ = "1213213",
                        Contato = "HEren",
                        PessoaJuridicaCodigo = 1
                    }
                });

            _pessoaService.AtualizarPessoa(new PessoaModel
            {
                EPessoaFisica = false,
                Nome = "Henrique",
                Endereco = new EnderecoModel
                {
                    Bairro = "A",
                    Cep = "dadasdasd",
                    Cidade = "sdasdas",
                    Logradouro = "sdasdas",
                    EnderecoId = 1,
                    Numero = "12345",
                    UFId = 1
                },
                MeioComunicacao = new MeioComunicacaoModel
                {
                    Telefone = "sdasdasd",
                    TelefoneId = 1
                },
                PapelPessoa = 1,
                CNPJ = "12132565465",
                Contato = "sdasdasd",
                Codigo = 1,
                RazaoSocial = "sdasdasd"
            });
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException), "Pessoa encontrada")]
        public void ExcluirPessoa_PessoaNaoEncontrada_Excecao()
        {
            _pessoaService.ExcluirPessoa(1);
        }

        [TestMethod]
        public void ExcluirPessoa_DadosValidos_PessoaExcluida()
        {
            _pessoaRepositoryMock
                .Setup(x => x.ObterPorCodigoComPessoaCompleta(1))
                .Returns(new Pessoa
                {
                    Nome = "Henrique",
                    PessoaCodigo = 1
                });

            _pessoaService.ExcluirPessoa(1);
        }
    }
}
