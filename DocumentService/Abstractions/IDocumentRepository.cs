using System.Collections.Generic;
using System.IO;
using DocumentService.Models;
using File = DocumentService.Models.File;

namespace DocumentService.Abstractions
{
    public interface IDocumentRepository
    {
        IList<Folder> LoadRootFolders();
        IList<Folder> GetFoldersByParentId(int id);
        IList<File> GetFilesInFolder(int id);
        IList<FileVersion> GetFileVersionsByFileId(int id);
        IList<Comment> GetCommentsByFileId(int id);
        IList<Comment> GetCommentsByVersionId(int versionId);
        Folder GetFolderById(int id);
        Folder InsertNewFolder(string folder, int parentFolderId, int ownerId);
        Folder GetFolderByName(string folderName, int parentFolderId);
        File GetFileByNameAndFolder(string fileName, int folderId);
        File GetFileById(int id); 
        FileVersion GetFileVersionById(int versionId);
        User GetUserById(int ownerId);
        Comment SaveComment(Comment newComment);
        void CreateNewVersionOfFile(File file, Stream inputStream);
        void CreateNewFile(File file, Stream inputStream);
        int GetUserId(string username, string password);
        
    }
}
