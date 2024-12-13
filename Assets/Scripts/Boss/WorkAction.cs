using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkAction : ASimpleAction
{
    private float _workTime;
    public WorkAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        _workTime = Random.Range(5, agent.GetAgentVariable("MaxWorkTime"));
        Debug.Log("El jefe está trabajando en su ordenador...");
        agent.SetBark("BossWork");
        agent.SetAnimation("Work");
        WorldManager.Instance.SetWorkerActivity(true);
    }

    public override void Exit()
    {
        WorldManager.Instance.SetWorkerActivity(false);
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        _workTime -= Time.deltaTime;
        if (_workTime <= 0)
        {
            Debug.Log("Fin del trabajo");
            finished = true;
        }
    }
}
