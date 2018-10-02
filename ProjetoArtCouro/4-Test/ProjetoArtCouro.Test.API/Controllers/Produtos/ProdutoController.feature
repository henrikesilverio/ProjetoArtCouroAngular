Funcionalidade: ProdutoController
	Para funcionalidade cadastro de produto
	Eu como um usuário do sistema
	Desejo utilizar os métodos CRUD do produto.

Cenario: Tentar cadastrar um produto sem preencher todos os campos
	Dado que preencha os dados do produto com as seguintes informações:
	| Field               | Value            |
	Quando realizar uma chamada Post ao endereço 'api/Produto/CriarProduto'
	Entao retorne erro

Cenario: Cadastrar um produto preenchendo todos os campos
	Dado que preencha os dados do produto com as seguintes informações:
	| Field         | Value  |
	| Descricao     | Sapato |
	| UnidadeCodigo | 3      |
	| PrecoCusto    | 10,00  |
	| PrecoVenda    | 20,00  |
	Quando realizar uma chamada Post ao endereço 'api/Produto/CriarProduto'
	Entao retorne sucesso

Cenario: Pesquisar todos os produtos com produtos cadastrados
	Dado que preencha os dados do produto com as seguintes informações:
	| Field         | Value  |
	| Descricao     | Sapato |
	| UnidadeCodigo | 3      |
	| PrecoCusto    | 10,00  |
	| PrecoVenda    | 20,00  |
	Quando realizar uma chamada Post ao endereço 'api/Produto/CriarProduto'
	Entao retorne sucesso
	Quando realizar uma chamada Get ao endereço 'api/Produto/ObterListaProduto'
	Entao retorne sucesso
	E que o retorno tenha uma lista com '1' itens

Cenario: Editar um produto preenchendo todos os campos
	Dado que preencha os dados do produto com as seguintes informações:
	| Field         | Value  |
	| Descricao     | Sapato |
	| UnidadeCodigo | 3      |
	| PrecoCusto    | 10,00  |
	| PrecoVenda    | 20,00  |
	Quando realizar uma chamada Post ao endereço 'api/Produto/CriarProduto'
	Entao retorne sucesso
	Dado que preencha os dados do produto com as seguintes informações:
	| Field         | Value  |
	| ProdutoCodigo | 1      |
	| Descricao     | Sapato |
	| UnidadeCodigo | 3      |
	| PrecoCusto    | 10,00  |
	| PrecoVenda    | 20,00  |
	Quando realizar uma chamada Put ao endereço 'api/Produto/EditarProduto'
	Entao retorne sucesso

Cenario: Excluir um produto por código
	Dado que preencha os dados do produto com as seguintes informações:
	| Field         | Value  |
	| Descricao     | Sapato |
	| UnidadeCodigo | 3      |
	| PrecoCusto    | 10,00  |
	| PrecoVenda    | 20,00  |
	Quando realizar uma chamada Post ao endereço 'api/Produto/CriarProduto'
	Entao retorne sucesso
	Quando realizar uma chamada Delete ao endereço 'api/Produto/ExcluirProduto/1'
	Entao retorne sucesso