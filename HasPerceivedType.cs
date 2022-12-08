using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.AI {
    public class HasPerceivedType<T> : PerceptionConditionalNode<T>
    {
        public HasPerceivedType(bool shouldMatch = true, string actionId = null, int instanceId = -1) : base(shouldMatch, actionId, instanceId){}  
        protected override bool TestPerceptionCondition(List<PerceptionRecord> perceptionRecords) {
            return perceptionRecords.Count > 0;
        }
    }
}
