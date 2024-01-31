using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAtkState : AIState
{
    public AIAtkState(AIStateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
       
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        Debug.Log("ATK!!!");
    }

}
