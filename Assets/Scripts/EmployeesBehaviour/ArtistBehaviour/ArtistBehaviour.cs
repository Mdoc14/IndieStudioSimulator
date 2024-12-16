using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtistBehaviour : EmployeeBehaviour
{

    UtilityBasedAction _goDrinkAction;
    UtilityBasedAction _lookOutsideAction;
    UtilityBasedAction _takePhotoAction;

    protected override void Start()
    {
        base.Start();
        InitializeGoBathAction();
        //InitializeLookOutsideAction();
        //InitializeTakePhotoAction();
        InitializeGoDrinkAction();
        workerUS = new UtilitySystem(new List<UtilityBasedAction>(utilityActions));
        _workerFSM.State = new ArtistWorkState(_workerFSM, this);
    }

    protected override void Update()
    {
        base.Update();

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    void InitializeGoDrinkAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor goDrinkFactor = new LeafFactor(this, _timeWithoutConsuming, 0, 150, 0, 0.7f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 100, 0, 0.3f, (x) => (100 - x));

        leafFactors.Add(goDrinkFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor goDrinkNecessityFactor = new FusionFactor(new List<LeafFactor>(leafFactors), 0.7f);

        ASimpleAction drinkingAction = new ChangeStateAction(_workerFSM, this, new DrinkState(_workerFSM, this));

        _goDrinkAction = new UtilityBasedAction(drinkingAction, goDrinkNecessityFactor);
        utilityActions.Add(_goDrinkAction);
    }
    void InitializeLookOutsideAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor stressFactor = new LeafFactor(this, _stress, 0, 100f, 0, 0.8f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 100f, 0, 0.2f, (x) => (100 - x));

        leafFactors.Add(stressFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor lookOutsideFactor = new FusionFactor(new List<LeafFactor>(leafFactors), 0.68f);

        ASimpleAction lookOutsideAction = new ChangeStateAction(_workerFSM, this, new LookOutsideState(_workerFSM, this));

        _lookOutsideAction = new UtilityBasedAction(lookOutsideAction, lookOutsideFactor);
        utilityActions.Add(_lookOutsideAction);
    }
    void InitializeTakePhotoAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor boredomFactor = new LeafFactor(this, _boredom, 0, 100f, 0, 0.2f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 100f, 0, 0.8f, (x) => (100 - x));

        leafFactors.Add(boredomFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor takePhotoFactor = new FusionFactor(new List<LeafFactor>(leafFactors), 0.68f);

        ASimpleAction takePhotoAction = new ChangeStateAction(_workerFSM, this, new TakePhotoState(_workerFSM, this));

        _takePhotoAction = new UtilityBasedAction(takePhotoAction, takePhotoFactor);
        utilityActions.Add(_takePhotoAction);
    }
    protected override void InitializeGoBathAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor goBathFactor = new LeafFactor(this, _timeWithoutBath, 0, 120f, 0, 0.6f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 100f, 0, 0.4f, (x) => (100 - x));

        leafFactors.Add(goBathFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor goBathNecessityFactor = new FusionFactor(new List<LeafFactor>(leafFactors), 0.70f);

        Action action = () => { this.SetAgentVariable(this.GetAgentGameObject().GetComponent<EmployeeBehaviour>().TimeWithoutBath, 0); };

        ASimpleAction bathroomAction = new ChangeStateAction(_workerFSM, this, new BathroomState(_workerFSM, this, new ArtistWorkState(_workerFSM, this), action));

        goBathAction = new UtilityBasedAction(bathroomAction, goBathNecessityFactor);
        utilityActions.Add(goBathAction);
    }
}
