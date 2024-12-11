using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class State_Dormir : AState
    {
        public State_Dormir(StateMachine FSM2, IAgent agent) : base(FSM2, agent) { }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado Dormir");

        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado Dormir");
        }

        public override void Update()
        {
            Debug.Log("Durmiendo...");
            if (agent.GetAgentVariable("lastState") == 1) {
                context.State = new State_TrabajarOficina(context, agent);
            }
            if (agent.GetAgentVariable("lastState") == 2)
            {
                context.State = new State_RepararIncidencia(context, agent);
            }

        }

        public override void FixedUpdate()
        {
        }
    }
}