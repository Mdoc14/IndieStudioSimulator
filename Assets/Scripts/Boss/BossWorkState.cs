using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWorkState : AState
{
    CompositeAction _workAction;
    private StateMachine _context;
    bool _forceUseComputer = false;
    bool initialized = false; //Indica si este estado ya ha sido inicializado anteriormente (importante para casos de apagón)
    public BossWorkState(StateMachine sm, IAgent agent, bool forceUseComputer = false) : base(sm, agent) { _context = sm; _forceUseComputer = forceUseComputer; }

    public override void Enter()
    {
        Debug.Log("ENTRANDO EN ESTADO DE TRABAJO...");
        WorldManager.Instance.SetWorkerActivity(true);
        context.PreviousStates.Push(this);
        if ((agent as AgentBehaviour).currentIncidence != null) context.State = new ReportIncidenceState(context, agent);
        //El jefe va a su silla y despu�s utiliza su ordenador o su tel�fono
        List<IAction> actions = new List<IAction>();
        if (!agent.GetChair().IsOccupied()) actions.Add(new GoToDeskAction(agent));
        if (!initialized)
        {
            if (Random.Range(0, 2) == 0 || _forceUseComputer)
            {
                actions.Add(new WorkAction(agent, _context));
                _forceUseComputer = true; //Para que si se va la luz al volver al estado se use el ordenador y no se ponga a llamar
            }
            else
            {
                actions.Add(new CallAction(agent));
                _forceUseComputer = false;
            }
        }
        else
        {
            if(_forceUseComputer) actions.Add(new WorkAction(agent, _context));
            else actions.Add(new CallAction(agent));
        }
        _workAction = new CompositeAction(actions);
        initialized = true;
    }

    public override void Exit()
    {
        Debug.Log("ESTADO DE TRABAJO FINALIZADO");
        WorldManager.Instance.SetWorkerActivity(false);
    }

    public override void FixedUpdate()
    {
        _workAction?.FixedUpdate();
    }

    public override void Update()
    {
        _workAction?.Update();
        if (_workAction.Finished)
        {
            agent.GetChair().Leave();
            float rand = Random.Range(0.0f, 1.0f);
            if (rand < 0.1f) context.State = new BathroomState(context, agent, new BossWorkState(context, agent));
            else if (rand < 0.35f) context.State = new BossReunionState(context, agent);
            else context.State = new PatrolState(context, agent);
        }
    }
}
