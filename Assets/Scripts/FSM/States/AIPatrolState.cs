using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolState : AIState
{
    Vector3 destination;
    public AIPatrolState(AIStateAgent agent) : base(agent)
    {
        AIStateTransition tran = new AIStateTransition(nameof(AIIdleState));

        tran.AddCondition(new FloatCondition(agent.destinationDistance, Condition.Predicate.LESS, 0));
        transitions.Add(tran);

        tran = new AIStateTransition(nameof(AIChaseState));
        tran.AddCondition(new BoolCondition(agent.enemySeen));
        transitions.Add(tran);


    }
    public override void OnEnter()
    {
       var navNode = AINavNode.GetRandomAINavNode();
        destination = navNode.transform.position;
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        agent.movement.MoveTowards(destination);
        
    }
}
