using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCrawling
{
    public class DFS
    {
        public string root;
        public string goal;
        public Tree DFSTree;
        public bool found;

        public DFS(string root, string goal)
        {
            this.root = root;
            this.goal = goal;
            this.found = false;
            this.DFSTree = new Tree();
            this.DFSTree.root = DFSRecursive(root);
        }

        public void showTree()
        {
            this.DFSTree.Print(DFSTree.root);
        }

        public void showRoot()
        {
            Console.WriteLine(this.root);
        }

        public void showGoal()
        {
            Console.WriteLine(this.goal);
        }

        public TreeNode DFSRecursive(string path)
        {
            string[] folders;
            string[] files;
            string fileName, folderName, pathName;
            Tree DFS = new Tree();

            pathName = PathUtil.removePath(path);
            folders = PathUtil.FoldersInPath(path);
            files = PathUtil.FilesInPath(path);
            DFS.root = new TreeNode(pathName, 1);

            if (folders == null && files == null)
            {
                return DFS.root;
            }
            else if (folders == null) //tidak ada folder
            {
                foreach (string file in files)
                {
                    fileName = PathUtil.removePath(file);
                    if (fileName == this.goal)
                    {
                        DFS.root.AddChild(fileName, 2);
                        this.found = true;
                        break;
                    }
                    DFS.root.AddChild(fileName, 1);
                }
                return DFS.root;
            }

            else if (files == null)//tidak ada files
            {
                foreach (string folder in folders)
                {
                    DFS.root.children.Add(DFSRecursive(folder));
                    if (this.found)
                    {
                        break;
                    }
                }
                return DFS.root;
            }

            else //ada file dan folder
            {
                foreach (string folder in folders)
                {
                    DFS.root.children.Add(DFSRecursive(folder));
                    if (this.found)
                    {
                        break;
                    }
                }

                foreach (string file in files)
                {
                    if (this.found)
                    {
                        break;
                    }
                    fileName = PathUtil.removePath(file);
                    if (fileName == this.goal)
                    {
                        DFS.root.AddChild(fileName, 2);
                        this.found = true;
                        break;
                    }
                    DFS.root.AddChild(fileName, 1);
                }
                return DFS.root;
            }
        }
    }
}
