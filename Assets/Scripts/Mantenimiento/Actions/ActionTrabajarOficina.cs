using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionTrabajarOficina : ASimpleAction
{
    private float _workTime;
    private StateMachine _context;
    public ActionTrabajarOficina(IAgent agent, StateMachine context) : base(agent) { _context = context; }
    
    public override void Enter()
    {
        base.Enter();
        agent.GetComputer().SetScreensContent(ScreenContent.Working);
        _workTime = Random.Range(5, 10);
        Debug.Log("Está trabajando en su ordenador...");
        agent.SetBark("MaintenanceWork");
        agent.SetAnimation("Work");
    }

    public override void Exit()
    {
        agent.GetComputer().SetScreensContent(ScreenContent.Off);
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        if (agent.GetComputer().broken) OnComputerBreak(); //Se hace en el Update porque en el caso del de mantenimiento da problemas al suscribirse a eventos
        _workTime -= Time.deltaTime;
        if (_workTime <= 0)
        {
            Debug.Log("Fin del trabajo");
            finished = true;
        }
    }

    private void OnComputerBreak()
    {
        _workTime = 1000;
        Exit();
        if ((agent as MaintenanceBehaviour).GetCurrentIncidence() != null) return;
        if(agent.GetChair().IsOccupied()) agent.GetChair().Leave();
        (agent as MaintenanceBehaviour).SetCurrentIncidence(agent.GetComputer());
        _context.State = new State_ExaminarIncidencia(_context, agent);
    }
}