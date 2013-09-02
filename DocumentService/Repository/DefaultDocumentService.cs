using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentService.Abstractions;
using DocumentService.Models;

namespace DocumentService.Repository
{
    public class DefaultDocumentService //: IDocumentService
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
    }
}
