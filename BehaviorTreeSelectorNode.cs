using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Alchemy.AI {
    public class BehaviorTreeSelectorNode: BehaviorTreeNode
    {
        
        public override bool Evaluate(BehaviorTreeContext context) {
            bool selected = false;
            foreach(BehaviorTreeNode child in children) {
                selected = child.Evaluate(context);
                
                if(selected) {
                    break;
                };
            }

            return selected;
        }

    }
}

