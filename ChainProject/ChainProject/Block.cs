using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainProject
{
    public class Block
    {
        public int Id { get; set; }
        //Left: address of user's wallet.  Right: Value of a transaction
        public Dictionary<int, int> DictTransactions { get; set; }
        public Block Preview { get; set; }
        public Block Next { get; set; }


        public Block()
        {
            
        }

        public void AddNextBlock(Dictionary<int,int> dictTransactions, int proposedHash)
        {
            var currentNode = this;

            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            }

            //currentNode is the last one in the sequence.

            if (currentNode == null || proposedHash == 0)
                return;

            Block myBlock = new Block();
            myBlock.Id = proposedHash;
            myBlock.Preview = currentNode;

            for (int i = 0; i < dictTransactions.Count; i++)
            {
                var dict = dictTransactions.ElementAt(i);
                myBlock.DictTransactions = new Dictionary<int, int>();
                myBlock.DictTransactions.Add(dict.Key, dict.Value);
            }


            if (currentNode.Next != null)
                currentNode.Next.Preview = myBlock;

            myBlock.Next = currentNode.Next;
            currentNode.Next = myBlock;
        }        
    }
}
