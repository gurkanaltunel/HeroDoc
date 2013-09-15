﻿using SharpShell.Interop;
using System.IO;

namespace FileWatcher
{
    internal class FolderSyncInformationBuilder:SyncInformationBuilder
    {
        public FolderSyncInformationBuilder(string path,FILE_ATTRIBUTE attributes)
            :base(path,attributes)
        {

        }
        public override SyncInformation BuildInformation(string path, FILE_ATTRIBUTE attributes)
        {
            return new FolderSyncInformation
            {
                Name = Path.GetDirectoryName(path)
            };
        }
    }

    internal class FolderSyncInformation : SyncInformation
    {

    }
}
