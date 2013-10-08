using System.Collections.Generic;
using System.IO;
using DocumentService.Models;
using File = DocumentService.Models.File;

namespace DocumentService.Abstractions
{
    public interface IDocumentService
    {
        FolderInformation GetFolderInformation(int id);
        FolderInformation GetFolderInformation();
        IList<Folder> GetFolderTree(int id = 0);
        void CreateDocument(Stream inputStream, int profileId, string fileName);
        string GetFolderPathById(int id);
        Folder CreateFolder(string folder, int parentFolderId);
        File GetFileById(int id);
        IList<FileVersion> GetFileVersionAndCommentsByFileId(int id);
        FileVersion GetFileVersionById(int id);
        void AddComment(int versionId, string comment);
        IList<Comment> GetCommentsByVersionId(int versionId);
        int GetUserId(string username, string password);
    }
}
