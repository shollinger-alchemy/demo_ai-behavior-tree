using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Alchemy.AI {
    public class BehaviorTreeAction : BehaviorTreeNode
    {
        private string actionId;

        public BehaviorTreeAction(string actionId) {
            this.actionId = actionId;
        }
        public override bool Evaluate(BehaviorTreeContext context) {
            context.signalReceiver.ReceiveSignal(actionId, context);
            
            return true;
        }

    }

    

    
}