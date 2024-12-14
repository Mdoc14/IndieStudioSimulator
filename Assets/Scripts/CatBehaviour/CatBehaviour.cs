using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour : AgentBehaviour
{
    StateMachine sm = new StateMachine();

    UtilitySystem us;
    public UtilitySystem US { get { return us; } }

    List<UtilityBasedAction> utilityActions = new List<UtilityBasedAction>();

    UtilityBasedAction goBathUAction;
    UtilityBasedAction purgueUAction;
    UtilityBasedAction eatUAction;
    UtilityBasedAction sleepUAction;
    UtilityBasedAction playUAction;
    UtilityBasedAction bePet;

    [SerializeField] Chair _catBed;
    public Chair CatBed { get { return _catBed; } }

    [SerializeField] Chair _catLitterBox;

    string _boredom = "boredom";
    string _tiredness = "tiredness";
    string _timeWithoutBath = "timeWithoutBath";
    string _timeWithoutPurguing = "timeWithoutPurguing";

    public string Boredom { get { return _boredom; } }
    public string Tiredness { get { return _tiredness; } }
    public string TimeWithoutBath { get { return _timeWithoutBath; } }
    public string TimeWithoutPurguing { get { return _timeWithoutPurguing; } }

    // Start is called before the first frame update
    void Start()
    {
        SetCurrentChair(_catLitterBox);

        agentVariables[_boredom] = 0f;
        agentVariables[_tiredness] = 0f;
        agentVariables[_timeWithoutBath] = 0f;

        sm.State = new WanderingState(sm, this);

        InitializeGoBathUAction();
        InitializeSleepUAction();

        us = new UtilitySystem(new List<UtilityBasedAction>(utilityActions));
    }

    // Update is called once per frame
    void Update()
    {
        agentVariables[_timeWithoutBath] += Time.deltaTime;

        us.UpdateBehaviour();
        sm.UpdateBehaviour();
    }

    void InitializeGoBathUAction()
    {
        IDecisionFactor bathNecessityFactor = new LeafFactor(this, _timeWithoutBath, 0, float.MaxValue, (150 / float.MaxValue));
        goBathUAction = new UtilityBasedAction(new UseLitterBoxAction(this), bathNecessityFactor);
        utilityActions.Add(goBathUAction);
    }

    void InitializePurgueUAction()
    {
        IDecisionFactor needsToPurgueFactor = new LeafFactor(this, _timeWithoutPurguing, 0, float.MaxValue, (120 / float.MaxValue));
        purgueUAction = new UtilityBasedAction(new PurgueAction(this), needsToPurgueFactor);
        utilityActions.Add(purgueUAction);
    }

    void InitializeSleepUAction()
    {
        IDecisionFactor sleepNecessity = new LeafFactor(this, _tiredness, 0, float.MaxValue, (420 / float.MaxValue));
        sleepUAction = new UtilityBasedAction(new SleepAction(this), sleepNecessity);
        utilityActions.Add(sleepUAction);
    }
}
