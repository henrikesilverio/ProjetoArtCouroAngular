using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoArtCouro.Domain.Contracts.IRepository.IUsuario;
using ProjetoArtCouro.Domain.Contracts.IService.IUsuario;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;
using ProjetoArtCouro.Domain.Models.Usuario;
using AutoMapper;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Domain.Exceptions;

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

            var usuario = Mapper.Map<Usuario>(model);
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
            var usuarios = _usuarioRepository
                .ObterLista(model.UsuarioNome, model.GrupoCodigo, model.Ativo);
            return Mapper.Map<List<UsuarioModel>>(usuarios);
        }

        public UsuarioModel PesquisarUsuarioPorCodigo(int codigoUsuario)
        {
            var usuario = _usuarioRepository.ObterPorCodigoComPermissoesEGrupo(codigoUsuario);
            return Mapper.Map<UsuarioModel>(usuario);
        }

        public void AlterarSenha(string usuarioNome, string senha)
        {
            AssertionConcern<BusinessException>
                .AssertArgumentNotEmpty(usuarioNome, Erros.InvalidUserName);

            var usuarioAtual = _usuarioRepository.ObterComPermissoesPorUsuarioNome(usuarioNome);
            usuarioAtual.Senha = PasswordAssertionConcern.Encrypt(senha);
            usuarioAtual.Validar();

            _usuarioRepository.Atualizar(usuarioAtual);
        }

        public void EditarUsuario(UsuarioModel model)
        {
            var usuario = Mapper.Map<Usuario>(model);
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
            var usuario = _usuarioRepository.ObterPorCodigo(codigoUsuario);
            //AssertionConcern.AssertArgumentNotNull(usuario, Erros.UserDoesNotExist);
            _usuarioRepository.Deletar(usuario);
        }

        public List<Usuario> ObterListaUsuario()
        {
            return _usuarioRepository.ObterListaComPermissoes();
        }

        public List<Permissao> ObterListaPermissao()
        {
            return _permissaoRepository.ObterLista();
        }

        public List<PermissaoModel> ObterPermissoesUsuarioLogado(string usuarioNome)
        {
            var usuario = _usuarioRepository.ObterComPermissoesPorUsuarioNome(usuarioNome);
            return Mapper.Map<List<PermissaoModel>>(usuario.Permissoes.ToList());
        }

        public GrupoPermissao ObterGrupoPermissaoPorCodigo(int codigo)
        {
            return _grupoPermissaoRepository.ObterPorCodigoComPermissoes(codigo);
        }

        public List<GrupoPermissao> PesquisarGrupo(string nome, int? codigo, bool todos)
        {
            return todos ? _grupoPermissaoRepository.ObterLista() : _grupoPermissaoRepository.ObterLista(nome, codigo);
        }

        public List<GrupoPermissao> ObterListaGrupoPermissao()
        {
            return _grupoPermissaoRepository.ObterLista();
        }

        public void CriarGrupoPermissao(GrupoPermissao grupoPermissao)
        {
            //AssertionConcern.AssertArgumentNotEmpty(grupoPermissao.GrupoPermissaoNome, Erros.EmptyGroupName);
            var temGrupo = _grupoPermissaoRepository.ObterPorGrupoPermissaoNome(grupoPermissao.GrupoPermissaoNome.ToLower());
            if (temGrupo != null)
            {
                throw new Exception(Erros.DuplicateGruopName);
            };
            AtualizarListaPermissao(grupoPermissao);
            _grupoPermissaoRepository.Criar(grupoPermissao);
        }

        public void EditarGrupoPermissao(GrupoPermissao grupoPermissao)
        {
            //AssertionConcern.AssertArgumentNotEmpty(grupoPermissao.GrupoPermissaoNome, Erros.EmptyGroupName);
            var bdGrupoPermissao =
                _grupoPermissaoRepository.ObterPorCodigoComPermissoesEUsuarios(grupoPermissao.GrupoPermissaoCodigo);
            var listaPermissao = _permissaoRepository.ObterLista();
            if (!listaPermissao.Any())
            {
                throw new Exception(Erros.PermissionsNotRegistered);
            }
            bdGrupoPermissao.Permissoes.Clear();
            var listaAdicionar = grupoPermissao.Permissoes.Select(x =>
                listaPermissao.FirstOrDefault(a => a.PermissaoCodigo.Equals(x.PermissaoCodigo))).ToList();
            listaAdicionar.ForEach(x =>
            {
                bdGrupoPermissao.Permissoes.Add(x);
            });
            _grupoPermissaoRepository.Atualizar(bdGrupoPermissao);
        }

        public void EditarPermissaoUsuario(int codigoUsuario, List<Permissao> permissoes)
        {
            //AssertionConcern.AssertArgumentNotEquals(codigoUsuario, 0, Erros.UserDoesNotExist);
            if (!permissoes.Any())
            {
                throw new Exception(Erros.EmptyAllowList);
            }
            var temUsuario = _usuarioRepository.ObterPorCodigoComPermissoes(codigoUsuario);
            //AssertionConcern.AssertArgumentNotEquals(temUsuario, null, Erros.UserDoesNotExist);
            var listaPermissao = _permissaoRepository.ObterLista();
            permissoes = permissoes.Select(x =>
                listaPermissao.FirstOrDefault(a => a.PermissaoCodigo.Equals(x.PermissaoCodigo))).ToList();
            temUsuario.Permissoes.Clear();
            temUsuario.Permissoes = permissoes;
            _usuarioRepository.Atualizar(temUsuario);
        }

        public void ExcluirGrupoPermissao(int codigoGrupoPermissao)
        {
            var grupoPermissao = _grupoPermissaoRepository.ObterPorCodigo(codigoGrupoPermissao);
            //AssertionConcern.AssertArgumentNotNull(grupoPermissao, Erros.GroupDoesNotExist);
            _grupoPermissaoRepository.Deletar(grupoPermissao);
        }

        private void AtualizarListaPermissao(GrupoPermissao grupoPermissao)
        {
            var listaPermissao = _permissaoRepository.ObterLista();
            if (!listaPermissao.Any())
            {
                throw new Exception(Erros.PermissionsNotRegistered);
            }
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
