﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.4.0.0
//      SpecFlow Generator Version:2.4.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace ProjetoArtCouro.Test.API.Controllers.Contas
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("ContaReceberController")]
    public partial class ContaReceberControllerFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ContaReceberController.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt-BR"), "ContaReceberController", "\t\tPara funcionalidade de contas a receber\r\n\t\tEu como um usuário do sistema\r\n\t\tDes" +
                    "ejo utilizar os métodos de consulta e de recebimento.", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 6
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table1.AddRow(new string[] {
                        "Nome",
                        "Henrique"});
            table1.AddRow(new string[] {
                        "CPF",
                        "123.456.789.09"});
            table1.AddRow(new string[] {
                        "RG",
                        "224445556"});
            table1.AddRow(new string[] {
                        "Sexo",
                        "M"});
            table1.AddRow(new string[] {
                        "EstadoCivilId",
                        "1"});
            table1.AddRow(new string[] {
                        "EPessoaFisica",
                        "true"});
            table1.AddRow(new string[] {
                        "PapelPessoa",
                        "4"});
#line 7
 testRunner.Given("que preencha os dados do fornecedor com as seguintes informações:", ((string)(null)), table1, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table2.AddRow(new string[] {
                        "Logradouro",
                        "Rua da vida"});
            table2.AddRow(new string[] {
                        "Bairro",
                        "Jardim mundo"});
            table2.AddRow(new string[] {
                        "Numero",
                        "400"});
            table2.AddRow(new string[] {
                        "Cidade",
                        "Sarandi"});
            table2.AddRow(new string[] {
                        "Cep",
                        "87112-540"});
            table2.AddRow(new string[] {
                        "UFId",
                        "16"});
#line 16
 testRunner.And("que preecha os dados do endereço do fornecedor com as seguintes informações:", ((string)(null)), table2, "E ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table3.AddRow(new string[] {
                        "Telefone",
                        "(44) 3232-5566"});
#line 24
 testRunner.And("que preecha os dados de meios de comunicação do fornecedor com as seguintes infor" +
                    "mações:", ((string)(null)), table3, "E ");
#line 27
 testRunner.When("realizar uma chamada Post ao endereço \'api/Fornecedor/CriarFornecedor\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table4.AddRow(new string[] {
                        "Nome",
                        "Henrique"});
            table4.AddRow(new string[] {
                        "CPF",
                        "123.456.789.09"});
            table4.AddRow(new string[] {
                        "RG",
                        "224445556"});
            table4.AddRow(new string[] {
                        "Sexo",
                        "M"});
            table4.AddRow(new string[] {
                        "EstadoCivilId",
                        "1"});
            table4.AddRow(new string[] {
                        "EPessoaFisica",
                        "true"});
            table4.AddRow(new string[] {
                        "PapelPessoa",
                        "4"});
#line 29
 testRunner.Given("que preencha os dados do cliente com as seguintes informações:", ((string)(null)), table4, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table5.AddRow(new string[] {
                        "Logradouro",
                        "Rua da vida"});
            table5.AddRow(new string[] {
                        "Bairro",
                        "Jardim mundo"});
            table5.AddRow(new string[] {
                        "Numero",
                        "400"});
            table5.AddRow(new string[] {
                        "Cidade",
                        "Sarandi"});
            table5.AddRow(new string[] {
                        "Cep",
                        "87112-540"});
            table5.AddRow(new string[] {
                        "UFId",
                        "16"});
#line 38
 testRunner.And("que preecha os dados do endereço do cliente com as seguintes informações:", ((string)(null)), table5, "E ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table6.AddRow(new string[] {
                        "Telefone",
                        "(44) 3232-5566"});
#line 46
 testRunner.And("que preecha os dados de meios de comunicação do cliente com as seguintes informaç" +
                    "ões:", ((string)(null)), table6, "E ");
#line 49
 testRunner.When("realizar uma chamada Post ao endereço \'api/Cliente/CriarCliente\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table7.AddRow(new string[] {
                        "Descricao",
                        "1 Parcela"});
            table7.AddRow(new string[] {
                        "QuantidadeParcelas",
                        "1"});
            table7.AddRow(new string[] {
                        "Ativo",
                        "true"});
#line 51
 testRunner.Given("que preencha os dados da condicao de pagamento com as seguintes informações:", ((string)(null)), table7, "Dado ");
#line 56
 testRunner.When("realizar uma chamada Post ao endereço \'api/CondicaoPagamento/CriarCondicaoPagamen" +
                    "to\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table8.AddRow(new string[] {
                        "Descricao",
                        "Cartão"});
            table8.AddRow(new string[] {
                        "Ativo",
                        "true"});
#line 58
 testRunner.Given("que preencha os dados da forma de pagamento com as seguintes informações:", ((string)(null)), table8, "Dado ");
#line 62
 testRunner.When("realizar uma chamada Post ao endereço \'api/FormaPagamento/CriarFormaPagamento\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table9.AddRow(new string[] {
                        "Descricao",
                        "Sapato"});
            table9.AddRow(new string[] {
                        "UnidadeCodigo",
                        "3"});
            table9.AddRow(new string[] {
                        "PrecoCusto",
                        "10,00"});
            table9.AddRow(new string[] {
                        "PrecoVenda",
                        "20,00"});
#line 64
 testRunner.Given("que preencha os dados do produto com as seguintes informações:", ((string)(null)), table9, "Dado ");
#line 70
 testRunner.When("realizar uma chamada Post ao endereço \'api/Produto/CriarProduto\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table10.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018 8:00"});
            table10.AddRow(new string[] {
                        "StatusCompra",
                        "Aberto"});
            table10.AddRow(new string[] {
                        "ValorTotalBruto",
                        "10,00"});
            table10.AddRow(new string[] {
                        "ValorTotalFrete",
                        "0,00"});
            table10.AddRow(new string[] {
                        "ValorTotalLiquido",
                        "10,00"});
#line 72
 testRunner.Given("que preencha os dados da compra com as seguintes informações:", ((string)(null)), table10, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Codigo",
                        "Descricao",
                        "Quantidade",
                        "PrecoVenda",
                        "ValorBruto",
                        "ValorLiquido"});
            table11.AddRow(new string[] {
                        "1",
                        "Cinto",
                        "1",
                        "10,00",
                        "10,00",
                        "10,00"});
#line 79
 testRunner.And("que preecha os dados do item de compra com as seguintes informações:", ((string)(null)), table11, "E ");
#line 82
 testRunner.When("realizar uma chamada Post ao endereço \'api/Compra/CriarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table12.AddRow(new string[] {
                        "CodigoCompra",
                        "1"});
            table12.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018 8:00"});
            table12.AddRow(new string[] {
                        "StatusCompra",
                        "Aberto"});
            table12.AddRow(new string[] {
                        "ValorTotalBruto",
                        "10,00"});
            table12.AddRow(new string[] {
                        "ValorTotalFrete",
                        "0,00"});
            table12.AddRow(new string[] {
                        "ValorTotalLiquido",
                        "10,00"});
            table12.AddRow(new string[] {
                        "FornecedorId",
                        "1"});
            table12.AddRow(new string[] {
                        "FormaPagamentoId",
                        "1"});
            table12.AddRow(new string[] {
                        "CondicaoPagamentoId",
                        "1"});
#line 84
 testRunner.Given("que preencha os dados da compra com as seguintes informações:", ((string)(null)), table12, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "Codigo",
                        "Descricao",
                        "Quantidade",
                        "PrecoVenda",
                        "ValorBruto",
                        "ValorLiquido"});
            table13.AddRow(new string[] {
                        "1",
                        "Cinto",
                        "1",
                        "10,00",
                        "10,00",
                        "10,00"});
#line 95
 testRunner.And("que preecha os dados do item de compra com as seguintes informações:", ((string)(null)), table13, "E ");
#line 98
 testRunner.When("realizar uma chamada Put ao endereço \'api/Compra/EditarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table14.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018 8:00"});
            table14.AddRow(new string[] {
                        "StatusVenda",
                        "Aberto"});
            table14.AddRow(new string[] {
                        "ValorTotalBruto",
                        "10,00"});
            table14.AddRow(new string[] {
                        "ValorTotalDesconto",
                        "0,50"});
            table14.AddRow(new string[] {
                        "ValorTotalLiquido",
                        "10,00"});
#line 100
 testRunner.Given("que preencha os dados da venda com as seguintes informações:", ((string)(null)), table14, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "Codigo",
                        "Descricao",
                        "Quantidade",
                        "PrecoVenda",
                        "ValorBruto",
                        "ValorDesconto",
                        "ValorLiquido"});
            table15.AddRow(new string[] {
                        "1",
                        "Cinto",
                        "1",
                        "10,00",
                        "10,00",
                        "0,50",
                        "10,00"});
#line 107
 testRunner.And("que preecha os dados do item de venda com as seguintes informações:", ((string)(null)), table15, "E ");
#line 110
 testRunner.When("realizar uma chamada Post ao endereço \'api/Venda/CriarVenda\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table16.AddRow(new string[] {
                        "CodigoVenda",
                        "1"});
            table16.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018 8:00"});
            table16.AddRow(new string[] {
                        "StatusVenda",
                        "Aberto"});
            table16.AddRow(new string[] {
                        "ValorTotalBruto",
                        "10,00"});
            table16.AddRow(new string[] {
                        "ValorTotalDesconto",
                        "0,50"});
            table16.AddRow(new string[] {
                        "ValorTotalLiquido",
                        "10,00"});
            table16.AddRow(new string[] {
                        "ClienteId",
                        "1"});
            table16.AddRow(new string[] {
                        "FormaPagamentoId",
                        "1"});
            table16.AddRow(new string[] {
                        "CondicaoPagamentoId",
                        "1"});
#line 112
 testRunner.Given("que preencha os dados da venda com as seguintes informações:", ((string)(null)), table16, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                        "Codigo",
                        "Descricao",
                        "Quantidade",
                        "PrecoVenda",
                        "ValorBruto",
                        "ValorDesconto",
                        "ValorLiquido"});
            table17.AddRow(new string[] {
                        "1",
                        "Cinto",
                        "1",
                        "10,00",
                        "10,00",
                        "0,50",
                        "10,00"});
#line 123
 testRunner.And("que preecha os dados do item de venda com as seguintes informações:", ((string)(null)), table17, "E ");
#line 126
 testRunner.When("realizar uma chamada Put ao endereço \'api/Venda/EditarVenda\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Pesquisar as contas a receber sem filtros")]
        public virtual void PesquisarAsContasAReceberSemFiltros()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Pesquisar as contas a receber sem filtros", null, ((string[])(null)));
#line 128
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
#line 129
 testRunner.Given("que preencha os dados do filtro de pesquisa de contas a receber com as seguintes " +
                    "informações:", ((string)(null)), table18, "Dado ");
#line 131
 testRunner.When("realizar uma chamada Post ao endereço \'api/ContaReceber/PesquisaContaReceber\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 132
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Pesquisar as contas a receber por código da venda, código do cliente, data de emi" +
            "ssão, data de vencimento, nome do cliente, CPFCNPJ, status")]
        public virtual void PesquisarAsContasAReceberPorCodigoDaVendaCodigoDoClienteDataDeEmissaoDataDeVencimentoNomeDoClienteCPFCNPJStatus()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Pesquisar as contas a receber por código da venda, código do cliente, data de emi" +
                    "ssão, data de vencimento, nome do cliente, CPFCNPJ, status", null, ((string[])(null)));
#line 134
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table19.AddRow(new string[] {
                        "CodigoVenda",
                        "1"});
            table19.AddRow(new string[] {
                        "CodigoCliente",
                        "1"});
            table19.AddRow(new string[] {
                        "DataEmissao",
                        "08/10/2018"});
            table19.AddRow(new string[] {
                        "DataVencimento",
                        "08/10/2018"});
            table19.AddRow(new string[] {
                        "NomeCliente",
                        "Henrique"});
            table19.AddRow(new string[] {
                        "CPFCNPJ",
                        "123.456.789.09"});
            table19.AddRow(new string[] {
                        "StatusId",
                        "1"});
#line 135
 testRunner.Given("que preencha os dados do filtro de pesquisa de contas a receber com as seguintes " +
                    "informações:", ((string)(null)), table19, "Dado ");
#line 144
 testRunner.When("realizar uma chamada Post ao endereço \'api/ContaReceber/PesquisaContaReceber\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 145
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Receber contas")]
        public virtual void ReceberContas()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Receber contas", null, ((string[])(null)));
#line 147
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table20 = new TechTalk.SpecFlow.Table(new string[] {
                        "CodigoContaReceber",
                        "CodigoVenda",
                        "CodigoCliente",
                        "DataEmissao",
                        "DataVencimento",
                        "NomeCliente",
                        "CPFCNPJ",
                        "StatusId",
                        "ValorDocumento",
                        "Recebido"});
            table20.AddRow(new string[] {
                        "1",
                        "1",
                        "1",
                        "08/10/2018",
                        "08/10/2018",
                        "Henrique",
                        "123.456.789.09",
                        "1",
                        "10,00",
                        "True"});
#line 148
 testRunner.Given("que preencha os dados da contas a receber com as seguintes informações:", ((string)(null)), table20, "Dado ");
#line 151
 testRunner.When("realizar uma chamada Put ao endereço \'api/ContaReceber/ReceberConta\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 152
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
