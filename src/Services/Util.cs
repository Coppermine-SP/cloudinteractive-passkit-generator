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

            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);
            return reader.ReadString();
        }
    }
}
