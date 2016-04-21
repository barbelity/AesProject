using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesProject
{
    class FileOperation
    {
		public List<Block> ReadFromFile(string pathFile)
		{
			byte[] array = File.ReadAllBytes(pathFile);
			List<Block> listToCrpt = doListByteArray(array);
			return listToCrpt;
		}

		private static List<Block> doListByteArray(byte[] array)
		{

			List<Block> listToCrpt = new List<Block>();
			int i = 0;
			int j = 16;
			while (j <= array.Length)
			{

				byte[] array16 = new List<byte>(array).GetRange(i, 16).ToArray();
				Block block16 = new Block(array16);
				listToCrpt.Add(block16);
				i = j;
				j = j + 16;

			}
			return listToCrpt;
		}

		public static void WriteToFile(List<byte[]> listAfterCrpt)
		{

			throw new NotImplementedException();
		}

    }
}
