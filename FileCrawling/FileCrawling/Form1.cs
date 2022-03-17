﻿using System;
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
        public Form1()
        {
            InitializeComponent();
        }
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
            foreach (TreeNode child in t.children)
            {
                if (child.category == 0)
                {
                    graph.AddEdge(t.name, child.name);
                }
                else if (child.category == 1)
                {
                    graph.AddEdge(t.name, child.name).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                }
                else
                {
                    graph.AddEdge(t.name, child.name).Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                }
                VisualizeTreeNode(child, graph);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tree = new Tree();

            string root;
            string goal;
            root = "D:/KULIAH WOY/AKADEMIK/Semester 4/Strategi Algoritma/Tubes 2/TEST";
            goal = "goal.txt";
            var d = new DFS(root, goal);
            Visualize(d.DFSTree);
        }
    }

}