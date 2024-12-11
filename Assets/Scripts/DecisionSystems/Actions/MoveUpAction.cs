using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class MoveUpAction : ASimpleAction
    {
        public MoveUpAction(IAgent agent) : base(agent)
        {

        }

        public override void Enter()
        {
            started = true;
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }

        public override void Update()
        {
            agent.GetAgentGameObject().transform.Translate(Vector3.up * (1 * Time.deltaTime));
        }

        public override void FixedUpdate()
        {
            //throw new System.NotImplementedException();
        }
    }
}
