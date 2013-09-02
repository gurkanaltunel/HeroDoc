using System;

namespace DocumentService.Exceptions
{
    public class FolderAlreadyExistsException:Exception
    {
        public FolderAlreadyExistsException(string folderName)
            :base(string.Format("{0} has already exist.",folderName))
        {

        }
    }
}
