Funcionalidade: FormaPagamentoController
	Para funcionalidade cadastro de forma de pagamento
	Eu como um usuário do sistema
	Desejo utilizar os métodos CRUD da forma de pagamento.

Cenario: Tentar cadastrar uma forma de pagamento sem preencher todos os campos
	Dado que preencha os dados da forma de pagamento com as seguintes informações:
	| Field               | Value            |
	Quando realizar uma chamada Post ao endereço 'api/FormaPagamento/CriarFormaPagamento'
	Entao retorne erro

Cenario: Cadastrar uma forma de pagamento preenchendo todos os campos
	Dado que preencha os dados da forma de pagamento com as seguintes informações:
	| Field     | Value  |
	| Descricao | Cartão |
	| Ativo     | true   |
	Quando realizar uma chamada Post ao endereço 'api/FormaPagamento/CriarFormaPagamento'
	Entao retorne sucesso

Cenario: Pesquisar todas as formas de pagamentos cadastradas
	Dado que preencha os dados da forma de pagamento com as seguintes informações:
	| Field     | Value  |
	| Descricao | Cartão |
	| Ativo     | true   |
	Quando realizar uma chamada Post ao endereço 'api/FormaPagamento/CriarFormaPagamento'
	Entao retorne sucesso
	Quando realizar uma chamada Get ao endereço 'api/FormaPagamento/ObterListaFormaPagamento'
	Entao retorne sucesso
	E que o retorno tenha uma lista com '1' itens

Cenario: Editar uma forma de pagamento preenchendo todos os campos
	Dado que preencha os dados da forma de pagamento com as seguintes informações:
	| Field     | Value  |
	| Descricao | Cartão |
	| Ativo     | true   |
	Quando realizar uma chamada Post ao endereço 'api/FormaPagamento/CriarFormaPagamento'
	Entao retorne sucesso
	Dado que preencha os dados da forma de pagamento com as seguintes informações:
	| Field                | Value  |
	| FormaPagamentoCodigo | 1      |
	| Descricao            | Cartão |
	| Ativo                | true   |
	Quando realizar uma chamada Put ao endereço 'api/FormaPagamento/EditarFormaPagamento'
	Entao retorne sucesso

Cenario: Excluir uma forma de pagamento por código
	Dado que preencha os dados da forma de pagamento com as seguintes informações:
	| Field     | Value  |
	| Descricao | Cartão |
	| Ativo     | true   |
	Quando realizar uma chamada Post ao endereço 'api/FormaPagamento/CriarFormaPagamento'
	Entao retorne sucesso
	Quando realizar uma chamada Delete ao endereço 'api/FormaPagamento/ExcluirFormaPagamento/1'
	Entao retorne sucesso
