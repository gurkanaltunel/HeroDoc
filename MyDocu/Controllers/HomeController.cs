﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentService.Abstractions;
using DocumentService.Exceptions;
using DocumentService.Models;
using MyDocu.Models;

namespace MyDocu.Controllers
{
    public class HomeController : Controller,IController
    {
        private readonly IDocumentService _documentService;
        private readonly IProfileRepository _profileRepository;
        private readonly ISessionHelper _sessionHelper;

        public HomeController(IProfileRepository profileRepository,
                               ISessionHelper sessionHelper,
                               IDocumentService documentService)
        {
            _documentService = documentService;
            _profileRepository = profileRepository;
            _sessionHelper = sessionHelper;
        }
        public JsonResult Login(string userName, string password)
        {
            int id = _documentService.GetUserId(userName, password);
            _sessionHelper.CreateLogin(new UserContext { Id = id });
            return Json(new { ok = true });
        }
        public JsonResult Index()
        {
            var currentFolder = _documentService.GetFolderInformation();
            var folders = _documentService.GetFolderTree();
            _sessionHelper.CurrentFolder = currentFolder;
            var data = new MainViewModel
            {
                Folders = folders,
                CurrentFolder = currentFolder
            };
           return Json(data, JsonRequestBehavior.AllowGet);
          // return View(data);
        }
        [HttpPost]
        public JsonResult AddFile(int profileName, HttpPostedFileBase file)
        {
            //temporary solution 
            var currentFolder = _documentService.GetFolderInformation(2);
            _sessionHelper.CurrentFolder = currentFolder;

            _documentService.CreateDocument(file.InputStream, profileName, file.FileName);
            return Json(new {ok=true});
        }
        [HttpGet]
        public JsonResult AddFile()
        {
            var addFileModel = new AddFileModel
            {
                Profiles = _profileRepository.GetProfileForUser(_sessionHelper.CurrentUser.Id)
            };
            return Json(addFileModel,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangeFolder(int id)
        {
            var model = id == 0 ? _documentService.GetFolderInformation() : _documentService.GetFolderInformation(id);
            _sessionHelper.CurrentFolder = model;
            model.CurrentPath = _documentService.GetFolderPathById(id);
            return Json(model);
        }
        [HttpGet]
        public ActionResult CreateFolder()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CreateFolder(string folder)
        {
            var currentFolder = _documentService.GetFolderInformation();
            _sessionHelper.CurrentFolder = currentFolder;
            try
            {
                _documentService.CreateFolder(folder, _sessionHelper.CurrentFolder.FolderId);
                return Json(new { ok = true, _sessionHelper.CurrentFolder.FolderId });
            }
            catch (FolderAlreadyExistsException ex)
            {
                return Json(new { ok = false, ex.Message });
            }
        }
        public ActionResult OpenFile(int id)
        {
            var model = new OpenFileViewModel
            {
                File = _documentService.GetFileById(id),
                FileVersions = _documentService.GetFileVersionAndCommentsByFileId(id)
            };
            _sessionHelper.CurrentFile = model.File;
            return View(model);
        }
        public ActionResult GetFile(int versionId)
        {
            var version = _documentService.GetFileVersionById(versionId);
            var file = _documentService.GetFileById(version.FileId);
            return File(version.File, "application/force-download", file.FileName);
        }
        [HttpPost]
        public ActionResult AddComment(AddCommentModel model)
        {
            _documentService.AddComment(model.Id, model.Value);
            return Json(new { ok = true });
        }
        public ActionResult GetComments(int id)
        {
            IList<Comment> comments = _documentService.GetCommentsByVersionId(id);
            return View(comments);
        }
    }
}
