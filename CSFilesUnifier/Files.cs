using System;
using System.Collections.Generic;
using System.IO;

namespace CSFilesUnifier
{
    public static class Files
    {
        static List<string> files = new List<string>();
        static List<string> usings = new List<string>();
        static List<string> allFiles = new List<string>();

        // Method for searching in selected directory 
        public static List<string> FindFilesInDirectory(string directory)
        {
            string[] selectedFiles = Directory.GetFiles(directory);
            string[] selectedDirectories = Directory.GetDirectories(directory);

            if (directory != null && directory != string.Empty)
            {
                foreach (var dir in selectedDirectories)
                {
                    if (!dir.ToLower().Contains("bin") && !dir.ToLower().Contains("obj") && !dir.ToLower().Contains("properties"))
                    {
                        string[] selectedFilesFromThisDirectory = Directory.GetFiles(dir);
                        FindFilesInFiles(selectedFilesFromThisDirectory);
                    }
                }

                FindFilesInFiles(selectedFiles);
            }
            return files;
        }

        // Searching in folders without containing folders
        public static List<string> FindFilesInFiles(string[] str)
        {
            foreach (var file in str)
            {
                if (file.EndsWith(".cs") && !files.Contains(file) && !file.Contains("AssemblyInfo.cs"))
                {
                    files.Add(file);
                }
            }

            return files;
        }

        // Method for cleaning the files from "using"-s
        public static List<string> CleanFiles(List<string> str)
        {
            foreach (var item in str)
            {
                using (StreamReader reader = new StreamReader(item))
                {
                    string line = reader.ReadLine();

                    while (line != null)
                    {
                        if (line.StartsWith("using") && !usings.Contains(line))
                        {
                            usings.Add(line);
                            line = reader.ReadLine();
                        }
                        else if (line.StartsWith("using") && (item != str[0]))
                        {
                            line = reader.ReadLine();
                        }
                        else
                        {
                            allFiles.Add(line);
                            line = reader.ReadLine();
                        }
                    }
                }
            }

            return str;
        }

        // "add using"-s on top
        public static void AddUsingsInNewFile()
        {
            using (StreamWriter writer = new StreamWriter(Environment.ExpandEnvironmentVariables(@"%UserProfile%\Desktop\text.cs"), true))
            {
                foreach (var item in usings)
                {
                    writer.WriteLine(item);
                }
                writer.WriteLine();
            }
        }

        // Unify the clean files in one
        public static void Unify()
        {
            AddUsingsInNewFile();
            using (StreamWriter writer = new StreamWriter(Environment.ExpandEnvironmentVariables(@"%UserProfile%\Desktop\text.cs"), true))
            {
                for (int i = 0; i < allFiles.Count; i++)
                {
                    string line = allFiles[i];
                    writer.WriteLine(line);
                }
            }
        }
    }
}
