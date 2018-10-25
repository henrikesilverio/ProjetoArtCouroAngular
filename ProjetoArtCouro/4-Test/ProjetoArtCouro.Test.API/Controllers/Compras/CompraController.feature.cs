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
namespace ProjetoArtCouro.Test.API.Controllers.Compras
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("CompraController")]
    public partial class CompraControllerFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "CompraController.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt-BR"), "CompraController", "\tPara funcionalidade de compra de produtos\r\n\tEu como um usuário do sistema\r\n\tDese" +
                    "jo utilizar os métodos CRUD da compra.", ProgrammingLanguage.CSharp, ((string[])(null)));
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
                        "Descricao",
                        "1 Parcela"});
            table4.AddRow(new string[] {
                        "QuantidadeParcelas",
                        "1"});
            table4.AddRow(new string[] {
                        "Ativo",
                        "true"});
#line 29
 testRunner.Given("que preencha os dados da condicao de pagamento com as seguintes informações:", ((string)(null)), table4, "Dado ");
#line 34
 testRunner.When("realizar uma chamada Post ao endereço \'api/CondicaoPagamento/CriarCondicaoPagamen" +
                    "to\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table5.AddRow(new string[] {
                        "Descricao",
                        "Cartão"});
            table5.AddRow(new string[] {
                        "Ativo",
                        "true"});
#line 36
 testRunner.Given("que preencha os dados da forma de pagamento com as seguintes informações:", ((string)(null)), table5, "Dado ");
#line 40
 testRunner.When("realizar uma chamada Post ao endereço \'api/FormaPagamento/CriarFormaPagamento\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table6.AddRow(new string[] {
                        "Descricao",
                        "Sapato"});
            table6.AddRow(new string[] {
                        "UnidadeCodigo",
                        "3"});
            table6.AddRow(new string[] {
                        "PrecoCusto",
                        "10,00"});
            table6.AddRow(new string[] {
                        "PrecoVenda",
                        "20,00"});
#line 42
 testRunner.Given("que preencha os dados do produto com as seguintes informações:", ((string)(null)), table6, "Dado ");
#line 48
 testRunner.When("realizar uma chamada Post ao endereço \'api/Produto/CriarProduto\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Tentar realizar um orçamento de compra sem preencher todos os campos")]
        public virtual void TentarRealizarUmOrcamentoDeCompraSemPreencherTodosOsCampos()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Tentar realizar um orçamento de compra sem preencher todos os campos", null, ((string[])(null)));
#line 50
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
#line 51
 testRunner.Given("que preencha os dados da compra com as seguintes informações:", ((string)(null)), table7, "Dado ");
#line 53
 testRunner.When("realizar uma chamada Post ao endereço \'api/Compra/CriarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 54
 testRunner.Then("retorne erro", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Gerar um orçamento preenchendo todos os campos")]
        public virtual void GerarUmOrcamentoPreenchendoTodosOsCampos()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Gerar um orçamento preenchendo todos os campos", null, ((string[])(null)));
#line 56
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table8.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018 8:00"});
            table8.AddRow(new string[] {
                        "StatusCompra",
                        "Aberto"});
            table8.AddRow(new string[] {
                        "ValorTotalBruto",
                        "10,00"});
            table8.AddRow(new string[] {
                        "ValorTotalFrete",
                        "0,00"});
            table8.AddRow(new string[] {
                        "ValorTotalLiquido",
                        "10,00"});
#line 57
 testRunner.Given("que preencha os dados da compra com as seguintes informações:", ((string)(null)), table8, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Codigo",
                        "Descricao",
                        "Quantidade",
                        "PrecoVenda",
                        "ValorBruto",
                        "ValorLiquido"});
            table9.AddRow(new string[] {
                        "1",
                        "Cinto",
                        "1",
                        "10,00",
                        "10,00",
                        "10,00"});
#line 64
 testRunner.And("que preecha os dados do item de compra com as seguintes informações:", ((string)(null)), table9, "E ");
#line 67
 testRunner.When("realizar uma chamada Post ao endereço \'api/Compra/CriarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 68
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Pesquisar uma compra sem filtros")]
        public virtual void PesquisarUmaCompraSemFiltros()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Pesquisar uma compra sem filtros", null, ((string[])(null)));
#line 70
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
#line 71
 testRunner.Given("que preencha os dados do filtro de pesquisa de compra com as seguintes informaçõe" +
                    "s:", ((string)(null)), table10, "Dado ");
#line 73
 testRunner.When("realizar uma chamada Post ao endereço \'api/Compra/PesquisarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 74
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Pesquisar uma compra por código da compra, código do fornecedor, data de cadastro" +
            ", status, nome do fornecedor, CPFCNPJ")]
        public virtual void PesquisarUmaCompraPorCodigoDaCompraCodigoDoFornecedorDataDeCadastroStatusNomeDoFornecedorCPFCNPJ()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Pesquisar uma compra por código da compra, código do fornecedor, data de cadastro" +
                    ", status, nome do fornecedor, CPFCNPJ", null, ((string[])(null)));
#line 76
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table11.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018 8:00"});
            table11.AddRow(new string[] {
                        "StatusCompra",
                        "Aberto"});
            table11.AddRow(new string[] {
                        "ValorTotalBruto",
                        "10,00"});
            table11.AddRow(new string[] {
                        "ValorTotalFrete",
                        "0,00"});
            table11.AddRow(new string[] {
                        "ValorTotalLiquido",
                        "10,00"});
#line 77
 testRunner.Given("que preencha os dados da compra com as seguintes informações:", ((string)(null)), table11, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Codigo",
                        "Descricao",
                        "Quantidade",
                        "PrecoVenda",
                        "ValorBruto",
                        "ValorLiquido"});
            table12.AddRow(new string[] {
                        "1",
                        "Cinto",
                        "1",
                        "10,00",
                        "10,00",
                        "10,00"});
#line 84
 testRunner.And("que preecha os dados do item de compra com as seguintes informações:", ((string)(null)), table12, "E ");
#line 87
 testRunner.When("realizar uma chamada Post ao endereço \'api/Compra/CriarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 88
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table13.AddRow(new string[] {
                        "CodigoCompra",
                        "1"});
            table13.AddRow(new string[] {
                        "CodigoFornecedor",
                        "1"});
            table13.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018"});
            table13.AddRow(new string[] {
                        "StatusId",
                        "1"});
            table13.AddRow(new string[] {
                        "NomeFornecedor",
                        "Henrique"});
            table13.AddRow(new string[] {
                        "CPFCNPJ",
                        "77.656.976/0001-41"});
#line 89
 testRunner.Given("que preencha os dados do filtro de pesquisa de compra com as seguintes informaçõe" +
                    "s:", ((string)(null)), table13, "Dado ");
#line 97
 testRunner.When("realizar uma chamada Post ao endereço \'api/Compra/PesquisarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 98
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Pesquisar uma compra por código da compra")]
        public virtual void PesquisarUmaCompraPorCodigoDaCompra()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Pesquisar uma compra por código da compra", null, ((string[])(null)));
#line 100
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table14.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018 8:00"});
            table14.AddRow(new string[] {
                        "StatusCompra",
                        "Aberto"});
            table14.AddRow(new string[] {
                        "ValorTotalBruto",
                        "10,00"});
            table14.AddRow(new string[] {
                        "ValorTotalFrete",
                        "0,00"});
            table14.AddRow(new string[] {
                        "ValorTotalLiquido",
                        "10,00"});
#line 101
 testRunner.Given("que preencha os dados da compra com as seguintes informações:", ((string)(null)), table14, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "Codigo",
                        "Descricao",
                        "Quantidade",
                        "PrecoVenda",
                        "ValorBruto",
                        "ValorLiquido"});
            table15.AddRow(new string[] {
                        "1",
                        "Cinto",
                        "1",
                        "10,00",
                        "10,00",
                        "10,00"});
#line 108
 testRunner.And("que preecha os dados do item de compra com as seguintes informações:", ((string)(null)), table15, "E ");
#line 111
 testRunner.When("realizar uma chamada Post ao endereço \'api/Compra/CriarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 112
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line 113
 testRunner.When("realizar uma chamada Post ao endereço \'api/Compra/PesquisarCompraPorCodigo/1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 114
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Apartir de um orçamento gerar uma compra preenchendo todos os campos")]
        public virtual void ApartirDeUmOrcamentoGerarUmaCompraPreenchendoTodosOsCampos()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Apartir de um orçamento gerar uma compra preenchendo todos os campos", null, ((string[])(null)));
#line 116
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table16.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018 8:00"});
            table16.AddRow(new string[] {
                        "StatusCompra",
                        "Aberto"});
            table16.AddRow(new string[] {
                        "ValorTotalBruto",
                        "10,00"});
            table16.AddRow(new string[] {
                        "ValorTotalFrete",
                        "0,00"});
            table16.AddRow(new string[] {
                        "ValorTotalLiquido",
                        "10,00"});
#line 117
 testRunner.Given("que preencha os dados da compra com as seguintes informações:", ((string)(null)), table16, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                        "Codigo",
                        "Descricao",
                        "Quantidade",
                        "PrecoVenda",
                        "ValorBruto",
                        "ValorLiquido"});
            table17.AddRow(new string[] {
                        "1",
                        "Cinto",
                        "1",
                        "10,00",
                        "10,00",
                        "10,00"});
#line 124
 testRunner.And("que preecha os dados do item de compra com as seguintes informações:", ((string)(null)), table17, "E ");
#line 127
 testRunner.When("realizar uma chamada Post ao endereço \'api/Compra/CriarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 128
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table18.AddRow(new string[] {
                        "CodigoCompra",
                        "1"});
            table18.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018 8:00"});
            table18.AddRow(new string[] {
                        "StatusCompra",
                        "Aberto"});
            table18.AddRow(new string[] {
                        "ValorTotalBruto",
                        "10,00"});
            table18.AddRow(new string[] {
                        "ValorTotalFrete",
                        "0,00"});
            table18.AddRow(new string[] {
                        "ValorTotalLiquido",
                        "10,00"});
            table18.AddRow(new string[] {
                        "FornecedorId",
                        "1"});
            table18.AddRow(new string[] {
                        "FormaPagamentoId",
                        "1"});
            table18.AddRow(new string[] {
                        "CondicaoPagamentoId",
                        "1"});
#line 129
 testRunner.Given("que preencha os dados da compra com as seguintes informações:", ((string)(null)), table18, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                        "Codigo",
                        "Descricao",
                        "Quantidade",
                        "PrecoVenda",
                        "ValorBruto",
                        "ValorLiquido"});
            table19.AddRow(new string[] {
                        "1",
                        "Cinto",
                        "1",
                        "10,00",
                        "10,00",
                        "10,00"});
#line 140
 testRunner.And("que preecha os dados do item de compra com as seguintes informações:", ((string)(null)), table19, "E ");
#line 143
 testRunner.When("realizar uma chamada Put ao endereço \'api/Compra/EditarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 144
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Cancelar uma compra preenchendo todos os campos")]
        public virtual void CancelarUmaCompraPreenchendoTodosOsCampos()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Cancelar uma compra preenchendo todos os campos", null, ((string[])(null)));
#line 146
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table20 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table20.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018 8:00"});
            table20.AddRow(new string[] {
                        "StatusCompra",
                        "Aberto"});
            table20.AddRow(new string[] {
                        "ValorTotalBruto",
                        "10,00"});
            table20.AddRow(new string[] {
                        "ValorTotalFrete",
                        "0,00"});
            table20.AddRow(new string[] {
                        "ValorTotalLiquido",
                        "10,00"});
#line 147
 testRunner.Given("que preencha os dados da compra com as seguintes informações:", ((string)(null)), table20, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table21 = new TechTalk.SpecFlow.Table(new string[] {
                        "Codigo",
                        "Descricao",
                        "Quantidade",
                        "PrecoVenda",
                        "ValorBruto",
                        "ValorLiquido"});
            table21.AddRow(new string[] {
                        "1",
                        "Cinto",
                        "1",
                        "10,00",
                        "10,00",
                        "10,00"});
#line 154
 testRunner.And("que preecha os dados do item de compra com as seguintes informações:", ((string)(null)), table21, "E ");
#line 157
 testRunner.When("realizar uma chamada Post ao endereço \'api/Compra/CriarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 158
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            TechTalk.SpecFlow.Table table22 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table22.AddRow(new string[] {
                        "CodigoCompra",
                        "1"});
            table22.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018 8:00"});
            table22.AddRow(new string[] {
                        "StatusCompra",
                        "Aberto"});
            table22.AddRow(new string[] {
                        "ValorTotalBruto",
                        "10,00"});
            table22.AddRow(new string[] {
                        "ValorTotalFrete",
                        "0,00"});
            table22.AddRow(new string[] {
                        "ValorTotalLiquido",
                        "10,00"});
            table22.AddRow(new string[] {
                        "FornecedorId",
                        "1"});
            table22.AddRow(new string[] {
                        "FormaPagamentoId",
                        "1"});
            table22.AddRow(new string[] {
                        "CondicaoPagamentoId",
                        "1"});
#line 159
 testRunner.Given("que preencha os dados da compra com as seguintes informações:", ((string)(null)), table22, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table23 = new TechTalk.SpecFlow.Table(new string[] {
                        "Codigo",
                        "Descricao",
                        "Quantidade",
                        "PrecoVenda",
                        "ValorBruto",
                        "ValorLiquido"});
            table23.AddRow(new string[] {
                        "1",
                        "Cinto",
                        "1",
                        "10,00",
                        "10,00",
                        "10,00"});
#line 170
 testRunner.And("que preecha os dados do item de compra com as seguintes informações:", ((string)(null)), table23, "E ");
#line 173
 testRunner.When("realizar uma chamada Put ao endereço \'api/Compra/EditarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 174
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            TechTalk.SpecFlow.Table table24 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table24.AddRow(new string[] {
                        "CodigoCompra",
                        "1"});
            table24.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018 8:00"});
            table24.AddRow(new string[] {
                        "StatusCompra",
                        "Confirmado"});
            table24.AddRow(new string[] {
                        "ValorTotalBruto",
                        "10,00"});
            table24.AddRow(new string[] {
                        "ValorTotalFrete",
                        "0,00"});
            table24.AddRow(new string[] {
                        "ValorTotalLiquido",
                        "10,00"});
#line 175
 testRunner.Given("que preencha os dados da compra com as seguintes informações:", ((string)(null)), table24, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table25 = new TechTalk.SpecFlow.Table(new string[] {
                        "Codigo",
                        "Descricao",
                        "Quantidade",
                        "PrecoVenda",
                        "ValorBruto",
                        "ValorLiquido"});
            table25.AddRow(new string[] {
                        "1",
                        "Cinto",
                        "1",
                        "10,00",
                        "10,00",
                        "10,00"});
#line 183
 testRunner.And("que preecha os dados do item de compra com as seguintes informações:", ((string)(null)), table25, "E ");
#line 186
 testRunner.When("realizar uma chamada Put ao endereço \'api/Compra/EditarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 187
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Excluir uma compra por código:")]
        public virtual void ExcluirUmaCompraPorCodigo()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Excluir uma compra por código:", null, ((string[])(null)));
#line 189
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table26 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table26.AddRow(new string[] {
                        "DataCadastro",
                        "08/10/2018 8:00"});
            table26.AddRow(new string[] {
                        "StatusCompra",
                        "Aberto"});
            table26.AddRow(new string[] {
                        "ValorTotalBruto",
                        "10,00"});
            table26.AddRow(new string[] {
                        "ValorTotalFrete",
                        "0,00"});
            table26.AddRow(new string[] {
                        "ValorTotalLiquido",
                        "10,00"});
#line 190
 testRunner.Given("que preencha os dados da compra com as seguintes informações:", ((string)(null)), table26, "Dado ");
#line hidden
            TechTalk.SpecFlow.Table table27 = new TechTalk.SpecFlow.Table(new string[] {
                        "Codigo",
                        "Descricao",
                        "Quantidade",
                        "PrecoVenda",
                        "ValorBruto",
                        "ValorLiquido"});
            table27.AddRow(new string[] {
                        "1",
                        "Cinto",
                        "1",
                        "10,00",
                        "10,00",
                        "10,00"});
#line 197
 testRunner.And("que preecha os dados do item de compra com as seguintes informações:", ((string)(null)), table27, "E ");
#line 200
 testRunner.When("realizar uma chamada Post ao endereço \'api/Compra/CriarCompra\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 201
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line 202
 testRunner.When("realizar uma chamada Delete ao endereço \'api/Compra/ExcluirCompra/1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 203
 testRunner.Then("retorne sucesso", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entao ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
