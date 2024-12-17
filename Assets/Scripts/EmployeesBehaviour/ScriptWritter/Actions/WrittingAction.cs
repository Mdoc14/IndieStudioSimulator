using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrittingAction : ASimpleAction
{
    private float _writtingTime;
    private StateMachine _context;

    public WrittingAction(IAgent agent, StateMachine sm) : base(agent) { _context = sm; }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Guionista está escribiendo...");
        _writtingTime = Random.Range(10, 30);
        agent.GetComputer().OnBreak += OnComputerBroken;
        agent.GetComputer().SetScreensContent(ScreenContent.Working);
        if (agent.GetComputer().broken) OnComputerBroken();
        agent.SetBark("Write");
        agent.SetAnimation("Write");
    }
    public override void Exit()
    {
        Debug.Log("Guionista ha terminado de escribir");
        agent.GetComputer().OnBreak -= OnComputerBroken;
    }
    public override void FixedUpdate()
    {
    }
    public override void Update()
    {
        _writtingTime -= Time.deltaTime;

        if (agent.GetAgentVariable((agent as EmployeeBehaviour).Motivation) >= 0f)
        {
            agent.SetAgentVariable((agent as EmployeeBehaviour).Motivation, agent.GetAgentVariable((agent as EmployeeBehaviour).Motivation) - Time.deltaTime);
        }
        else { agent.SetAgentVariable((agent as EmployeeBehaviour).Motivation, 0f); }

        if (agent.GetAgentVariable((agent as EmployeeBehaviour).Boredom) <= 100f)
        {
            agent.SetAgentVariable((agent as EmployeeBehaviour).Boredom, agent.GetAgentVariable((agent as EmployeeBehaviour).Boredom) + Time.deltaTime);
        }
        else { agent.SetAgentVariable((agent as EmployeeBehaviour).Boredom, 100f); }

        if (agent.GetAgentVariable((agent as EmployeeBehaviour).Stress) <= 100f)
        {
            agent.SetAgentVariable((agent as EmployeeBehaviour).Stress, agent.GetAgentVariable((agent as EmployeeBehaviour).Stress) + Time.deltaTime);
        }
        else { agent.SetAgentVariable((agent as EmployeeBehaviour).Stress, 100f); }

        if (_writtingTime <= 0)
        {
            Debug.Log("Guionista ha terminado de escribir");
            finished = true;
        }
    }
    public void OnComputerBroken()
    {
        _writtingTime = 1000; //Se evitan transiciones indeseadas
        (agent as AgentBehaviour).currentIncidence = agent.GetComputer();
        Exit(); //Se hace el exit manualmente, pues al no poner finished = true en ningún momento no se va a hacer automaticamente
        _context.State = new ReportIncidenceState(_context, agent, new ScriptWritterWorkState(_context, agent));
    }
}
