using SyncGuardian.Enums;
using SyncGuardian.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SyncGuardian
{
    internal class Helper
    {
        /// <summary>
        /// Centers a given string
        /// </summary>
        public static void CenterText(string text)
        {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.WriteLine(text);
        }

        /// <summary>
        /// Clears console and executes method that respects signature of the delegate
        /// </summary>
        public static void ClearConsoleAndDoAction(Action Execute)
        {
            Console.Clear();
            Execute();
        }

        /// <summary>
        /// Clears console and executes 2 methods that respects signature of the delegate
        /// </summary>
        
        public static void ClearConsoleAndDoAction(Action Execute1, Action Execute2)
        {
            Console.Clear();
            Execute1();
            Execute2();
        }

        /// <summary>
        /// Does while loop where  execution depends from the type parameter to get an user input
        /// </summary>
        /// <returns>string that represents userInput</returns>
        public static string DoWhileUserInput(string askMessage, string validateMessage, int type, int? menuItemCounter = null)

        {
            string? input;
            int counter = 0;
            do
            {
                if (counter == 0)
                {
                    Console.WriteLine(string.Format(askMessage + menuItemCounter));
                    counter++;
                }
                else
                {
                    Console.WriteLine(string.Format(validateMessage + menuItemCounter));
                }

                Console.WriteLine();
                input = Console.ReadLine()?.Trim();
                if(input is not null) 
                    input = Helper.ProcessInput(type, input);
                else input = string.Empty;
            }
            while (!Helper.IsInputValid(input, type, menuItemCounter));

            return input;
        }

        /// <summary>
        /// part of the do while loop where userInput is processed depending on the type
        /// </summary>
        /// <returns>string that was processes by the method</returns>
        private static string ProcessInput(int type, string input)
        {
            try
            {



                if (type == (int)InputType.FolderPathInput)
                {
                    if (!input.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    {
                        input += Path.DirectorySeparatorChar;
                    }
                }
                else if (type == (int)InputType.TimeIntervalInput)
                {
                    double milliseconds = 0;
                    String[] inputArray = input.Split('-');
                    // inputArray[0] - days, inputArray[1] - hours, inputArray[2] - minutes
                    for (var index = 0; index < inputArray.Length; index++)
                    {
                        if (index == 0 && double.TryParse(inputArray[0], out double days))
                        {
                            milliseconds = +days * 24 * 60 * 60 * 1000;
                        }
                        if (index == 1 && double.TryParse(inputArray[1], out double hours))
                        {
                            milliseconds = +hours * 60 * 60 * 1000;
                        }
                        if (index == 2 && double.TryParse(inputArray[2], out double minutes))
                        {
                            milliseconds = +minutes * 60 * 1000;
                        }
                    }
                    input = milliseconds.ToString();
                }
                else if (type == (int)InputType.YesAnswer)
                {
                    return input;
                }
                else if (type == (int)InputType.IsDelete)
                {
                    if (input == GeneralResources.DELETE)
                        return input;
                    else
                        return string.Empty;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "-" + ex.InnerException + " - " + ex.StackTrace);
            };
            return input;
        }

        /// <summary>
        /// Validates userInput depending from the type. Depending on the output, the loop stops or continues
        /// </summary>
        /// <returns>true the input is valid </returns>
        public static bool IsInputValid(string input, int type , int? menuItemNumber = null)
        {
            bool isValid = false;
            if (input == GeneralResources.YES)
                return isValid;
            try
            {
                if (type == (int)InputType.TimeIntervalInput)
                {
                    if (input == "0")
                        return false;
                    string[] inputArray = input.Split("-");

                    if(string.IsNullOrWhiteSpace(input)
                        || inputArray.Count() != 3
                        || inputArray[0].Count() != 2 
                        || inputArray[1].Count() != 2
                        || inputArray[2].Count() != 2)
                    {  isValid = true; }  
                }
                else if(type == (int)InputType.FolderPathInput) {
                    isValid = !string.IsNullOrWhiteSpace(input) && (new FileInfo(input).Directory.Exists);
                }
                else if (type == (int)InputType.MenuSelectionInput)
                {
                    isValid = int.TryParse(input, out int selection)
                            || selection >= menuItemNumber
                            || selection > 0;
                }
                else if (type == (int)InputType.YesAnswer)
                {
                    isValid = input == GeneralResources.YES;
                }
                else if (type == (int)InputType.IsDelete)
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "-" + ex.InnerException + " - " + ex.StackTrace);
            }

            return isValid;
        }
    }
}   
