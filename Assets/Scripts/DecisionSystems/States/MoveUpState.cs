using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class MoveUpState : AState
    {
        MoveUpAction moveUpAction;

        public MoveUpState(StateMachine sm, IAgent agent) : base(sm, agent)
        {

        }

        public override void Enter()
        {
            moveUpAction = new MoveUpAction(agent);
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }

        public override void Update()
        {
            moveUpAction.Update();
        }

        public override void FixedUpdate()
        {
            //throw new System.NotImplementedException();
        }
    }
}
