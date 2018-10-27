Funcionalidade: PessoaController
	Para funcionalidade cadastro de pessoas
	Eu como um usuário do sistema
	Desejo utilizar os métodos consulta relacionados a pessoa.

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
	

Cenario: Pesquisar todos os estados federativos
	Quando realizar uma chamada Get ao endereço 'api/Pessoa/ObterListaEstado'
	Entao retorne sucesso

Cenario: Pesquisar todos os estados civis
	Quando realizar uma chamada Get ao endereço 'api/Pessoa/ObterListaEstadoCivil'
	Entao retorne sucesso

Cenario: Pesquisar todas as pessoas
	Quando realizar uma chamada Get ao endereço 'api/Pessoa/ObterListaPessoa'
	Entao retorne sucesso