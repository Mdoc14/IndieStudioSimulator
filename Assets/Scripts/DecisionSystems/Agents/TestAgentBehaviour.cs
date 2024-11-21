using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class TestAgentBehaviour : MonoBehaviour, IAgent
    {
        StateMachine behaviourSystem;

        public GameObject GetAgentGameObject()
        {
            return gameObject;
        }

        // Start is called before the first frame update
        void Start()
        {
            behaviourSystem = new StateMachine();
            behaviourSystem.State = new MoveState(behaviourSystem, this);
        }

        // Update is called once per frame
        void Update()
        {
            behaviourSystem.UpdateBehaviour();
        }
    }
}
