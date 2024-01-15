using System.Text.RegularExpressions;
using System.Text;
using System.Drawing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace WebSellPhoneAPI.Controllers.Helper
{
    public class Global
    {
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower().Replace(" ", "");
        }

        public static void SaveImg(string base64String, string savePath)
        {

            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64String = regex.Replace(base64String, string.Empty);
            byte[] decodedBytes = Convert.FromBase64String(base64String);
            using (MemoryStream stream = new MemoryStream(decodedBytes))
            using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load<Rgba32>(stream))
            {
                image.Save(savePath, new PngEncoder());
            }
        }
        public static void changeFileName(string path, string newName)
        {
            if (File.Exists(path))
            {
                string newFilePath = Path.Combine(Path.GetDirectoryName(path), newName);
                File.Move(path, newFilePath);
            }
        }
        public static string GetFileNameFromUrl(string url)
        {
            string[] parts = url.Split('/');
            return parts[parts.Length - 1];
        }
    }
}
