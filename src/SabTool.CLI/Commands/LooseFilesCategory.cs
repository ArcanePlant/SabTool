﻿namespace SabTool.CLI.Commands;

using SabTool.CLI.Base;
using SabTool.Depot;

public class LooseFilesCategory : BaseCategory
{
    // TODO: move this data when extracting into a json/xml next to the files, so custom files can be unpacked and repacked
    public static IDictionary<string, uint> LooseFilesBinPCStaticFiles = new Dictionary<string, uint>
    {
        { "France.shaders", 0xEE2E6431 },
        { @"Cinematics\Conversations\Conversations.cnvpack", 0x46350DCF },
        { @"Cinematics\Cinematics.cinpack", 0x0F3C8F37 },
        { "global.map", 0x6B37A7CB },
        { "France.map", 0x25B96F25 },
        { @"France\EditNodes\EditNodes.pack", 0xC965B7EA },
        { "GameTemplates.wsd", 0x12CDBDE3 }
    };

    public override string Key => "loose-files";
    public override string Shortcut => "l";
    public override string Usage => "<sub command name>";

    public class UnpackCommand : BaseCommand
    {
        public override string Key { get; } = "unpack";
        public override string Shortcut { get; } = "u";
        public override string Usage { get; } = "<game base path> <output directory path>";

        public override bool Execute(IEnumerable<string> arguments)
        {
            if (arguments.Count() < 2)
            {
                Console.WriteLine("ERROR: Not enough arguments given!");
                return false;
            }

            ResourceDepot.Instance.Initialize(arguments.ElementAt(0));
            ResourceDepot.Instance.Load(Resource.LooseFiles);

            var outputFolder = arguments.ElementAt(1);

            Directory.CreateDirectory(outputFolder);

            var looseFiles = ResourceDepot.Instance.GetLooseFiles();
            if (looseFiles == null)
            {
                Console.WriteLine("ERROR: LooseFiles were not loaded!");
                return false;
            }

            foreach (var file in looseFiles)
            {
                var outFilePath = Path.Combine(outputFolder, file.Name);
                var directoryName = Path.GetDirectoryName(outFilePath);
                if (directoryName == null)
                {
                    Console.WriteLine("ERROR: Output directory is invalid!");
                    return false;
                }

                Directory.CreateDirectory(directoryName);

                Console.WriteLine($"Unpacking {file}...");

                var looseFile = ResourceDepot.Instance.GetLooseFile(file.Name);
                if (looseFile == null)
                {
                    Console.WriteLine("ERROR: LooseFile can't be read!");
                    return false;
                }

                File.WriteAllBytes(outFilePath, looseFile.GetBuffer());
            }

            Console.WriteLine("Files successfully unpacked!");
            return true;
        }
    }

    public class PackCommand : BaseCommand
    {
        public override string Key { get; } = "pack";
        public override string Shortcut { get; } = "p";
        public override string Usage { get; } = "<loose file path> <input directory path>";

        public override bool Execute(IEnumerable<string> arguments)
        {
            if (arguments.Count() < 2)
            {
                Console.WriteLine("ERROR: Not enough arguments given!");
                return false;
            }

            var filePath = arguments.ElementAt(0);
            var inputFolder = arguments.ElementAt(1);

            if (File.Exists(filePath))
            {
                Console.WriteLine("ERROR: Loose file already exist!");
                return false;
            }

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine("ERROR: Input directory doesn't exist!");
                return false;
            }

            /*var looseFile = new LooseFile();

            if (Path.GetFileName(filePath) == "loosefiles_BinPC.pack")
            {
                foreach (var pair in LooseFilesBinPCStaticFiles)
                {
                    var path = Path.Combine(inputFolder, pair.Key);
                    if (!File.Exists(path))
                    {
                        Console.WriteLine($"ERROR: Missing file in input folder: {pair.Key}!");
                        return false;
                    }

                    var entry = new LooseFileEntry(pair.Value, pair.Key, path);

                    looseFile.Files.Add(entry);

                    Console.WriteLine($"Adding: \"{entry}\" to loose file...");
                }
            }
            else
            {
                var files = Directory.GetFiles(inputFolder, "*", SearchOption.AllDirectories);

                if (files.Length == 0)
                {
                    Console.WriteLine("ERROR: No files found in the input directory!");
                    return false;
                }

                foreach (var file in files)
                {
                    var entry = new LooseFileEntry(0, file.Substring(inputFolder.Length + 1), file);

                    looseFile.AddFile(entry);

                    Console.WriteLine($"Adding: \"{entry}\" to loose file...");
                }
            }

            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                looseFile.Write(fs);

            Console.WriteLine("Loose file successfully packed! Path: {0}", filePath);
            return true;*/
            Console.WriteLine("Temporarily disabled...");
            return false;
        }
    }
}
