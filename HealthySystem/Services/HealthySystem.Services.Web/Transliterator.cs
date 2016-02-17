namespace HealthySystem.Services.Web
{
    using System.Linq;
    using System.Text;
    using HealthySystem.Services.Web.Contracts;

    public class Transliterator : ITransliterator
    {
        public string GetTextInEnglish(string text)
        {
            var sb = new StringBuilder();

            var input = text.ToLower().Trim();

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLetter(input[i]))
                {
                    var letter = this.GetLetterInEnglish(input[i]);
                    sb.Append(letter);
                }
                else if (char.IsDigit(input[i]))
                {
                    sb.Append(input[i]);
                }
                else if (input[i] == ' ')
                {
                    if (sb.ToString().Last() != '-')
                    {
                        sb.Append("-");
                    }
                }
                else if (!char.IsLetterOrDigit(input[i]) && input[i] != ' ')
                {
                    continue;
                }
            }

            return sb.ToString();
        }

        public string GetLetterInEnglish(char letter)
        {
            string result;

            switch (letter)
            {
                case 'а': result = "a"; break;
                case 'б': result = "b"; break;
                case 'в': result = "v"; break;
                case 'г': result = "g"; break;
                case 'д': result = "d"; break;
                case 'е': result = "e"; break;
                case 'ж': result = "zh"; break;
                case 'и': result = "i"; break;
                case 'й': result = "y"; break;
                case 'к': result = "k"; break;
                case 'з': result = "z"; break;
                case 'л': result = "l"; break;
                case 'м': result = "m"; break;
                case 'н': result = "n"; break;
                case 'о': result = "o"; break;
                case 'п': result = "p"; break;
                case 'р': result = "r"; break;
                case 'с': result = "s"; break;
                case 'т': result = "t"; break;
                case 'у': result = "u"; break;
                case 'ф': result = "f"; break;
                case 'х': result = "h"; break;
                case 'ц': result = "ts"; break;
                case 'ч': result = "ch"; break;
                case 'ш': result = "sh"; break;
                case 'щ': result = "sht"; break;
                case 'ъ': result = "a"; break;
                case 'ь': result = "y"; break;
                case 'ю': result = "yu"; break;
                case 'я': result = "ya"; break;
                default:
                    result = letter.ToString();
                    break;
            }

            return result;
        }
    }
}