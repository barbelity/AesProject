using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AesProject
{
    static class FileOperation
    {
		public static List<Block> ReadFromFile(string pathFile)
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
				Block block16 = new Block(array16, true);
				listToCrpt.Add(block16);
				i = j;
				j = j + 16;

			}
			return listToCrpt;
		}

		public static void WriteToFile(List<Block> writeData, string filePath)
		{
			if (File.Exists(filePath))
				File.Delete(filePath);
			//using (FileStream fs = File.Create(filePath)) ;
			string newPath = Directory.GetCurrentDirectory() + "/" + filePath;
			using(File.Create(newPath));
			
			/*
			{
				byte[] allData = new byte[writeData.Count() * 16];
				foreach (Block block in writeData)
					allData.Concat(block.toByteArray());
				fs.Write(allData, 0, allData.Length);
			}*/
			using (var fileStream = new FileStream(newPath, FileMode.Append, FileAccess.Write, FileShare.None))
			using (var bw = new BinaryWriter(fileStream))
			{
				foreach (Block block in writeData)
					bw.Write(block.toByteArray());
			}
			/*
			byte[] allData = new byte[writeData.Count() * 16];
			int counter = 0;
			foreach (Block block in writeData)
			{
				allData.Concat(block.toByteArray())
				counter++;
			}
				//allData.(block.toByteArray());
			File.WriteAllBytes(filePath, allData);
			//File.
			 * */
		}

    }
}
