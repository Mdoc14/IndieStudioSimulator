using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class State_AtenderIncidencia : AState
    {
        public State_AtenderIncidencia(StateMachine FSM2, IAgent agent) : base(FSM2, agent) { }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado AtenderIncidencia");

        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado AtenderIncidencia");
        }

        public override void Update()
        {
            Debug.Log("Atendiendo incidencia...");

        }

        public override void FixedUpdate()
        {
        }
    }
}