using System.Collections.Generic;
using System.Linq;
using ProjetoArtCouro.Domain.Contracts.IRepository.IUsuario;
using ProjetoArtCouro.Domain.Contracts.IService.IAutenticacao;
using ProjetoArtCouro.Domain.Entities.Usuarios;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resource.Validation;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Business.AutenticacaoService
{
    public class AutenticacaoService : IAutenticacao
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AutenticacaoService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario AutenticarUsuario(string usuarioNome, string senha)
        {
            var usuario = _usuarioRepository.ObterPorUsuarioNome(usuarioNome);
            AssertionConcern<BusinessException>
                .AssertArgumentNotNull(usuario, Erros.UserDoesNotExist);

            AssertionConcern<BusinessException>
                .AssertArgumentEquals(PasswordAssertionConcern.Encrypt(senha), 
                usuario.Senha, Erros.InvalidUserPassword);

            return usuario;
        }

        public List<Permissao> ObterPermissoes(string usuarioNome)
        {
            var usuario = _usuarioRepository.ObterPorUsuarioNomeComPermissoesEGrupo(usuarioNome);
            var permissoesUsuario = usuario.Permissoes;
            var permissoesGrupo = usuario.GrupoPermissao.Permissoes;
            var permissoes = permissoesGrupo.Union(permissoesUsuario).ToList();
            return permissoes;
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
        }
    }
}
