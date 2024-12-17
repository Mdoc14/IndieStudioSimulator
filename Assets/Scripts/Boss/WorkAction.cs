using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class WorkAction : ASimpleAction
{
    private float _workTime;
    private StateMachine _context;

    public WorkAction(IAgent agent, StateMachine sm) : base(agent) { _context = sm; }

    public override void Enter()
    {
        base.Enter();
        _workTime = Random.Range(5, agent.GetAgentVariable("MaxWorkTime"));
        agent.SetBark("BossWork");
        agent.SetAnimation("Work");
        agent.GetComputer().OnBreak += OnComputerBroken;
        agent.GetComputer().SetScreensContent(ScreenContent.Working);
        if (agent.GetComputer().broken) OnComputerBroken();
    }

    public override void Exit()
    {
        agent.GetComputer().OnBreak -= OnComputerBroken;
        agent.GetComputer().SetScreensContent(ScreenContent.Off);
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        _workTime -= Time.deltaTime;
        if (_workTime <= 0)
        {
            finished = true;
        }
    }

    public void OnComputerBroken()
    {
        _workTime = 1000; //Se evitan transiciones indeseadas
        (agent as AgentBehaviour).currentIncidence = agent.GetComputer();
        Exit(); //Se hace el exit manualmente, pues al no poner finished = true en ningún momento no se va a hacer automaticamente
        _context.State = new ReportIncidenceState(_context, agent, new BossWorkState(_context, agent, true));
    }
}
