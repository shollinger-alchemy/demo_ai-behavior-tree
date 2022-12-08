using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace Alchemy.AI {

    [System.Serializable]
    public class BehaviorTreeSignal : UnityEvent<BehaviorTreeContext> {}

    [System.Serializable]
    public class BehaviorTreeSignalMapping {
        public string signalId;
        public BehaviorTreeSignal handlers;
    }

    [System.Serializable]
    public class BehaviorTreeSignalReceiver : MonoBehaviour
    {
        [SerializeField]
        public List<BehaviorTreeSignalMapping> signals;

        public string lastReceivedSignalId;
        public float latestSignalTimestamp = 0f;

        public void ReceiveSignal(string signalId, BehaviorTreeContext context)
        {
            BehaviorTreeSignalMapping match = signals.Find((x)=>signalId == x.signalId);

            context.signalId = signalId;

            if(match != null && !ShouldDampenSignal(signalId)) {
                match.handlers.Invoke(context);

                Debug.Log("Invoking Listeners for: " + signalId);
            }

            lastReceivedSignalId = signalId;
            latestSignalTimestamp = Time.time;

            PerceiveAction(context);
        }

        public void PerceiveAction(BehaviorTreeContext context) {
            foreach(Perceptor perceptor in GetComponentsInChildren<Perceptor>()) {
                perceptor.RecordAgentAction(context);
            }
        }

        public void ResetDampener() {
            lastReceivedSignalId = null;
        }

        private bool ShouldDampenSignal(string signalId) {
            return lastReceivedSignalId == signalId;
        }
    }
}
