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

        InitializeGoBathAction();

        WorldManager.Instance.OnNotifyEmployeesStart += MeetingStarted;
    }

    protected virtual void Update()
    {
        agentVariables[_timeWithoutBath] += Time.deltaTime;
        agentVariables[_timeWithoutConsuming] += Time.deltaTime;
        
        _workerFSM.UpdateBehaviour();
        workerUS.UpdateBehaviour();
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

        _workerFSM.State = new MeetingState(_workerFSM, this, (AState)_workerFSM.State);
    }
    protected void InitializeGoBathAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor goBathFactor = new LeafFactor(this, _timeWithoutBath, 0, 120f, 0, 0.6f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 100f, 0, 0.4f,(x)=>(100-x));

        leafFactors.Add(goBathFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor goBathNecessityFactor = new FusionFactor(new List<LeafFactor>(leafFactors), 0.70f);

        Action action = () => { this.SetAgentVariable(this.GetAgentGameObject().GetComponent<EmployeeBehaviour>().TimeWithoutBath, 0); };

        ASimpleAction bathroomAction = new ChangeStateAction(_workerFSM,this, new BathroomState(_workerFSM, this, new ProgrammerWorkState(_workerFSM, this), action));

        goBathAction = new UtilityBasedAction(bathroomAction, goBathNecessityFactor);
        utilityActions.Add(goBathAction);
    }

    public StateMachine GetContext()
    {
        return _workerFSM;
    }
}
