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
			Dictionary<string, string> cliArguments = new Dictionary<string, string>();

			for (int i = 0; i < args.Length;)
			{
				if (args[i].Equals("-e") || args[i].Equals("-d") || args[i].Equals("-b") || args[i].Equals("–e") || args[i].Equals("–d") || args[i].Equals("–b"))
				{
					cliArguments.Add("operation", args[i]);
					i++;
					continue;
				}
				else
				{
					cliArguments.Add(args[i], args[i + 1]);
					i += 2;
				}
			}

			
			string command = cliArguments["operation"];
			string useAlgorithm = cliArguments.ContainsKey("-a") ? cliArguments["-a"] : cliArguments["–a"];
			string outputPath = cliArguments.ContainsKey("-o") ? cliArguments["-o"] : cliArguments["–o"];

			if ((command.Equals("-b") || command.Equals("–b")))
			{
				//perform break
				string messagePath = cliArguments.ContainsKey("-m") ? cliArguments["-m"] : cliArguments["–m"];
				string cypherPath = cliArguments.ContainsKey("-c") ? cliArguments["-c"] : cliArguments["–c"];

				List<Block> breakMessage = FileOperation.ReadFromFile(messagePath);
				List<Block> breakCypher = FileOperation.ReadFromFile(cypherPath);

				List<Block> breakKey = new List<Block>();
				breakKey.Add(Aes.BreakAes1(breakMessage[0], breakCypher[0]));
				FileOperation.WriteToFile(breakKey, outputPath);
			}
			else
			{
				//extract commands
				string keyPath = cliArguments.ContainsKey("-k") ? cliArguments["-k"] : cliArguments["–k"];
				string inputPath = cliArguments.ContainsKey("-i") ? cliArguments["-i"] : cliArguments["–i"];

				List<Block> encKey = FileOperation.ReadFromFile(keyPath);

				if (command.Equals("-e") || command.Equals("–e"))
				{

				}
				else if (command.Equals("-d") || command.Equals("–d"))
				{
					//perform decrypt
				}
			}
			
			
			//Tests.StartTest();
			
			System.Console.Write("");
        }
    }
}
