using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTrashAction : ASimpleAction
{
    float timer = 0f;
    float throwTotalTime = 0.5f;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
