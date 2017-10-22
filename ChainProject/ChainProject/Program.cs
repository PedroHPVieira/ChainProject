using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChainProject.Block;

namespace ChainProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Method called to generate the Ficonacci hash id
            var nextHashNumber = Mine(1);

            //the list of blocks during the program
            var myBlocks = new Block();

            //Variable to validate if the fibonacci order is being followed after a insertion.
            var valid = true;

            //Final balance
            var balance = 0;
            
            myBlocks.Id = nextHashNumber;
            myBlocks.DictTransactions = new Dictionary<int, int>();
            myBlocks.DictTransactions.Add(1,12);

            valid = CheckFibonacci(myBlocks);

            //Checking to see if in the correct order
            if (valid)
            {
                nextHashNumber = Mine(2);

                myBlocks.AddNextBlock(new Dictionary<int, int> { { 1, 1 } }, nextHashNumber);

                valid = CheckFibonacci(myBlocks);

                //Checking to see if in the correct order
                if (valid)
                {
                    nextHashNumber = Mine(3);

                    myBlocks.AddNextBlock(new Dictionary<int, int> { { 1, -11 } }, nextHashNumber);

                    valid = CheckFibonacci(myBlocks);

                    //Checking to see if in the correct order
                    if (valid)
                    {
                        nextHashNumber = Mine(4);

                        myBlocks.AddNextBlock(new Dictionary<int, int> { { 1, 6 } }, nextHashNumber);

                        valid = CheckFibonacci(myBlocks);

                        //Checking to see if in the correct order
                        if (valid)
                        {
                            balance = GetBalance(myBlocks);
                        }                       
                    }
                    
                }                
            }

            Console.WriteLine("Balance: " + balance);

            PrintList(myBlocks);        
        }

        //Method to get the value of Fibonacci next position
        public static int Mine(int nextPosition)
        {
            if (nextPosition == 0)
                return 0;
            if (nextPosition == 1)
                return 1;

            return Mine(nextPosition - 1) + Mine(nextPosition - 2);
        }

        //Fibonacci Iterative for checking
        //Performance of O(n)
        public static string GenerateFibonacciCheck(int quantity)
        {
            int a = 1;
            int b = 1;
            int c = 0;

            StringBuilder builder = new StringBuilder();

            if (quantity == 1)
                return "1";

            builder.Append(a);
            builder.Append(b);

            for (int i = 2; i < quantity; i++)
            {
                c = a + b;
                builder.Append(c);
                a = b;
                b = c;
            }

            return builder.ToString();     
        }

        //Performance of O(n2)
        //Method to calcualte the balance of the transactions
        public static int GetBalance(Block block)
        {
            if (block == null)
                return 0;

            var balance = 0;

            while (block != null)
            {
                for (int i = 0; i < block.DictTransactions.Count; i++)
                {
                    var dict = block.DictTransactions.ElementAt(i);

                    balance += dict.Value;
                }

                block = block.Next;
            }

            return balance;
        }

        //Performance of O(n)
        //Method to list the nodes of the blocks
        public static void PrintList(Block block)
        {
            if (block == null)
                Console.WriteLine("No blocks in this list.");

            while (block != null)
            {
                Console.Write(block.Id + ", ");
                block = block.Next;
            }
        }

        //Performance of O(n)
        //Method to check Fibonacci
        public static bool CheckFibonacci(Block block)
        {
            StringBuilder builder = new StringBuilder();

            while (block != null)
            {
                builder.Append(block.Id);
                block = block.Next;
            }

            var quantityBlocks = builder.Length;

            var fibonacciSequence = GenerateFibonacciCheck(quantityBlocks);

            for (int i = 0; i < builder.Length; i++)
            {
                if (builder.ToString() != fibonacciSequence)
                    return false;
            }

            return true;
        }
    }
}
