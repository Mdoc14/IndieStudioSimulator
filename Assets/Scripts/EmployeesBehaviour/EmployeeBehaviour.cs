using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeBehaviour : AgentBehaviour
{
    public GameObject SmokePos;

    ////////////////////////////////////////
    /// UTILITY SYSTEM
    ////////////////////////////////////////

    protected UtilitySystem workerUS;
    public UtilitySystem WorkerUS { get { return workerUS; } }

    protected List<UtilityBasedAction> utilityActions = new List<UtilityBasedAction>();
    protected UtilityBasedAction goBathAction;

    protected string _boredom = "boredom";
    protected string _motivation = "motivation";
    protected string _stress = "stress";
    protected string _timeWithoutBath = "timeWithoutBath";
    protected string _timeWithoutConsuming = "timeWithoutConsuming";
    public string Boredom { get { return _boredom; } }
    public string Motivation { get { return _motivation; } }
    public string Stress { get { return _stress; } }
    public string TimeWithoutBath { get { return _timeWithoutBath; } }
    public string TimeWithoutConsuming { get { return _timeWithoutConsuming; } }

    GameObject _currentWaitingLine;

    public bool isSlacking = false;

    public int numScolds = 0;
    ////////////////////////////////////////



    ////////////////////////////////////////
    /// FSM
    ////////////////////////////////////////
    public bool working;
    GameObject reunionChair;

    protected StateMachine _workerFSM = new StateMachine();
    ////////////////////////////////////////
    
    protected virtual void Start()
    {
        agentVariables[_motivation] = UnityEngine.Random.Range(80f, 100f);
        agentVariables[_stress] = UnityEngine.Random.Range(0f, 20f);
        agentVariables[_boredom] = UnityEngine.Random.Range(0f, 20f);
        agentVariables[_timeWithoutBath] = 0;
        agentVariables[_timeWithoutConsuming] = 0;

        WorldManager.Instance.OnNotifyEmployeesStart += MeetingStarted;
    }

    protected virtual void Update()
    {
        agentVariables[_timeWithoutBath] += Time.deltaTime;
        agentVariables[_timeWithoutConsuming] += Time.deltaTime;
        
        _workerFSM.UpdateBehaviour();
        workerUS.UpdateBehaviour();
    }

    private void OnDisable()
    {
        WorldManager.Instance.OnNotifyEmployeesStart -= MeetingStarted;
    }

    protected virtual void FixedUpdate()
    {
        _workerFSM.FixedUpdateBehaviour();
        workerUS.FixedUpdateBehaviour();
    }
    public void SetReunionChair(GameObject chair)
    {
        reunionChair = chair;
    }
    public GameObject GetReunionChair() 
    {
        return reunionChair;
    }
    public void MeetingStarted()
    {
        if (working)
        {
            working = false;
            WorldManager.Instance.SetWorkerActivity(false);
        }
        if (GetChair().IsOccupied())
        {
            GetChair().Leave();
        }
        if (GetCurrentChair()!= null)
        {
            if (GetCurrentChair().IsOccupied())
            {
                GetCurrentChair().Leave();
            }
        }

        _workerFSM.State = new MeetingState(_workerFSM, this, new ProgrammerWorkState(_workerFSM, this));
    }
    protected virtual void InitializeGoBathAction(){}

    public StateMachine GetContext()
    {
        return _workerFSM;
    }

    public virtual void SetState(string stateName) { }
}
