using System;
using System.IO;

namespace Setup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"本程序用于解决在Revit中载入某些族时，被族库大师拦截，导致载入失败的问题");
            Console.WriteLine(@"请输入您要安装的Revit版本：");
            var versionString = Console.ReadLine();
            if (int.TryParse(versionString, out int version))
            {
                var path =
                    $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\Autodesk\Revit\Addins\{version}";
                if (!Directory.Exists(path))
                {
                    Console.WriteLine($@"您可能没有安装Revit{version},请先确认");
                }
                else
                {
                    try
                    {
                        WriteBinaryFile(Properties.Resources.assembly, $@"{path}\UnlockFamilyCheck.dll");
                        WriteBinaryFile(Properties.Resources.addin, $@"{path}\UnlockFamilyCheck.addin");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine(@"安装失败");
                    }
                }
            }
        }

        private static void WriteBinaryFile(byte[] binary, string path)
        {
            using (var file = File.Create(path))
            {
                file.Seek(0, SeekOrigin.Begin);
                file.Write(binary, 0, binary.Length);
            }
        }
    }
}
