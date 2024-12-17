using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : AInteractable
{
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private Transform trashSpawn;
    public bool Lying { get; private set; } = false;

    public override void Interact()
    {
        if (Lying) return;
        base.Interact();
        transform.position += positionOffset;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationOffset);
        WorldManager.Instance.GenerateTrash(trashSpawn.position);
        Lying = true;
    }

    public override void Repair()
    {
        if (!Lying) return;
        base.Repair();
        base.ResetInteractable();
        transform.position -= positionOffset;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - rotationOffset);
        Lying = false;
    }
}
