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
            _sessionHelper.CreateLogin(new UserContext { Id = 1 });
        }
        public ActionResult Index()
        {
            var currentFolder = _documentService.GetFolderInformation();
            var folders = _documentService.GetFolderTree();
            _sessionHelper.CurrentFolder = currentFolder;
            return View(new MainViewModel
            {
                Folders=folders,
                CurrentFolder=currentFolder
            });
        }
        [HttpPost]
        public ActionResult AddFile(int profileName, string returnUrl, HttpPostedFileBase file)
        {
            _documentService.CreateDocument(file.InputStream, profileName, file.FileName);
            return Redirect(returnUrl);
        }
        [HttpGet]
        public ActionResult AddFile()
        {
            var addFileModel = new AddFileModel
            {
                Profiles = _profileRepository.GetProfileForUser(_sessionHelper.CurrentUser.Id)
            };
            return View(addFileModel);
        }
        [HttpPost]
        public ActionResult ChangeFolder(int id)
        {
            var model = id == 0 ? _documentService.GetFolderInformation() : _documentService.GetFolderInformation(id);
            _sessionHelper.CurrentFolder = model;
            model.CurrentPath = _documentService.GetFolderPathById(id);
            return View("Folder", model);
        }
        [HttpGet]
        public ActionResult CreateFolder()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CreateFolder(string folder)
        {
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
    }
}
