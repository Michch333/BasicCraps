using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;

namespace DiceRoller
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playCrapsAgain;
            bool isOpen = true;
            int thePoint = 0;

            Console.WriteLine("Hey there! Let's play some craps!!");
            do
            {
                bool result = playCraps(isOpen, thePoint, out bool shouldBeOpen,out int shouldBeThePoint);
                isOpen = shouldBeOpen;
                thePoint = shouldBeThePoint;
                playCrapsAgain = result;

            } while (playCrapsAgain);

            static int RollDice(int _numberOfSides)
            {
                var rand = new Random();
                return rand.Next(1, _numberOfSides + 1);
            }

            static bool playCraps(bool _isOpen, int _thePoint, out bool shouldBeOpen, out int shouldBeThePoint)
            {
                bool isOpen = _isOpen;
                int thePoint = _thePoint;
                Console.WriteLine("Press any key to roll");
                Console.ReadLine();

                int userRoll = (RollDice(6) + RollDice(6));
                Console.WriteLine($"You rolled a {userRoll}");
                if (userRoll == 7 || userRoll == 11)
                {
                    if (isOpen)
                    {
                        Console.WriteLine("You win!");

                        shouldBeOpen = isOpen;
                        shouldBeThePoint = thePoint;

                        return askToPlayAgain();
                    }
                    else
                    {
                        Console.WriteLine("You Lose :( ");
                        thePoint = 0;
                        isOpen = true;
                        shouldBeOpen = isOpen;
                        shouldBeThePoint = thePoint;
                        return askToPlayAgain();

                    }
                }
                else if (userRoll == 4 || userRoll == 5 || userRoll == 6 || userRoll == 7 || userRoll == 8 || userRoll == 9 || userRoll == 10)
                {
                    if (!isOpen)
                    {
                        if (userRoll == thePoint)
                        {
                            Console.WriteLine("You win!");
                            thePoint = 0;
                            isOpen = true;

                            shouldBeOpen = isOpen;
                            shouldBeThePoint = thePoint;

                            return askToPlayAgain(); ;
                        }
                        else
                        {
                            Console.WriteLine($"Pay out space {userRoll}");

                            shouldBeOpen = isOpen;
                            shouldBeThePoint = thePoint;

                            return true;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"The point is now {userRoll}");
                        isOpen = false;
                        thePoint = userRoll;

                        shouldBeOpen = isOpen;
                        shouldBeThePoint = thePoint;

                        return true;
                    }
                }
                else 
                {
                    shouldBeOpen = isOpen;
                    shouldBeThePoint = thePoint;
                    return true;
                } 
            }
        }

        static bool askToPlayAgain()
        {
            Console.WriteLine("Would you like to play again?");
            string userContinueInput = Console.ReadLine();

            Regex yesPattern = new Regex(@"^(Y|y)(es)?$");
            if (yesPattern.IsMatch(userContinueInput))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Thanks For Playing!");
                return false;
            }
        }
    }
}
