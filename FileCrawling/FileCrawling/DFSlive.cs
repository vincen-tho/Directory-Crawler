using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileCrawling
{
    public partial class Form1 : Form
    {
        public class DFSlive : Form1
        {
            public string rootX;
            public string goalX;
            public List<string> solution;
            public Tree DFSTree;
            public bool found;
            public bool allOcc;

            public DFSlive(string root, string goal, bool allOcc)
            {
                Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
                this.rootX = root;
                this.goalX = goal;
                this.found = false;
                this.allOcc = allOcc;
                this.solution = new List<string>();
                this.DFSTree = new Tree();
                this.DFSTree.root = DFSRecursive(root, graph);
                PathUtil.addSolution(this.solution, this.rootX, DFSTree.root);
                this.DFSTree.root.SetName(root);
                gViewer1.Graph = graph;
            }

            public void showTree()
            {
                this.DFSTree.Print(DFSTree.root);
            }

            public void showRoot()
            {
                Console.WriteLine(this.rootX);
            }

            public void showGoal()
            {
                Console.WriteLine(this.goalX);
            }

            public void showSolution()
            {
                Console.WriteLine(this.solution[0]);
            }

            public TreeNode DFSRecursive(string path, Microsoft.Msagl.Drawing.Graph graph)
            {
                string[] folders;
                string[] files;
                string fileName, folderName, pathName;
                Tree DFSlive = new Tree();

                pathName = PathUtil.removePath(path);
                folders = PathUtil.FoldersInPath(path);
                files = PathUtil.FilesInPath(path);
                DFSlive.root = new TreeNode(pathName, 1);


                string vertexPName;
                string vertexCName;
                int tempID;
                vertexPName = DFSlive.root.id.ToString() + " " + DFSlive.root.name;
                tempID = TreeNode.getAccessedCount();
                if (folders == null && files == null)
                {
                    DFSlive.root.AddChild("EMPTY DIRECTORY", 1);
                }

                else if (folders == null) //tidak ada folder
                {

                    foreach (string file in files)
                    {
                        fileName = PathUtil.removePath(file);
             
                        vertexCName = tempID.ToString() + " " + fileName;
                        if (!this.found)
                        {
                            if (fileName == this.goalX)
                            {
                                this.solution.Add(file);
                                DFSlive.root.AddChild(fileName, 2);
                                graph.AddEdge(vertexPName, vertexCName);
                                
                                if (!this.allOcc)
                                {
                                    this.found = true;
                                }
                            }
                            else
                            {
                                DFSlive.root.AddChild(fileName, 1);
                                graph.AddEdge(vertexPName, vertexCName);
                            }
                        }
                        else
                        {
                            DFSlive.root.AddChild(fileName, 0);
                            graph.AddEdge(vertexPName, vertexCName);
                        }

                    }
            
                }

                else if (files == null)//tidak ada files
                {
                    foreach (string folder in folders)
                    {
                        vertexCName = tempID.ToString() + " " + folder;
                        if (!this.found)
                        {
                            
                            DFSlive.root.children.Add(DFSRecursive(folder, graph));
                            graph.AddEdge(vertexPName, vertexCName);
                        }
                        else
                        {
                            folderName = PathUtil.removePath(folder);
                            vertexCName = tempID.ToString() + " " + folderName;
                            DFSlive.root.AddChild(folderName, 0);
                            graph.AddEdge(vertexPName, vertexCName);
                        }
                        gViewer1.Graph = graph;
                    }
                    gViewer1.Graph = graph;
                }

                else //ada file dan folder
                {
                    foreach (string folder in folders)
                    {
                        vertexCName = tempID.ToString() + " " + folder;
                        if (!this.found)
                        {
                            DFSlive.root.children.Add(DFSRecursive(folder, graph));
                            graph.AddEdge(vertexPName, vertexCName);
                        }
                        else
                        {
                            folderName = PathUtil.removePath(folder);
                            vertexCName = tempID.ToString() + " " + folderName;
                            DFSlive.root.AddChild(folderName, 0);
                            graph.AddEdge(vertexPName, vertexCName);
                        }
                        gViewer1.Graph = graph;
                    }

                    foreach (string file in files)
                    {
                        
                        fileName = PathUtil.removePath(file);
                        vertexCName = tempID.ToString() + " " + fileName;
                        if (!this.found)
                        {
                            if (fileName == this.goalX)
                            {
                                this.solution.Add(file);
                                DFSlive.root.AddChild(fileName, 2);
                                graph.AddEdge(vertexPName, vertexCName);
                                if (!this.allOcc)
                                {
                                    this.found = true;
                                }
                            }
                            else
                            {
                                DFSlive.root.AddChild(fileName, 1);
                                graph.AddEdge(vertexPName, vertexCName);
                            }
                            gViewer1.Graph = graph;
                        }
                        else
                        {
                            DFSlive.root.AddChild(fileName, 0);
                            graph.AddEdge(vertexPName, vertexCName);
                        }
                        gViewer1.Graph = graph;
                    }
                }
                gViewer1.Graph = graph;

                return DFSlive.root;
            }
        }
    }
}
