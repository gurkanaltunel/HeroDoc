using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using DocumentService.Abstractions;
using DocumentService.Interceptors;
using DocumentService.Models;
using ServiceStack.OrmLite;
using File = DocumentService.Models.File;

namespace DocumentService.Repository
{
    public class SqlDocumentRepository:IDocumentRepository,IRepository
    {
        [Cache]
        public virtual IList<Folder> LoadRootFolders()
        {
            using (var db = DbHelper.CreateConnection())
            {
                return db.Select<Folder>(folder => folder.ParentFolder == null).OrderBy(folder => folder.Name).ToList();
            }
        }
        [RequestCache]
        public virtual IList<Folder> GetFoldersByParentId(int id)
        {
            using (var db = DbHelper.CreateConnection())
            {
                return db.Select<Folder>(folder => folder.ParentFolder == id).OrderBy(folder => folder.Name).ToList();
            }
        }
        [RequestCache]
        public virtual IList<File> GetFilesInFolder(int id)
        {
            using (var db = DbHelper.CreateConnection())
            {
                return db.Select<File>(file => file.FolderId == id).OrderBy(folder => folder.FileName).ToList();
            }
        }
        [Cache]
        public virtual IList<FileVersion> GetFileVersionsByFileId(int id)
        {
            using (var db = DbHelper.CreateConnection())
            {
                return db.Select<FileVersion>(version => version.FileId == id);
            }
        }
        [RequestCache]
        public virtual IList<Comment> GetCommentsByFileId(int id)
        {
            using (var db = DbHelper.CreateConnection())
            {
                return db.Select<Comment>(comment => comment.FileId == id);
            }
        }
        [RequestCache]
        public IList<Comment> GetCommentsByVersionId(int versionId)
        {
            using (var db = DbHelper.CreateConnection())
            {
                return db.Select<Comment>(comment => comment.FileVersionId == versionId);
            }
        }
        [RequestCache]
        public virtual Folder GetFolderById(int id)
        {
            using (var db = DbHelper.CreateConnection())
            {
                return db.Select<Folder>(folder => folder.Id == id).FirstOrDefault();
            }
        }

        public Folder InsertNewFolder(string folder, int parentFolderId, int ownerId)
        {
            using (var db = DbHelper.CreateConnection())
            {
                var objs = new Folder
                {
                    CreateDate = DateTime.Now,
                    ParentFolder = parentFolderId == 0 ? null : new int?(parentFolderId),
                    Name = folder,
                    Owner = ownerId
                };
                db.Insert(objs);
                objs.Id = (int)db.GetLastInsertId();
                return objs;
            }
        }
        [RequestCache]
        public virtual Folder GetFolderByName(string folderName, int parentFolderId)
        {
            using (var db = DbHelper.CreateConnection())
            {
                return db.Select<Folder>(folder => folder.Name == folderName && folder.ParentFolder == parentFolderId).FirstOrDefault();
            }
        }

        public File GetFileByNameAndFolder(string fileName, int folderId)
        {
            using (var db = DbHelper.CreateConnection())
            {
                return db.Select<File>(file => file.FileName == fileName && file.FolderId == folderId).FirstOrDefault();
            }
        }
        [Cache]
        public virtual File GetFileById(int id)
        {
            using (var db = DbHelper.CreateConnection())
            {
                return db.Select<File>(file => file.Id == id).FirstOrDefault();
            }
        }
        [RequestCache]
        public virtual FileVersion GetFileVersionById(int versionId)
        {
            using (var db = DbHelper.CreateConnection())
            {
                return db.Select<FileVersion>(version => version.Id == versionId).FirstOrDefault();
            }
        }
        [Cache]
        public virtual User GetUserById(int ownerId)
        {
            using (var db = DbHelper.CreateConnection())
            {
                return db.Select<User>(user => user.Id == ownerId).FirstOrDefault();
            }
        }

        public Comment SaveComment(Comment newComment)
        {
            using (var db = DbHelper.CreateConnection())
            {
                db.Insert(newComment);
                newComment.Id = (int)db.GetLastInsertId();
                return newComment;
            }
        }

        public void CreateNewVersionOfFile(File file, Stream inputStream)
        {
            using (var db = DbHelper.CreateConnection())
            {
                var lastVersion = db.Select<FileVersion>(version => version.FileId == file.Id)
                    .OrderByDescending(version => version.Id).FirstOrDefault();
                CreateNewVersion(lastVersion == null ? 1 : lastVersion.VersionNumber + 1, file, inputStream, db);
            }
        }
        private void CreateNewVersion(int versionNumber,File file,Stream inputStream,IDbConnection db)
        {
            byte[] binaryFile;
            using (var stream = new MemoryStream())
            {
                inputStream.CopyTo(stream);
                binaryFile = stream.ToArray();
            }
            var version = new FileVersion
            {
                VersionNumber = versionNumber,
                File = binaryFile,
                FileId = file.Id
            };
            db.Insert(version);
        }
        public void CreateNewFile(File file, Stream inputStream)
        {
            using (var db = DbHelper.CreateConnection())
            {
                var transaction = db.BeginTransaction();
                try
                {
                    db.Insert(file);
                    file.Id = (int)db.GetLastInsertId();
                    CreateNewVersion(1, file, inputStream, db);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public int GetUserId(string userName, string password)
        {
            using (var db = DbHelper.CreateConnection())
            {
               var obj= db.Select<User>(user =>  user.Username== userName&&user.Password==password).FirstOrDefault();
               return obj.Id;
            }
        }
    }
}
