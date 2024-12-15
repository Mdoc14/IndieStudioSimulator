using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeBehaviour : AgentBehaviour
{

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
    ////////////////////////////////////////



    ////////////////////////////////////////
    /// FSM
    ////////////////////////////////////////
    public bool working;
    GameObject reunionChair;

    protected StateMachine _workerFSM = new StateMachine();
    ////////////////////////////////////////
    
    protected void Start()
    {
        agentVariables[_motivation] = Random.Range(0.8f, 1f);
        agentVariables[_stress] = Random.Range(0f, 0.2f);
        agentVariables[_boredom] = Random.Range(0f, 0.2f);
        agentVariables[_timeWithoutBath] = Random.Range(0f, 0.2f);
        agentVariables[_timeWithoutConsuming] = Random.Range(0f, 0.2f);

        InitializeGoBathAction();

        WorldManager.Instance.OnNotifyEmployeesStart += MeetingStarted;
    }

    void Update()
    {
        _workerFSM.UpdateBehaviour();
    }
    void FixedUpdate()
    {
        _workerFSM.FixedUpdateBehaviour();
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
    void InitializeGoBathAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor goBathFactor = new LeafFactor(this, _timeWithoutBath, 0, float.MaxValue, (100 / float.MaxValue), 0.6f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 1f, 0.3f, 1f, (x)=>(1 - x));

        leafFactors.Add(goBathFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor goBathNecessityFactor = new FusionFactor(new List<LeafFactor>(leafFactors), (180 / float.MaxValue));

        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToPositionAction(this, GameObject.Find("Bathroom").transform.position));
        actions.Add(new WaitingBathroomAction(this));
        actions.Add(new UseBathroomAction(this));
        CompositeAction bathroomAction = new CompositeAction(actions);

        goBathAction = new UtilityBasedAction(bathroomAction, goBathNecessityFactor);
        utilityActions.Add(goBathAction);
    }
}
