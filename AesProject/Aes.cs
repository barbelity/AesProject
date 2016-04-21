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

        public static Block ShiftRows(Block block)
        {
			byte[] arrayAfterShift = new byte[16];
			// firsr row
			for (int i = 0; i < 4; i++)
				arrayAfterShift[i] = block[0, i];

			// second row
			for (int i = 0; i < 3; i++)
				arrayAfterShift[i + 4] = block[1, i + 1];
			arrayAfterShift[7] = block[1, 0];

			// third row
			for (int i = 0; i < 4; i++)
				arrayAfterShift[i + 10] = block[2, i];
			arrayAfterShift[8] = block[2, 2];
			arrayAfterShift[9] = block[2, 3];

			//  fourth row
			arrayAfterShift[12] = block[3, 3];
			arrayAfterShift[13] = block[3, 0];
			arrayAfterShift[14] = block[3, 1];
			arrayAfterShift[15] = block[3, 2];
			Block blokAfterShift = new Block(arrayAfterShift);

			return blokAfterShift;
        }

		/// <summary>
		/// receives the message to mix and the operation to perform - enc or dec
		/// </summary>
		/// <param name="block">message block to mix it's columns</param>
		/// <param name="isEncrypt">true if encrypt, false if decrypt</param>
		/// <returns>mixed message</returns>
        public static Block MixColumns(Block block, bool isEncrypt)
        {
			byte[] mixedResult = new byte[16];
			int[,] mulTable = isEncrypt ? Tables.MixColumns_enc : Tables.MixColumns_dec;

			for (int i = 0; i < 4; i++)
			{
				byte[] mixedSingleColumn = MixSingleColumn(block, i, mulTable);
				
				for (int j = 0; j < 4; j++)
				{
					mixedResult[i + j *4] = mixedSingleColumn[j];
				}
			}

			return new Block(mixedResult);
        }

        public static Block AddRoundKey(Block message, Block key)
        {
			byte[] resultArray = new byte[16];

			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					int xorResult = message[i, j] ^ key[i, j];
					resultArray[i * 4 + j] = (byte)xorResult;
				}
			}

			return new Block(resultArray);
        }

		#region private methods

		private static byte[] MixSingleColumn(Block block, int colNumber, int[,] mulTable)
		{
			byte[] mixedSingleColumn = new byte[4];
			int sumResult = 0;
			for (int i = 0; i < 4; i++)
			{
				sumResult = MultResult(mulTable[i, 0], (byte)(block[0, colNumber]));
				for (int j = 1; j < 4; j++)
					sumResult ^= MultResult(mulTable[i, j], (byte)(block[j, colNumber]));
				mixedSingleColumn[i] = (byte)sumResult;
			}

			return mixedSingleColumn;
		}

		private static byte MultResult(int tableNumber, byte multiplyValue)
		{
			if (tableNumber == 1)
				return multiplyValue;
			else if (tableNumber == 2)
				return Tables.RF_M2[(int)multiplyValue];
			else
				return Tables.RF_M3[(int)multiplyValue];

			throw new Exception("Invalid value for multiplication");
		}

		#endregion
	}
}
