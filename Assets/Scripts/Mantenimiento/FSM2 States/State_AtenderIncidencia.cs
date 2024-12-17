using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharactersBehaviour
{
    public class State_AtenderIncidencia : AState
    {
        private CompositeAction _incidenceAction;
        public static bool incidenceTaken = false;
    
        public State_AtenderIncidencia(StateMachine FSM2, IAgent agent) : base(FSM2, agent) { }

        public override void Enter()
        {
            List<IAction> _actions = new List<IAction>();
            if (!agent.GetChair().IsOccupied()) _actions.Add(new GoToDeskAction(agent));
            _actions.Add(new ListenIncidenceAction(agent));
            _incidenceAction = new CompositeAction(_actions);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            _incidenceAction?.Update();
            if (_incidenceAction.Finished)
            {
                incidenceTaken = true;
                context.State = new State_ExaminarIncidencia(context, agent);
            }
        }

        public override void FixedUpdate()
        {
            _incidenceAction?.FixedUpdate();
        }
    }
}