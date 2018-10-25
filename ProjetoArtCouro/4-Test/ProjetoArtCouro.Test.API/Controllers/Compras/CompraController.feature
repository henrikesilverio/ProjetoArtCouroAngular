Funcionalidade: CompraController
	Para funcionalidade de compra de produtos
	Eu como um usuário do sistema
	Desejo utilizar os métodos CRUD da compra.

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

Cenario: Tentar realizar um orçamento de compra sem preencher todos os campos
	Dado que preencha os dados da compra com as seguintes informações:
	| Field               | Value            |
	Quando realizar uma chamada Post ao endereço 'api/Compra/CriarCompra'
	Entao retorne erro

Cenario: Gerar um orçamento preenchendo todos os campos
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
	Entao retorne sucesso

Cenario: Pesquisar uma compra sem filtros
	Dado que preencha os dados do filtro de pesquisa de compra com as seguintes informações:
	| Field         | Value |
	Quando realizar uma chamada Post ao endereço 'api/Compra/PesquisarCompra'
	Entao retorne sucesso

Cenario: Pesquisar uma compra por código da compra, código do fornecedor, data de cadastro, status, nome do fornecedor, CPFCNPJ
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
	Entao retorne sucesso
	Dado que preencha os dados do filtro de pesquisa de compra com as seguintes informações:
	| Field            | Value              |
	| CodigoCompra     | 1                  |
	| CodigoFornecedor | 1                  |
	| DataCadastro     | 08/10/2018         |
	| StatusId         | 1                  |
	| NomeFornecedor   | Henrique           |
	| CPFCNPJ          | 77.656.976/0001-41 |
	Quando realizar uma chamada Post ao endereço 'api/Compra/PesquisarCompra'
	Entao retorne sucesso

Cenario: Pesquisar uma compra por código da compra
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
	Entao retorne sucesso
	Quando realizar uma chamada Get ao endereço 'api/Compra/PesquisarCompraPorCodigo/1'
	Entao retorne sucesso

Cenario: Apartir de um orçamento gerar uma compra preenchendo todos os campos
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
	Entao retorne sucesso
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
	Entao retorne sucesso

Cenario: Cancelar uma compra preenchendo todos os campos
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
	Entao retorne sucesso
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
	Entao retorne sucesso
	Dado que preencha os dados da compra com as seguintes informações:
	| Field             | Value           |
	| CodigoCompra      | 1               |
	| DataCadastro      | 08/10/2018 8:00 |
	| StatusCompra      | Confirmado      |
	| ValorTotalBruto   | 10,00           |
	| ValorTotalFrete   | 0,00            |
	| ValorTotalLiquido | 10,00           |
	E que preecha os dados do item de compra com as seguintes informações:
	| Codigo | Descricao | Quantidade | PrecoVenda | ValorBruto | ValorLiquido |
	| 1      | Cinto     | 1          | 10,00      | 10,00      | 10,00        |
	Quando realizar uma chamada Put ao endereço 'api/Compra/EditarCompra'
	Entao retorne sucesso

Cenario: Excluir uma compra por código:
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
	Entao retorne sucesso
	Quando realizar uma chamada Delete ao endereço 'api/Compra/ExcluirCompra/1'
	Entao retorne sucesso