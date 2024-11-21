using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public interface IAction
    {
        public bool FirstTimeExecuted { get; }
        public bool HasFinished { get; }
        public void Enter();
        public void Exit();
        public void Update();
        public void FixedUpdate();
    }
}
