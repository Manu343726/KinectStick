using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KinectStick.Commands;
using KinectStick.Instructions;

using System.Collections;

namespace KinectStick.Instructions
{
    public class InstructionsTreeNode
    {
        protected InstructionsTreeNode parent; 
        private ArrayList childs;
        private Command command;
        private int phraseIndex;
        private int level;

        public bool IsChild { get { return childs.Count == 0; } }
        public bool IsRoot { get { return parent == null; } }
        public int Level { get { return level; } }
        public InstructionsTreeNode Parent { get{ return parent; } }
        public Command Command { get { return command; } }
        public int PhraseIndex{get{return phraseIndex;}}

        public InstructionsTreeNode(InstructionsTreeNode parent)
        {
            this.parent = parent;
            this.phraseIndex=-1;
            this.childs=new ArrayList();
            this.command=null;

            if (parent == null)
                this.level = 0;
            else
                this.level = parent.Level + 1;
        }

        public InstructionsTreeNode(int phraseIndex,InstructionsTreeNode parent,Command command=null)
        {
            this.phraseIndex = phraseIndex;
            this.parent = parent;
            this.command = null;
            this.childs = new ArrayList();

            if (parent == null)
                this.level = 0;
            else
                this.level = parent.Level + 1;
        }

        public void AddChild(InstructionsTreeNode child)
        {
            child.parent = this;
            childs.Add(child);
        }

        public InstructionsTreeNode SearchNode(int phraseIndex)
        {
            foreach (InstructionsTreeNode child in childs)
                if (child.PhraseIndex == phraseIndex) return child;

            return null;
        }

        public void GenerateBranch(PreCompiledInstruction instruction)
        {
            int i=0;
            InstructionsTreeNode child;
            bool searching = true;

            this.phraseIndex = instruction.phraseIndices[this.level];

            if (this.level < instruction.Length - 1)
            {
                if (childs.Count > 0)
                {
                    while (i < childs.Count && searching)
                    {
                        child = ((InstructionsTreeNode)childs[i]);

                        if (instruction.phraseIndices[level + 1] == child.phraseIndex)
                        {
                            child.GenerateBranch(instruction);
                            searching = false;
                        }
                        else
                            ++i;
                    }

                    if (searching)
                    {
                        childs.Add(new InstructionsTreeNode(this));
                        ((InstructionsTreeNode)childs[childs.Count - 1]).GenerateBranch(instruction);
                    }
                }
                else
                {
                    childs.Add(new InstructionsTreeNode(this));
                    ((InstructionsTreeNode)childs[childs.Count - 1]).GenerateBranch(instruction);
                }
            }
            else
                this.command = instruction.command;
        }
    }

    public class InstructionsTree
    {
        private InstructionsTreeNode root;

        public int RootPrhaseIndex { get { return root.PhraseIndex; } }
        public InstructionsTreeNode Root { get { return root; } }

        public InstructionsTree(int rootPhraseIndex=-1, Command rootCommand = null)
        {
            root = new InstructionsTreeNode(rootPhraseIndex, null, rootCommand);
        }

        public InstructionsTreeNode SearchNode(int phraseIndex) { return root.SearchNode(phraseIndex); }
        public void GenerateBranch(PreCompiledInstruction instruction)
        {
            root.GenerateBranch(instruction);
        }
    }
}