using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyncGuardian.Enums;
using SyncGuardian.Resources;
using SyncGuardian.Services;

namespace SyncGuardian
{
    public class ActionMethods
    {
        /// <summary>
        /// starts the process to star the files backup
        /// </summary>
        public static void StartMenu()
        {
            Helper.ClearConsoleAndDoAction(ConsoleMenu.ShowTitle);
            Console.WriteLine(GeneralResources.GO_TO_MENU_START_BACKUP);
            Console.WriteLine();
            Console.WriteLine();

            string sourcePath;
            string backupPath;
            double timeInterval;
            bool isToDeleteFiles = false;

            sourcePath = Helper.DoWhileUserInput(GeneralResources.ASK_SOURCE_DIRECTORY, GeneralResources.ASK_DIRECTORY_ERROR, (int)InputType.FolderPathInput);
            if (sourcePath.TrimEnd('\\') == GeneralResources.YES ) 
            {
                ConsoleMenu.BackToMenu();
                return;
            }

            Console.WriteLine();
            backupPath = Helper.DoWhileUserInput(GeneralResources.ASK_BACKUP_DIRECTORY, GeneralResources.ASK_DIRECTORY_ERROR, (int)InputType.FolderPathInput);
            if (backupPath.TrimEnd('\\') == GeneralResources.YES)
            {
                ConsoleMenu.BackToMenu();
                return;
            }

            Console.WriteLine();
            timeInterval = Convert.ToDouble(Helper.DoWhileUserInput(GeneralResources.ASK_TIME_INTERVAL, GeneralResources.ASK_TIME_INTERVAL_ERROR, (int)InputType.TimeIntervalInput));
            if (timeInterval == 0)
            {
                ConsoleMenu.BackToMenu();
                return;
            }

            Helper.ClearConsoleAndDoAction(ConsoleMenu.ShowTitle);
            Console.WriteLine();
            string isToDeleteFilesString = Helper.DoWhileUserInput(GeneralResources.ASK_IS_DELETE, GeneralResources.ASK_IS_DELETE_ERROR, (int)InputType.IsDelete);
     
            isToDeleteFiles = isToDeleteFilesString == GeneralResources.DELETE;

            ; LogService.LogAction( string.Format("{0}", isToDeleteFiles == false
                                                            ? "Backup with no File/Folder delete"
                                                            : "ATENTION: After copying files, all files and folder that are not on the original path will be deleted"
                                                 ), backupPath );

            Helper.ClearConsoleAndDoAction(ConsoleMenu.ShowTitle);

            BackupController backupController = new BackupController(sourcePath, backupPath, timeInterval, isToDeleteFiles);
            backupController.StartBackup();
            LogService.LogActionSpacer(backupPath);
            Console.WriteLine(GeneralResources.GO_TO_MENU_DISPOSE);
            bool isToStopWhile = false;
            while (!isToStopWhile)
            {
                Thread.Sleep(100);
                if (backupController.IsBackupRoutineRunning)
                {
                    isToStopWhile = false;
                }
                else
                {
                    isToStopWhile = true;
                    Console.ReadLine();
                    ConsoleMenu.BackToMenu();
                }
            }
            //if (backupController.IsBackupRoutineRunning)
            //{
            //    string? pressedKey = Console.ReadKey().ToString();
            //    while (pressedKey is not null) { }
            //}
            //else
            //{
            //    Console.WriteLine(GeneralResources.GO_TO_MENU_DISPOSE);
            //    Console.ReadLine();
            //}
            Console.ReadLine();
            
        }

        /// <summary>
        /// Opens the Instructions Menu
        /// </summary>
        public static void InstructionsMenu() 
        {
            Helper.ClearConsoleAndDoAction(ConsoleMenu.ShowTitle);
            Console.WriteLine();
            Console.WriteLine(GeneralResources.INSTRUCTIONS);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(GeneralResources.GO_TO_MENU);
            Console.ReadLine();
            ConsoleMenu.BackToMenu();

        }
        /// <summary>
        /// Opens the About Menu
        /// </summary>
        public static void AboutMenu()
        {
            Helper.ClearConsoleAndDoAction(ConsoleMenu.ShowTitle);
            Console.WriteLine();
            Console.WriteLine(GeneralResources.ABOUT);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(GeneralResources.GO_TO_MENU);
            Console.ReadLine();

            ConsoleMenu.BackToMenu();
        }

        /// <summary>
        /// Ends the app
        /// </summary>
        public static void QuitApp() 
        {
            Environment.Exit(0);
        }

        
    }
}
