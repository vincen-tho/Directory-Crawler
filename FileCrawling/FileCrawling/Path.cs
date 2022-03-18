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
        
        public static void addSolution(List<string> solution, string root, TreeNode treeRoot)
        {
            List<string> directory = new List<string>();
            string[] folder;
            foreach (string sol in solution)
            {
                directory.Add(PathUtil.splitPath(root, sol));
            }
            //iterasiin
            foreach (string dir in directory)
            {
                folder = dir.Split(Path.DirectorySeparatorChar);
                folder = folder.Skip(1).ToArray();
                goalPath(folder, treeRoot);
            }
        }

        public static void goalPath(string[] folder, TreeNode node)
        {
            if (folder.Length != 0)
            {
                foreach (TreeNode child in node.children)
                {
                    if (child.name == folder[0])
                    {
                        child.SetCategory(2);
                        folder = folder.Skip(1).ToArray();
                        goalPath(folder, child);
                        break;
                    }
                }
            }
        }
    }
}
