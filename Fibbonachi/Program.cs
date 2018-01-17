using System;
using System.Collections.Generic;
using System.Security;

namespace Fibbonachi
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            DoWork doWork = new DoWork();
            doWork.InputCountOfTests();
            for (int i = 0; i < doWork.CountOfTests; i++)
            {
                doWork.InputTestAndStartChecking();
                Console.Write(doWork.ResultIndex + " ");
            }
        }
    }

    class DoWork
    {
        private static readonly ulong PowNum = (ulong)Math.Pow(10, 18);
        
        private List<string> partsOfSum = new List<string>();
        private string testString, nextNumberString, firstNumberString, secondNumberString;
        private ulong testInt;
        private ulong firstNumber;
        private ulong secondNumber;
        private ulong nextNumber;


        public int ResultIndex { get; private set; }
        public int CountOfTests { get; private set; }

        public void InputCountOfTests()
        {
            CountOfTests = int.Parse(Console.ReadLine());
        }

        public void InputTestAndStartChecking()
        {
            firstNumber = 0;
            secondNumber = 1;
            testString = Console.ReadLine();

            if (testString.Length < 19)
                startStandartCheck();
            else
                startCheckWithMod();
        }

        private void startStandartCheck()
        {
            testInt = ulong.Parse(testString);
            if (checkIsZerroOrOne())
                return;
            while (true)
            {
                nextNumber = firstNumber + secondNumber;

                if (nextNumber == testInt)
                    break;

                ResultIndex++;
                firstNumber = secondNumber;
                secondNumber = nextNumber;
            }
        }

        private void startCheckWithMod()
        {
            while (nextNumber < PowNum)
            {
                nextNumber = firstNumber + secondNumber;
                ResultIndex++;
                firstNumber = secondNumber;
                secondNumber = nextNumber;
            }
            
            nextNumberString = nextNumber.ToString();
            firstNumberString = firstNumber.ToString();
            secondNumberString = secondNumber.ToString();

            while (true)
            {
                if (nextNumberString == testString)
                    break;
                FindNextNumberString();
                doResultFromList();
                ResultIndex++;
            }
        }

        private bool checkIsZerroOrOne()
        {
            if (testInt == 0)
            {
                ResultIndex = 0;
                return true;
            }
            else if (testInt == 1)
            {
                ResultIndex = 1;
                return true;
            }
            else
            {
                ResultIndex = 2;
                return false;
            }
        }

        private  void FindNextNumberString()
        {
            ulong CorrectionIndex = 0;
            ulong tempFirst, tempSecond, sum;

            if (firstNumberString == null)
                tempFirst = 0;
            else if (firstNumberString.Length > 18)
                tempFirst = ulong.Parse(firstNumberString.Substring(firstNumberString.Length - 18, 18));
            else
            {
                tempFirst = ulong.Parse(firstNumberString);
                firstNumberString = null;
            }
            
            if (secondNumberString == null)
                tempSecond = 0;
            else if (secondNumberString.Length > 18)
                tempSecond = ulong.Parse(secondNumberString.Substring(secondNumberString.Length - 18, 18));
            else
            {
                tempSecond = ulong.Parse(secondNumberString);
                secondNumberString = null;
            }

            if (tempFirst == 0 && tempSecond == 0)
                return;

            sum = tempFirst + tempSecond;
            CorrectionIndex = (sum - sum % PowNum) / PowNum;
            partsOfSum.Add(((sum % PowNum)+CorrectionIndex).ToString());

            firstNumberString = firstNumberString.Substring(0, firstNumberString.Length - 18);
            secondNumberString = secondNumberString.Substring(0, secondNumberString.Length - 18);
            
            FindNextNumberString();
        }

        private void doResultFromList()
        {
            nextNumberString = String.Empty;
            partsOfSum.Reverse();
            for (int i = 0; i < partsOfSum.Count-1; i++)
            {
                if(i != 1 && partsOfSum[i].Length < 18)
                   partsOfSum[i] = addZero(partsOfSum[i]);

                nextNumberString += partsOfSum[i];
            }
        }

        private string addZero(string v)
        {
            int targetAdd = 18 - v.Length;
            for (int i = 0; i <targetAdd; i++)
            {
                v = v.Insert(0, "0");
            }
            return v;
        }
    }
}