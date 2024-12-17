using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPCAction : ASimpleAction
{
    private float _playTime;
    private EmployeeBehaviour _programmerBehaviour;
    StateMachine _context;
    public PlayPCAction(IAgent agent, StateMachine sm) : base(agent) { _context = sm; }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Programador está jugando...");
        _playTime = Random.Range(10,25);
        _programmerBehaviour = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
        agent.GetComputer().OnBreak += OnComputerBroken;
        agent.GetComputer().SetScreensContent(ScreenContent.Working);
        if (agent.GetComputer().broken) OnComputerBroken();
        agent.SetBark("PlayPC");
        agent.SetAnimation("PlayPC");
        agent.GetComputer().SetScreensContent(ScreenContent.Slacking);
    }
    public override void Exit()
    {
        agent.GetComputer().OnBreak -= OnComputerBroken;
    }
    public override void FixedUpdate()
    {
    }
    public override void Update()
    {
        _playTime -= Time.deltaTime;

        if (agent.GetAgentVariable(_programmerBehaviour.Motivation) <= 100f)
        {
            agent.SetAgentVariable(_programmerBehaviour.Motivation, agent.GetAgentVariable(_programmerBehaviour.Motivation) + Time.deltaTime);
        }
        else { agent.SetAgentVariable(_programmerBehaviour.Motivation, 100f); }

        if (_playTime <= 0)
        {
            agent.SetAgentVariable(_programmerBehaviour.Boredom, Random.Range(0,10));
            Debug.Log("Programador ha terminado de jugar");
            finished = true;
        }
    }
    public void OnComputerBroken()
    {
        _playTime = 1000; //Se evitan transiciones indeseadas
        (agent as AgentBehaviour).currentIncidence = agent.GetComputer();
        Exit(); //Se hace el exit manualmente, pues al no poner finished = true en ningún momento no se va a hacer automaticamente
        _context.State = new ReportIncidenceState(_context, agent, new ProgrammerWorkState(_context, agent));
    }
}
