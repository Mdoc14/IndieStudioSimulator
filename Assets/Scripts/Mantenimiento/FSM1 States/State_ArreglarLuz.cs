using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class State_ArreglarLuz : AState
    {
        public State_ArreglarLuz(StateMachine FSM1, IAgent agent) : base(FSM1, agent) { }
        CompositeAction arreglarLuz;
        public override void Enter()
        {
            Debug.Log("Entrando en el estado ArreglarLuz");
            //Va a su silla y despues utiliza su ordenador 
            List<IAction> actions = new List<IAction>();
            if (agent.GetChair().IsOccupied())
            {
                agent.GetChair().Leave();
            }
            actions.Add(new GoToLightAction(agent));
            actions.Add(new ActionFixLight(agent));
            arreglarLuz = new CompositeAction(actions);

        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado ArreglarLuz");
        }

        public override void Update()
        {
            arreglarLuz?.Update();

            if (arreglarLuz != null && arreglarLuz.Finished)
            {

                float cansancio = agent.GetAgentVariable("cansancio") + Random.Range(0.1f, 0.5f);
                agent.SetAgentVariable("cansancio", cansancio);
                Debug.Log(agent.GetAgentVariable("cansancio"));

                if (cansancio < agent.GetAgentVariable("maxCansancio")) context.State = new State_TrabajarOficina(context, agent);
                else context.State = new State_Dormir(context, agent);
            }

        }

        public override void FixedUpdate()
        {
            arreglarLuz?.FixedUpdate();
        }
    }
}