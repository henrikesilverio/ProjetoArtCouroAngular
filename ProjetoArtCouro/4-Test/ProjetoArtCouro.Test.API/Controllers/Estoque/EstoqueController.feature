Funcionalidade: EstoqueController
	Para funcionalidade de estoque de produtos
	Eu como um usuário do sistema
	Desejo utilizar consultar o estoque.

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

Cenario: Pesquisar o estoque sem preencher filtros
	Dado que preencha os dados do filtro de pesquisa de estoque com as seguintes informações:
	| Field         | Value |
	Quando realizar uma chamada Post ao endereço 'api/Estoque/PesquisarEstoque'
	Entao retorne sucesso

Cenario: Pesquisar o estoque por código do produto, descrição do produto, nome do fornecedor, código do fornecedor, quantidade
	Dado que preencha os dados do filtro de pesquisa de estoque com as seguintes informações:
	| Field            | Value    |
	| CodigoProduto    | 1        |
	| DescricaoProduto | 1        |
	| NomeFornecedor   | Henrique |
	| CodigoFornecedor | 1        |
	| QuantidaEstoque  | 1        |
	Quando realizar uma chamada Post ao endereço 'api/Estoque/PesquisarEstoque'
	Entao retorne sucesso