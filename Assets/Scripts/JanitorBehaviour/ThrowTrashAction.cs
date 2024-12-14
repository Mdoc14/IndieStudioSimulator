using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTrashAction : ASimpleAction
{
    float timer = 0f;
    float throwTotalTime = 1.33f; //Coincide con la duracion de la animacion Using

    Room currentRoom;

    public ThrowTrashAction(IAgent agent, Room currentRoom) : base(agent)
    {
        this.currentRoom = currentRoom;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Accion: tirar basura");
        agent.SetBark("CleanBathroom");
        agent.SetAnimation("Throw");
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

        if (timer >= throwTotalTime)
        {
            timer = 0f;
            currentRoom.DeleteTrash();
            finished = true;
        }
    }
}
