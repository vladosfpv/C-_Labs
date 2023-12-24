/*using System;
using System.IO;
using System.IO.Compression;

class lab082
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите путь к папке, в которой нужно выполнить поиск:");
        string searchDirectory = Console.ReadLine();

        Console.WriteLine("Введите имя файла для поиска (с расширением, например, example.txt):");
        string targetFileName = Console.ReadLine();

        string[] files = Directory.GetFiles(searchDirectory, targetFileName, SearchOption.AllDirectories);

        if (files.Length == 0)
        {
            Console.WriteLine("Файл не найден.");
        }
        else
        {
            foreach (string filePath in files)
            {
                Console.WriteLine($"Найден файл: {filePath}");

                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string fileContent = reader.ReadToEnd();
                        Console.WriteLine($"Содержимое файла:\n{fileContent}");
                    }
                }

                string compressedFilePath = Path.ChangeExtension(filePath, ".gz");
                using (FileStream originalFileStream = File.OpenRead(filePath))
                {
                    using (FileStream compressedFileStream = File.Create(compressedFilePath))
                    {
                        using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(compressionStream);
                            Console.WriteLine($"Файл сжат и сохранен как: {compressedFilePath}");
                        }
                    }
                }
            }
        }
    }
}*/