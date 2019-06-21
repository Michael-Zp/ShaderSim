using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			FileStream fs = File.Create(Directory.GetCurrentDirectory() + "code.txt");

			string[] names = new[] { "X", "Y", "Z", "W" };
			string[] shown = new[] { "R", "G", "B", "A" };
			//shown = names;

			string code = "";
			for (int x = 0; x < 4; x++)
			{
				for (int y = 0; y < 4; y++)
				{
					for (int z = 0; z < 4; z++)
					{
						for (int w = 0; w < 4; w++)
						{

							if (x != y && x != z && y != z && x != w && y != w && z != w)
							{
								code +=
									$"public Vector4 {shown[x]}{shown[y]}{shown[z]}{shown[w]}{{\nget{{return new Vector4({names[x]}, {names[y]}, {names[z]}, {shown[w]});}}\nset{{\n{names[x]}=value.X;\n{names[y]}=value.Y;\n{names[z]}=value.Z;\n{names[w]}=value.W;\n}}\n}}\n";
							}
							else
							{
								code +=
									$"public Vector4 {shown[x]}{shown[y]}{shown[z]}{shown[w]} => new Vector4({names[x]}, {names[y]}, {names[z]}, {shown[w]});\n";
							}
						}
					}
				}
			}
			Console.Write(code);
			Byte[] info = new UTF8Encoding(true).GetBytes(code);
			fs.Write(info, 0, info.Length);
			Console.Read();
		}
	}
}
