using System.Text.RegularExpressions;

namespace Cloudinteractive.PassKitGenerator.Services
{
    internal static class Util
    {
        public static byte[]? FileToByteArray(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists) return null;

            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            int len = Convert.ToInt32(stream.Length);
            BinaryReader reader = new BinaryReader(stream);
            byte[] buff = reader.ReadBytes(len);
            return buff;
        }

        public static string? FileToString(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists) return null;

            return File.ReadAllText(fileInfo.FullName);
        }

        public static byte[] StreamToByteArray(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static string GeneratePassID()
        {
            Random random = new Random();
            string result = "";

            for (int i = 0; i < 4; i++)
            {
                int choice = random.Next(0, 2);
                if (choice == 0)
                {
                    result += random.Next(0, 10).ToString();
                }
                else
                {
                    char letter = (char)('A' + random.Next(0, 26));
                    result += letter;
                }
            }
            return result;
        }
    }
}
