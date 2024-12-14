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
    UtilityBasedAction purgeUAction;
    UtilityBasedAction eatUAction;
    UtilityBasedAction sleepUAction;
    UtilityBasedAction playUAction;
    UtilityBasedAction bePet;

    [SerializeField] Chair _catBed;
    public Chair CatBed { get { return _catBed; } }

    [SerializeField] Chair catBath;

    string _boredom = "boredom";
    string _tiredness = "tiredness";
    string _timeWithoutBath = "timeWithoutBath";

    public string Boredom { get { return _boredom; } }
    public string Tiredness { get { return _tiredness; } }
    public string TimeWithoutBath { get { return _timeWithoutBath; } }

    // Start is called before the first frame update
    void Start()
    {
        SetCurrentChair(catBath);

        agentVariables[_boredom] = 0f;
        agentVariables[_tiredness] = 0f;
        agentVariables[_timeWithoutBath] = 0f;

        sm.State = new WanderingState(sm, this);

        InitializeGoBathUAction();

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
        IDecisionFactor timeWithoutBathFactor = new LeafFactor(this, _timeWithoutBath, 0, float.MaxValue, (300/float.MaxValue));
        goBathUAction = new UtilityBasedAction(new UseLitterBox(this), timeWithoutBathFactor);
        utilityActions.Add(goBathUAction);
    }
}
