using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateAgent : AiAgent
{
    [SerializeField] AiPerception enemyPerception;

    AIStateMachine stateMachine = new AIStateMachine();

    private void Start()
    {
        stateMachine.AddState(nameof(AIIdleState), new AIIdleState(this));
        stateMachine.AddState(nameof(AIDeathState), new AIDeathState(this));
        stateMachine.AddState(nameof(AIPatrolState), new AIPatrolState(this));
        stateMachine.AddState(nameof(AIAtkState), new AIAtkState(this));

        stateMachine.SetState(nameof(AIIdleState));

        
    }

    private void Update()
    {
        var enemies = enemyPerception.GetGameObjects();
        if(enemies.Length > 0) { stateMachine.SetState(nameof(AIAtkState)); } 
        else
        {
            stateMachine.SetState(nameof(AIIdleState));
        }


        stateMachine.Update();
    }


   

}
