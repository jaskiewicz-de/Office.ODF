namespace Net.DevDone.Office.ODF
{
    using System.IO;
    using System.IO.Compression;

    public interface IODF
    {
        ZipArchive GetDocumentArchive(string archivePath, ZipArchiveMode mode);
        ZipArchiveEntry GetDocumentEntryByName(ZipArchive odfArchive, string entryName);
        Stream GetEntryContent(ZipArchiveEntry entry);
    }
}