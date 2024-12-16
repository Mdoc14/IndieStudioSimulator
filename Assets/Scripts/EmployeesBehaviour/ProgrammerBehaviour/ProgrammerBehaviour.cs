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
        InitializePlayPCAction();
        InitializeSmokeAction();
        InitializeGoEatAction();
        workerUS = new UtilitySystem(new List<UtilityBasedAction>(utilityActions));
        _workerFSM.State = new ProgrammerWorkState(_workerFSM, this);
    }

    protected override void Update()
    {
        base.Update();

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    void InitializeGoEatAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor goEatFactor = new LeafFactor(this, _timeWithoutConsuming, 0, 150, 0, 0.7f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 100, 0, 0.3f, (x) => (100 - x));

        leafFactors.Add(goEatFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor goEatNecessityFactor = new FusionFactor(new List<LeafFactor>(leafFactors), 0.7f); 

        GameObject vendingMachine = GameObject.Find("VendingMachine");
        ASimpleAction eatingAction = new ChangeStateAction(_workerFSM, this, new EatState(_workerFSM, this));

        _goEatAction = new UtilityBasedAction(eatingAction, goEatNecessityFactor);
        utilityActions.Add(_goEatAction);
    }
    void InitializePlayPCAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor boredomFactor = new LeafFactor(this, _boredom, 0, 100f, 0, 0.2f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 100f, 0, 0.8f, (x) => (100 - x));

        leafFactors.Add(boredomFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor goPlayPCNecessityFactor = new FusionFactor(new List<LeafFactor>(leafFactors),0.68f); 

        ASimpleAction playPCAction = new ChangeStateAction(_workerFSM, this, new PlayPCState(_workerFSM, this));

        _playPCAction = new UtilityBasedAction(playPCAction, goPlayPCNecessityFactor);
        utilityActions.Add(_playPCAction);
    }
    void InitializeSmokeAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor stressFactor = new LeafFactor(this, _stress, 0, 100f, 0, 0.8f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 100f, 0, 0.2f, (x) => (100 - x));

        leafFactors.Add(stressFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor smokeNecessityFactor = new FusionFactor(new List<LeafFactor>(leafFactors), 0.68f); 

        ASimpleAction smokeAction = new ChangeStateAction(_workerFSM, this, new SmokeState(_workerFSM, this));

        _smokeAction = new UtilityBasedAction(smokeAction, smokeNecessityFactor);
        utilityActions.Add(_smokeAction);
    }
}
