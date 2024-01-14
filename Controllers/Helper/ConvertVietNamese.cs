using System.Text.RegularExpressions;
using System.Text;

namespace WebSellPhoneAPI.Controllers.Helper
{
    public class ConvertVietNamese
    {
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower().Replace(" ", "");
        }
    }
}
