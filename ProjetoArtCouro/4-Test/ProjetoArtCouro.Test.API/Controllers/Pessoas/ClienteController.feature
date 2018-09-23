Funcionalidade: ClienteController
	Para funcionalidade cadastro de cliente
	Eu como um usuário do sistema
	Desejo utilizar os métodos CRUD do cliente.

Cenario: Tentar cadastrar um Cliente sem preencher todos os parâmetros
	Dado que preencha os dados do cliente com as seguintes informações:
	| Field               | Value            |
	Quando realizar uma chamada Post ao endereço 'api/Cliente/CriarCliente'
	Entao retorne erro