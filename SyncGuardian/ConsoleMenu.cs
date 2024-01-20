using SyncGuardian.Enums;
using SyncGuardian.Resources;

namespace SyncGuardian
{

    public class ConsoleMenu
    {
        private List<MenuItem> MainMenuList = [];

        public ConsoleMenu()
        {
            PopulateMenuItems();
        }

        /// <summary>
        /// Starts application by showing the title, subtitle, and Menu
        /// </summary>
        public void Initialize()
        {
            Helper.ClearConsoleAndDoAction(ShowTitle);
            GoToMainMenu();
        }

        /// <summary>
        /// Populates the property that contains menu entries
        /// </summary>
        private void PopulateMenuItems()
        {
            MainMenuList = new List<MenuItem>
            {
                new MenuItem(MenuItemDescriptions.START, ActionMethods.StartMenu, 1),
                new MenuItem(MenuItemDescriptions.INSTRUCTIONS, ActionMethods.InstructionsMenu, 2),
                new MenuItem(MenuItemDescriptions.ABOUT, ActionMethods.AboutMenu, 3),
                new MenuItem(MenuItemDescriptions.QUIT, ActionMethods.QuitApp, 4)
            };

        }

        /// <summary>
        /// Calls the function responsible to show the title and the main menu
        /// </summary>
        private void GoToMainMenu()
        {
            Helper.ClearConsoleAndDoAction(ShowTitle, ShowMainMenu);
        }

        /// <summary>
        /// Show Application title on the console
        /// </summary>
        public static void ShowTitle()
        {
            Console.WriteLine(new string('=', Console.WindowWidth));
            Helper.CenterText(GeneralResources.APP_NAME);
            Console.WriteLine(new string('=', Console.WindowWidth));
            Helper.CenterText(GeneralResources.APP_SMALL_DESCRIPTION);
            Console.WriteLine();
        }

        /// <summary>
        /// Shows main Menu
        /// </summary>
        private void ShowMainMenu()
        {
            MainMenu();
            Console.WriteLine();
            ExecuteUserMenuSelection();
        }

        /// <summary>
        /// Fill the screen with all menu entries
        /// </summary>
        private void MainMenu()
        {
            foreach (MenuItem item in MainMenuList)
            {
                Console.WriteLine($" {item.Position}. {item.Description}");
            }
        }

        /// <summary>
        /// Asks user to select a menu and execute actions associated to it
        /// </summary>
        private void ExecuteUserMenuSelection()
        {
            int selection = 0;

            string input = Helper.DoWhileUserInput(GeneralResources.ENTER_CHOICE, GeneralResources.WRONG_ENTRY, (int)InputType.MenuSelectionInput, MainMenuList.Count);

            if (input == GeneralResources.YES)
            {
                ConsoleMenu.BackToMenu();
            }

                if (int.TryParse(input, out selection))
                {
                    MenuItem selectedMenuItem = MainMenuList
                                   .Where(x => x.Position == selection)
                                   .First();

                    Helper.ClearConsoleAndDoAction(selectedMenuItem.Execute);
                }
                else { ExecuteUserMenuSelection(); };
            
        }

        /// <summary>
        /// Reinitializes the Menu 
        /// </summary>
        /// <returns>true if the hashes are the same</returns>
        public static void BackToMenu() 
        {
            ConsoleMenu consoleMenu = new ConsoleMenu();
            consoleMenu.Initialize();
        }
            

    }
}
