Funcionalidade: VendaController
	Para funcionalidade de venda de produtos
	Eu como um usuário do sistema
	Desejo utilizar os métodos CRUD da venda.

Contexto: Incluir dependências no banco
	Dado que preencha os dados do cliente com as seguintes informações:
	| Field         | Value          |
	| Nome          | Henrique       |
	| CPF           | 123.456.789.09 |
	| RG            | 224445556      |
	| Sexo          | M              |
	| EstadoCivilId | 1              |
	| EPessoaFisica | true           |
	| PapelPessoa   | 4              |
	E que preecha os dados do endereço do cliente com as seguintes informações:
	| Field      | Value        |
	| Logradouro | Rua da vida  |
	| Bairro     | Jardim mundo |
	| Numero     | 400          |
	| Cidade     | Sarandi      |
	| Cep        | 87112-540    |
	| UFId       | 16           |
	E que preecha os dados de meios de comunicação do cliente com as seguintes informações:
	| Field    | Value          |
	| Telefone | (44) 3232-5566 |
	Quando realizar uma chamada Post ao endereço 'api/Cliente/CriarCliente'

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

Cenario: Tentar realizar um orçamento de venda sem preencher todos os campos
	Dado que preencha os dados da venda com as seguintes informações:
	| Field               | Value            |
	Quando realizar uma chamada Post ao endereço 'api/Venda/CriarVenda'
	Entao retorne erro

Cenario: Gerar um orçamento preenchendo todos os campos
	Dado que preencha os dados da venda com as seguintes informações:
	| Field              | Value           |
	| DataCadastro       | 08/10/2018 8:00 |
	| StatusVenda        | Aberto          |
	| ValorTotalBruto    | 10,00           |
	| ValorTotalDesconto | 0,50            |
	| ValorTotalLiquido  | 10,00           |
	E que preecha os dados do item de venda com as seguintes informações:
	| Codigo | Descricao | Quantidade | PrecoVenda | ValorBruto | ValorDesconto | ValorLiquido |
	| 1      | Cinto     | 1          | 10,00      | 10,00      | 0,50          | 10,00        |
	Quando realizar uma chamada Post ao endereço 'api/Venda/CriarVenda'
	Entao retorne sucesso

Cenario: Pesquisar uma venda sem filtros
	Dado que preencha os dados do filtro de pesquisa de venda com as seguintes informações:
	| Field         | Value |
	Quando realizar uma chamada Post ao endereço 'api/Venda/PesquisarVenda'
	Entao retorne sucesso

Cenario: Pesquisar uma venda por código da venda, código do cliente, data de cadastro, status, nome do cliente, CPFCNPJ
	Dado que preencha os dados da venda com as seguintes informações:
	| Field              | Value           |
	| DataCadastro       | 08/10/2018 8:00 |
	| StatusVenda        | Aberto          |
	| ValorTotalBruto    | 10,00           |
	| ValorTotalDesconto | 0,50            |
	| ValorTotalLiquido  | 10,00           |
	E que preecha os dados do item de venda com as seguintes informações:
	| Codigo | Descricao | Quantidade | PrecoVenda | ValorBruto | ValorDesconto | ValorLiquido |
	| 1      | Cinto     | 1          | 10,00      | 10,00      | 0,50          | 10,00        |
	Quando realizar uma chamada Post ao endereço 'api/Venda/CriarVenda'
	Entao retorne sucesso
	Dado que preencha os dados do filtro de pesquisa de venda com as seguintes informações:
	| Field         | Value          |
	| CodigoVenda   | 1              |
	| CodigoCliente | 1              |
	| DataCadastro  | 08/10/2018     |
	| StatusId      | 1              |
	| NomeCliente   | Henrique       |
	| CPFCNPJ       | 123.456.789.09 |
	Quando realizar uma chamada Post ao endereço 'api/Venda/PesquisarVenda'
	Entao retorne sucesso

Cenario: Pesquisar uma venda por código da venda
	Dado que preencha os dados da venda com as seguintes informações:
	| Field              | Value           |
	| DataCadastro       | 08/10/2018 8:00 |
	| StatusVenda        | Aberto          |
	| ValorTotalBruto    | 10,00           |
	| ValorTotalDesconto | 0,50            |
	| ValorTotalLiquido  | 10,00           |
	E que preecha os dados do item de venda com as seguintes informações:
	| Codigo | Descricao | Quantidade | PrecoVenda | ValorBruto | ValorDesconto | ValorLiquido |
	| 1      | Cinto     | 1          | 10,00      | 10,00      | 0,50          | 10,00        |
	Quando realizar uma chamada Post ao endereço 'api/Venda/CriarVenda'
	Entao retorne sucesso
	Quando realizar uma chamada Get ao endereço 'api/Venda/PesquisarVendaPorCodigo/1'
	Entao retorne sucesso

Cenario: Apartir de um orçamento gerar uma venda preenchendo todos os campos
	Dado que preencha os dados da venda com as seguintes informações:
	| Field              | Value           |
	| DataCadastro       | 08/10/2018 8:00 |
	| StatusVenda        | Aberto          |
	| ValorTotalBruto    | 10,00           |
	| ValorTotalDesconto | 0,50            |
	| ValorTotalLiquido  | 10,00           |
	E que preecha os dados do item de venda com as seguintes informações:
	| Codigo | Descricao | Quantidade | PrecoVenda | ValorBruto | ValorDesconto | ValorLiquido |
	| 1      | Cinto     | 1          | 10,00      | 10,00      | 0,50          | 10,00        |
	Quando realizar uma chamada Post ao endereço 'api/Venda/CriarVenda'
	Entao retorne sucesso
	Dado que preencha os dados da venda com as seguintes informações:
	| Field               | Value           |
	| CodigoVenda         | 1               |
	| DataCadastro        | 08/10/2018 8:00 |
	| StatusVenda         | Aberto          |
	| ValorTotalBruto     | 10,00           |
	| ValorTotalDesconto  | 0,50            |
	| ValorTotalLiquido   | 10,00           |
	| ClienteId           | 1               |
	| FormaPagamentoId    | 1               |
	| CondicaoPagamentoId | 1               |
	E que preecha os dados do item de venda com as seguintes informações:
	| Codigo | Descricao | Quantidade | PrecoVenda | ValorBruto | ValorDesconto | ValorLiquido |
	| 1      | Cinto     | 1          | 10,00      | 10,00      | 0,50          | 10,00        |
	Quando realizar uma chamada Put ao endereço 'api/Venda/EditarVenda'
	Entao retorne sucesso

Cenario: Cancelar uma venda preenchendo todos os campos
	Dado que preencha os dados da venda com as seguintes informações:
	| Field              | Value           |
	| DataCadastro       | 08/10/2018 8:00 |
	| StatusVenda        | Aberto          |
	| ValorTotalBruto    | 10,00           |
	| ValorTotalDesconto | 0,50            |
	| ValorTotalLiquido  | 10,00           |
	E que preecha os dados do item de venda com as seguintes informações:
	| Codigo | Descricao | Quantidade | PrecoVenda | ValorBruto | ValorDesconto | ValorLiquido |
	| 1      | Cinto     | 1          | 10,00      | 10,00      | 0,50          | 10,00        |
	Quando realizar uma chamada Post ao endereço 'api/Venda/CriarVenda'
	Entao retorne sucesso
	Dado que preencha os dados da venda com as seguintes informações:
	| Field               | Value           |
	| CodigoVenda         | 1               |
	| DataCadastro        | 08/10/2018 8:00 |
	| StatusVenda         | Aberto          |
	| ValorTotalBruto     | 10,00           |
	| ValorTotalDesconto  | 0,50            |
	| ValorTotalLiquido   | 10,00           |
	| ClienteId           | 1               |
	| FormaPagamentoId    | 1               |
	| CondicaoPagamentoId | 1               |
	E que preecha os dados do item de venda com as seguintes informações:
	| Codigo | Descricao | Quantidade | PrecoVenda | ValorBruto | ValorDesconto | ValorLiquido |
	| 1      | Cinto     | 1          | 10,00      | 10,00      | 0,50          | 10,00        |
	Quando realizar uma chamada Put ao endereço 'api/Venda/EditarVenda'
	Entao retorne sucesso
	Dado que preencha os dados da venda com as seguintes informações:
	| Field              | Value           |
	| CodigoVenda        | 1               |
	| DataCadastro       | 08/10/2018 8:00 |
	| StatusVenda        | Confirmado      |
	| ValorTotalBruto    | 10,00           |
	| ValorTotalDesconto | 0,50            |
	| ValorTotalLiquido  | 10,00           |
	E que preecha os dados do item de venda com as seguintes informações:
	| Codigo | Descricao | Quantidade | PrecoVenda | ValorBruto | ValorDesconto | ValorLiquido |
	| 1      | Cinto     | 1          | 10,00      | 10,00      | 0,50          | 10,00        |
	Quando realizar uma chamada Put ao endereço 'api/Venda/EditarVenda'
	Entao retorne sucesso

Cenario: Excluir uma venda por código:
	Dado que preencha os dados da venda com as seguintes informações:
	| Field              | Value           |
	| DataCadastro       | 08/10/2018 8:00 |
	| StatusVenda        | Aberto          |
	| ValorTotalBruto    | 10,00           |
	| ValorTotalDesconto | 0,50            |
	| ValorTotalLiquido  | 10,00           |
	E que preecha os dados do item de venda com as seguintes informações:
	| Codigo | Descricao | Quantidade | PrecoVenda | ValorBruto | ValorDesconto | ValorLiquido |
	| 1      | Cinto     | 1          | 10,00      | 10,00      | 0,50          | 10,00        |
	Quando realizar uma chamada Post ao endereço 'api/Venda/CriarVenda'
	Entao retorne sucesso
	Quando realizar uma chamada Delete ao endereço 'api/Venda/ExcluirVenda/1'
	Entao retorne sucesso