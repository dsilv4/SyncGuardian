using System.IO;
using System.Security.Cryptography;

namespace SyncGuardian.Services
{
    public class FileHashComparer : FileComparer
    {

        public FileHashComparer(string sourceDirectory, string backupDirectory) : base(sourceDirectory, backupDirectory) { }

        /// <summary>
        /// Computes the hash value for two given Stream objects and compares them and returns true if they are the same
        /// </summary>
        /// <returns>true if the hashes are the same</returns>
        public override bool Compare()
        {
            MD5 md5Tool = MD5.Create();

            Stream sourceStream = SourceFileInfo.OpenRead();
            byte[] sourceHash = md5Tool.ComputeHash(sourceStream);

            Stream backupStream = BackupFileInfo.OpenRead();
            byte[] backupHash = md5Tool.ComputeHash(backupStream);

            for (var index = 0; index < sourceHash.Length; index++)
            {
                if (sourceHash[index] != backupHash[index])
                {
                    return false;
                }
            }

            sourceStream.Close();
            backupStream.Close();
            return true;
        }
    }
}


