using DocumentService.Abstractions;
using DocumentService.Models;
using Moq;
using MyDocu.Controllers;
using MyDocu.Models;

namespace MyDocu.Tests.Entities
{
    public class TestableHomeController : HomeController
    {
        public readonly Mock<IProfileRepository> ProfileRepo;
        public readonly Mock<IDocumentService> DocumentService;
        public readonly Mock<ISessionHelper> SessionHelper;

        TestableHomeController(Mock<IProfileRepository> profileRepo, Mock<IDocumentService> documentService, Mock<ISessionHelper> sessionHelper)
            : base(profileRepo.Object,sessionHelper.Object,documentService.Object)
        {
            ProfileRepo = profileRepo;
            DocumentService = documentService;
            SessionHelper = sessionHelper;
        }
        public static TestableHomeController Create(UserContext user = null)
        {
            var sessionHelper = new Mock<ISessionHelper>();
            if (user!=null)
            {
                sessionHelper.Setup(helper => helper.CurrentUser).Returns(user);
            }
            var profileRepo = new Mock<IProfileRepository>();
            var documentService = new Mock<IDocumentService>();
            return new TestableHomeController(profileRepo, documentService, sessionHelper);
        }
    }
}
