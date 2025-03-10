using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptWritterBehaviour : EmployeeBehaviour
{

    UtilityBasedAction _goDrinkAction;
    UtilityBasedAction _readBookAction;
    UtilityBasedAction _playMobileAction;

    protected override void Start()
    {
        base.Start();
        InitializeGoBathAction();
        InitializeReadBookAction();
        InitializePlayMobileAction();
        InitializeGoDrinkAction();
        workerUS = new UtilitySystem(new List<UtilityBasedAction>(utilityActions));
        _workerFSM.State = new ScriptWritterWorkState(_workerFSM, this);
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
        LeafFactor goDrinkFactor = new LeafFactor(this, _timeWithoutConsuming, 0, 150, 0, 0.4f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 100, 0, 0.6f, (x) => (100 - x));

        leafFactors.Add(goDrinkFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor goDrinkNecessityFactor = new FusionFactor(new List<LeafFactor>(leafFactors), 0.7f);

        ASimpleAction drinkingAction = new ChangeStateAction(_workerFSM, this, new DrinkState(_workerFSM, this));

        _goDrinkAction = new UtilityBasedAction(drinkingAction, goDrinkNecessityFactor);
        utilityActions.Add(_goDrinkAction);
    }
    void InitializeReadBookAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor stressFactor = new LeafFactor(this, _stress, 0, 100f, 0, 0.8f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 100f, 0, 0.2f, (x) => (100 - x));

        leafFactors.Add(stressFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor readBookFactor = new FusionFactor(new List<LeafFactor>(leafFactors), 0.68f);

        ASimpleAction readBookAction = new ChangeStateAction(_workerFSM, this, new ReadingState(_workerFSM, this));

        _readBookAction = new UtilityBasedAction(readBookAction, readBookFactor);
        utilityActions.Add(_readBookAction);
    }
    void InitializePlayMobileAction()
    {
        List<LeafFactor> leafFactors = new List<LeafFactor>();
        LeafFactor boredomFactor = new LeafFactor(this, _boredom, 0, 100f, 0, 0.8f);
        LeafFactor motivationFactor = new LeafFactor(this, _motivation, 0, 100f, 0, 0.2f, (x) => (100 - x));

        leafFactors.Add(boredomFactor);
        leafFactors.Add(motivationFactor);

        IDecisionFactor playMobileFactor = new FusionFactor(new List<LeafFactor>(leafFactors), 0.68f);

        ASimpleAction playMobileAction = new ChangeStateAction(_workerFSM, this, new PlayMobileState(_workerFSM, this));

        _playMobileAction = new UtilityBasedAction(playMobileAction, playMobileFactor);
        utilityActions.Add(_playMobileAction);
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

        ASimpleAction bathroomAction = new ChangeStateAction(_workerFSM, this, new BathroomState(_workerFSM, this, new ScriptWritterWorkState(_workerFSM, this), action));

        goBathAction = new UtilityBasedAction(bathroomAction, goBathNecessityFactor);
        utilityActions.Add(goBathAction);
    }
    public override void SetState(string stateName)
    {
        if (numScolds >= 3)
        {
            _workerFSM.State = new FiredState(_workerFSM, this);
            return;
        }
        if (stateName.Equals("WORK")) _workerFSM.State = new ScriptWritterWorkState(_workerFSM, this);
        else if (stateName.Equals("SCOLDED")) _workerFSM.State = new ScoldedState(_workerFSM, this, false);
        else if (stateName.Equals("SCOLDED_OFFICE")) _workerFSM.State = new ScoldedState(_workerFSM, this, true);
    }
}
