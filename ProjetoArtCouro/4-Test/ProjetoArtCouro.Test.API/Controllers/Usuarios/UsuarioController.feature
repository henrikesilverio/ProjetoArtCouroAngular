Funcionalidade: UsuarioController
	Para funcionalidade cadastro de usuário
	Eu como um usuário do sistema
	Desejo utilizar os métodos CRUD do usuário.

Cenario: Tentar cadastrar um usuário sem preencher todos os campos
	Dado que preencha os dados do usuario com as seguintes informações:
	| Field               | Value            |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarUsuario'
	Entao retorne erro

Cenario: Cadastrar um usuário preenchendo todos os campos
	Dado que preencha os dados do usuario com as seguintes informações:
	| Field          | Value    |
	| UsuarioNome    | Operador |
	| Senha          | !@#Mudar |
	| ConfirmarSenha | !@#Mudar |
	| Ativo          | True     |
	| GrupoCodigo    | 1        |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarUsuario'
	Entao retorne sucesso

Cenario: Pesquisar um usuário sem filtros
	Dado que preencha os dados do filtro de pesquisa de usuario com as seguintes informações:
	| Field         | Value |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/PesquisarUsuario'
	Entao retorne sucesso

Cenario: Pesquisar um usuário por nome, situacao e grupo de permissão
	Dado que preencha os dados do usuario com as seguintes informações:
	| Field          | Value    |
	| UsuarioNome    | Operador |
	| Senha          | !@#Mudar |
	| ConfirmarSenha | !@#Mudar |
	| Ativo          | True     |
	| GrupoCodigo    | 1        |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarUsuario'
	Entao retorne sucesso
	Dado que preencha os dados do filtro de pesquisa de usuario com as seguintes informações:
	| Field       | Value    |
	| UsuarioNome | Operador |
	| Ativo       | True     |
	| GrupoCodigo | 1        |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/PesquisarUsuario'
	Entao retorne sucesso
	E que o retorno tenha uma lista com '1' itens

Cenario: Pesquisar um usuário por código
	Dado que preencha os dados do usuario com as seguintes informações:
	| Field          | Value    |
	| UsuarioNome    | Operador |
	| Senha          | !@#Mudar |
	| ConfirmarSenha | !@#Mudar |
	| Ativo          | True     |
	| GrupoCodigo    | 1        |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarUsuario'
	Entao retorne sucesso
	Quando realizar uma chamada Get ao endereço 'api/Usuario/PesquisarUsuarioPorCodigo/2'
	Entao retorne sucesso

Cenario: Editar um usuário preenchendo todos os campos
	Dado que preencha os dados do usuario com as seguintes informações:
	| Field          | Value    |
	| UsuarioNome    | Operador |
	| Senha          | !@#Mudar |
	| ConfirmarSenha | !@#Mudar |
	| Ativo          | True     |
	| GrupoCodigo    | 1        |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarUsuario'
	Entao retorne sucesso
	Dado que preencha os dados do usuario com as seguintes informações:
	| Field          | Value   |
	| UsuarioCodigo  | 2       |
	| UsuarioNome    | Usuario |
	| Senha          | 11254!@ |
	| ConfirmarSenha | 11254!@ |
	| Ativo          | True    |
	| GrupoCodigo    | 1       |
	Quando realizar uma chamada Put ao endereço 'api/Usuario/EditarUsuario'
	Entao retorne sucesso

Cenario: Editar senha do usuário logado
	Dado que preencha os dados do usuario com as seguintes informações:
	| Field          | Value    |
	| Senha          | !@#Mudar |
	Quando realizar uma chamada Put ao endereço 'api/Usuario/AlterarSenha'
	Entao retorne sucesso
	Dado que preencha os dados do usuario com as seguintes informações:
	| Field | Value |
	| Senha | admin |
	Quando realizar uma chamada Put ao endereço 'api/Usuario/AlterarSenha'
	Entao retorne sucesso

Cenario: Editar permissões do usuário
	Dado que preencha os dados do usuario com as seguintes informações:
	| Field          | Value    |
	| UsuarioNome    | Operador |
	| Senha          | !@#Mudar |
	| ConfirmarSenha | !@#Mudar |
	| Ativo          | True     |
	| GrupoCodigo    | 1        |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarUsuario'
	Entao retorne sucesso
	Dado que preencha os dados de configuracao do usuario com as seguintes informações:
	| Field         | Value |
	| UsuarioCodigo | 2     |
	E que preencha os dados das permissoes do usuario com as seguintes informações:
	| Codigo |
	| 1      |
	| 2      |
	| 3      |
	Quando realizar uma chamada Put ao endereço 'api/Usuario/EditarPermissaoUsuario'
	Entao retorne sucesso
	
Cenario: Excluir um usuário por código
	Dado que preencha os dados do usuario com as seguintes informações:
	| Field          | Value    |
	| UsuarioNome    | Operador |
	| Senha          | !@#Mudar |
	| ConfirmarSenha | !@#Mudar |
	| Ativo          | True     |
	| GrupoCodigo    | 1        |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarUsuario'
	Entao retorne sucesso
	Quando realizar uma chamada Delete ao endereço 'api/Usuario/ExcluirUsuario/2'
	Entao retorne sucesso

Cenario: Tentar cadastrar um grupo sem preencher todos os campos
	Dado que preencha os dados do grupo com as seguintes informações:
	| Field     | Value |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarGrupo'
	Entao retorne erro

Cenario: Cadastrar um grupo preenchendo todos os campos
	Dado que preencha os dados do grupo com as seguintes informações:
	| Field     | Value |
	| GrupoNome | Novo  |
	E que preencha os dados das permissoes do usuario com as seguintes informações:
	| Codigo |
	| 1      |
	| 2      |
	| 3      |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarGrupo'
	Entao retorne sucesso

Cenario: Pesquisar todos os grupo
	Dado que preencha os dados do grupo com as seguintes informações:
	| Field     | Value |
	| GrupoNome | Novo  |
	E que preencha os dados das permissoes do usuario com as seguintes informações:
	| Codigo |
	| 1      |
	| 2      |
	| 3      |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarGrupo'
	Entao retorne sucesso
	Quando realizar uma chamada Get ao endereço 'api/Usuario/ObterListaGrupo'
	Entao retorne sucesso
	E que o retorno tenha uma lista com '2' itens

Cenario: Pesquisar um grupo sem filtros
	Dado que preencha os dados do filtro de pesquisa de grupo com as seguintes informações:
	| Field         | Value |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/PesquisarGrupo'
	Entao retorne sucesso

Cenario: Pesquisar um grupo por código e nome
	Dado que preencha os dados do grupo com as seguintes informações:
	| Field     | Value |
	| GrupoNome | Novo  |
	E que preencha os dados das permissoes do usuario com as seguintes informações:
	| Codigo |
	| 1      |
	| 2      |
	| 3      |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarGrupo'
	Entao retorne sucesso
	Dado que preencha os dados do filtro de pesquisa de grupo com as seguintes informações:
	| Field       | Value |
	| GrupoCodigo | 2     |
	| GrupoNome   | Novo  |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/PesquisarGrupo'
	Entao retorne sucesso
	E que o retorno tenha uma lista com '1' itens

Cenario: Pesquisar um grupo por código
	Dado que preencha os dados do grupo com as seguintes informações:
	| Field     | Value |
	| GrupoNome | Novo  |
	E que preencha os dados das permissoes do usuario com as seguintes informações:
	| Codigo |
	| 1      |
	| 2      |
	| 3      |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarGrupo'
	Entao retorne sucesso
	Quando realizar uma chamada Get ao endereço 'api/Usuario/PesquisarGrupoPorCodigo/2'
	Entao retorne sucesso

Cenario: Editar um grupo preenchendo todos os campos
	Dado que preencha os dados do grupo com as seguintes informações:
	| Field     | Value |
	| GrupoNome | Novo  |
	E que preencha os dados das permissoes do usuario com as seguintes informações:
	| Codigo |
	| 1      |
	| 2      |
	| 3      |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarGrupo'
	Entao retorne sucesso
	Dado que preencha os dados do grupo com as seguintes informações:
	| Field       | Value |
	| GrupoCodigo | 2     |
	| GrupoNome   | Velho |
	E que preencha os dados das permissoes do usuario com as seguintes informações:
	| Codigo |
	| 4      |
	| 5      |
	| 6      |
	Quando realizar uma chamada Put ao endereço 'api/Usuario/EditarGrupo'
	Entao retorne sucesso

Cenario: Excluir um grupo por código
	Dado que preencha os dados do grupo com as seguintes informações:
	| Field     | Value |
	| GrupoNome | Novo  |
	E que preencha os dados das permissoes do usuario com as seguintes informações:
	| Codigo |
	| 1      |
	| 2      |
	| 3      |
	Quando realizar uma chamada Post ao endereço 'api/Usuario/CriarGrupo'
	Entao retorne sucesso
	Quando realizar uma chamada Delete ao endereço 'api/Usuario/ExcluirGrupo/2'
	Entao retorne sucesso