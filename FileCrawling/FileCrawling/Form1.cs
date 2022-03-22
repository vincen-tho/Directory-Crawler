using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace FileCrawling
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string root;
        public string goal;
        public List<string> globalSol;
        public void Print()
        {
            //create a form 
            System.Windows.Forms.Form form = new Form1();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //create the graph content 
            graph.AddEdge("A", "B").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.AddEdge("B", "D").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.AddEdge("B", "E").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.AddEdge("C", "F").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.AddEdge("C", "G").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
            graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
            graph.FindNode("C").Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
            graph.FindNode("D").Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
            graph.FindNode("E").Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
            graph.FindNode("F").Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
            graph.FindNode("G").Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
            gViewer1.Graph = graph;
            //bind the graph to the viewer 
            //associate the viewer with the form 
        }
        public void Visualize(Tree t)
        {
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            VisualizeTreeNode(t.root, graph);

            gViewer1.Graph = graph;

            //associate the viewer with the form 
            //show the form 
            

        }
        public void VisualizeTreeNode(TreeNode t, Microsoft.Msagl.Drawing.Graph graph)
        {
            string vertexPName;
            string vertexCName;
            vertexPName = t.id.ToString() + " " + t.name;
            foreach (TreeNode child in t.children)
            {
  
                vertexCName = child.id.ToString() + " " + child.name;
                if (child.category == 0)
                {
                    
                    graph.AddEdge(vertexPName, vertexCName);
                }
                else if (child.category == 1)
                {
                    graph.AddEdge(vertexPName, vertexCName).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                }
                else
                {
                    graph.AddEdge(vertexPName, vertexCName).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                }
                VisualizeTreeNode(child, graph);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch SW = new Stopwatch();
            SW.Start();
            goal = textBox1.Text;
            linkLabel2.Links.Clear();
            if (radioButton1.Checked)
            {
                TreeNode.Reset();
                DFS d;
                if (checkBox1.Checked)
                {
                    d = new DFS(root, goal, true);
                }
                else
                {

                    d = new DFS(root, goal, false);
                }
                Visualize(d.DFSTree);
                if (d.found)
                {
                    linkLabel2.Enabled = true;
                }
                else
                {
                    linkLabel2.Enabled = false;
                    linkLabel2.Text = "Goal not found";
                }
                globalSol = new List<string>(d.solution);
            }
            else if (radioButton2.Checked)
            {
                // BFS DISINI
                TreeNode.Reset();
                BFS b;
                if (checkBox1.Checked)
                {
                    b = new BFS(root, goal, true);
                }
                else
                {

                    b = new BFS(root, goal, false);
                }
                Visualize(b.BFSTree);

                if (b.found)
                {
                    linkLabel2.Enabled = true;
                }
                else
                {
                    linkLabel2.Enabled = false;

                }
                globalSol = b.solution;
            }
            SW.Stop();
            label2.Text = "Time elapsed: " + SW.ElapsedMilliseconds + " ms";
            int curStart = 0;
            string txt;
            if (globalSol.Count() > 0) {
                linkLabel2.Text = "";
                linkLabel2.Enabled = true;
            
                for (int i = 0; i < globalSol.Count(); i++)
                {
                    txt = ("Goal" + (i+1).ToString() + "\n");
                    linkLabel2.Text += txt;
                    linkLabel2.Links.Add(curStart, curStart + (txt.Length - 1), txt);
                    curStart += txt.Length;

                }
            }
            else
            {
                linkLabel2.Enabled = false;
            }
      
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolder1 = new FolderBrowserDialog();
            openFolder1.ShowDialog();
            root = openFolder1.SelectedPath;
            string rootShow = root;
            int maxLength = 80;
            if (root.Length > maxLength)
            {
                rootShow = root.Substring(0, maxLength) + "...";
            }
            linkLabel1.Text = rootShow;
            linkLabel1.Enabled = true;

            
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            goal = textBox1.Text;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void gViewer1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", root);


        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int i = linkLabel2.Links.IndexOf(e.Link);
            string folderPath = Path.GetDirectoryName(globalSol[i]);
            System.Diagnostics.Process.Start("explorer.exe", folderPath);

        }
    }

}
