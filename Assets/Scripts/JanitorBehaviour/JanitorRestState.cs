using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharactersBehaviour;
using UnityEngine.AI;

public class JanitorRestState : AState
{
    float timer;
    string restVariableName;
    NavMeshAgent navMeshAgent;
    bool isOnChair = false;

    public JanitorRestState(StateMachine sm, IAgent agent, string restVariable) : base(sm, agent) 
    {
        restVariableName = restVariable;
    }

    public override void Enter()
    {
        GameObject.FindObjectOfType<LightSwitch>().LightsOut += OnLightsOff;
        GameObject.FindObjectOfType<LightSwitch>().LightsOn += OnLightsOn;
        //El conserje debe estar en su oficina para poder dormir
        navMeshAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(agent.GetChair().transform.position);
        agent.SetBark("Walk");
        agent.SetAnimation("Walk");

        //Iniciar animacion de descansar
        Debug.Log("El conserje descansará " + agent.GetAgentVariable(restVariableName) + " segundos");
        timer = 0f;
    }

    public override void Exit()
    {
        if(!GameObject.FindObjectOfType<LightSwitch>().IsOn) (agent as JanitorBehaviour).ToggleFlashlight(true);
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        //Cuando llegue a su silla, el conserje comenzara a descansar
        if (!isOnChair && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            //navMeshAgent.isStopped = true;
            //navMeshAgent.ResetPath();

            agent.GetChair().Sit(agent.GetAgentGameObject());
            isOnChair = true;
            agent.SetBark("Sleep");
            agent.SetAnimation("Idle");
            GameObject.FindObjectOfType<LightSwitch>().LightsOut -= OnLightsOff;
            GameObject.FindObjectOfType<LightSwitch>().LightsOn -= OnLightsOn;
            (agent as JanitorBehaviour).ToggleFlashlight(false);
            //navMeshAgent.isStopped = true;
        }

        //Al descansar, comenzara a contar el tiempo
        if (isOnChair)
        {
            timer += Time.deltaTime;
            if (timer >= agent.GetAgentVariable(restVariableName))
            {
                Debug.Log("el conserje ya terminó de descansar...");
                agent.GetChair().Leave();
                context.State = new JanitorWalkState(context, agent, agent.GetAgentGameObject().GetComponent<JanitorBehaviour>().GetOfficeRooms());
            }
        }
    }

    private void OnLightsOff()
    {
        (agent as JanitorBehaviour).ToggleFlashlight(true);
    }

    private void OnLightsOn()
    {
        (agent as JanitorBehaviour).ToggleFlashlight(false);
    }
}
