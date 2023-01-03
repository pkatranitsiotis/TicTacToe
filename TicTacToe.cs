using System;
using System.Dynamic;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using System.Xml;

namespace TicTacToe
{
    internal class Program
    {
        public static string[,] GameInit()
        {
            //initialize the board
            string[,] board = new string[3, 3]
            {
                {"1", "2", "3"},
                {"4","5", "6"},
                {"7","8","9"}
            };
            return board;
        }
        

        

        static void BoardVisualization(string[,] boardArray)
        {
            Console.Clear();
            for (int i = 0; i < boardArray.GetLength(0); i++)
            {
                for (int j = 0; j < boardArray.GetLength(1); j++)
                {
                    //Console.Write(" | ");
                    if (i == 0 && j == 0)
                        Console.WriteLine("     |     | ");
                    if (j == boardArray.GetLength(1) - 1)
                        Console.Write("  "+boardArray[i, j]);
                    else
                        Console.Write("  "+boardArray[i, j] + "  |");
                }
                if (i != boardArray.GetLength(0) - 1)
                    Console.Write("\n_____|_____|_____ \n     |     | \n");
                else
                    Console.WriteLine("\n     |     | \n");
            }
        }

        static void Gameplay(int player, string[,] board)
        {
            if (player % 2 == 0)
                player = 2;
            else
                player = 1;

            Console.WriteLine("Player {0} Choose a field:",player.ToString());
            bool correctInput = false;
            bool correctInputNum=false;
            //bool availableField = true;
            string field = Console.ReadLine();

           
            //chech correct format input
            do
            {
                
                try
                {
                    int fieldInput = int.Parse(field);
                    correctInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Wrong Format. Please enter a number");
                    field = Console.ReadLine();
                }
                finally
                {
                    correctInputNum = CheckInput(field, board);
                    /*availableField = CheckOccupation(board);
                    if (!availableField)
                    {
                        Console.WriteLine("Problem");
                    }
*/
                    if (correctInputNum == false)
                    {
                        Console.WriteLine("This field is occupied. Try another one.");
                        field = Console.ReadLine();
                    }
                }
            } while (correctInput == false || correctInputNum==false);
            

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == field)
                    {
                        if (player==1)
                            board[i, j] = "X";
                        else
                            board[i, j] = "O";
                    }
                }
            }

            BoardVisualization(board);
        }


        static bool CheckInput(string input, string[,] board)
        {
            //checks if the field that user entered is available
            for (int i=0; i<board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (input == board[i, j])
                        return true;
                }
            }
            return false;
            
        }


        static bool Checker(string[,] board)
        {
            //horizontal and vertical checks
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return true;
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return true;
            }
            //diagonal checks 
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return true;
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                return true;
            return false;
            
        }

        static void CheckWinner(int player, bool win)
        {
            //check who player won
            if (!win)
                return;
            if (player % 2 == 1)
            {
                Console.WriteLine("------- Player 2 won!!! ----------");
            }
            else
            {
                Console.WriteLine("------- Player 1 won!!! ----------");
            }

        }

       public static bool CheckOccupation(string[,] board)
        {
            //check if there are available fields in board is full
            //return false if the board is full
            string[] tempBoard = new string[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            
            for (int value=0; value<tempBoard.Length; value++)
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[i, j] == tempBoard[value])
                        {
                            return true;
                        }
                        else
                        {
                            ;
                        }
                    }
                }
            }
            return false;
            
        }


        static void Main(string[] args)
        {
            bool gameReseter = false;

            
            do
            {
                string[,] board = GameInit();
                BoardVisualization(board);

                int i = 1;
                int player;

                while (!Checker(board))
                {
                    /*Gameplay(1);
                    Console.WriteLine("The winner is player 1");
                    if (!Checker(board))
                    {
                        Gameplay(2);
                        Console.WriteLine("The winner is player 2");
                    }
                    */

                    /*if (!CheckOccupation(board))
                    {
                        Console.WriteLine("No fields available");
                        break;
                    }*/

                    Gameplay(i,board);
                    i++;

                    /*if (!Checker(board))
                    {
                        Gameplay(i,board);
                        i++;
                    }*/

                    if (!Checker(board) && CheckOccupation(board))
                    {
                        
                        Gameplay(i,board);
                        i++;
                        
                    }

                    //Console.WriteLine(Checker(board));
                    if (Checker(board))
                    {

                        if (i % 2 == 0)
                            player = 2;
                        else
                            player = 1;

                        
                        CheckWinner(player, Checker(board));

                    }
                    else if (!CheckOccupation(board) && !Checker(board))
                    {
                        //Console.WriteLine("Board is full");
                        Console.WriteLine("Draw!");
                        break;
                    }
                    

                }

                Console.WriteLine("Press esc to exit  or any other key to replay.");
                if (Console.ReadKey().Key != ConsoleKey.Escape)
                    gameReseter = true;
                else
                    gameReseter = false;
                
                

            }while (gameReseter==true);
            Console.WriteLine("------------ Game has ended!!! -------------------");

            

           
        }
    }
}
