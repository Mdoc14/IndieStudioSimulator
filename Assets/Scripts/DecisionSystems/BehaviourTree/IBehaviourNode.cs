using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public interface IBehaviourNode
    {
        public BehaviourState Execute();
    }

    public enum BehaviourState
    {
        Failure,
        Success,
        Running
    }
}
