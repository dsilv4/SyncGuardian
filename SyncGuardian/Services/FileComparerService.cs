using SyncGuardian.Resources;

namespace SyncGuardian.Services
{

    public class FileComparer
    {
        protected FileInfo SourceFileInfo;

        protected FileInfo BackupFileInfo;

        public FileComparer(string sourceDirectory, string backupDirectory)
        {
            SourceFileInfo = new FileInfo(sourceDirectory);
            BackupFileInfo = new FileInfo(backupDirectory);
        }

        /// <summary>
        /// Verifies if FileInfo not null and compares the two given files in both length and path
        /// </summary>
        /// <returns>true if the files are the same</returns>
        public virtual bool Compare()
        { 
            return  (!IsDifferentLengthandExists() && IsBackupFileExistantFilesExistent());
        }

        /// <summary>
        /// Compares the two files Length
        /// </summary>
        /// <returns>true if files have different length</returns>
        private bool IsDifferentLengthandExists()
        {
            if (BackupFileInfo.Exists)
                return SourceFileInfo.Length == BackupFileInfo.Length;
            return false;
        }

        /// <summary>
        /// Confirms the existence of both files
        /// </summary>
        private bool IsBackupFileExistantFilesExistent()
        {
            try
            {
                if (!SourceFileInfo.Exists)
                {
                    throw new ArgumentNullException(nameof(SourceFileInfo), ErrorMsg.SOURCE_PATH_ERROR);
                }
                if (!BackupFileInfo.Exists)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // method to write to logfile  the error message
                // method for asking the user for the correct path based on the 
                return false;
            }

        }

    }
}