using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.AI {
    public class BehaviorTreeExecutionNode : BehaviorTreeNode
    {
        private bool executeParallel;

        public BehaviorTreeExecutionNode(bool executeParallel = false) {
            this.executeParallel = executeParallel;
        }

        public override bool Evaluate(BehaviorTreeContext context) {
            return EvaluateChildren(context);
        }

        private IEnumerator EvaluateChildParallel(BehaviorTreeNode child, BehaviorTreeContext context) {
            child.Evaluate(context);

            yield return new WaitForSeconds(0.001f);
        }

        private bool EvaluateChildren(BehaviorTreeContext context) {
            bool continueChain = true;

            foreach(BehaviorTreeNode child in children) {
                if(continueChain && executeParallel) {
                    context.signalReceiver.StartCoroutine(EvaluateChildParallel(child, context));

                } else {
                    continueChain = continueChain && child.Evaluate(context);
                }
            }

            return continueChain;
        }
    }
}
