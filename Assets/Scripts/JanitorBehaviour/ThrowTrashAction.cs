using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTrashAction : ASimpleAction
{
    public ThrowTrashAction(IAgent agent) : base(agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Tirando basura");
    }
    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
