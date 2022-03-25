using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileCrawling
{
    public class DFS
    {
        public string root;
        public string goal;
        public List<string> solution;
        public Tree DFSTree;
        public bool found;
        public bool allOcc;

        public DFS(string root, string goal, bool allOcc)
        {
            this.root = root;
            this.goal = goal;
            this.found = false;
            this.allOcc = allOcc;
            this.solution = new List<string>();
            this.DFSTree = new Tree();
            this.DFSTree.root = DFSRecursive(root);
            PathUtil.addSolution(this.solution, this.root, DFSTree.root);
            this.DFSTree.root.SetName(root);
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

        public void showSolution()
        {
            Console.WriteLine(this.solution[0]);
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
                DFS.root.AddChild("EMPTY DIRECTORY", 1);
            }
            
            if (folders != null)//tidak ada files
            {
                foreach (string folder in folders)
                {
                    if (!this.found)
                    {
                        DFS.root.children.Add(DFSRecursive(folder));
                    }
                    else
                    {
                        folderName = PathUtil.removePath(folder);
                        DFS.root.AddChild(folderName, 0);
                    }
                }
            }
            
            if (files != null) //tidak ada folder
            {
                foreach (string file in files)
                {
                    fileName = PathUtil.removePath(file);
                    if (!this.found)
                    {
                        if (fileName == this.goal)
                        {
                            this.solution.Add(file);
                            DFS.root.AddChild(fileName, 2);
                            if (!this.allOcc)
                            {
                                this.found = true;
                            }
                        }
                        else
                        {
                            DFS.root.AddChild(fileName, 1);
                        }
                    }
                    else
                    {
                        DFS.root.AddChild(fileName, 0);
                    }
                }
            }
            return DFS.root;
        }
    }
}
