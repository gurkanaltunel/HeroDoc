using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentService.Abstractions;

namespace MyDocuWithCommand
{
    class Program
    {
        private readonly IDocumentService _documentService;
        private readonly IProfileRepository _profileRepository;
        private readonly ISessionHelper _sessionHelper;

        public void CreateFolder(string commandText,string folderName)
        {
            if (commandText=="mkdir")
            {
                _documentService.CreateFolder(folderName, _sessionHelper.CurrentFolder.FolderId);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("enter the folder name");
            string folderName = Console.ReadLine();
            Program prg = new Program();
            prg.CreateFolder("mkdir", folderName);
        }
    }
}
