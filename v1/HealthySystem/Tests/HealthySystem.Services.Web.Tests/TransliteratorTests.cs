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
            var bulgarianChars = "абвгдежийкзлмнопрстуфхцчшщъьюя";
            var englishChars = "abvgdezhiykzlmnoprstufhtschshshtayyuya";

            var converted = this.transliterator.GetTextInEnglish(bulgarianChars);

            Assert.AreEqual(englishChars, converted);
        }

        [TestMethod]
        public void TestAllCharactersCorrectConversionDifferentCasingFromBgToEn()
        {
            var bulgarianChars = "АбвгдежийкЗлмнопрсТуФхцчшщъьюЯ";
            var englishChars = "abvgdezhiykzlmnoprstufhtschshshtayyuya";

            var converted = this.transliterator.GetTextInEnglish(bulgarianChars);

            Assert.AreEqual(englishChars, converted);
        }

        [TestMethod]
        public void TestFromBgToEnWithDashes()
        {
            var bulgarianChars = "Абвг - деЗлм - - нопрсТуФх - - ч   aaaA";
            var englishChars = "abvg-dezlm-noprstufh-ch-aaaa";

            var converted = this.transliterator.GetTextInEnglish(bulgarianChars);

            Assert.AreEqual(englishChars, converted);
        }

        [TestMethod]
        public void TestFromBgToEnWithSymbols()
        {
            var bulgarianChars = "Абвг - деЗлм - ! * ~- нопрсТуФх - - ч ^!(@  aaaA";
            var englishChars = "abvg-dezlm-noprstufh-ch-aaaa";

            var converted = this.transliterator.GetTextInEnglish(bulgarianChars);

            Assert.AreEqual(englishChars, converted);
        }
    }
}
