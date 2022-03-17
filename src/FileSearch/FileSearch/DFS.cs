using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearch
{
    public class DFS
    {
        public string root;
        public string goal;
        public List<string> solution;
        public Tree DFSTree;
        public bool found;
        
        public DFS(string root, string goal)
        {
            this.root = root;
            this.goal = goal;
            this.found = false;
            this.solution = new List<string>();
            this.DFSTree = new Tree();
            this.DFSTree.root = DFSRecursive(root);
            //this.addSolution();
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

            else if (folders == null) //tidak ada folder
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
                            this.found = true;
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

            else if (files == null)//tidak ada files
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

            else //ada file dan folder
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

                foreach (string file in files)
                {
                    fileName = PathUtil.removePath(file);
                    if (!this.found)
                    {
                        if (fileName == this.goal)
                        {
                            this.solution.Add(file);
                            DFS.root.AddChild(fileName, 2);
                            this.found = true;
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

        public void addSolution()
        {
            List<string> directory = new List<string>();
            string[] folder;
            foreach(string sol in solution)
            {
                directory.Add(PathUtil.splitPath(this.root, sol));
            }
            //iterasiin
            foreach (string dir in directory)
            {
                folder = dir.Split(Path.DirectorySeparatorChar);
                goalPath(folder, DFSTree.root);
            }
        }

        public void goalPath (string[] folder, TreeNode node)
        {   
            if (folder.Length != 0)
            {
                foreach(TreeNode child in node.children)
                {
                    if (node.name == folder[0])
                    {
                        node.SetCategory(2);
                        folder = folder.Skip(1).ToArray();
                        goalPath(folder, node);
                    }
                }
            }
        }
    }
}
