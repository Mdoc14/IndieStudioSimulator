using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class MoveForwardState : AState
    {
        MoveForwardAction moveAction;
        float currentTime = 0f;
        float maxTime = 5f;


        public MoveForwardState(StateMachine sm, IAgent agent) : base(sm, agent)
        {

        }

        public override void Enter()
        {
            moveAction = new MoveForwardAction(agent);
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }

        public override void Update()
        {
            moveAction.Update();

            currentTime += Time.deltaTime;

            if (currentTime > maxTime)
            {
                context.State = new MoveUpState(context, agent);
            }
        }

        public override void FixedUpdate()
        {
            //throw new System.NotImplementedException();
        }
    }
}
