Funcionalidade: ClienteController
	Para funcionalidade cadastro de cliente
	Eu como um usuário do sistema
	Desejo utilizar os métodos CRUD do cliente.

Cenario: Tentar cadastrar um Cliente sem preencher todos os campos
	Dado que preencha os dados do cliente com as seguintes informações:
	| Field               | Value            |
	Quando realizar uma chamada Post ao endereço 'api/Cliente/CriarCliente'
	Entao retorne erro

Cenario: Cadastrar um cliente do tipo pessoa fisica preenchendo todos os campos
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
	Entao retorne sucesso

Cenario: Cadastrar um cliente do tipo pessoa juridica preenchendo todos os campos
	Dado que preencha os dados do cliente com as seguintes informações:
	| Field         | Value               |
	| RazaoSocial   | Baianus Tuning LTDA |
	| CNPJ          | 77.656.976/0001-41  |
	| Contato       | Henrique            |
	| EPessoaFisica | false               |
	| PapelPessoa   | 4                   |
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
	Entao retorne sucesso

Cenario: Pesquisar um cliente do tipo pessoa fisica sem filtros
	Dado que preencha os dados do filtro de pesquisa de cliente com as seguintes informações:
	| Field         | Value |
	| EPessoaFisica | true  |
	Quando realizar uma chamada Post ao endereço 'api/Cliente/PesquisarCliente'
	Entao retorne sucesso

Cenario: Pesquisar um cliente do tipo pessoa fisica por código, nome, CPFCNPJ, email 
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
	| Field    | Value           |
	| Telefone | (44) 3232-5566  |
	| Email    | teste@gmail.com |
	Quando realizar uma chamada Post ao endereço 'api/Cliente/CriarCliente'
	Entao retorne sucesso
	Dado que preencha os dados do filtro de pesquisa de cliente com as seguintes informações:
	| Field         | Value           |
	| CodigoCliente | 1               |
	| Nome          | Henrique        |
	| CPFCNPJ       | 123.456.789.09  |
	| Email         | teste@gmail.com |
	| EPessoaFisica | true            |
	Quando realizar uma chamada Post ao endereço 'api/Cliente/PesquisarCliente'
	Entao retorne sucesso