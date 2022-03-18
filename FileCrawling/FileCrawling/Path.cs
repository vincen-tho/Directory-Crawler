using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileCrawling
{
    public class PathUtil
    {
        public static string[] FoldersInPath(string workingDirectory)
        {
            try
            {
                return Directory.GetDirectories(workingDirectory);
            }
            catch
            {
                return null;
            }
        }

        public static string[] FilesInPath(string workingDirectory)
        {
            try
            {
                return Directory.GetFiles(workingDirectory);
            }
            catch
            {
                return null;
            }
        }

        public static string removePath(string path)
        {
            return Path.GetFileName(path);
        }

        public static string addPath(string path, string child)
        {
            return Path.Combine(path, child);
        }

        public static string splitPath(string root, string path)
        {
            int pos = root.Length;
            return path.Remove(0, pos);
        }
    }
}
