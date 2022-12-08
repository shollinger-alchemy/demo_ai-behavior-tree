using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Alchemy.AI {
    public class BehaviorTreeContext : ScriptableObject
    {
        public string signalId;
        public BehaviorTreeSignalReceiver signalReceiver;
        public AlchemyAgent agent;

    }
}
