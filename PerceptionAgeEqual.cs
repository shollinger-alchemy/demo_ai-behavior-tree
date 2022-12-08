using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.AI {
    public class PerceptionAgeEqual<T> : PerceptionConditionalNode<T>
    {
        private Vector2 secondsUntilAgedRange;

        public PerceptionAgeEqual(Vector2 secondsUntilAgedRange, bool shouldMatch = true, string actionId = null, int instanceId = -1) : base(shouldMatch, actionId, instanceId){
            this.secondsUntilAgedRange = secondsUntilAgedRange;

        }
        protected override bool TestPerceptionCondition(List<PerceptionRecord> perceptionRecords) {
            float secondsUntilAged = UnityEngine.Random.Range(secondsUntilAgedRange.x, secondsUntilAgedRange.y);

            return perceptionRecords.Any((x)=> TimeElapsed(x) == secondsUntilAged);
        }
 
        private float TimeElapsed(PerceptionRecord perceptionRecord) {
            return perceptionRecord.perceptor.latestSampleTimestamp - perceptionRecord.timestamp;
        }
    }
}
