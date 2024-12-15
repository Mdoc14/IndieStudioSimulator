using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public abstract class ASimpleAction : IAction
    {
        protected IAgent agent;
        protected bool started = false;
        protected bool finished = false;

        public ASimpleAction(IAgent agent)
        {
            this.agent = agent;
        }

        public void RestartAction()
        {
            started = false;
            finished = false;
        }

        public bool Started { get { return started; } set { started = value; } }
        public bool Finished { get { return finished; } set { finished = value; } }
        public virtual void Enter() { started = true; }
        public abstract void Exit();
        public abstract void Update();
        public abstract void FixedUpdate();
    }
}
