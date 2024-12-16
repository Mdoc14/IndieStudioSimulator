using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokingAction : ASimpleAction
{
    private float _smokeTime;
    private EmployeeBehaviour _programmerBehaviour;
    GameObject _smokePos;
    public SmokingAction(IAgent agent, GameObject smokePos) : base(agent) { _smokePos = smokePos; }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Programador está fumando...");
        agent.GetAgentGameObject().transform.rotation = _smokePos.transform.rotation;
        _smokeTime = Random.Range(10, 50);
        _programmerBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
        agent.SetBark("Smoke");
        agent.SetAnimation("Smoke");
    }
    public override void Exit()
    {
        Debug.Log("Programador ha terminado de fumar");
    }
    public override void FixedUpdate()
    {
    }
    public override void Update()
    {
        _smokeTime -= Time.deltaTime;

        if (agent.GetAgentVariable(_programmerBehaviour.Stress) >= 0f)
        {
            agent.SetAgentVariable(_programmerBehaviour.Stress, agent.GetAgentVariable(_programmerBehaviour.Stress) - Time.deltaTime);
        }
        else { agent.SetAgentVariable(_programmerBehaviour.Stress, 0f); }

        if (_smokeTime <= 0)
        {
            finished = true;
        }
    }
}
