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

        static void Main(string[] args)
        {
            Target target = new Adapter();
            target.Request();
        }
    }
}
