Funcionalidade: Cadastro de cliente
	Como usuário desejo incluir, consultar,
	editar e remover clientes.


Cenario: Incluir um cliente sem preencher todas as informações
	Dado que tenha um cliente sem preencher todas as informações
	Quando chamar o metodo CriarCliente
	Entao retorno um erro