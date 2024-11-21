using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public interface IBehaviourSystem
    {
        public void UpdateBehaviour();
        public void FixedUpdateBehaviour();
    }
}
