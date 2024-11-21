using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public abstract class AAction
    {
        protected IAgent agent;
        protected bool started = false;
        protected bool finished = false;

        public AAction(IAgent agent)
        {
            this.agent = agent;
        }

        public bool HasStarted { get { return started; } }
        public bool HasFinished { get { return finished; } }
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
        public abstract void FixedUpdate();
    }
}
