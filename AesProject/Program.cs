using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesProject
{
    class Program
    {
        static void Main(string[] args)
        {
			/*
						String command = System.Console.ReadLine();

						switch (command)
						{
							case "something":
								break;

							//Commands

							default:
								System.Console.WriteLine("finished");
								break;

						}
			 */

			#region test members

			//starting block
			Block block = new Block(new byte[]{(byte)0x19, (byte)0xa0, (byte)0x9a, (byte)0xe9,
												(byte)0x3d, (byte)0xf4, (byte)0xc6, (byte)0xf8,
												(byte)0xe3, (byte)0xe2, (byte)0x8d, (byte)0x48,
												(byte)0xbe, (byte)0x2b, (byte)0x2a, (byte)0x08});
			//after sub bytes
			Block block2 = new Block(new byte[]{(byte)0xd4, (byte)0xe0, (byte)0xb8, (byte)0x1e,
												(byte)0x27, (byte)0xbf, (byte)0xb4, (byte)0x41,
												(byte)0x11, (byte)0x98, (byte)0x5d, (byte)0x52,
												(byte)0xae, (byte)0xf1, (byte)0xe5, (byte)0x30});
			//after shift rows
			Block block3 = new Block(new byte[]{(byte)0xd4, (byte)0xe0, (byte)0xb8, (byte)0x1e,
												(byte)0xbf, (byte)0xb4, (byte)0x41, (byte)0x27,
												(byte)0x5d, (byte)0x52, (byte)0x11, (byte)0x98,
												(byte)0x30, (byte)0xae, (byte)0xf1, (byte)0xe5});
			//after mix columns
			Block block4 = new Block(new byte[]{(byte)0x04, (byte)0xe0, (byte)0x48, (byte)0x28,
												(byte)0x66, (byte)0xcb, (byte)0xf8, (byte)0x06,
												(byte)0x81, (byte)0x19, (byte)0xd3, (byte)0x26,
												(byte)0xe5, (byte)0x9a, (byte)0x7a, (byte)0x4c});
			//after addRoundKey
			Block block5 = new Block(new byte[]{(byte)0xa4, (byte)0x68, (byte)0x6b, (byte)0x02,
												(byte)0x9c, (byte)0x9f, (byte)0x5b, (byte)0x6a,
												(byte)0x7f, (byte)0x35, (byte)0xea, (byte)0x50,
												(byte)0xf2, (byte)0x2b, (byte)0x43, (byte)0x49});
			//key
			Block key = new Block(new byte[]{(byte)0xa0, (byte)0x88, (byte)0x23, (byte)0x2a,
												(byte)0xfa, (byte)0x54, (byte)0xa3, (byte)0x6c,
												(byte)0xfe, (byte)0x2c, (byte)0x39, (byte)0x76,
												(byte)0x17, (byte)0xb1, (byte)0x39, (byte)0x05});
			#endregion


			#region check encrypt

			//checking subBytes
			Block newBlock = Aes.SubBytes(block);
			bool res = (newBlock == block2);

			//checking shiftRows
			Block newBlock2 = Aes.ShiftRows(block2);
			bool res2 = (block3 == newBlock2);

			//checking mixColumns
			Block newBlock3 = Aes.MixColumns(block3, true);
			bool res3 = (block4 == newBlock3);

			//checking addRoundKey
			Block newBlock4 = Aes.AddRoundKey(block4, key);
			bool res4 = (block5 == newBlock4);

			#endregion

			#region check decrypt

			//checking inverseSubBytes
			Block newBlock6 = Aes.inverseSubBytes(block2);
			bool res6 = (newBlock6 == block);

			//checking inverseShiftRows
			Block newBlock5 = Aes.InverseShiftRows(block3);
			bool res5 = (newBlock5 == block2);

			//checking inverseMixColumns
			Block newBlock7 = Aes.MixColumns(block4, false);
			bool res7 = (block3 == newBlock7);

			//checking final AddRoundKey
			Block newBlock8 = Aes.AddRoundKey(block5, key);
			bool res8 = (block4 == newBlock8);

			#endregion

			//checking
			Block checking = Aes.EncryptAes1(block, block5);
			bool resss = (key == checking);

			System.Console.Write("");
        }
    }
}
