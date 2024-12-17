using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProgrammingAction : ASimpleAction
{
    private float _programmingTime;
    private EmployeeBehaviour _programmerBehaviour;
    private StateMachine _context;

    public ProgrammingAction(IAgent agent, StateMachine sm) : base(agent) { _context = sm; }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Programador está programando...");
        _programmerBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
        _programmingTime = Random.Range(10, 30);
        agent.GetComputer().OnBreak += OnComputerBroken;
        agent.GetComputer().SetScreensContent(ScreenContent.Working);
        if (agent.GetComputer().broken) OnComputerBroken();
        agent.SetBark("Program");
        agent.SetAnimation("Program");
        agent.GetComputer().SetScreensContent(ScreenContent.Working);
    }
    public override void Exit()
    {
        Debug.Log("Programador ha llegado al escritorio");
        agent.GetComputer().OnBreak -= OnComputerBroken;
    }
    public override void FixedUpdate()
    {
    }
    public override void Update()
    {
        _programmingTime -= Time.deltaTime;

        if (agent.GetAgentVariable(_programmerBehaviour.Motivation) >= 0f) 
        {
            agent.SetAgentVariable(_programmerBehaviour.Motivation, agent.GetAgentVariable(_programmerBehaviour.Motivation) - Time.deltaTime);
        } 
        else{ agent.SetAgentVariable(_programmerBehaviour.Motivation, 0f); }

        if (agent.GetAgentVariable(_programmerBehaviour.Boredom) <= 100f)
        {
            agent.SetAgentVariable(_programmerBehaviour.Boredom, agent.GetAgentVariable(_programmerBehaviour.Boredom) + Time.deltaTime);
        }
        else { agent.SetAgentVariable(_programmerBehaviour.Boredom, 100f); }

        if (agent.GetAgentVariable(_programmerBehaviour.Stress) <= 100f)
        {
            agent.SetAgentVariable(_programmerBehaviour.Stress, agent.GetAgentVariable(_programmerBehaviour.Stress) + Time.deltaTime);
        }
        else { agent.SetAgentVariable(_programmerBehaviour.Stress, 100f); }

        if (_programmingTime <= 0)
        {
            Debug.Log("Programador ha terminado de programar");
            finished = true;
        }
    }
    public void OnComputerBroken()
    {
        _programmingTime = 1000; //Se evitan transiciones indeseadas
        (agent as AgentBehaviour).currentIncidence = agent.GetComputer();
        Exit(); //Se hace el exit manualmente, pues al no poner finished = true en ningún momento no se va a hacer automaticamente
        _context.State = new ReportIncidenceState(_context, agent, new ProgrammerWorkState(_context, agent));
    }
}
