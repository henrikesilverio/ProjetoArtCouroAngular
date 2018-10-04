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