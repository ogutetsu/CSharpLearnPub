using System;
using System.IO;
using System.Text;
using LearnLib;

namespace LearnProject.Beginning
{
    public class FileIO : ConsoleMenu
    {
        public override void Run()
        {
            
            Console.Write($"作成するファイル名を入力してください : ");
            var filename = Console.ReadLine();

            string text = $"これはテスト書き込みです";

            using (var fileStream = File.Create(filename))
            {
                var code = new UTF8Encoding(true).GetBytes(text);
                fileStream.Write(code, 0, code.Length);
            }
            Console.WriteLine($"書き込み完了");

            Console.WriteLine($"{filename}ファイルを読み込みます");
            using (var fileStream = File.OpenRead(filename))
            {
                var bytes = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fileStream.Read(bytes, 0, bytes.Length) > 0)
                {
                    Console.WriteLine(temp.GetString(bytes));
                }
            }
            
            File.Delete(filename);
            Console.WriteLine($"{filename}ファイルを削除しました。");

        }
    }
}