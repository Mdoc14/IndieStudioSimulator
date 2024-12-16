using CharactersBehaviour;
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
        LeafFactor boredomFactor = new LeafFactor(this, _boredom, 0, 100f, 0, 0.2f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 100f, 0, 0.8f, (x) => (100 - x));

        leafFactors.Add(boredomFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor goPlayPCNecessityFactor = new FusionFactor(new List<LeafFactor>(leafFactors), 0.68f);

        ASimpleAction playPCAction = new ChangeStateAction(_workerFSM, this, new PlayPCState(_workerFSM, this));

        _lookOutsideAction = new UtilityBasedAction(playPCAction, goPlayPCNecessityFactor);
        utilityActions.Add(_lookOutsideAction);
    }
    void InitializeTakePhotoAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor stressFactor = new LeafFactor(this, _stress, 0, 100f, 0, 0.8f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 100f, 0, 0.2f, (x) => (100 - x));

        leafFactors.Add(stressFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor smokeNecessityFactor = new FusionFactor(new List<LeafFactor>(leafFactors), 0.68f);

        ASimpleAction smokeAction = new ChangeStateAction(_workerFSM, this, new SmokeState(_workerFSM, this));

        _takePhotoAction = new UtilityBasedAction(smokeAction, smokeNecessityFactor);
        utilityActions.Add(_takePhotoAction);
    }
}
