using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
    
    public AIIdleState(AIStateAgent agent) : base(agent)
    {
        AIStateTransition tran = new AIStateTransition(nameof(AIPatrolState));

        tran.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
        transitions.Add(tran);

        tran = new AIStateTransition(nameof(AIChaseState));
        tran.AddCondition(new BoolCondition(agent.enemySeen));
        transitions.Add(tran);
    }
    public override void OnEnter()
    {
        //agent.movement.Stop();
        //agent.movement.Velocity = Vector3.zero;
        agent.timer.value = Time.time + Random.Range(1, 2);
        
        Debug.Log("idle enter");
    }

    public override void OnExit()
    {
        Debug.Log("idle exit");
    }

    public override void OnUpdate()
    {

        agent.timer.value -= Time.deltaTime;
    }
}
