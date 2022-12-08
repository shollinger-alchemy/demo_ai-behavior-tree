using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.AI {
    public class LatestAction : BehaviorTreeConditionalNode
    {
        private string actionId;
        public LatestAction(string actionId, bool shouldMatch = true) : base(shouldMatch) {
            this.actionId = actionId;
        }

        protected override bool TestCondition(BehaviorTreeContext context) {
            return context.signalReceiver.lastReceivedSignalId == actionId;
        }
    }
}
