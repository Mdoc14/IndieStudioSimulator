using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class TestAgentBehaviour : MonoBehaviour, IAgent
    {
        BehaviourTree _tree;

        public GameObject GetAgentGameObject()
        {
            return gameObject;
        }

        // Start is called before the first frame update
        void Start()
        {
            _tree = new BehaviourTree();
            List<IBehaviourNode> treeNodes = new List<IBehaviourNode>();
            treeNodes.Add(new ConditionNode(() => { return Input.GetKey(KeyCode.W); }));
            treeNodes.Add(new ActionNode(new MoveAction(this), _tree));
            _tree.Root = new SequenceNode(treeNodes);
        }

        // Update is called once per frame
        void Update()
        {
            _tree.UpdateBehaviour();
        }
    }
}
