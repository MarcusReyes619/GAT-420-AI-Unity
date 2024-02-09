using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAtkState : AIState
{
    
    public AIAtkState(AIStateAgent agent) : base(agent)
    {
        AIStateTransition tran = new AIStateTransition(nameof(AIPatrolState));

        tran.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
        transitions.Add(tran);
    }

    public override void OnEnter()
    {
        agent.movement.Stop();
        agent.movement.Velocity = Vector3.zero;

        agent.animator?.SetTrigger("Attack");
        agent.timer.value = Time.time + 2;

    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        //if(Time.time >= timer)
        //{
        //    agent.stateMachine.SetState(nameof(AIIdleState));
        //}
    }

}
