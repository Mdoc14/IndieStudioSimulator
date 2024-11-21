using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class MoveState : AState
    {
        MoveAction moveAction;

        public MoveState(StateMachine sm, IAgent agent) : base(sm, agent)
        {

        }

        public override void Enter()
        {
            moveAction = new MoveAction(agent);
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }

        public override void Update()
        {
            moveAction.Update();
        }

        public override void FixedUpdate()
        {
            //throw new System.NotImplementedException();
        }
    }
}
