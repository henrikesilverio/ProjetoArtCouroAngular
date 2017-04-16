using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoArtCouro.Resources.Resources;
using ProjetoArtCouro.Resources.Validation;

namespace ProjetoArtCouro.Test.Resources.Validation
{
    [TestClass]
    public class ValidationContractUnitTest
    {
        private TestNotifiable _testNotifiable;

        [TestInitialize]
        public void Initialize()
        {
            _testNotifiable = new TestNotifiable();
        }

        [TestMethod]
        public void TestIsRequiredStringNull()
        {
            new ValidationContract<TestNotifiable>(_testNotifiable)
                .IsRequired(x => x.PropertyString);
            Assert.IsFalse(_testNotifiable.IsValid(), "Esta valido");
            var mensagens = _testNotifiable.GetMergeNotifications();
            Assert.AreEqual(mensagens, string.Format(Erros.FieldIsRequired, "PropertyString"), "Mensagem invalida");
        }

        [TestMethod]
        public void TestIsRequiredStringEmpty()
        {
            _testNotifiable.PropertyString = string.Empty;
            new ValidationContract<TestNotifiable>(_testNotifiable)
                .IsRequired(x => x.PropertyString);
            Assert.IsFalse(_testNotifiable.IsValid(), "Esta valido");
            var mensagens = _testNotifiable.GetMergeNotifications();
            Assert.AreEqual(mensagens, string.Format(Erros.FieldIsRequired, "PropertyString"), "Mensagem invalida");
        }

        [TestMethod]
        public void TestIsRequiredStringWithSpaces()
        {
            _testNotifiable.PropertyString = "      ";
            new ValidationContract<TestNotifiable>(_testNotifiable)
                .IsRequired(x => x.PropertyString);
            Assert.IsFalse(_testNotifiable.IsValid(), "Esta valido");
            var mensagens = _testNotifiable.GetMergeNotifications();
            Assert.AreEqual(mensagens, string.Format(Erros.FieldIsRequired, "PropertyString"), "Mensagem invalida");
        }

        [TestMethod]
        public void TestIsRequiredTypeBoolean()
        {
            new ValidationContract<TestNotifiable>(_testNotifiable)
                .IsRequired(x => x.PropertyBoolean);
            Assert.IsTrue(_testNotifiable.IsValid(), "Esta valido");
            var mensagens = _testNotifiable.GetMergeNotifications();
            Assert.AreEqual(mensagens, "", "Mensagem invalida");
        }

        [TestMethod]
        public void TestIsRequiredTypeDecimal()
        {
            new ValidationContract<TestNotifiable>(_testNotifiable)
                .IsRequired(x => x.PropertyDecimal);
            Assert.IsTrue(_testNotifiable.IsValid(), "Esta valido");
            var mensagens = _testNotifiable.GetMergeNotifications();
            Assert.AreEqual(mensagens, "", "Mensagem invalida");
        }

        [TestMethod]
        public void TestIsNotNull()
        {
            new ValidationContract<TestNotifiable>(_testNotifiable)
                .IsNotNull(x => x.PropertyObject);
            Assert.IsFalse(_testNotifiable.IsValid(), "Esta valido");
            var mensagens = _testNotifiable.GetMergeNotifications();
            Assert.AreEqual(mensagens, string.Format(Erros.FieldCannotBeNull, "PropertyObject"), "Mensagem invalida");
        }

        [TestMethod]
        public void TestIsNotZero()
        {
            new ValidationContract<TestNotifiable>(_testNotifiable)
                .IsNotZero(x => x.PropertyDecimal);
            Assert.IsFalse(_testNotifiable.IsValid(), "Esta valido");
            var mensagens = _testNotifiable.GetMergeNotifications();
            Assert.AreEqual(mensagens, string.Format(Erros.FieldCannotBeZero, "PropertyDecimal"), "Mensagem invalida");
        }

        [TestMethod]
        public void TestIsNotEqualsEnum()
        {
            new ValidationContract<TestNotifiable>(_testNotifiable)
                .IsNotEquals(x => x.PropertyEnum, TestEnum.None);
            Assert.IsFalse(_testNotifiable.IsValid(), "Esta valido");
            var mensagens = _testNotifiable.GetMergeNotifications();
            Assert.AreEqual(mensagens, string.Format(Erros.FieldCannotBe, "PropertyEnum", (int)TestEnum.None), "Mensagem invalida");
        }

        [TestMethod]
        public void TestHasMaxLenght()
        {
            _testNotifiable.PropertyString = new string('A', 11);
            new ValidationContract<TestNotifiable>(_testNotifiable)
                .HasMaxLenght(x => x.PropertyString, 10);
            Assert.IsFalse(_testNotifiable.IsValid(), "Esta valido");
            var mensagens = _testNotifiable.GetMergeNotifications();
            Assert.AreEqual(mensagens, string.Format(Erros.FieldMustHaveMaxCharacters, "PropertyString", 10), "Mensagem invalida");

            _testNotifiable = new TestNotifiable {PropertyString = new string('A', 10)};
            new ValidationContract<TestNotifiable>(_testNotifiable)
                .HasMaxLenght(x => x.PropertyString, 10);
            Assert.IsTrue(_testNotifiable.IsValid(), "Esta invalido");
            mensagens = _testNotifiable.GetMergeNotifications();
            Assert.AreEqual(mensagens, "", "Mensagem invalida");

            _testNotifiable = new TestNotifiable { PropertyString = new string('A', 9) };
            new ValidationContract<TestNotifiable>(_testNotifiable)
                .HasMaxLenght(x => x.PropertyString, 10);
            Assert.IsTrue(_testNotifiable.IsValid(), "Esta invalido");
            mensagens = _testNotifiable.GetMergeNotifications();
            Assert.AreEqual(mensagens, "", "Mensagem invalida");
        }

        internal class TestNotifiable : Notifiable
        {
            public string PropertyString { get; set; }
            public bool PropertyBoolean { get; set; }
            public decimal PropertyDecimal { get; set; }
            public object PropertyObject { get; set; }
            public TestEnum PropertyEnum { get; set; }
        }

        internal enum TestEnum
        {
            None,
            Value
        }
    }
}
