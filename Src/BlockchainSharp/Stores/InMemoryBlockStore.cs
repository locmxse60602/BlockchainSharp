﻿namespace BlockchainSharp.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Core;
    using BlockchainSharp.Core.Types;

    public class InMemoryBlockStore : IBlockStore
    {
        private IDictionary<BlockHash, Block> blocks = new Dictionary<BlockHash, Block>();
        private IDictionary<ulong, IList<Block>> blocksbynumber = new Dictionary<ulong, IList<Block>>();
        private IDictionary<BlockHash, IList<Block>> blocksbyparenthash = new Dictionary<BlockHash, IList<Block>>();

        public Block GetByBlockHash(BlockHash hash)
        {
            if (this.blocks.ContainsKey(hash))
                return this.blocks[hash];

            return null;
        }

        public IEnumerable<Block> GetByNumber(ulong number)
        {
            if (this.blocksbynumber.ContainsKey(number))
                return this.blocksbynumber[number];

            return new List<Block>();
        }

        public IEnumerable<Block> GetByParentHash(BlockHash hash)
        {
            if (this.blocksbyparenthash.ContainsKey(hash))
                return this.blocksbyparenthash[hash];

            return new List<Block>();
        }

        public void Save(Block block)
        {
            this.blocks[block.Hash] = block;

            IList<Block> bs;

            if (this.blocksbynumber.ContainsKey(block.Number))
                bs = this.blocksbynumber[block.Number];
            else
            {
                bs = new List<Block>();
                this.blocksbynumber[block.Number] = bs;
            }

            bs.Add(block);

            if (block.ParentHash == null)
                return;

            IList<Block> bs2;

            if (this.blocksbyparenthash.ContainsKey(block.ParentHash))
                bs2 = this.blocksbyparenthash[block.ParentHash];
            else
            {
                bs2 = new List<Block>();
                this.blocksbyparenthash[block.ParentHash] = bs2;
            }

            bs2.Add(block);
        }

        public void Remove(BlockHash key)
        {
            Block block = this.blocks[key];

            if (block == null)
                return;

            this.blocks.Remove(key);

            this.blocksbynumber[block.Number].Remove(block);
            
            if (block.ParentHash != null)
                this.blocksbyparenthash[block.ParentHash].Remove(block);
        }
    }
}
