Funcionalidade: ContaPagarController
		Para funcionalidade de contas a pagar
		Eu como um usuário do sistema
		Desejo utilizar os métodos de consulta e de pagamento.

Contexto: Incluir dependências no banco
	Dado que preencha os dados do fornecedor com as seguintes informações:
	| Field         | Value          |
	| Nome          | Henrique       |
	| CPF           | 123.456.789.09 |
	| RG            | 224445556      |
	| Sexo          | M              |
	| EstadoCivilId | 1              |
	| EPessoaFisica | true           |
	| PapelPessoa   | 4              |
	E que preecha os dados do endereço do fornecedor com as seguintes informações:
	| Field      | Value        |
	| Logradouro | Rua da vida  |
	| Bairro     | Jardim mundo |
	| Numero     | 400          |
	| Cidade     | Sarandi      |
	| Cep        | 87112-540    |
	| UFId       | 16           |
	E que preecha os dados de meios de comunicação do fornecedor com as seguintes informações:
	| Field    | Value          |
	| Telefone | (44) 3232-5566 |
	Quando realizar uma chamada Post ao endereço 'api/Fornecedor/CriarFornecedor'

	Dado que preencha os dados da condicao de pagamento com as seguintes informações:
	| Field              | Value     |
	| Descricao          | 1 Parcela |
	| QuantidadeParcelas | 1         |
	| Ativo              | true      |
	Quando realizar uma chamada Post ao endereço 'api/CondicaoPagamento/CriarCondicaoPagamento'

	Dado que preencha os dados da forma de pagamento com as seguintes informações:
	| Field     | Value  |
	| Descricao | Cartão |
	| Ativo     | true   |
	Quando realizar uma chamada Post ao endereço 'api/FormaPagamento/CriarFormaPagamento'

	Dado que preencha os dados do produto com as seguintes informações:
	| Field         | Value  |
	| Descricao     | Sapato |
	| UnidadeCodigo | 3      |
	| PrecoCusto    | 10,00  |
	| PrecoVenda    | 20,00  |
	Quando realizar uma chamada Post ao endereço 'api/Produto/CriarProduto'

	Dado que preencha os dados da compra com as seguintes informações:
	| Field             | Value           |
	| DataCadastro      | 08/10/2018 8:00 |
	| StatusCompra      | Aberto          |
	| ValorTotalBruto   | 10,00           |
	| ValorTotalFrete   | 0,00            |
	| ValorTotalLiquido | 10,00           |
	E que preecha os dados do item de compra com as seguintes informações:
	| Codigo | Descricao | Quantidade | PrecoVenda | ValorBruto | ValorLiquido |
	| 1      | Cinto     | 1          | 10,00      | 10,00      | 10,00        |
	Quando realizar uma chamada Post ao endereço 'api/Compra/CriarCompra'

	Dado que preencha os dados da compra com as seguintes informações:
	| Field               | Value           |
	| CodigoCompra        | 1               |
	| DataCadastro        | 08/10/2018 8:00 |
	| StatusCompra        | Aberto          |
	| ValorTotalBruto     | 10,00           |
	| ValorTotalFrete     | 0,00            |
	| ValorTotalLiquido   | 10,00           |
	| FornecedorId        | 1               |
	| FormaPagamentoId    | 1               |
	| CondicaoPagamentoId | 1               |
	E que preecha os dados do item de compra com as seguintes informações:
	| Codigo | Descricao | Quantidade | PrecoVenda | ValorBruto | ValorLiquido |
	| 1      | Cinto     | 1          | 10,00      | 10,00      | 10,00        |
	Quando realizar uma chamada Put ao endereço 'api/Compra/EditarCompra'

Cenario: Pesquisar as contas a pagar sem filtros
	Dado que preencha os dados do filtro de pesquisa de contas a pagar com as seguintes informações:
	| Field         | Value |
	Quando realizar uma chamada Post ao endereço 'api/ContaPagar/PesquisaContaPagar'
	Entao retorne sucesso

Cenario: Pesquisar as contas a pagar por código da compra, código do fornecedor, data de emissão, data de vencimento, nome do fornecedor, CPFCNPJ, status
	Dado que preencha os dados do filtro de pesquisa de contas a pagar com as seguintes informações:
	| Field            | Value          |
	| CodigoCompra     | 1              |
	| CodigoFornecedor | 1              |
	| DataEmissao      | 08/10/2018     |
	| DataVencimento   | 08/10/2018     |
	| NomeFornecedor   | Henrique       |
	| CPFCNPJ          | 123.456.789.09 |
	| StatusId         | 1              |
	Quando realizar uma chamada Post ao endereço 'api/ContaPagar/PesquisaContaPagar'
	Entao retorne sucesso

Cenario: Pagar contas
	Dado que preencha os dados da contas a pagar com as seguintes informações:
	| CodigoContaPagar | CodigoCompra | CodigoFornecedor | DataEmissao | DataVencimento | NomeFornecedor | CPFCNPJ        | StatusId | ValorDocumento | Pago |
	| 1                | 1            | 1                | 08/10/2018  | 08/10/2018     | Henrique       | 123.456.789.09 | 1        | 10,00          | True |
	Quando realizar uma chamada Put ao endereço 'api/ContaPagar/PagarConta'
	Entao retorne sucesso