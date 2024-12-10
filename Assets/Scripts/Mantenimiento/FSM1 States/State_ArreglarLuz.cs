using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class State_ArreglarLuz : AState
    {
        public State_ArreglarLuz(StateMachine FSM1, IAgent agent) : base(FSM1, agent) { }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado ArreglarLuz");

        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado ArreglarLuz");
        }

        public override void Update()
        {
            Debug.Log("Arreglando la luz...");

        }

        public override void FixedUpdate()
        {
        }
    }
}