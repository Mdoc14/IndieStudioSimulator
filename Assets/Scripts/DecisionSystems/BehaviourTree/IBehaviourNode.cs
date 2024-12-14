using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public interface IBehaviourNode
    {
        public BehaviourState Execute();
        public void RestartNode();
    }

    public enum BehaviourState
    {
        Failure,
        Success,
        Running
    }
}
