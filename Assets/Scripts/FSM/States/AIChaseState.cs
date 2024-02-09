using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseState : AIState
{
    float initialSpeed;
    public AIChaseState(AIStateAgent agent) : base(agent)
    {
        AIStateTransition tran = new AIStateTransition(nameof(AIAtkState));
        tran.AddCondition(new BoolCondition(agent.enemySeen));
        tran.AddCondition(new FloatCondition(agent.enemyDesomation, Condition.Predicate.LESS, 1));
        transitions.Add(tran);

        tran = new AIStateTransition(nameof(AIIdleState));
        tran.AddCondition(new BoolCondition(agent.enemySeen, false));
        transitions.Add(tran);
    }
    public override void OnEnter()
    {
        agent.movement.Resume();
        initialSpeed = agent.movement.maxSpeed;
        agent.movement.maxSpeed *= 2;
    }

    public override void OnExit()
    {
        agent.movement.maxSpeed = initialSpeed;
    }

    public override void OnUpdate()
    {
        if (agent.enemySeen) agent.movement.Destination = agent.enemy.transform.position;
    }
}
