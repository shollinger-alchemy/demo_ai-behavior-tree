using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alchemy.AI {
    public class BehaviorTreeEntryNode : BehaviorTreeNode
    {
        public override bool Evaluate(BehaviorTreeContext context) {
            BehaviorTreeNode first = this.children.First();

            if(first != null) {
                return first.Evaluate(context);
            } else {
                return false;
            }
        }
    }
}
