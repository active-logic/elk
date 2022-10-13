using System;
using System.Collections.Generic;
using System.Text;
using Elk.Memory;

namespace Elk.Basic.Runtime{
public partial class CallGraph{

    Node root;
    Stack<Node> stack = new Stack<Node>();
    public Formatter format = new DefaultFormatter();

    public Elk.Stack CallStack(Cog client, Record record)
    => new StackTrace(stack.Peek(), client, record);

    public void Push(string arg, ulong id){
        var node = new Node(arg, id);
        var parent = stack.Count > 0 ? stack.Peek() : null;
        if(parent != null){
            parent.Add(node);
        }else{
            root = node;
        }
        stack.Push(node);
    }

    public string Peek() => stack.Peek().info;

    public void Pop(object returnValue){
        var str = format.ReturnValue(returnValue);
        var node = stack.Pop();
        node.info = str + " " + node.info;
    }

    public void PushPopProp(string name, object value){
        Push(format.Property(name, value), 0);
        stack.Pop();
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

    public class Node{

        public Node parent{ get; private set; }
        public string info;
        public readonly ulong id;
        public List<Node> children { get; private set; }

        public Node(string info, ulong id){
            this.info = info;
            this.id = id;
        }

        public void Add(Node arg){
            if(children == null) children = new List<Node>(2);
            children.Add(arg);
            arg.parent = this;
        }

    }

}}
