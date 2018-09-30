using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Domain.Exceptions;
using ProjetoArtCouro.Resource.Validation;

namespace ProjetoArtCouro.Test.Resources.Validation
{
    [TestClass]
    public class AssertionConcernUnitTest
    {
        [TestMethod]
        public void TestAssertArgumentEquals()
        {
            try
            {
                AssertionConcern<BusinessException>.AssertArgumentEquals("A", "B", "Erro A diferente de B");
                Assert.Fail("Deveria retornar um erro");
            }
            catch (BusinessException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(BusinessException));
                Assert.AreEqual(ex.Message, "Erro A diferente de B", "Mensagem invalida");
            }
        }

        [TestMethod]
        public void TestAssertArgumentFalse()
        {
            try
            {
                AssertionConcern<BusinessException>.AssertArgumentFalse(true, "Valor esperado é falso");
                Assert.Fail("Deveria retornar um erro");
            }
            catch (BusinessException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(BusinessException));
                Assert.AreEqual(ex.Message, "Valor esperado é falso", "Mensagem invalida");
            }
        }

        [TestMethod]
        public void TestAssertArgumentTrue()
        {
            try
            {
                AssertionConcern<BusinessException>.AssertArgumentTrue(false, "Valor esperado é verdadeiro");
                Assert.Fail("Deveria retornar um erro");
            }
            catch (BusinessException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(BusinessException));
                Assert.AreEqual(ex.Message, "Valor esperado é verdadeiro", "Mensagem invalida");
            }
        }

        [TestMethod]
        public void TestAssertArgumentLengthMaxValue()
        {
            try
            {
                AssertionConcern<BusinessException>.AssertArgumentLength("AAA", 2, "Valor esperado é maior que o maxímo");
                Assert.Fail("Deveria retornar um erro");
            }
            catch (BusinessException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(BusinessException));
                Assert.AreEqual(ex.Message, "Valor esperado é maior que o maxímo", "Mensagem invalida");
            }
        }

        [TestMethod]
        public void TestAssertArgumentLengthMaxMinValue()
        {
            try
            {
                AssertionConcern<BusinessException>.AssertArgumentLength("A", 2, 3, "Valor esperado não esta contido no intervalo");
                Assert.Fail("Deveria retornar um erro");
            }
            catch (BusinessException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(BusinessException));
                Assert.AreEqual(ex.Message, "Valor esperado não esta contido no intervalo", "Mensagem invalida");
            }
        }

        [TestMethod]
        public void TestAssertArgumentMatches()
        {
            try
            {
                AssertionConcern<BusinessException>.AssertArgumentMatches("[1-9]", "0", "Valor esperado não atende o padrão expecificado");
                Assert.Fail("Deveria retornar um erro");
            }
            catch (BusinessException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(BusinessException));
                Assert.AreEqual(ex.Message, "Valor esperado não atende o padrão expecificado", "Mensagem invalida");
            }
        }

        [TestMethod]
        public void TestAssertArgumentNotEmpty()
        {
            try
            {
                AssertionConcern<BusinessException>.AssertArgumentNotEmpty(null, "Valor não pode ser nulo");
                Assert.Fail("Deveria retornar um erro");
            }
            catch (BusinessException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(BusinessException));
                Assert.AreEqual(ex.Message, "Valor não pode ser nulo", "Mensagem invalida");
            }
        }

        [TestMethod]
        public void TestAssertArgumentNotEquals()
        {
            try
            {
                AssertionConcern<BusinessException>.AssertArgumentNotEquals("A", "A", "Valor não pode ser igual");
                Assert.Fail("Deveria retornar um erro");
            }
            catch (BusinessException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(BusinessException));
                Assert.AreEqual(ex.Message, "Valor não pode ser igual", "Mensagem invalida");
            }
        }

        [TestMethod]
        public void TestAssertArgumentNotNull()
        {
            try
            {
                AssertionConcern<BusinessException>.AssertArgumentNotNull(null, "Valor não pode ser nulo");
                Assert.Fail("Deveria retornar um erro");
            }
            catch (BusinessException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(BusinessException));
                Assert.AreEqual(ex.Message, "Valor não pode ser nulo" +
                    "", "Mensagem invalida");
            }
        }

        [TestMethod]
        public void TestAssertArgumentRange()
        {
            try
            {
                AssertionConcern<BusinessException>.AssertArgumentRange(1, 2, 3, "Valor esperado não esta contido no intervalo");
                Assert.Fail("Deveria retornar um erro");
            }
            catch (BusinessException ex)
            {
                Assert.IsInstanceOfType(ex, typeof(BusinessException));
                Assert.AreEqual(ex.Message, "Valor esperado não esta contido no intervalo", "Mensagem invalida");
            }
        }
    }
}
