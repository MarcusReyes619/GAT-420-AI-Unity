using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateAgent : AiAgent
{
    public Animator animator;
    public float health;
    public AIStateMachine stateMachine = new AIStateMachine();

    [SerializeField] public AiPerception enemyPerception;

    public ValueRef<float> helath = new ValueRef<float>();
    public ValueRef<float> timer = new ValueRef<float>();
    public ValueRef<float> destinationDistance = new ValueRef<float>();

    public ValueRef<bool> enemySeen = new ValueRef<bool>();
    public ValueRef<float> enemyDesomation = new ValueRef<float>();
    public ValueRef<float> enemyHP = new ValueRef<float>();

    public AIStateAgent enemy { get; private set; }
    private void Start()
    {
        stateMachine.AddState(nameof(AIIdleState), new AIIdleState(this));
        stateMachine.AddState(nameof(AIDeathState), new AIDeathState(this));
        stateMachine.AddState(nameof(AIPatrolState), new AIPatrolState(this));
        stateMachine.AddState(nameof(AIAtkState), new AIAtkState(this));
        stateMachine.AddState(nameof(AIChaseState), new AIChaseState(this));


        stateMachine.SetState(nameof(AIIdleState));

        
    }
    

    private void Update()
    {
        if(health <= 0)
        {
            stateMachine.SetState(nameof(AIDeathState));
        }

        var enemies = enemyPerception.GetGameObjects();

        timer.value -= Time.deltaTime;
        enemySeen.value = (enemies.Length > 0);

        if (enemySeen)
        {
            enemy = enemies[0].TryGetComponent(out AIStateAgent stateAgent) ? stateAgent : null;
            enemyDesomation.value = Vector3.Distance(transform.position, enemy.transform.position);
            enemyHP.value = enemy.enemyHP;
        }

        animator?.SetFloat("Speed", movement.Velocity.magnitude);

       

        //check for trastion 
        foreach (var transtion in stateMachine.CurrentState.transitions)
        {
            if (transtion.ToTransition())
            {
                stateMachine.SetState(transtion.nextState); break;
            }
        }
        stateMachine.Update();
    }


    private void OnGUI()
    {
        // draw label of current state above agent
        GUI.backgroundColor = Color.black;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        Rect rect = new Rect(0, 0, 100, 20);
        // get point above agent
        Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
        rect.x = point.x - (rect.width / 2);
        rect.y = Screen.height - point.y - rect.height - 20;
        // draw label with current state name
        GUI.Label(rect, stateMachine.CurrentState.name);
    }

    private void Attack()
    {
        // check for collision with surroundings
        var colliders = Physics.OverlapSphere(transform.position, 1);
        foreach (var collider in colliders)
        {
            // don't hit self or objects with the same tag
            if (collider.gameObject == gameObject || collider.gameObject.CompareTag(gameObject.tag)) continue;

            // check if collider object is a state agent, reduce health
            if (collider.gameObject.TryGetComponent<AIStateAgent>(out var stateAgent))
            {
                stateAgent.ApplyDamage(Random.Range(20, 50));
            }
        }
    }
    public void ApplyDamage(float damage)
    {
        health -= damage;
        if (health > 0) stateMachine.SetState(nameof(AIDeathState));
    }

}
