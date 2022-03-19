using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileCrawling
{
    public class BFS
    {
        public List<string> queue;
        public string root;
        public string goal;
        public List<string> solution;
        public Tree BFSTree;
        public bool found;
        public bool allOcc;

        public BFS(string root, string goal, bool allOcc)
        {
            this.queue = new List<string>();
            this.root = root;
            this.goal = goal;
            this.found = false;
            this.allOcc = allOcc;
            this.solution = new List<string>();
            this.BFSTree = new Tree();
            this.BFSTree.root = BFSSearch(root);
            PathUtil.addSolution(this.solution, this.root, BFSTree.root);
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

        public TreeNode BFSSearch(string path)
        {
            string[] folders;
            string[] files;
            string fileName, folderName, pathName;
            Tree BFS = new Tree();

            pathName = PathUtil.removePath(path);
            folders = PathUtil.FoldersInPath(path);
            files = PathUtil.FilesInPath(path);
            BFS.root = new TreeNode(pathName, 1);

            if (folders == null && files == null)
            {
                DFS.root.AddChild("EMPTY DIRECTORY", 1);
            }

            // Queue contains all the folders in the same level
            foreach (string folder in folders)
            {
                queue.Enqueue(folder);
            }

            while (queue.Count > 0 && !this.found)
            {
                string currentFolder = queue.Dequeue();
                string currentFolderName = PathUtil.removePath(currentFolder);

                string[] currentFolders = PathUtil.FoldersInPath(currentFolder);
                string[] currentFiles = PathUtil.FilesInPath(currentFolder);

                BFS.root.AddChild(currentFolderName, 1);


                if (currentFolders != null)
                {
                    foreach (string folder in currentFolders)
                    {
                        queue.Enqueue(folder);
                    }
                }

                if (files != null)
                {
                    foreach (string file in files)
                    {

                        fileName = PathUtil.removePath(file);

                        if (!this.found)
                        {
                            if (fileName == this.goal)
                            {
                                BFS.root.AddChild(fileName, 2);
                                this.solution.Add(file);
                                if (!this.allOcc)
                                {
                                    this.found = true;
                                }
                            }
                            else
                            {
                                BFS.root.AddChild(fileName, 1);
                            }
                        }
                        // file already found
                        else
                        {
                            BFS.root.AddChild(fileName, 0);
                        }
                    }
                }
            }
            return BFS.root;
        }
        
        public TreeNode findBFS(string path)
        {
            string[] folders;
            string[] files;
            string folderName, fileName, currentFolder;
            TreeNode currentNode;
            Tree BFS = new Tree();

            BFS.root = new TreeNode(path, 1);
            this.queue.Add(path);
            this.nodes.Add(BFS.root);

            while (this.queue.Count > 0 && !this.found)
            {
                currentFolder = this.queue[0];
                this.queue.RemoveAt(0);
                currentNode = this.nodes[0];
                this.nodes.RemoveAt(0);
                folders = PathUtil.FoldersInPath(currentFolder);
                files = PathUtil.FilesInPath(currentFolder);

                if (folders == null && files == null)
                {
                    currentNode.AddChild("EMPTY DIRECTORY", 1);
                }

                if (folders != null)
                {
                    foreach (string folder in folders)
                    {
                        this.queue.Add(folder);
                        folderName = PathUtil.removePath(folder);
                        var c = new TreeNode(folderName, 1);
                        this.nodes.Add(c);
                        currentNode.children.Add(c);
                    }
                }

                if (files != null)
                {
                    foreach (string file in files)
                    {
                        fileName = PathUtil.removePath(file);
                        if (!this.found)
                        {
                            if (fileName == this.goal)
                            {
                                currentNode.AddChild(fileName, 2);
                                this.solution.Add(file);
                                if (!this.allOcc)
                                {
                                    this.found = true;
                                }
                            }
                            else
                            {
                                currentNode.AddChild(fileName, 1);
                            }
                        }
                        else
                        {
                            currentNode.AddChild(fileName, 0);
                        }
                    }
                }
            }
            return BFS.root;
        }
    }
}
