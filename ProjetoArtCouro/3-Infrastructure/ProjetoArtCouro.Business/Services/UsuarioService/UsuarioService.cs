using System.Collections.Generic;
using System.Linq;
using ProjetoArtCouro.Domain.Contracts.IRepository.IUsuario;
using ProjetoArtCouro.Domain.Contracts.IService.IUsuario;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;
using ProjetoArtCouro.Domain.Models.Usuario;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Mapping;

namespace ProjetoArtCouro.Business.Services.UsuarioService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPermissaoRepository _permissaoRepository;
        private readonly IGrupoPermissaoRepository _grupoPermissaoRepository;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IPermissaoRepository permissaoRepository,
            IGrupoPermissaoRepository grupoPermissaoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _permissaoRepository = permissaoRepository;
            _grupoPermissaoRepository = grupoPermissaoRepository;
        }

        public void CriarUsuario(UsuarioModel model)
        {
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(model.GrupoCodigo, 0, string.Format(Erros.NotZeroParameter, "GrupoId"));

            AssertionConcern<BusinessException>
                .AssertArgumentEquals(model.Senha, model.ConfirmarSenha, Erros.ConfirmPasswordAndPasswordNotMatch);

            var usuario = Map<Usuario>.MapperTo(model);
            usuario.Validar();

            var temUsuario = _usuarioRepository.ObterPorUsuarioNome(usuario.UsuarioNome);
            AssertionConcern<BusinessException>
                .AssertArgumentNull(temUsuario, Erros.DuplicateUserName);

            var grupo = _grupoPermissaoRepository.ObterPorCodigoComPermissoes(model.GrupoCodigo);
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(grupo, null, Erros.GroupDoesNotExist);

            usuario.Senha = PasswordAssertionConcern.Encrypt(usuario.Senha);
            usuario.GrupoPermissao = grupo;

            _usuarioRepository.Criar(usuario);
        }

        public List<UsuarioModel> PesquisarUsuario(PesquisaUsuarioModel model)
        {
            var filtro = Map<PesquisaUsuario>.MapperTo(model);
            var usuarios = _usuarioRepository.ObterListaPorFiltro(filtro);
            return Map<List<UsuarioModel>>.MapperTo(usuarios);
        }

        public UsuarioModel PesquisarUsuarioPorCodigo(int codigoUsuario)
        {
            var usuario = _usuarioRepository.ObterPorCodigoComPermissoesEGrupo(codigoUsuario);
            return Map<UsuarioModel>.MapperTo(usuario);
        }

        public void AlterarSenha(string usuarioNome, string senha)
        {
            AssertionConcern<BusinessException>
                .AssertArgumentNotEmpty(usuarioNome, Erros.InvalidUserName);

            var usuarioAtual = _usuarioRepository.ObterPorUsuarioNomeComPermissoes(usuarioNome);
            usuarioAtual.Senha = PasswordAssertionConcern.Encrypt(senha);
            usuarioAtual.Validar();

            _usuarioRepository.Atualizar(usuarioAtual);
        }

        public void EditarUsuario(UsuarioModel model)
        {
            var usuario = Map<Usuario>.MapperTo(model);
            var usuarioAtual = _usuarioRepository.ObterPorCodigoComPermissoesEGrupo(usuario.UsuarioCodigo);
            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(usuarioAtual, Erros.UserDoesNotExist);

            if (usuarioAtual.GrupoPermissao.GrupoPermissaoCodigo != usuario.GrupoPermissao.GrupoPermissaoCodigo)
            {
                var novoGrupo = _grupoPermissaoRepository
                    .ObterPorCodigoComPermissoes(usuario.GrupoPermissao.GrupoPermissaoCodigo);

                AssertionConcern<BusinessException>
                    .AssertArgumentNotNull(novoGrupo, Erros.GroupDoesNotExist);

                usuarioAtual.GrupoPermissao = novoGrupo;
            }

            usuarioAtual.Ativo = usuario.Ativo;
            usuarioAtual.UsuarioNome = usuario.UsuarioNome;

            if (!string.IsNullOrEmpty(usuario.Senha))
            {
                usuarioAtual.Senha = PasswordAssertionConcern.Encrypt(usuario.Senha);
            }

            usuarioAtual.Validar();
            _usuarioRepository.Atualizar(usuarioAtual);
        }

        public void ExcluirUsuario(int codigoUsuario)
        {
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(codigoUsuario, 0,
                string.Format(Erros.NotZeroParameter, "codigoUsuario"));

            var usuario = _usuarioRepository.ObterPorCodigo(codigoUsuario);
            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(usuario, Erros.UserDoesNotExist);

            _usuarioRepository.Deletar(usuario);
        }

        public void EditarPermissaoUsuario(ConfiguracaoUsuarioModel model)
        {
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(model.UsuarioCodigo, 0,
                string.Format(Erros.NotZeroParameter, "codigoUsuario"));

            AssertionConcern<BusinessException>
                .AssertArgumentTrue(model.Permissoes.Any(), Erros.EmptyAllowList);

            var usuario = _usuarioRepository.ObterPorCodigoComPermissoes(model.UsuarioCodigo);
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(usuario, null, Erros.UserDoesNotExist);

            var permissoes = Map<List<Permissao>>.MapperTo(model.Permissoes);
            var permissoesDB = _permissaoRepository.ObterLista();

            usuario.Permissoes.Clear();
            usuario.Permissoes = permissoesDB
                .Where(permissao => permissoes
                .Any(x => x.PermissaoCodigo == permissao.PermissaoCodigo))
                .ToList();

            _usuarioRepository.Atualizar(usuario);
        }

        public List<UsuarioModel> ObterListaUsuario()
        {
            var usuarios = _usuarioRepository.ObterListaComPermissoes();
            return Map<List<UsuarioModel>>.MapperTo(usuarios);
        }

        public List<PermissaoModel> ObterListaPermissao()
        {
            var permissoes = _permissaoRepository.ObterLista();
            return Map<List<PermissaoModel>>.MapperTo(permissoes);
        }

        public List<PermissaoModel> ObterPermissoesUsuarioLogado(string usuarioNome)
        {
            var usuario = _usuarioRepository.ObterPorUsuarioNomeComPermissoes(usuarioNome);
            return Map<List<PermissaoModel>>.MapperTo(usuario.Permissoes.ToList());
        }

        public GrupoModel ObterGrupoPermissaoPorCodigo(int codigoGrupo)
        {
            var grupo = _grupoPermissaoRepository.ObterPorCodigoComPermissoes(codigoGrupo);
            return Map<GrupoModel>.MapperTo(grupo);
        }

        public List<GrupoModel> PesquisarGrupo(PesquisaGrupoModel model)
        {
            var grupos = new List<GrupoPermissao>();
            if (model.Todos)
            {
                grupos = _grupoPermissaoRepository.ObterLista();
            }
            else
            {
                grupos = _grupoPermissaoRepository.ObterLista(model.GrupoNome, model.GrupoCodigo);
            }
            return Map<List<GrupoModel>>.MapperTo(grupos);
        }

        public List<GrupoModel> ObterListaGrupoPermissao()
        {
            var grupos = _grupoPermissaoRepository.ObterLista();
            return Map<List<GrupoModel>>.MapperTo(grupos);
        }

        public void CriarGrupoPermissao(GrupoModel model)
        {
            var grupo = Map<GrupoPermissao>.MapperTo(model);
            grupo.Validar();

            var temGrupo = _grupoPermissaoRepository.ObterPorGrupoPermissaoNome(grupo.GrupoPermissaoNome.ToLower());
            AssertionConcern<BusinessException>
                .AssertArgumentNull(temGrupo, Erros.DuplicateGruopName);

            AtualizarListaPermissao(grupo);
            _grupoPermissaoRepository.Criar(grupo);
        }

        public void EditarGrupoPermissao(GrupoModel model)
        {
            var grupo = Map<GrupoPermissao>.MapperTo(model);
            grupo.Validar();

            var grupoPermissaoDB =
                _grupoPermissaoRepository.ObterPorCodigoComPermissoesEUsuarios(grupo.GrupoPermissaoCodigo);

            var listaPermissao = _permissaoRepository.ObterLista();
            AssertionConcern<BusinessException>
                .AssertArgumentTrue(listaPermissao.Any(), Erros.PermissionsNotRegistered);

            grupoPermissaoDB.Permissoes.Clear();
            grupoPermissaoDB.Permissoes = grupo.Permissoes
                .Select(x => listaPermissao
                .FirstOrDefault(a => a.PermissaoCodigo == x.PermissaoCodigo))
                .ToList();

            _grupoPermissaoRepository.Atualizar(grupoPermissaoDB);
        }

        public void ExcluirGrupoPermissao(int codigoGrupoPermissao)
        {
            AssertionConcern<BusinessException>
                .AssertArgumentNotEquals(codigoGrupoPermissao, 0,
                string.Format(Erros.NotZeroParameter, "codigoGrupoPermissao"));

            var grupoPermissao = _grupoPermissaoRepository.ObterPorCodigo(codigoGrupoPermissao);
            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(grupoPermissao, Erros.GroupDoesNotExist);

            _grupoPermissaoRepository.Deletar(grupoPermissao);
        }

        private void AtualizarListaPermissao(GrupoPermissao grupoPermissao)
        {
            var listaPermissao = _permissaoRepository.ObterLista();
            AssertionConcern<BusinessException>
                .AssertArgumentTrue(listaPermissao.Any(), Erros.PermissionsNotRegistered);

            grupoPermissao.Permissoes = grupoPermissao.Permissoes.Select(x =>
                listaPermissao.FirstOrDefault(a => a.PermissaoCodigo.Equals(x.PermissaoCodigo))).ToList();
            grupoPermissao.GrupoPermissaoNome = grupoPermissao.GrupoPermissaoNome.ToUpper();
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
            _permissaoRepository.Dispose();
            _grupoPermissaoRepository.Dispose();
        }
    }
}
