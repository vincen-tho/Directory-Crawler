
using System;
namespace FileSearch
{
    public class Tes
    {
        public static void Main(string[] args)
        {
			var tree = new Tree();

			tree.root = new TreeNode("F1", 0);
			tree.root.AddChild("F2", 0);
			tree.root.AddChild("F3", 0);
			tree.root.children[0].AddChild("F4", 0);
			tree.root.children[0].AddChild("F5", 0);
			tree.root.children[0].AddChild("F6", 0);
			// Add child node [9,11] in node (1)
			tree.root.children[0].children[1].AddChild("F7", 0);
			tree.root.children[0].children[1].AddChild("F8", 0);
			// Add child node [7  8 3  4] in node (5)
			tree.root.children[1].AddChild("F9", 0);
			tree.root.children[1].AddChild("F10", 0);
			tree.root.children[1].AddChild("F11", 0);
			tree.root.children[1].AddChild("F12", 0);
			// Add child node [-7] in node (4)
			tree.root.children[1].children[0].AddChild("F13", 0);
			// Add child node [2,1,3] in node (7)
			tree.root.children[1].children[3].AddChild("F14", 0);
			tree.root.children[1].children[3].AddChild("F15", 0);
			tree.root.children[1].children[3].AddChild("F16", 0);
			tree.root.children[1].children[3].AddChild("F17", 0);
			Console.Write("\n  Directory : \n");
			// Print tree element
			tree.Print(tree.root);
		
	}
    }
}