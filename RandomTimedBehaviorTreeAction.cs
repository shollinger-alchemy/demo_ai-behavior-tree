using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using System.Diagnostics;

namespace Alchemy.AI {
    public class RandomTimedBehaviorTreeAction : BehaviorTreeNode
    {
        private string actionId;
        private Vector2 secondsRange;
        private float timeToWait = 0f;

        private Stopwatch stopwatch;

        public RandomTimedBehaviorTreeAction(string actionId, Vector2 secondsRange) {
            this.actionId = actionId;
            this.secondsRange = secondsRange;
            
        }
        public override bool Evaluate(BehaviorTreeContext context) {
            if(stopwatch == null || !stopwatch.IsRunning) {

                this.timeToWait = Random.Range(secondsRange.x, secondsRange.y);
                stopwatch = new Stopwatch();
                stopwatch.Start();    
            }

            context.signalReceiver.StartCoroutine(FireActionAfterWait(context));
            
            return true;
        }

        private IEnumerator FireActionAfterWait(BehaviorTreeContext context) {
            if(timeToWait <= stopwatch.ElapsedMilliseconds / 1000) {
                stopwatch.Stop();

                context.signalReceiver.ReceiveSignal(actionId, context);

                yield break;
            } 

            yield return new WaitForSeconds(0.0001f);
        }

    }   
}