namespace HealthySystem.Services.Web.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TransliteratorTests
    {
        private Transliterator transliterator;

        [TestInitialize]
        public void Init()
        {
            this.transliterator = new Transliterator();
        }

        [TestMethod]
        public void TestAllCharactersCorrectConversionFromBgToEn()
        {
            var bgChars = "абвгдежийкзлмнопрстуфхцчшщъьюя";
            var enChars = "abvgdezhiykzlmnoprstufhtschshshtayyuya";

            var converted = this.transliterator.GetTextInEnglish(bgChars);

            Assert.AreEqual(enChars, converted);
        }

        [TestMethod]
        public void TestAllCharactersCorrectConversionDifferentCasingFromBgToEn()
        {
            var bgChars = "АбвгдежийкЗлмнопрсТуФхцчшщъьюЯ";
            var enChars = "abvgdezhiykzlmnoprstufhtschshshtayyuya";

            var converted = this.transliterator.GetTextInEnglish(bgChars);

            Assert.AreEqual(enChars, converted);
        }

        [TestMethod]
        public void TestFromBgToEnWithDashes()
        {
            var bgChars = "Абвг - деЗлм - - нопрсТуФх - - ч   aaaA";
            var enChars = "abvg-dezlm-noprstufh-ch-aaaa";

            var converted = this.transliterator.GetTextInEnglish(bgChars);

            Assert.AreEqual(enChars, converted);
        }

        [TestMethod]
        public void TestFromBgToEnWithSymbols()
        {
            var bgChars = "Абвг - деЗлм - ! * ~- нопрсТуФх - - ч ^!(@  aaaA";
            var enChars = "abvg-dezlm-noprstufh-ch-aaaa";

            var converted = this.transliterator.GetTextInEnglish(bgChars);

            Assert.AreEqual(enChars, converted);
        }
    }
}
