using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookOutsideAction : ASimpleAction
{
    private float _lookOutsideTime;
    private EmployeeBehaviour _employeeBehaviour;
    GameObject _lookOutsidePos;
    public LookOutsideAction(IAgent agent, GameObject smokePos) : base(agent) { _lookOutsidePos = smokePos; }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Programador está fumando...");
        agent.GetAgentGameObject().transform.rotation = _lookOutsidePos.transform.rotation;
        _lookOutsideTime = Random.Range(10, 50);
        _employeeBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
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
        _lookOutsideTime -= Time.deltaTime;

        if (agent.GetAgentVariable(_employeeBehaviour.Stress) >= 0f)
        {
            agent.SetAgentVariable(_employeeBehaviour.Stress, agent.GetAgentVariable(_employeeBehaviour.Stress) - Time.deltaTime);
        }
        else { agent.SetAgentVariable(_employeeBehaviour.Stress, 0f); }

        if (_lookOutsideTime <= 0)
        {
            finished = true;
        }
    }
}

