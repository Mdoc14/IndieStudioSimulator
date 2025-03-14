using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharactersBehaviour
{
    public class State_RepararIncidencia : AState
    {
        private CompositeAction _incidenceAction;

        public State_RepararIncidencia(StateMachine FSM2, IAgent agent) : base(FSM2, agent) { }

        public override void Enter()
        {
            List<IAction> _actions = new List<IAction>();
            _actions.Add(new GoToPositionAction(agent, (agent as MaintenanceBehaviour).GetCurrentIncidence().transform.position));
            _actions.Add(new RepairAction(agent));
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
                float cansancio = agent.GetAgentVariable("cansancio") + Random.Range(0.2f, 0.6f);
                agent.SetAgentVariable("cansancio", cansancio);
                if (cansancio > agent.GetAgentVariable("maxCansancio")) context.State = new State_Dormir(context, agent);
                else context.State = new State_TrabajarOficina(context, agent);
            }
        }

        public override void FixedUpdate()
        {
            _incidenceAction?.FixedUpdate();
        }
    }
}