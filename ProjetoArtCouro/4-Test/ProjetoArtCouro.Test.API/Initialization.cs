using BoDi;
using ProjetoArtCouro.Api.AutoMapper;
using ProjetoArtCouro.Business.Services.PessoaService;
using ProjetoArtCouro.DataBase.DataBase;
using ProjetoArtCouro.DataBase.Repositorios.PessoaRepository;
using ProjetoArtCouro.Domain.Contracts.IRepository.IPessoa;
using ProjetoArtCouro.Domain.Contracts.IService.IPessoa;
using TechTalk.SpecFlow;

namespace ProjetoArtCouro.Test.API
{
    [Binding]
    public sealed class Initialization
    {
        //private readonly IObjectContainer _objectContainer;

        //public Initialization(IObjectContainer objectContainer)
        //{
        //    _objectContainer = objectContainer;
        //}

        //[BeforeScenario]
        //public void BeforeScenario()
        //{
        //    _objectContainer.RegisterInstanceAs(new DataBaseContext());
        //    _objectContainer.RegisterTypeAs<PessoaService, IPessoaService>();
        //    _objectContainer.RegisterTypeAs<PessoaRepository, IPessoaRepository>();
        //    _objectContainer.RegisterTypeAs<PapelRepository, IPapelRepository>();
        //    _objectContainer.RegisterTypeAs<PessoaFisicaRepository, IPessoaFisicaRepository>();
        //    _objectContainer.RegisterTypeAs<PessoaJuridicaRepository, IPessoaJuridicaRepository>();
        //    _objectContainer.RegisterTypeAs<MeioComunicacaoRepository, IMeioComunicacaoRepository>();
        //    _objectContainer.RegisterTypeAs<EnderecoRepository, IEnderecoRepository>();
        //    _objectContainer.RegisterTypeAs<EstadoCivilRepository, IEstadoCivilRepository>();
        //    _objectContainer.RegisterTypeAs<EstadoRepository, IEstadoRepository>();
        //    AutoMapperConfig.RegisterMappings();
        //}

        //[AfterScenario]
        //public void AfterScenario()
        //{
        //    //TODO: implement logic that has to run after executing each scenario
        //}
    }
}
