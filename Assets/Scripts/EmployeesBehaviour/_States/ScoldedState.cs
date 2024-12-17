using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoldedState : AState
{
    public ScoldedState(StateMachine sm, IAgent agent, bool inOffice) : base(sm, agent)
    {
        _inOffice = inOffice;
    }

    bool _inOffice = false;
    bool _toggled = false; //Si ya ha cambiado la animación o no cuando llega a la oficina en caso de que le regañe en la oficina
    ASimpleAction _goToOfficeAction;

    public override void Enter()
    {
        if (_inOffice)
        {
            if (agent.GetChair().IsOccupied()) agent.GetChair().Leave();
            //if (agent.GetCurrentChair().IsOccupied()) agent.GetCurrentChair().Leave();
            agent.SetBark("Scolded");
            agent.SetAnimation("Walk");
            _goToOfficeAction = new GoToDeskAction(agent, GameObject.FindWithTag("ScoldedChair").GetComponent<Chair>());
            _goToOfficeAction.Enter();
        }
        else
        {
            agent.SetBark("Scolded");
            agent.SetAnimation("Scolded");
        }
    }

    public override void Exit()
    {
        
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        if(_goToOfficeAction != null)
        {
            if(!_goToOfficeAction.Finished) _goToOfficeAction?.Update();
            if (!_toggled && _goToOfficeAction.Finished)
            {
                _toggled = true;
                agent.SetAnimation("Scolded");
                agent.SetBark("Scolded");
            }
        }

        if (agent.GetAgentVariable("motivation") <= 100f)
        {
            agent.SetAgentVariable("motivation", agent.GetAgentVariable("motivation") + Time.deltaTime);
        }
        else { agent.SetAgentVariable("motivation", 100f); }

        if (agent.GetAgentVariable("stress") <= 100f)
        {
            agent.SetAgentVariable("stress", agent.GetAgentVariable("stress") + Time.deltaTime);
        }
        else { agent.SetAgentVariable("stress", 100f); }
    }
}
