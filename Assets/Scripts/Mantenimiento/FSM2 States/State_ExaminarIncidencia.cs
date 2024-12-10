using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class State_ExaminarIncidencia : AState
    {
        public State_ExaminarIncidencia(StateMachine FSM2, IAgent agent) : base(FSM2, agent) { }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado ExaminarIncidencia");

        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado ExaminarIncidencia");
        }

        public override void Update()
        {
            Debug.Log("Examinando incidencia...");

        }

        public override void FixedUpdate()
        {
        }
    }
}