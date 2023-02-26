using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class Task3
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до папки");
            string path = Console.ReadLine();
            var di = new DirectoryInfo(path);
            if (di.Exists)
            {
                try
                {
                    var result = DirSize(di);                   
                    Console.WriteLine(di.Name + $"- Исходный размер папки: {result.size}");
                    Console.WriteLine(di.Name + $"- Исходное количество файлов в папке: {result.fil}");
                    Console.WriteLine();
                    DeletFile(di);
                    var result2 = DirSize(di);
                    var finishVolume = result.size - result2.size;
                    var finishFile = result.fil - result2.fil;
                    Console.WriteLine(di.Name + $"- Удаленный объем: {finishVolume}");
                    Console.WriteLine(di.Name + $"- Удалено файлов: {finishFile}");
                    Console.WriteLine(di.Name + $"- Текущее количество файлов в папке: {result2.fil}");
                    Console.WriteLine(di.Name + $"- Текущий размер папки: {result2.size}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(di.Name + $"- Не удалось рассчитать размер:{ex.Message}");
                }

            }
            else { Console.WriteLine("Такой папки нет"); }
        }

        public static (long size, int fil) DirSize(DirectoryInfo d)
        {
            long size = 0;
            int fil = 0;

            foreach (FileInfo fi in d.GetFiles())
            {
                size += fi.Length;
                fil++;
            }
            foreach (DirectoryInfo fi in d.GetDirectories())
            {
                var rt = DirSize(fi);
                size += rt.size;
                fil += rt.fil;
            }
            return (size, fil);
        }

        public static void DeletFile(DirectoryInfo di)
        {
            var time = DateTime.Now;
            try
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    if (time - file.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        file.Delete();
                    }
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    if (time - dir.LastAccessTime > TimeSpan.FromMinutes(30))
                    {
                        dir.Delete(true);
                    }
                }
            }
            catch (Exception ex) {Console.WriteLine(di.Name + $"- Не удалось удалить:{ex.Message}"); }
        }
    }
}
