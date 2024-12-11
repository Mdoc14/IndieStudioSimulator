using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTrashAction : ASimpleAction
{
    float timer = 0f;
    float pickUpTotalTime = 1.5f;

    Room currentRoom;
    public PickUpTrashAction(IAgent agent, Room currentRoom) : base(agent)
    {
        this.currentRoom = currentRoom;
    }

    public override void Enter()
    {
        Debug.Log("Recogiendo basura");
        base.Enter();
        //Iniciar animacion de recoger 
        agent.SetBark("Take");
    }
    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= pickUpTotalTime)
        {
            currentRoom.DeleteTrash();
            //finished = true;
        }
    }
}
