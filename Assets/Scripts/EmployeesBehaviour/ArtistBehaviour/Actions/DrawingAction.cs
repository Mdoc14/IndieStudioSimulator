using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingAction : ASimpleAction
{
    private float _drawingTime;
    private EmployeeBehaviour _employeeBehavior;
    private StateMachine _context;

    public DrawingAction(IAgent agent, StateMachine sm) : base(agent) { _context = sm; }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Artista está dibujando...");
        _employeeBehavior = agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>();
        _drawingTime = Random.Range(10, 30);
        agent.GetComputer().OnBreak += OnComputerBroken;
        agent.GetComputer().SetScreensContent(ScreenContent.Working);
        if (agent.GetComputer().broken) OnComputerBroken();
        agent.SetBark("Draw");
        agent.SetAnimation("Draw");
    }
    public override void Exit()
    {
        Debug.Log("Artista ha terminado de dibujar");
        agent.GetComputer().OnBreak -= OnComputerBroken;
    }
    public override void FixedUpdate()
    {
    }
    public override void Update()
    {
        _drawingTime -= Time.deltaTime;

        if (agent.GetAgentVariable(_employeeBehavior.Motivation) >= 0f)
        {
            agent.SetAgentVariable(_employeeBehavior.Motivation, agent.GetAgentVariable(_employeeBehavior.Motivation) - Time.deltaTime);
        }
        else { agent.SetAgentVariable(_employeeBehavior.Motivation, 0f); }

        if (agent.GetAgentVariable(_employeeBehavior.Boredom) <= 100f)
        {
            agent.SetAgentVariable(_employeeBehavior.Boredom, agent.GetAgentVariable(_employeeBehavior.Boredom) + Time.deltaTime);
        }
        else { agent.SetAgentVariable(_employeeBehavior.Boredom, 100f); }

        if (agent.GetAgentVariable(_employeeBehavior.Stress) <= 100f)
        {
            agent.SetAgentVariable(_employeeBehavior.Stress, agent.GetAgentVariable(_employeeBehavior.Stress) + Time.deltaTime);
        }
        else { agent.SetAgentVariable(_employeeBehavior.Stress, 100f); }

        if (_drawingTime <= 0)
        {
            Debug.Log("Artista ha terminado de dibujar");
            finished = true;
        }
    }
    public void OnComputerBroken()
    {
        _drawingTime = 1000; //Se evitan transiciones indeseadas
        (agent as AgentBehaviour).currentIncidence = agent.GetComputer();
        Exit(); //Se hace el exit manualmente, pues al no poner finished = true en ningún momento no se va a hacer automaticamente
        _context.State = new ReportIncidenceState(_context, agent, new ArtistWorkState(_context, agent));
    }
}
