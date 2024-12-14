using System.Collections;
using System.Collections.Generic;
using CharactersBehaviour;
using UnityEngine;

public class ThrowCatFecesAction : ASimpleAction
{
    float timer = 0f;
    float throwTotalTime = 1.33f; //Coincide con la duracion de la animacion Using

    Room currentRoom;

    public ThrowCatFecesAction(IAgent agent, Room currentRoom) : base(agent)
    {
        this.currentRoom = currentRoom;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Accion: tirar heces de gato");
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
            currentRoom.GetCatBox().CleanBox();
            finished = true;
        }
    }
}
