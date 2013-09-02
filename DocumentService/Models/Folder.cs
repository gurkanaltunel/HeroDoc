using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace DocumentService.Models
{
   public class User
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Isactive { get; set; }
    }
   public class Folder
   {
       [AutoIncrement]
       public int Id { get; set; }
       public string Name { get; set; }
       public DateTime CreateDate { get; set; }
       [References(typeof(Folder))]
       public int? ParentFolder { get; set; }
       [References(typeof(User))]
       public int? Owner { get; set; }
   }
   public class File
   {
       [AutoIncrement]
       public int Id { get; set; }
       [References(typeof(Folder))]
       public int FolderId { get; set; }
       public string FileName { get; set; }
       [References(typeof(User))]
       public int Owner { get; set; }
       public DateTime CreateDate { get; set; }
   }
   public class FileVersion
   {
       private IList<Comment> comments;

       [AutoIncrement]
       public int Id { get; set; }
       public int VersionNumber { get; set; }
       public byte[] File { get; set; }
       [References(typeof(File))]
       public int FileId { get; set; }
       [Ignore]
       public IList<Comment> Comments
       {
           get { return comments = comments ?? new List<Comment>(); }
           set { comments = value; } 
       }
   }
}
