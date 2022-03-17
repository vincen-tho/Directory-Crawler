﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearch
{
    public class TreeNode
    {
        public string name;
        public int category; // 0 = in queue, 1 = accessed node, 2 = goal path
        public List<TreeNode> children;

        public TreeNode(string name, int category)
        {
            this.name = name;
            this.category = category;
            this.children = new List<TreeNode>();
        }
        public void AddChild(string name, int category)
        {
            var c = new TreeNode(name, category);
            this.children.Add(c);
        }
    }

    public class Tree
    {
        public TreeNode root;
        public Tree()
        {
            this.root = null;
        }
        public void Print(TreeNode t)
        {
            if (t == null)
            {
                Console.Write("EMPTY DIRECTORY\n");
            }
            else
            {
                PrintLevel(t, 0);

            }
        }
        public void PrintLevel(TreeNode t, int level)
        {
            for (int i = 0; i < level; i++)
            {
                Console.Write("  ");
            }
            if (level != 0)
            {
                Console.Write("|-");
            }
            Console.WriteLine(t.name);
            
            foreach (TreeNode child in t.children)
            {
                PrintLevel(child, level+1);
            }

        }
    }
}