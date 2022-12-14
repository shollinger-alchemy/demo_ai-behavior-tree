using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Alchemy.Core;
using Alchemy.AI;

/**
*  A sample behaviour script for an AI Agent that uses the Behaviour Tree.  The Setup method contains the Behaviour tree definition - it is where the Tree is created, with all parameters and configurables defined.
*  A full definition file would have all parameters driven from other places, but these values are defined in-line for brevity.
*/
public class BasicAggressive : MonoBehaviour
{
    public GameObject player;

    public Vector2 guardTime;

    public Vector2 peekDelayTime;
    public Vector2 peekTime;

    public Vector2 attackTime;

    public LayerMask poiLayer;

    private BehaviorTreeNode root;

    private List<Perceptor> perceptors;
    
    void Start()
    {
        perceptors = GetComponentsInChildren<Perceptor>().ToList();
        Setup();
    }

    void Update()
    {
        root.Evaluate(CreateContext());
    }

    private BehaviorTreeContext CreateContext() {
        BehaviorTreeContext context = ScriptableObject.CreateInstance<BehaviorTreeContext>();
        context.agent = GetComponent<AlchemyAgent>();
        context.signalReceiver = GetComponent<BehaviorTreeSignalReceiver>();

        return context;
    }
    
    /** General Agent Flow
    * 
    * [x] if [been in cover for x time], peek
    *  if [peeking] and [see player], attack
    * if [peeking] and [don't see player], move in
    *  if [don't see player] for x time, patrol
    */
    private BehaviorTreeNode Setup() {
        root = new BehaviorTreeEntryNode();

        root.AddChild(
            new BehaviorTreeSelectorNode().AddChild(
                new BehaviorTreeExecutionNode().AddChild(
                    new LatestAction("Peek")
                ).AddChild(
                    new PerceptionAgeLT<AlchemyPlayer>(new Vector2(1, 1))
                ).AddChild(
                    new BehaviorTreeAction("Attack")
                )
            ).AddChild(
                new BehaviorTreeExecutionNode().AddChild(
                    new LatestAction("Attack")
                ).AddChild(
                    new PerceptionAgeGT<AlchemyPlayer>(new Vector2(1, 1))
                ).AddChild(
                    new BehaviorTreeAction("Peek")
                )
            ).AddChild(
                new BehaviorTreeExecutionNode().AddChild(
                    new HasPerceivedType<AlchemyPlayer>()
                ).AddChild(
                    new PerceptionAgeGT<AlchemyPlayer>(new Vector2(10, 10), false)
                ).AddChild(
                    new BehaviorTreeSelectorNode().AddChild(
                        new BehaviorTreeExecutionNode().AddChild(
                            new LatestAction("Peek")
                        ).AddChild (
                            new PerceptionAgeGT<AlchemyAgent>(peekTime, true, "Peek", this.GetComponentInParent<AlchemyAgent>().gameObject.GetInstanceID())
                        ) 
                    ).AddChild(
                        new BehaviorTreeExecutionNode().AddChild(
                            new LatestAction("Attack")
                        ).AddChild(
                            new PerceptionAgeGT<AlchemyAgent>(attackTime, true, "Attack", this.GetComponentInParent<AlchemyAgent>().gameObject.GetInstanceID())
                        )
                    ).AddChild(
                        new BehaviorTreeSelectorNode().AddChild(
                            new LatestAction("Guard") 
                        ).AddChild(
                            new LatestAction("Patrol")
                        ).AddChild(
                            new LatestAction("Cover")
                        )
                    )
                ).AddChild(
                    new BehaviorTreeAction("Cover")
                ).AddChild(
                    new RandomTimedBehaviorTreeAction("Peek", peekDelayTime)
                )
            ).AddChild(
                 new BehaviorTreeExecutionNode().AddChild(
                    new AgentInRangeOfDestination()
                ).AddChild(
                     new BehaviorTreeSelectorNode().AddChild(
                         new HasPerceivedType<AlchemyPlayer>(false)
                     ).AddChild(
                         new PerceptionAgeGT<AlchemyPlayer>(new Vector2(10, 10))
                     )
                ).AddChild(
                    new BehaviorTreeAction("Guard")
                ).AddChild(
                    new RandomTimedBehaviorTreeAction("Patrol", guardTime)
                )
            )
        );

        return root;
    }

    

}
