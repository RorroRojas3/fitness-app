namespace fitness_web_api.Models.Requests
{
    public class FileByChunksRequest
    {
        public string FileSize { get; set; }

        public string FileName { get; set; }

        public string Offset { get; set; }

        public bool IsLastChunk { get; set; }

        public byte[] Bytes { get; set; }
    }
}
