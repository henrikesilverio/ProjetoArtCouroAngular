Funcionalidade: ClienteController
	Para funcionalidade cadastro de cliente
	Eu como um usuário do sistema
	Desejo utilizar os métodos CRUD do cliente.

Cenario: Tentar cadastrar um cliente sem preencher todos os campos
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

Cenario: Pesquisar um cliente do tipo pessoa juridica sem filtros
	Dado que preencha os dados do filtro de pesquisa de cliente com as seguintes informações:
	| Field         | Value |
	| EPessoaFisica | false |
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
	| Codigo        | 1               |
	| Nome          | Henrique        |
	| CPFCNPJ       | 123.456.789.09  |
	| Email         | teste@gmail.com |
	| EPessoaFisica | true            |
	Quando realizar uma chamada Post ao endereço 'api/Cliente/PesquisarCliente'
	Entao retorne sucesso

Cenario: Pesquisar um cliente do tipo pessoa juridica por código, nome, CPFCNPJ 
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
	Dado que preencha os dados do filtro de pesquisa de cliente com as seguintes informações:
	| Field         | Value               |
	| Codigo        | 1                   |
	| Nome          | Baianus Tuning LTDA |
	| CPFCNPJ       | 77.656.976/0001-41  |
	| EPessoaFisica | false               |
	Quando realizar uma chamada Post ao endereço 'api/Cliente/PesquisarCliente'
	Entao retorne sucesso

Cenario: Pesquisar um cliente por código
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
	Quando realizar uma chamada Get ao endereço 'api/Cliente/ObterClientePorCodigo/1'
	Entao retorne sucesso

Cenario: Editar um cliente do tipo pessoa fisica preenchendo todos os campos
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
	Dado que preencha os dados do cliente com as seguintes informações:
	| Field         | Value          |
	| Codigo        | 1              |
	| Nome          | Carla          |
	| CPF           | 109.720.760-93 |
	| RG            | 229999556      |
	| Sexo          | F              |
	| EstadoCivilId | 2              |
	| EPessoaFisica | true           |
	| PapelPessoa   | 4              |
	E que preecha os dados do endereço do cliente com as seguintes informações:
	| Field      | Value        |
	| EnderecoId | -1           |
	| Logradouro | Rua da morte |
	| Bairro     | Jardim vazio |
	| Numero     | 800          |
	| Cidade     | Sarandi      |
	| Cep        | 87112-540    |
	| UFId       | 16           |
	E que preecha os dados de meios de comunicação do cliente com as seguintes informações:
	| Field      | Value          |
	| TelefoneId | -1             |
	| Telefone   | (11) 2332-5566 |
	Quando realizar uma chamada Put ao endereço 'api/Cliente/EditarCliente'
	Entao retorne sucesso

Cenario: Editar um cliente do tipo pessoa juridica preenchendo todos os campos
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
	Dado que preencha os dados do cliente com as seguintes informações:
	| Field         | Value              |
	| Codigo        | 1                  |
	| RazaoSocial   | Nova               |
	| CNPJ          | 50.020.765/0001-61 |
	| Contato       | Vida               |
	| EPessoaFisica | false              |
	| PapelPessoa   | 4                  |
	E que preecha os dados do endereço do cliente com as seguintes informações:
	| Field      | Value        |
	| EnderecoId | -1           |
	| Logradouro | Rua da morte |
	| Bairro     | Jardim vazio |
	| Numero     | 800          |
	| Cidade     | Sarandi      |
	| Cep        | 87112-540    |
	| UFId       | 16           |
	E que preecha os dados de meios de comunicação do cliente com as seguintes informações:
	| Field      | Value          |
	| TelefoneId | -1             |
	| Telefone   | (11) 2332-5566 |
	Quando realizar uma chamada Put ao endereço 'api/Cliente/EditarCliente'
	Entao retorne sucesso

Cenario: Excluir um cliente por código
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
	Quando realizar uma chamada Delete ao endereço 'api/Cliente/ExcluirCliente/1'
	Entao retorne sucesso