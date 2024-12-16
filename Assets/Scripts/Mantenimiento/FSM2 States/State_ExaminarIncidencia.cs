using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharactersBehaviour
{
    public class State_ExaminarIncidencia : AState
    {
        private CompositeAction _incidenceAction;

        public State_ExaminarIncidencia(StateMachine FSM2, IAgent agent) : base(FSM2, agent) { }

        public override void Enter()
        {
            List<IAction> _actions = new List<IAction>();
            _actions.Add(new GoToPositionAction(agent, (agent as MaintenanceBehaviour).GetCurrentIncidence().transform.position));
            _actions.Add(new LookIncidenceAction(agent));
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
                context.State = new State_BuscarHerramientas(context, agent);
            }
        }

        public override void FixedUpdate()
        {
            _incidenceAction?.FixedUpdate();
        }
    }
}