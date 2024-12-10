using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class State_BuscarHerramientas: AState
    {
        public State_BuscarHerramientas(StateMachine FSM2, IAgent agent) : base(FSM2, agent) { }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado BuscarHerramientas");

        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado BuscarHerramientas");
        }

        public override void Update()
        {
            Debug.Log("Buscando herramientas...");
            

        }

        public override void FixedUpdate()
        {
        }
    }
}