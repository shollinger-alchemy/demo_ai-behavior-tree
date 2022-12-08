using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.AI {
    public abstract class BehaviorTreeNode
    {
        protected List<BehaviorTreeNode> children;
        protected BehaviorTreeNode parent;

        public BehaviorTreeNode() {
            children = new List<BehaviorTreeNode>();
        }

        public abstract bool Evaluate(BehaviorTreeContext context);

        public BehaviorTreeNode AddChild(BehaviorTreeNode child) {
            children.Add(child);
            child.SetParent(this);

            return this;
        }

        public void SetParent(BehaviorTreeNode parent) {
            this.parent = parent;
        }

    }
}
