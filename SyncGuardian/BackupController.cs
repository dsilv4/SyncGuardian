using SyncGuardian.Resources;
using SyncGuardian.Services;
using System.IO;
using Timer = System.Timers.Timer;
namespace SyncGuardian
{
    public class BackupController
    {
        private static FileInfo? SourceFileInfo { get; set; }
        private static FileInfo? BackupFileInfo { get; set; }
        public Timer BackupTimer { get; set; }
        public bool IsBackupRoutineRunning {  get; set; }
        private bool IsToDeleteFiles { get; set; }

        public BackupController(string sourceDirectory, string backupDirectory, double timeInterval, bool isToDeleteFiles)
        {
            SourceFileInfo = new FileInfo(sourceDirectory is null ? string.Empty : sourceDirectory);
            BackupFileInfo = new FileInfo(backupDirectory is null ? string.Empty : backupDirectory);
            IsToDeleteFiles = isToDeleteFiles;
            IsBackupRoutineRunning = false;
            BackupTimer = new Timer();
            BackupTimer.Interval = timeInterval;
            BackupTimer.Elapsed += BackupRoutine;

            LogService.LogAction((string.Format("Timer initialized with a {0}s interval", timeInterval / 1000)), backupDirectory);
            IsToDeleteFiles = isToDeleteFiles;
        }

        public  void StartBackup()
        {
           BackupTimer.Start();
            LogService.LogAction(string.Format("Timer Started", BackupTimer.Interval / 1000), BackupFileInfo.FullName);
        }

        public void BackupRoutine(object? sender, System.Timers.ElapsedEventArgs e)
        {
            IsBackupRoutineRunning = true;
            BackupFiles();

            if (IsToDeleteFiles)
            {
                DeleteFilesOnlyOnBackup();
                DeleteEmptyDirectories();
            }
            IsBackupRoutineRunning = false;
            LogService.LogAction(string.Format("Backup finished sucessfuly"), BackupFileInfo.FullName);
            Console.WriteLine();
        }

        private string[] GetArrayofFilesPaths(string mainDirectory, SearchOption option) 
        {
            return Directory.GetFiles(mainDirectory, "*", option);
        } 


        private void BackupFiles()
        {
            

            try
            {
               
                LogService.LogAction(string.Format("Timer elapsed. Executing the Backup."), BackupFileInfo.FullName);
                LogService.LogAction(string.Format("Copying files"), BackupFileInfo.FullName);


                string[] files = GetArrayofFilesPaths(SourceFileInfo.FullName, SearchOption.AllDirectories);

                foreach (string filePath in files)
                {
                    if(filePath.EndsWith("_LogFile.txt"))
                        continue;

                    string relativePath = filePath.Substring(SourceFileInfo.FullName.Length);

                    string backUpFilePath = Path.Combine(BackupFileInfo.FullName, relativePath);
                    FileInfo backUpFilePathInfo = new FileInfo(backUpFilePath);

                    if (backUpFilePathInfo.Directory is null)
                    {
                        throw new Exception(string.Format(ErrorMsg.IS_NULL, backUpFilePathInfo, "BackupFiles"));
                    }

                    // Create the directory structure if it doesn't exist
                    if (backUpFilePathInfo.DirectoryName is not null && !backUpFilePathInfo.Directory.Exists)
                        Directory.CreateDirectory(backUpFilePathInfo.DirectoryName);

                    FileComparer fileComparer = new FileComparer(filePath, backUpFilePathInfo.FullName);
                    FileHashComparer fileHashComparer = new FileHashComparer(filePath, backUpFilePathInfo.FullName);

                    bool isFileAttributesEqual = fileComparer.Compare();

                    bool isFileHashEqual = true;
                    if (isFileAttributesEqual)
                    {
                        isFileHashEqual = fileHashComparer.Compare();
                    }
                    if ((!isFileHashEqual || !isFileAttributesEqual)
                        && backUpFilePath != null && backUpFilePath != string.Empty)
                    {
                        // Copy the file to the destination directory
                        File.Copy(filePath, backUpFilePath, true);
                        LogService.LogAction(string.Format("File Copied Sucessfully - {0}", filePath), BackupFileInfo.FullName);
                    }
                    else 
                    {
                        LogService.LogAction(string.Format("File has no changes - {0}", filePath), BackupFileInfo.FullName);
                    }
                    

                }

            }
            catch (Exception ex)
            {
                LogService.LogAction(string.Format("{0} - {1} - {2}", ex.Message, ex.InnerException, ex.StackTrace), BackupFileInfo.FullName);
            }
        }

        private void DeleteFilesOnlyOnBackup()
        {
            try
            {

                LogService.LogAction(string.Format("Deleting files only on backup folder"), BackupFileInfo.FullName);


                string[] files = Directory.GetFiles(BackupFileInfo.FullName, "*", SearchOption.AllDirectories);

                foreach (string filePath in files)
                {
                    // Calculate the destination file path
                    string relativePath = filePath.Substring(BackupFileInfo.FullName.Length);
                    string sourceFilePath = Path.Combine(SourceFileInfo.FullName, relativePath);
                    FileInfo sourceFilePathInfo = new FileInfo(sourceFilePath);
                    if (!sourceFilePathInfo.Exists && (new FileInfo(filePath).Exists) && !(new FileInfo(filePath).Name.StartsWith("_Log")))
                    {
                        File.Delete(filePath);

                        LogService.LogAction(string.Format("File deleted - {0}", filePath), BackupFileInfo.FullName);

                    }

                }
                LogService.LogAction(string.Format("All files non existent on the original folder deleted"), BackupFileInfo.FullName);
            }
            catch (Exception ex)
            {

                LogService.LogAction(string.Format("{0} - {1} - {2}", ex.Message, ex.InnerException, ex.StackTrace), BackupFileInfo.FullName);
            }
        }

        private void DeleteEmptyDirectories()
        {
            try
            {


                LogService.LogAction(string.Format("Deleting empty directories"), BackupFileInfo.FullName);

                List<string> directories = (Directory.GetDirectories(BackupFileInfo.FullName, "*", SearchOption.AllDirectories)).ToList();
                List<string> sortedDirectories = directories.OrderByDescending(x => x.Split("\\").Length).ToList();
                foreach (string directory in sortedDirectories)
                {
                    if (Directory.GetFiles(directory, "*", SearchOption.TopDirectoryOnly).Count() == 0
                        && Directory.GetDirectories(directory, "*", SearchOption.TopDirectoryOnly).Count() == 0)
                    {
                        Directory.Delete(directory);
                    }

                }
                LogService.LogAction(string.Format("Empy diretories deleted"), BackupFileInfo.FullName);
            }
            catch (Exception ex)
            {

                LogService.LogAction(string.Format("{0} - {1} - {2}", ex.Message, ex.InnerException, ex.StackTrace), BackupFileInfo.FullName);
            }
        }
    }
}
