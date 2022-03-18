using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearch
{
    public class BFS
    {
        public string root;
        public string goal;
        public List<string> solution;
        public Tree BFSTree;
        public bool found;

        public BFS(string root, string goal)
        {
            this.root = root;
            this.goal = goal;
            this.found = false;
            this.solution = new List<string>();
            this.BFSTree = new Tree();
            this.BFSTree.root = BFSRecursive(root);
            this.addSolution();
            this.BFSTree.root.SetName(root);
        }

        public void showTree()
        {
            this.BFSTree.Print(BFSTree.root);
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

        public TreeNode BFSRecursive(string path)
        {
            // Harus dimasukin ke dalam loop
            string[] folders;
            string[] files;
            string fileName, folderName, pathName;
            Tree BFS = new Tree();

            pathName = PathUtil.removePath(path);
            folders = PathUtil.FoldersInPath(path);
            files = PathUtil.FilesInPath(path);
            BFS.root = new TreeNode(pathName, 1);

            // Queue contains all the folders in the same level
            Queue<string> queue = new Queue<string>();
            foreach (string folder in folders)
            {
                folderName = PathUtil.removePath(folder);
                queue.Enqueue(folderName);
            }

            while (queue.Count > 0 && !this.found)
            {
                string currentFolderName = queue.Dequeue();
                BFS.root.AddChild(currentFolderName, 1);

                if (files != null)
                {
                    foreach (string file in files)
                    {

                        fileName = PathUtil.removePath(file);

                        if (!this.found)
                        {
                            if (fileName == this.goal)
                            {
                                this.found = true;
                                BFS.root.AddChild(fileName, 2);
                                this.solution.Add(file);
                            }
                            else
                            {
                                BFS.root.AddChild(fileName, 1);
                            }
                        }
                        // file already found
                        else
                        {
                            BFS.root.AddChild(fileName, 1);
                        }
                    }
                }

                if (folders != null)
                {
                    foreach (string folder in folders)
                    {
                        // harusnya current folder
                        folderName = PathUtil.removePath(folder);
                        queue.Enqueue(folderName);
                    }
                }
            }
        }
    }
}
