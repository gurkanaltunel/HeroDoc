using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentService.Abstractions;
using DocumentService.Models;
using DocumentService.Repository;
using File = DocumentService.Models.File;
using ServiceStack.OrmLite;
using DocumentService.Exceptions;

namespace DocumentService
{
    public class DefaultDocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ISessionHelper _sessionHelper;
        public DefaultDocumentService(IDocumentRepository documentRepository, ISessionHelper sessionHelper)
        {
            _documentRepository = documentRepository;
            _sessionHelper = sessionHelper;
        }
        public FolderInformation GetFolderInformation(int id)
        {
            var folders = _documentRepository.GetFoldersByParentId(id);
            var folder = _documentRepository.GetFolderById(id);
            var files = _documentRepository.GetFilesInFolder(id);
            return new FolderInformation
            {
                FolderId = id,
                ParentFolder = folder.ParentFolder == null ? 0 : folder.ParentFolder.Value,
                FolderName = folder.Name,
                Folders = folders,
                Files = files
            };
        }
        public FolderInformation GetFolderInformation()
        {
            var folders = _documentRepository.LoadRootFolders();
            return new FolderInformation
            {
                Folders = folders,
                FolderName = "$",
                Files = new List<File>()
            };
        }
        public IList<Folder> GetFolderTree(int id = 0)
        {
            return id == 0 ? _documentRepository.LoadRootFolders() : _documentRepository.GetFoldersByParentId(id);
        }
        public void CreateDocument(Stream inputStream, int profileName, string fileName)
        {
            File file = _documentRepository.GetFileByNameAndFolder(fileName, _sessionHelper.CurrentFolder.FolderId);
            if (file != null)
            {
                _documentRepository.CreateNewVersionOfFile(file, inputStream);
            }
            else
            {
                _documentRepository.CreateNewFile(new File
                {
                    FolderId = _sessionHelper.CurrentFolder.FolderId,
                    FileName = fileName,
                    CreateDate = DateTime.Now,
                    Owner = _sessionHelper.CurrentUser.Id
                }, inputStream);
            }
        }
        public string GetFolderPathById(int id)
        {
            List<Folder> folders;
            using (var db = DbHelper.CreateConnection())
            {
                folders = db.Select<Folder>();
            }
            return GetFolderPath(folders, id);
        }
        public Folder CreateFolder(string folderName, int parenFolderId)
        {
            var folder = _documentRepository.GetFolderByName(folderName, parenFolderId);
            if (folder == null)
            {
                return _documentRepository.InsertNewFolder(folderName, parenFolderId, _sessionHelper.CurrentUser.Id);
            }
            throw new FolderAlreadyExistsException(folderName);
        }
        public File GetFileById(int id)
        {
            var file = _documentRepository.GetFileById(id);
            if (file == null)
            {
                throw new FileNotFoundException(string.Format("File is with the id '{0}' not found.", id));
            }
            return file;
        }
        public IList<FileVersion> GetFileVersionAndCommentsByFileId(int id)
        {
            var comments = _documentRepository.GetCommentsByVersionId(id).OrderBy(comment => comment.FileVersionId);
            var fileVersions = _documentRepository.GetFileVersionsByFileId(id);
            foreach (var fileVersion in fileVersions)
            {
                fileVersion.Comments = comments.Where(comment => comment.FileVersionId == fileVersion.Id).ToList();
            }
            foreach (var comment in comments)
            {
                comment.OwnerUser = _documentRepository.GetUserById(comment.OwnerId);
            }
            return fileVersions;
        }
        public FileVersion GetFileVersionById(int versionId)
        {
            var version = _documentRepository.GetFileVersionById(versionId);
            if (version == null)
            {
                throw new FileNotFoundException(string.Format("File with the version id '{0}' not found.", versionId));
            }
            return version;
        }
        public void AddComment(int versionId, string comment)
        {
            var newComment = new Comment
            {
                FileVersionId=versionId,
                OwnerId=_sessionHelper.CurrentUser.Id,
                FileId=_sessionHelper.CurrentFile.Id,
                CommentDate=DateTime.Now,
                Text=comment
            };
            _documentRepository.SaveComment(newComment);
        }
        public IList<Comment> GetCommentsByVersionId(int versionId)
        {
            var comments = _documentRepository.GetCommentsByVersionId(versionId);
            foreach (var comment in comments)
            {
                comment.OwnerUser = _documentRepository.GetUserById(comment.OwnerId);
            }
            return comments;
        }
        private static string GetFolderPath(IEnumerable<Folder> folders, int id, string currentPath = "")
        {
            var enumerable = folders as Folder[] ?? folders.ToArray();
            var folder = enumerable.FirstOrDefault(folder1 => folder1.Id == id);
            if (folder == null)
            {
                return currentPath;
            }
            currentPath = string.Format("{0}/{1}", folder.Name, currentPath);
            return folder.ParentFolder == null
                ? currentPath
                : GetFolderPath(enumerable, folder.ParentFolder.Value, currentPath);
        }
    }
}
