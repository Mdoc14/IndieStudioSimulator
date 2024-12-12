using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public interface IAction
    {
        public bool Started { get; set; }
        public bool Finished { get; set; }
        public void Enter();
        public void Exit();
        public void Update();
        public void FixedUpdate();
    }
}
