using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.AI {
    public abstract class PerceptionConditionalNode<T> : BehaviorTreeConditionalNode
    {
        protected int instanceId = -1;
        protected string actionId = null;

        protected abstract bool TestPerceptionCondition(List<PerceptionRecord> perceptionRecords);

        public PerceptionConditionalNode(bool shouldMatch = true, string actionId = null, int instanceId = -1) : base(shouldMatch) {
            this.instanceId = instanceId;
            this.actionId = actionId;
        }

        protected override bool TestCondition(BehaviorTreeContext context) {
           return TestPerceptionCondition(FilteredPerceptionRecords(context.agent.GetPerceptionRecordsWithComponent<T>()));
        }

        protected List<PerceptionRecord> FilteredPerceptionRecords(List<PerceptionRecord> perceptionRecords) {
            List<PerceptionRecord> filtered = perceptionRecords;
            if(this.instanceId > -1) {
                filtered = filtered.FindAll((x) => x.perceivedObject.GetInstanceID() == this.instanceId);
            }

            if(this.actionId != null) {
                filtered = filtered.FindAll((x) => x.perceivedActionId == this.actionId);
            }

            return filtered;
        }
    }
}
