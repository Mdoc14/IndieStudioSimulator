using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class State_RepararIncidencia : AState
    {
        public State_RepararIncidencia(StateMachine FSM2, IAgent agent) : base(FSM2, agent) { }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado RepararIncidencia");

        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado RepararIncidencia");
        }

        public override void Update()
        {
            Debug.Log("Reparando incidencia...");

        }

        public override void FixedUpdate()
        {
        }
    }
}