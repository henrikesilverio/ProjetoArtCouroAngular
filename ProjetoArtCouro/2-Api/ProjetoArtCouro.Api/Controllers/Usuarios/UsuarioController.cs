using System.Web.Http;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IUsuario;
using ProjetoArtCouro.Domain.Models.Usuario;
using WebApi.OutputCache.V2;

namespace ProjetoArtCouro.Api.Controllers.Usuarios
{
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : BaseApiController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Route("CriarUsuario")]
        [Authorize(Roles = "NovoUsuario")]
        [InvalidateCacheOutput("ObterListaUsuario")]
        [InvalidateCacheOutput("PesquisarUsuarioPorCodigo")]
        [HttpPost]
        public IHttpActionResult CriarUsuario(UsuarioModel model)
        {
            _usuarioService.CriarUsuario(model);
            return OkRetornoBase();
        }

        [Route("ObterListaUsuario")]
        [Authorize(Roles = "ConfiguracaoUsuario")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaUsuario()
        {
            var listaUsuarioModel = _usuarioService.ObterListaUsuario();
            return OkRetornoBase(listaUsuarioModel);
        }

        [Route("PesquisarUsuario")]
        [Authorize(Roles = "PesquisaUsuario")]
        [HttpPost]
        public IHttpActionResult PesquisarUsuario(PesquisaUsuarioModel model)
        {
            var listaUsuario = _usuarioService.PesquisarUsuario(model);
            return OkRetornoBase(listaUsuario);
        }

        [Route("PesquisarUsuarioPorCodigo/{codigoUsuario:int:min(1)}")]
        [Authorize(Roles = "EditarUsuario")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult PesquisarUsuarioPorCodigo(int codigoUsuario)
        {
            var usuarioModel = _usuarioService.PesquisarUsuarioPorCodigo(codigoUsuario);
            return OkRetornoBase(usuarioModel);
        }

        [Route("ObterPermissoesUsuarioLogado")]
        [Authorize]
        [HttpGet]
        public IHttpActionResult ObterPermissoesUsuarioLogado()
        {
            var listaPermissao = _usuarioService
                .ObterPermissoesUsuarioLogado(User.Identity.Name);
            return OkRetornoBase(listaPermissao);
        }

        [Route("EditarUsuario")]
        [Authorize(Roles = "EditarUsuario")]
        [InvalidateCacheOutput("ObterListaUsuario")]
        [InvalidateCacheOutput("PesquisarUsuarioPorCodigo")]
        [HttpPut]
        public IHttpActionResult EditarUsuario(UsuarioModel model)
        {
            _usuarioService.EditarUsuario(model);
            return OkRetornoBase();
        }

        [Route("AlterarSenha")]
        [Authorize(Roles = "AlterarSenha")]
        [HttpPut]
        public IHttpActionResult AlterarSenha(UsuarioModel model)
        {
            _usuarioService.AlterarSenha(User.Identity.Name, model.Senha);
            return OkRetornoBase();
        }

        [Route("ExcluirUsuario/{codigoUsuario:int:min(1)}")]
        [Authorize(Roles = "ExcluirUsuario")]
        [InvalidateCacheOutput("ObterListaUsuario")]
        [InvalidateCacheOutput("PesquisarUsuarioPorCodigo")]
        [HttpDelete]
        public IHttpActionResult ExcluirUsuario(int codigoUsuario)
        {
            _usuarioService.ExcluirUsuario(codigoUsuario);
            return OkRetornoBase();
        }

        [Route("EditarPermissaoUsuario")]
        [Authorize(Roles = "ConfiguracaoUsuario")]
        [InvalidateCacheOutput("ObterListaUsuario")]
        [InvalidateCacheOutput("ObterListaPermissao")]
        [InvalidateCacheOutput("PesquisarUsuarioPorCodigo")]
        [HttpPut]
        public IHttpActionResult EditarPermissaoUsuario(ConfiguracaoUsuarioModel model)
        {
            _usuarioService.EditarPermissaoUsuario(model);
            return OkRetornoBase();
        }

        [Route("CriarGrupo")]
        [Authorize(Roles = "NovoGrupo")]
        [InvalidateCacheOutput("ObterListaUsuario")]
        [InvalidateCacheOutput("ObterListaGrupo")]
        [HttpPost]
        public IHttpActionResult CriarGrupo(GrupoModel model)
        {
            _usuarioService.CriarGrupoPermissao(model);
            return OkRetornoBase();
        }

        [Route("ObterListaGrupo")]
        [Authorize(Roles = "PesquisaUsuario, NovoUsuario, EditarUsuario")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaGrupo()
        {
            var listaGrupoModel = _usuarioService.ObterListaGrupoPermissao();
            return OkRetornoBase(listaGrupoModel);
        }

        [Route("PesquisarGrupo")]
        [Authorize(Roles = "PesquisaGrupo")]
        [HttpPost]
        public IHttpActionResult PesquisarGrupo(PesquisaGrupoModel model)
        {
            var listaGrupoModel = _usuarioService.PesquisarGrupo(model);
            return OkRetornoBase(listaGrupoModel);
        }

        [Route("PesquisarGrupoPorCodigo/{codigoGrupo:int:min(1)}")]
        [Authorize(Roles = "EditarGrupo")]
        [InvalidateCacheOutput("ObterListaUsuario")]
        [HttpGet]
        public IHttpActionResult PesquisarGrupoPorCodigo(int codigoGrupo)
        {
            var grupoModel = _usuarioService.ObterGrupoPermissaoPorCodigo(codigoGrupo);
            return OkRetornoBase(grupoModel);
        }

        [Route("EditarGrupo")]
        [Authorize(Roles = "EditarGrupo")]
        [InvalidateCacheOutput("ObterListaUsuario")]
        [InvalidateCacheOutput("ObterListaGrupo")]
        [HttpPut]
        public IHttpActionResult EditarGrupo(GrupoModel model)
        {
            _usuarioService.EditarGrupoPermissao(model);
            return OkRetornoBase();
        }

        [Route("ExcluirGrupo/{codigoGrupo:int:min(1)}")]
        [Authorize(Roles = "ExcluirGrupo")]
        [InvalidateCacheOutput("ObterListaUsuario")]
        [InvalidateCacheOutput("ObterListaGrupo")]
        [HttpDelete]
        public IHttpActionResult ExcluirGrupo(int codigoGrupo)
        {
            _usuarioService.ExcluirGrupoPermissao(codigoGrupo);
            return OkRetornoBase();
        }

        [Route("ObterListaPermissao")]
        [Authorize(Roles = "NovoGrupo, EditarGrupo, ConfiguracaoUsuario")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public IHttpActionResult ObterListaPermissao()
        {
            var listaPermissaoModel = _usuarioService.ObterListaPermissao();
            return OkRetornoBase(listaPermissaoModel);
        }

        protected override void Dispose(bool disposing)
        {
            _usuarioService.Dispose();
        }
    }
}
