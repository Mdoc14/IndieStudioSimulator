using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammerBehaviour : EmployeeBehaviour
{

    UtilityBasedAction _goEatAction;
    UtilityBasedAction _playPCAction;
    UtilityBasedAction _smokeAction;

    protected override void Start()
    {
        base.Start();
        _workerFSM.State = new ProgrammerWorkState(_workerFSM, this);
        InitializeGoEatAction();
        InitializePlayPCAction();
        InitializeSmokeAction();
        workerUS = new UtilitySystem(new List<UtilityBasedAction>(utilityActions));


    }

    void Update()
    {
        _workerFSM.UpdateBehaviour();

    }
    void FixedUpdate()
    {
        _workerFSM.FixedUpdateBehaviour();
    }
    void InitializeGoEatAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor goEatFactor = new LeafFactor(this, _timeWithoutConsuming, 0, float.MaxValue, (150 / float.MaxValue), 0.7f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 1f, 0.3f, 0.3f, (x) => (1 - x));

        leafFactors.Add(goEatFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor goEatNecessityFactor = new FusionFactor(new List<LeafFactor>(leafFactors));

        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToPositionAction(this, GameObject.Find("RestArea").GetComponent<Room>().GetMachinePosition()));
        actions.Add(new TakeFoodAction(this));
        actions.Add(new WaitForFoodAction(this));
        actions.Add(new ProgrammerEatingAction(this, _workerFSM));
        CompositeAction eatingAction = new CompositeAction(actions);

        _goEatAction = new UtilityBasedAction(eatingAction, goEatNecessityFactor);
        utilityActions.Add(_goEatAction);
    }
    void InitializePlayPCAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor boredomFactor = new LeafFactor(this, _boredom, 0, 1f, 0.6f, 0.2f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 1f, 0.8f, 0.8f, (x) => (1 - x));

        leafFactors.Add(boredomFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor goPlayPCNecessityFactor = new FusionFactor(new List<LeafFactor>(leafFactors));

        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToDeskAction(this, this.GetChair()));
        actions.Add(new PlayPCAction(this));
        CompositeAction bathroomAction = new CompositeAction(actions);

        _playPCAction = new UtilityBasedAction(bathroomAction, goPlayPCNecessityFactor);
        utilityActions.Add(_playPCAction);
    }
    void InitializeSmokeAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor stressFactor = new LeafFactor(this, _stress, 0, 1f, 0.7f, 0.8f, (x)=>(1 / (1 + Mathf.Exp( -20 * (x-0.5f) ))));
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 1f, 0.3f, 0.2f, (x) => (1 - x));

        leafFactors.Add(stressFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor smokeNecessityFactor = new FusionFactor(new List<LeafFactor>(leafFactors));

        List<IAction> actions = new List<IAction>();
        actions.Add(new GoToPositionAction(this, GameObject.Find("Terrace").transform.position));
        actions.Add(new SmokeAction(this));
        CompositeAction smokeAction = new CompositeAction(actions);

        _smokeAction = new UtilityBasedAction(smokeAction, smokeNecessityFactor);
        utilityActions.Add(_smokeAction);
    }
}
