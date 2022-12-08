using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.AI {
    public abstract class BehaviorTreeConditionalNode : BehaviorTreeNode
    {
        protected bool matchTrue;
        public BehaviorTreeConditionalNode(bool matchTrue = true) {
            this.matchTrue = matchTrue;
        }
        public override bool Evaluate(BehaviorTreeContext context) {
            bool result = TestCondition(context);
            return matchTrue ? result : !result;
        }

        protected abstract bool TestCondition(BehaviorTreeContext context);
    }
}
