using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammerBehaviour : EmployeeBehaviour
{
    void Awake()
    {
        base.Awake();
        _workerFSM.State = new ProgrammerWorkState(_workerFSM, this);
        

    }

    void Update()
    {
        _workerFSM.UpdateBehaviour();

    }
    void FixedUpdate()
    {
        _workerFSM.FixedUpdateBehaviour();
    }
}
