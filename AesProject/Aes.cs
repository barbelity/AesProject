using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesProject
{
    public static class Aes
	{
		public static List<Block> EncryptAes1(List<Block> message, Block key)
		{
			List<Block> res = new List<Block>();
			for (int i = 0; i < message.Count; i++)
			{
				Block encMessage = SubBytes(message[i]);
				encMessage = ShiftRows(encMessage);
				encMessage = MixColumns(encMessage, true);
				encMessage = AddRoundKey(encMessage, key);
				res.Add(encMessage);
			}

			return res;
		}

		public static List<Block> DecryptAes1(List<Block> encMessage, Block key)
		{
			List<Block> res = new List<Block>();
			for (int i = 0; i < encMessage.Count; i++)
			{
				Block decMessage = AddRoundKey(encMessage[i], key);
				decMessage = MixColumns(decMessage, false);
				decMessage = InverseShiftRows(decMessage);
				decMessage = inverseSubBytes(decMessage);
				res.Add(decMessage);
			}

			return res;
		}

		public static Block BreakAes1(Block message, Block cypher)
		{
			//return AddRoundKey(MixColumns(ShiftRows(SubBytes(message)), true), cypher);
			Block res = SubBytes(message);
			res = ShiftRows(res);
			res = MixColumns(res, true);
			res = AddRoundKey(res, cypher);
			return res;
		}

		private static List<Block> encryptAes1Star(List<Block> message, Block key)
		{
			List<Block> res = new List<Block>();
			for (int i = 0; i < message.Count; i++)
			{
				Block encMessage = ShiftRows(message[i]);
				encMessage = AddRoundKey(encMessage, key);
				res.Add(encMessage);
			}

			return res;
		}

		public static List<Block> decryptAes1Star(List<Block> encMessage, Block key)
		{
			List<Block> res = new List<Block>();
			for (int i = 0; i < encMessage.Count; i++)
			{
				Block decMessage = AddRoundKey(encMessage[i], key);
				decMessage = InverseShiftRows(decMessage);
				res.Add(decMessage);
			}

			return res;
		}

		public static List<Block> EncryptAes3(List<Block> message, List<Block> keys)
		{
			List<Block> res = encryptAes1Star(message, keys[0]);
			res = encryptAes1Star(res, keys[1]);
			res = encryptAes1Star(res, keys[2]);

			return res;
		}

		public static List<Block> DecryptAes3(List<Block> message, List<Block> keys)
		{
			List<Block> res = decryptAes1Star(message, keys[2]);
			res = decryptAes1Star(res, keys[1]);
			res = decryptAes1Star(res, keys[0]);

			return res;
		}

		public static List<Block> BreakAes3(List<Block> message, Block cypher)
		{
			List<Block> res = new List<Block>();
			Random rnd = new Random();
			byte[] key0 = new byte[16];
			byte[] key1 = new byte[16];

			rnd.NextBytes(key0);
			rnd.NextBytes(key1);
			res.Add(new Block(key0));
			res.Add(new Block(key1));

			List<Block> encMessage = new List<Block>();
			encMessage.Add(message[0]);
			encMessage = encryptAes1Star(encMessage, res[0]);
			encMessage = encryptAes1Star(encMessage, res[1]);

			Block asd = ShiftRows(encMessage[0]);
			res.Add(AddRoundKey(asd, cypher));

			return res;
		}

        public static Block SubBytes(Block block)
        {
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

		public static Block inverseSubBytes(Block block)
		{
			byte[] subMessageArray = new byte[16];

			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					byte by = block[i, j];
					subMessageArray[i * 4 + j] = Tables.InverseSbox[by >> 4, by % 16];
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
			
			//for (int i = 0; i < 4; i++)
			//	for (int j = 0; j < 4; j++)
			//		arrayAfterShift[i * 4 + j] = block[i, (4 + i + j) % 4];

			//return new Block(arrayAfterShift);			
        }

		public static Block InverseShiftRows(Block block)
		{
			byte[] arrayAfterShift = new byte[16];

			for (int i = 0; i < 4; i++)
				for (int j = 0; j < 4; j++)
					arrayAfterShift[i * 4 + j] = block[i, (4 - i + j) % 4];

			return new Block(arrayAfterShift);
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
			else if (tableNumber == 3)
				return Tables.RF_M3[(int)multiplyValue];
			else if (tableNumber == 9)
				return Tables.RF_M9[(int)multiplyValue];
			else if (tableNumber == 11)
				return Tables.RF_M11[(int)multiplyValue];
			else if (tableNumber == 13)
				return Tables.RF_M13[(int)multiplyValue];
			else if (tableNumber == 14)
				return Tables.RF_M14[(int)multiplyValue];

			throw new Exception("Invalid value for multiplication");
		}

		#endregion
	}
}
