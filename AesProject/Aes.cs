using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesProject
{
    public static class Aes
	{

        public static Block SubBytes(Block block)
        {
			//Block retBlock = new Block();
			byte[] subMessageArray = new byte[16];

			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					byte by = block[i, j];
					subMessageArray[i * 4 + j] = Tables.Sbox[by >> 4, by % 16]; 
				}
			}

			return new Block(subMessageArray);
        }

        private static Block ShiftRows(Block block)
        {
            throw new NotImplementedException();
        }

        private static Block MixColumns(Block block)
        {
            throw new NotImplementedException();
        }

        private static Block AddRoundKey(Block block)
        {
            throw new NotImplementedException();
        }
    }
}
