using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanCatBoxAction : ASimpleAction
{
    float timer = 0f;
    float cleanTotalTime = 1.167f;
    Room currentRoom;
    public CleanCatBoxAction(IAgent agent, Room room) : base(agent)
    {
        currentRoom = room;
    }

    public override void Enter()
    {
        base.Enter();
        //Iniciar animacion de limpair caja
        Debug.Log("Limpiando caja del gato...");
        agent.SetBark("CleanBathroom");
        agent.SetAnimation("PickUp");
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

        if (timer >= cleanTotalTime)
        {
            timer = 0f;
            finished = true;
            currentRoom.GetCatBox().HideFeces();
        }
    }

}
