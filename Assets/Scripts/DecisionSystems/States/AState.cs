using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public abstract class AState : IState
    {
        protected StateMachine context;
        protected IAgent agent;

        public AState(StateMachine sm, IAgent agent)
        {
            context = sm;
            this.agent = agent;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
        public abstract void FixedUpdate();
    }
}
