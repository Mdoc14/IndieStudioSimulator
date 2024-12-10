using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTrashAction : ASimpleAction
{
    public PickUpTrashAction(IAgent agent) : base(agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Iniciar animacion de recoger 

        Debug.Log("Recogiendo basura...");
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
}
