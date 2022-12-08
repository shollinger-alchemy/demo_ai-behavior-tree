using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.AI {
    public class PerceptionAgeLT<T> : PerceptionConditionalNode<T>
    {
        private Vector2 secondsUntilAgedRange;

        public PerceptionAgeLT(Vector2 secondsUntilAgedRange, bool shouldMatch = true, string actionId = null, int instanceId = -1) : base(shouldMatch, actionId, instanceId){
            this.secondsUntilAgedRange = secondsUntilAgedRange;

        }
        protected override bool TestPerceptionCondition(List<PerceptionRecord> perceptionRecords) {
            float secondsUntilAged = UnityEngine.Random.Range(secondsUntilAgedRange.x, secondsUntilAgedRange.y);

            return perceptionRecords.All((x)=> TimeElapsed(x) < secondsUntilAged);
        }
 
        private float TimeElapsed(PerceptionRecord perceptionRecord) {
            return perceptionRecord.perceptor.latestSampleTimestamp - perceptionRecord.timestamp;
        }
    }
}
