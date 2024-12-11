using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class TestAgentBehaviour : AgentBehaviour
    {
        BehaviourTree bt;

        // Start is called before the first frame update
        void Start()
        {
            bt = new BehaviourTree();
            bt.Root = new DelayTNode(new ActionNode(new MoveForwardAction(this), bt), 5f);
        }

        // Update is called once per frame
        void Update()
        {
            bt.UpdateBehaviour();
        }
    }
}
