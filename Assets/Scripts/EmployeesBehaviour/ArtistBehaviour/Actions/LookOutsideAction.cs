using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookOutsideAction : ASimpleAction
{
    private float _lookOutsideTime;
    GameObject _lookOutsidePos;
    public LookOutsideAction(IAgent agent, GameObject lookOutsidePos) : base(agent) { _lookOutsidePos = lookOutsidePos; }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Artista esta mirando por la ventana...");
        agent.GetAgentGameObject().transform.rotation = _lookOutsidePos.transform.rotation;
        _lookOutsideTime = Random.Range(10, 30);
        agent.SetBark("Look");
        agent.SetAnimation("Looking");
    }
    public override void Exit()
    {
        Debug.Log("Artista ha terminado de mirar por la ventana");
    }
    public override void FixedUpdate()
    {
    }
    public override void Update()
    {
        _lookOutsideTime -= Time.deltaTime;

        if (agent.GetAgentVariable((agent as EmployeeBehaviour).Stress) >= 0f)
        {
            agent.SetAgentVariable((agent as EmployeeBehaviour).Stress, agent.GetAgentVariable((agent as EmployeeBehaviour).Stress) - Time.deltaTime);
        }
        else { agent.SetAgentVariable((agent as EmployeeBehaviour).Stress, 0f); }

        if (_lookOutsideTime <= 0)
        {
            finished = true;
        }
    }
}

