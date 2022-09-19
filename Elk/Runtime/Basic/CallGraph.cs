using System;
using System.Collections.Generic;
using System.Text;

namespace Elk.Basic{
public class CallGraph{

    Node root;
    Stack<Node> stack = new Stack<Node>();
    public Func<object, string> returnValueFormatter;

    public void Push(string arg){
        var node = new Node(arg);
        var parent = stack.Count > 0 ? stack.Peek() : null;
        if(parent != null){
            parent.Add(node);
        }else{
            root = node;
        }
        stack.Push(node);
    }

    public void Pop(object returnValue, bool found){
        var str = returnValueFormatter?.Invoke(returnValue)
                  ?? returnValue?.ToString() ?? "null";
        var node = stack.Pop();
        node.info = str + " " + node.info + (!found ? " [not found]" : null);
    }

    public string Format(){
        var @out = new StringBuilder();
        Format(root, @out, null);
        return @out.ToString();
    }

    void Format(Node node, StringBuilder @out, string spaces){
        @out.Append(spaces);
        @out.Append(node.info);
        @out.Append("\n");
        if(node.children == null) return;
        foreach(var child in node.children)
            Format(child, @out, spaces + "  ");
    }

    // =============================================================

    class Node{

        public string info;
        public List<Node> children;

        public Node(string info) => this.info = info;

        public void Add(Node arg){
            if(children == null) children = new List<Node>(2);
            children.Add(arg);
        }

    }

}}
