namespace Scripts.File
{
    public static class File
    {
        public static string GetUrlFromLocalFile(string fn)
            => $"file://{fn}".Replace('\\', '/');
    }
}