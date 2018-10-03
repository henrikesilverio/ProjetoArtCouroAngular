Funcionalidade: CondicaoPagamentoController
	Para funcionalidade cadastro de condição de pagamento
	Eu como um usuário do sistema
	Desejo utilizar os métodos CRUD da condição de pagamento.

Cenario: Tentar cadastrar uma condição de pagamento sem preencher todos os campos
	Dado que preencha os dados da condicao de pagamento com as seguintes informações:
	| Field               | Value            |
	Quando realizar uma chamada Post ao endereço 'api/CondicaoPagamento/CriarCondicaoPagamento'
	Entao retorne erro

Cenario: Cadastrar uma condição de pagamento preenchendo todos os campos
	Dado que preencha os dados da condicao de pagamento com as seguintes informações:
	| Field              | Value     |
	| Descricao          | 1 Parcela |
	| QuantidadeParcelas | 1         |
	| Ativo              | true      |
	Quando realizar uma chamada Post ao endereço 'api/CondicaoPagamento/CriarCondicaoPagamento'
	Entao retorne sucesso

Cenario: Pesquisar todas as formas de pagamentos cadastradas
	Dado que preencha os dados da condicao de pagamento com as seguintes informações:
	| Field              | Value     |
	| Descricao          | 1 Parcela |
	| QuantidadeParcelas | 1         |
	| Ativo              | true      |
	Quando realizar uma chamada Post ao endereço 'api/CondicaoPagamento/CriarCondicaoPagamento'
	Entao retorne sucesso
	Quando realizar uma chamada Get ao endereço 'api/CondicaoPagamento/ObterListaCondicaoPagamento'
	Entao retorne sucesso
	E que o retorno tenha uma lista com '1' itens

Cenario: Editar uma forma de pagamento preenchendo todos os campos
	Dado que preencha os dados da condicao de pagamento com as seguintes informações:
	| Field              | Value     |
	| Descricao          | 1 Parcela |
	| QuantidadeParcelas | 1         |
	| Ativo              | true      |
	Quando realizar uma chamada Post ao endereço 'api/CondicaoPagamento/CriarCondicaoPagamento'
	Entao retorne sucesso
	Dado que preencha os dados da condicao de pagamento com as seguintes informações:
	| Field                   | Value     |
	| CondicaoPagamentoCodigo | 1         |
	| Descricao               | 1 Parcela |
	| QuantidadeParcelas      | 1         |
	| Ativo                   | true      |
	Quando realizar uma chamada Put ao endereço 'api/CondicaoPagamento/EditarCondicaoPagamento'
	Entao retorne sucesso

Cenario: Excluir uma forma de pagamento por código
	Dado que preencha os dados da condicao de pagamento com as seguintes informações:
	| Field              | Value     |
	| Descricao          | 1 Parcela |
	| QuantidadeParcelas | 1         |
	| Ativo              | true      |
	Quando realizar uma chamada Post ao endereço 'api/CondicaoPagamento/CriarCondicaoPagamento'
	Entao retorne sucesso
	Quando realizar uma chamada Delete ao endereço 'api/CondicaoPagamento/ExcluirCondicaoPagamento/1'
	Entao retorne sucesso
