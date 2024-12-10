using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharactersBehaviour;

public class JanitorRestState : AState
{
    float timer;
    string restVariableName;

    public JanitorRestState(StateMachine sm, IAgent agent, string restVariable) : base(sm, agent) 
    {
        restVariableName = restVariable;
    }

    public override void Enter()
    {
        Debug.Log("el conserje está descansando...");
        //Iniciar animacion de descansar
        timer = 0f;

    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= agent.GetAgentVariable(restVariableName)) 
        {
            Debug.Log("el conserje ya terminó de descansar...");
            context.State = new JanitorWalkState(context, agent, agent.GetAgentGameObject().GetComponent<JanitorBehaviour>().GetOfficeRooms());
        }
    }
}
