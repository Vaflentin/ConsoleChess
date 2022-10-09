using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace chess
{
    public class ChessMenu
    {
        private static MenuCommand currentButtonState;
        private static MenuCommand buttonState;
        private static bool isCommandChoosen = false;
        private static bool isFirstDeletionCall = true;
        enum MenuCommand
        {
          
            Start,
            FreeMode,
            Exit

        }

        private static string[,] menu = new string[9, 17];

        private static ConsoleKey ProccesKeyEvent()
        {
            OutPutMenu();
      
            ConsoleKey button = (ConsoleKey)Char.ToUpper(Console.ReadKey().KeyChar);

           

            if ((!Regex.IsMatch(button.ToString(), "(?=.{1}$)[wWSs]")) && button != (ConsoleKey.Enter))
            {

                ChessMessages.OutPutErrorMessages(Errors.WrongKey);
                ProccesKeyEvent();

            }

            return (button);
        }

        private static void ProccesKeyPressing()
        {
      
           
            ConsoleKey pressedButton;
            buttonState = currentButtonState;


            if (isCommandChoosen == false)
            {
                pressedButton = ProccesKeyEvent();
             
                switch (pressedButton)
                {
                    case (ConsoleKey.W):
                        {
                            if (currentButtonState != (int)MenuCommand.Start)
                            {

                                currentButtonState--;

                            }
                            else
                            {
                                currentButtonState = MenuCommand.Exit;
                            }
                          

                        }
                        break;


                    case (ConsoleKey.S):
                        {
                            if ((int)currentButtonState != (int)MenuCommand.Exit && (int)currentButtonState < 3)
                            {

                                currentButtonState++;

                            }
                            else
                            {
                                currentButtonState = MenuCommand.Start;
                            }
                            

                        }
                        break;

                    case (ConsoleKey.Enter):
                        {

                            switch (currentButtonState)
                            {

                                case MenuCommand.Start:
                                    {
                                        
                                        ChessCommand.ProcessUsersCommand(UserCommands.ng);
                                    }
                                    break;
                                case MenuCommand.FreeMode:
                                    {
                                        ChessCommand.ProcessUsersCommand(UserCommands.fm);
                                    }
                                    break;
                                case MenuCommand.Exit:
                                    {

                                    }
                                    break;
                               
                            }
                     
                        }
                        break;
                    default:
                        break;
                }
       
                ProccesCommandOutPut(currentButtonState);
            }

            ProccesKeyEvent();

        }

        private static void InsertSelection(int i, int j,  int jSecond)
        {

            menu[i, j] = "|";
            menu[i, jSecond] = "|";
            
        }
        private static void DeleteSelection()
        {
            

            if (!isFirstDeletionCall)
            {
                switch (buttonState)
                {
                    case MenuCommand.Start:
                        {
                            menu[2, 5] = " ";
                            menu[2, 11] = " ";
                        }
                        break;
                    case MenuCommand.FreeMode:
                        {
                            menu[4, 3] = " ";
                            menu[4, 13] = " ";
                        }
                        break;
                    case MenuCommand.Exit:
                        {
                            menu[6, 5] = " ";
                            menu[6, 10] = " ";

                        }
                        break;
                    default:
                        break;
                }
              
            }
            isFirstDeletionCall = false;


        }

        private static void ProccesCommandOutPut(MenuCommand buttonStatus)
        {
       
           
            switch (buttonStatus)
                    {
                        case MenuCommand.Start:
                            {
                            

                                InsertSelection(2,5,11);
    

                            }

                            break;

                        case MenuCommand.FreeMode:
                            {

                            InsertSelection(4,3,13);


                            }
                            break;

                        case  MenuCommand.Exit:
                            {



                                InsertSelection(6,5,10);




                            }       
                                break;

                        default:
                            break;
                    }

            DeleteSelection();
            ProccesKeyPressing();
           
        }

        private static void MakeFrameAngles(int i, int j)
        {
            string variable = Convert.ToString(i) + j;

            switch (variable)
            {
                case "00":

                    {
                        menu[i, j] = "┏";
                    }
                    break;

                case "80":

                    {
                        menu[i, j] = "┗";

                    }
                    break;

                case "016":

                    {
                        menu[i, j] = "┓";
                    }

                    break;

                case "816":

                    {
                        menu[i, j] = "┛";
                    }

                    break;

                default:
                    break;
            }
        }

        public static string[,] CreateMenuCommands()
        {

            var countForStart = 0;
            var countForFreeMode = 0;
            var countForExit = 0;
            string[,] menuTempolar = new string[10, 18];
            string startVariable = "Start";
            string freeModeVariable = "Free Mode";
            string exitVariable = "Exit";


            for (int i = 0; i < menuTempolar.GetLength(0); i++)
            {
                for (int j = 0; j < menuTempolar.GetLength(1); j++)
                {


                    if (i == 2 && (j > 5 && countForStart < 5))
                    {
                        menuTempolar[i, j] = startVariable[countForStart].ToString();
                        countForStart++;
                    }
                    else
                    if (i == 4 && (j > 3 && j < (freeModeVariable.Length + 5) && countForFreeMode < 9))
                    {
                        menuTempolar[i, j] = freeModeVariable[countForFreeMode].ToString();
                        countForFreeMode++;
                    }
                    else

                    if (i == 6 && (j > 5 && countForExit < 4))
                    {
                        menuTempolar[i, j] = exitVariable[countForExit].ToString();
                        countForExit++;
                    }

                    else
                    {
                        menuTempolar[i, j] = " ";
                    }



                }


            }
           
            return menuTempolar;

        }

        public static void CreateMenuFrame()
        {

            var menuCommand = CreateMenuCommands();

       

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            for (int i = 0; i < menu.GetLength(0); i++)
            {
                for (int j = 0; j < menu.GetLength(1); j++)
                {

                    if ((i <= 0) || ((i == 8)))
                    {

                        menu[i, j] = "━";

                    }
                    else

                    if (((j == 0) || (j == 16)))
                    {
                        menu[i, j] = "┃";

                    }
                    else
                    {
                        if (((i >= 1) && (j >= 1)) && (i < 17) && (j < 17) && (i < 9))
                        {
                            menu[i, j] = menuCommand[i, j];
                        }


                    }
                   
                    MakeFrameAngles(i, j);

                }

            }


            ProccesCommandOutPut(MenuCommand.Start);

            ProccesKeyPressing();
        }
        public static void OutPutMenu()
        {
            Console.Clear();

            for (int i = 0; i < menu.GetLength(0); i++)
            {

                for (int j = 0; j < menu.GetLength(1); j++)
                {
                    Console.Write(menu[i, j]);
                }

                Console.WriteLine();
            }
        }


    }
}
