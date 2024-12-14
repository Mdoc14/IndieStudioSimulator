using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour : AgentBehaviour
{
    [SerializeField] Chair _catBed;
    public Chair CatBed { get { return _catBed; } }

    [SerializeField] List<GameObject> _catBowls;
    public List<GameObject> CatBowls { get { return _catBowls; } }

    [SerializeField] Chair _catLitterBox;

    UtilitySystem us;
    public UtilitySystem US { get { return us; } }

    List<UtilityBasedAction> utilityActions = new List<UtilityBasedAction>();

    UtilityBasedAction goBathUAction;
    UtilityBasedAction purgeUAction;
    UtilityBasedAction eatUAction;
    UtilityBasedAction sleepUAction;
    UtilityBasedAction playUAction;
    UtilityBasedAction bePet;

    string _boredom = "boredom";
    string _tiredness = "tiredness";
    string _timeWithoutBath = "timeWithoutBath";
    string _timeWithoutPurging = "timeWithoutPurging";
    string _timeWithoutEating = "timeWithoutEating";
    string _timeWithoutSocializing = "timeWithoutSocializing";

    public string Boredom { get { return _boredom; } }
    public string Tiredness { get { return _tiredness; } }
    public string TimeWithoutBath { get { return _timeWithoutBath; } }
    public string TimeWithoutPurging { get { return _timeWithoutPurging; } }
    public string TimeWithoutEating { get { return _timeWithoutEating; } }
    public string TimeWithoutSocializing { get { return _timeWithoutSocializing; } }

    StateMachine sm = new StateMachine();

    // Start is called before the first frame update
    void Start()
    {
        SetCurrentChair(_catLitterBox);

        agentVariables[_boredom] = 0f;
        agentVariables[_tiredness] = 0f;
        agentVariables[_timeWithoutBath] = 0f;
        agentVariables[_timeWithoutPurging] = 0f;
        agentVariables[_timeWithoutEating] = 0f;
        agentVariables[_timeWithoutSocializing] = 0f;

        InitializeGoBathUAction();
        InitializePurgeUAction();
        InitializeEatUAction();
        InitializeSleepUAction();

        us = new UtilitySystem(new List<UtilityBasedAction>(utilityActions));
        sm.State = new WanderingState(sm, this);
    }

    // Update is called once per frame
    void Update()
    {
        agentVariables[_timeWithoutBath] += Time.deltaTime;
        agentVariables[_timeWithoutPurging] += Time.deltaTime;
        agentVariables[_timeWithoutEating] += Time.deltaTime;
        agentVariables[_timeWithoutSocializing] += Time.deltaTime;

        us.UpdateBehaviour();
        sm.UpdateBehaviour();
    }

    void InitializeGoBathUAction()
    {
        IDecisionFactor goBathNecessityFactor = new LeafFactor(this, _timeWithoutBath, 0, float.MaxValue, (150 / float.MaxValue));
        goBathUAction = new UtilityBasedAction(new UseLitterBoxAction(this), goBathNecessityFactor);
        utilityActions.Add(goBathUAction);
    }

    void InitializePurgeUAction()
    {
        IDecisionFactor purgeNecessityFactor = new LeafFactor(this, _timeWithoutPurging, 0, float.MaxValue, (120 / float.MaxValue));
        purgeUAction = new UtilityBasedAction(new PurgeAction(this), purgeNecessityFactor);
        utilityActions.Add(purgeUAction);
    }

    void InitializeEatUAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();

        LeafFactor eatDesireFactor = new LeafFactor(this, _timeWithoutEating, 0, float.MaxValue, (180 / float.MaxValue), 0.6f);
        LeafFactor energyNecessityFactor = new LeafFactor(this, _tiredness, 0, float.MaxValue, (360 / float.MaxValue), 0.4f);

        leafFactors.Add(eatDesireFactor);
        leafFactors.Add(energyNecessityFactor);

        IDecisionFactor hungerFactor = new FusionFactor(new List<LeafFactor>(leafFactors), (180 / float.MaxValue));

        eatUAction = new UtilityBasedAction(new EatAction(this), hungerFactor);
        utilityActions.Add(eatUAction);
    }

    void InitializeSleepUAction()
    {
        IDecisionFactor energyNecessityFactor = new LeafFactor(this, _tiredness, 0, float.MaxValue, (360 / float.MaxValue));
        sleepUAction = new UtilityBasedAction(new SleepAction(this), energyNecessityFactor);
        utilityActions.Add(sleepUAction);
    }
}
