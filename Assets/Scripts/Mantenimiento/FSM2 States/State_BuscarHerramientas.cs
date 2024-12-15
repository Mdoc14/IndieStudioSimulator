using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharactersBehaviour
{
    public class State_BuscarHerramientas : AState
    {
        private CompositeAction _incidenceAction;

        public State_BuscarHerramientas(StateMachine FSM2, IAgent agent) : base(FSM2, agent) { }

        public override void Enter()
        {
            List<IAction> _actions = new List<IAction>();
            _actions.Add(new GoToPositionAction(agent, GameObject.FindWithTag("Toolbox").transform.position));
            _actions.Add(new SearchToolAction(agent));
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
                context.State = new State_RepararIncidencia(context, agent);
            }
        }

        public override void FixedUpdate()
        {
            _incidenceAction?.FixedUpdate();
        }
    }
}