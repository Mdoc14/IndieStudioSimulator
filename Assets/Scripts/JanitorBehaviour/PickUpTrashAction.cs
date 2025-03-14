using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTrashAction : ASimpleAction
{
    float timer = 0f;
    float pickUpTotalTime = 1.167f; //Coincide con la duracion de la animacion Take

    Room currentRoom;
    public PickUpTrashAction(IAgent agent, Room currentRoom) : base(agent)
    {
        this.currentRoom = currentRoom;
    }

    public override void Enter()
    {
        Debug.Log("Accion: recoger basura");
        base.Enter();
        //Iniciar animacion de recoger 
        agent.SetAnimation("PickUp");
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
            timer = 0f;
            currentRoom.HideTrash();
            float count = agent.GetAgentVariable("TrashCleaned") + 1;
            agent.SetAgentVariable("TrashCleaned", count);
            finished = true;
        }
    }
}
