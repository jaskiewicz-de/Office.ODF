namespace Net.DevDone.Office.ODF
{
    using System.IO;
    using System.IO.Compression;

    public class ODF
    {
        public ZipArchive GetDocumentArchive(string archivePath, ZipArchiveMode mode)
        {
            return ZipFile.Open(archivePath, mode);
        }

        public ZipArchiveEntry GetDocumentEntryByName(ZipArchive odfArchive, string entryName)
        {
            foreach (ZipArchiveEntry entry in odfArchive.Entries)
            {
                if (entry.Name == entryName)
                {
                    return entry;
                }
            }

            return default(ZipArchiveEntry);
        }

        public Stream GetEntryContent(ZipArchiveEntry entry)
        {
            return entry.Open();
        }
    }
}