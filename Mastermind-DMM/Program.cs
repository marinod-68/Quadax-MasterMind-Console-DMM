using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastermind_DMM
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello\n");

            const int MAX_ATTEMPTS = 10;
            const char A_SPACE = ' ';
            int attempts = 0;

            Random rnd = new Random();
            int[] answer = new int[4];
            int[] guess = new int[4];
            char[] answerKey = new char[4];
            char[] guessKey = new char[4];
            String[] digits = new String[4]{"Enter first digit:  ", "\nEnter second digit:  ", "\nEnter third digit:  ", "\nEnter fourth digit:  "};

            answer[0] = rnd.Next(1, 6);
            answer[1] = rnd.Next(1, 6);
            answer[2] = rnd.Next(1, 6);
            answer[3] = rnd.Next(1, 6);

            ConsoleKeyInfo keyPressed;

            bool clearConsole, combinationFound = false;

            try
            {
                do
                {
                    answerKey[0] = answerKey[1] = answerKey[2] = answerKey[3] = guessKey[0] = guessKey[1] = guessKey[2] = guessKey[3] = A_SPACE;
                    Console.WriteLine("Enter a four digit number, where each digit is between 1 and 6 (inclusive)\n");
                    int keysSelected = 0;
                    int whichDigit = 0;
                    do
                    {
                        String prompt = digits[keysSelected];
                        Console.Write(prompt);
                        keyPressed = Console.ReadKey();
                        if (keyPressed.Key >= ConsoleKey.D1 && keyPressed.Key <= ConsoleKey.D6)
                        {
                            char myKey = keyPressed.KeyChar;
                            guess[whichDigit] = int.Parse(myKey.ToString());
                            keysSelected++;
                            whichDigit++;
                        }
                        else
                        {
                            String str1 = String.Format("\nInvalid key:  {0} selected, try again\n", keyPressed.KeyChar);
                            Console.WriteLine(str1);
                        }
                    } while (keysSelected < 4);

                    if (Compare.CompareAnswerAndGuess(answer, guess, answerKey, guessKey))
                    {
                        combinationFound = true;
                        String congrats = String.Format("\n\nThe combination of {0} {1} {2} {3} was found, congratulations!\n", answer[0], answer[1], answer[2], answer[3]);
                        Console.WriteLine(congrats);
                        break;
                    }
                    else
                    {
                        String yourGuess = String.Format("\n\nYour guess:  {0}{1} {2}{3} {4}{5} {6}{7} was not quite right.  You have {8} attempts left\n",
                                                         guess[0], guessKey[0], guess[1], guessKey[1], guess[2], guessKey[2], guess[3], guessKey[3], (MAX_ATTEMPTS - attempts) - 1);
                        Console.WriteLine(yourGuess);
                        Console.WriteLine("Press the Enter key to continue or Q to quit");

                        bool done = false;
                        clearConsole = false;
                        do
                        {
                            ConsoleKey key = Console.ReadKey().Key;
                            if (key == ConsoleKey.Enter)
                            {
                                done = true;
                                clearConsole = true;
                            }
                            else if (key == ConsoleKey.Q)
                            {
                                done = true;
                                attempts = MAX_ATTEMPTS;
                            }
                        } while (!done);
                    } //end else

                    if (clearConsole)
                        Console.Clear();
                    String lastGuess = String.Format("Your last guess was:  {0}{1} {2}{3} {4}{5} {6}{7}", guess[0], guessKey[0], guess[1], guessKey[1], guess[2], guessKey[2], guess[3], guessKey[3]);
                    Console.WriteLine(lastGuess);
                } while (++attempts < MAX_ATTEMPTS);

                if (!combinationFound)
                {
                    String finalAnswer = String.Format("\n\nYou have lost, the combination was:  {0}{1}{2}{3}", answer[0], answer[1], answer[2], answer[3]);
                    Console.WriteLine(finalAnswer);
                } //end if (!combinationFound)
            } //end try
            catch (Exception e)

            {
                String exp = String.Format("{0} threw an exceptions:  {1}", e.Source, e.Message);
                Console.WriteLine(exp);
                Console.WriteLine(e.StackTrace);
            } //end catch (Exception e)
        } //end static void Main(string[] args)

    } //end class Program

    class Compare
    {
        public static bool CompareAnswerAndGuess(int[] answer, int[] guess, char[] answerKey, char[] guessKey)
        {
            bool status = false;
            const char PLUS = '+';
            const char MINUS = '-';

            try
            {
                for (int i = 0; i < 4; i++)
                {
                    if (guess[i] == answer[i])
                        answerKey[i] = guessKey[i] = PLUS;
                } //end for (int i = 0; i < 4; i++)

                for (int i = 0; i < 4; i++)
                {
                    if (guessKey[i] == PLUS)
                        continue;

                    for (int j = 0; j < 4; j++)
                    {
                        if ((answerKey[j] != PLUS) && (answerKey[j] != MINUS))
                        {
                            if (guess[i] == answer[j])
                            {
                                guessKey[i] = MINUS;
                                answerKey[j] = MINUS;
                                break;
                            } //end if (guess[i] == answer[j])
                        } //end if ((answerKey[j] != PLUS) && (answerKey[j] != MINUS))
                    } //end for (int j = 0; j < 4; j++)
                } //end for (int i = 0; i < 4; i++)

                return answer.SequenceEqual(guess);
            } //end try
            catch (Exception e)
            {
                throw e;
            } //end catch (Exception e)
        } //end public static bool CompareAnswerAndGuess(int[] answer, int[] guess, char[] answerKey, char[] guessKey)
    } //end class Compare
} //end namespace Mastermind_DMM
