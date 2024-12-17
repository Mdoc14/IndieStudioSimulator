using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingAction : ASimpleAction
{
    private float _photoTime;
    public ReadingAction(IAgent agent) : base(agent) { }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Guionista está leyendo...");
        _photoTime = Random.Range(10, 25);
        agent.SetBark("Read");
        agent.SetAnimation("Read");
    }
    public override void Exit()
    {
    }
    public override void FixedUpdate()
    {
    }
    public override void Update()
    {
        _photoTime -= Time.deltaTime;

        if (agent.GetAgentVariable((agent as EmployeeBehaviour).Stress) >= 0f)
        {
            agent.SetAgentVariable((agent as EmployeeBehaviour).Stress, agent.GetAgentVariable((agent as EmployeeBehaviour).Stress) - Time.deltaTime);
        }
        else { agent.SetAgentVariable((agent as EmployeeBehaviour).Stress, 0f); }

        if (_photoTime <= 0)
        {
            Debug.Log("Guionista ha terminado de leer");
            finished = true;
        }
    }
}
