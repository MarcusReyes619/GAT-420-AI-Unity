using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine 
{
    private Dictionary<string, AIState> states = new Dictionary<string, AIState>();
    public AIState CurrentState { get; private set; } = null;

    public void Update()
    {
        CurrentState?.OnUpdate();
    }

    public void AddState(string name, AIState state)
    {
        Debug.Assert(!states.ContainsKey(name), "State machine already contains state " + name);
        states[name] = state;
    }

    public void SetState(string name)
    {
        Debug.Assert(states.ContainsKey(name), "State machine does not  contains state " + name);

        var nextState = states[name];

        //don't re-enter state
        if (CurrentState == nextState) return;

        //exit current state
        CurrentState?.OnExit();
        //sets new state
        CurrentState = nextState;
        //enters nexts states
        CurrentState?.OnEnter();

    }


}
