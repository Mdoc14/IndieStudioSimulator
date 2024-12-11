using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Chair : MonoBehaviour
{
    [SerializeField] private Transform _sitTransform; //Posición en la que se sientan en la silla
    private GameObject _agent; //Agente que se sienta
    private bool _occupied = false; //Si la silla está ocupada
    public bool selected = false; //Si la silla ha sido seleccionada por un agente para sentarse en ella
    public event Action OnSit;
    public event Action OnLeave;

    private void Update()
    {
        if(_agent != null) //Pone al agente en _sitTransform
        {
            _agent.transform.position = Vector3.Lerp(_agent.transform.position, _sitTransform.position, 10*Time.deltaTime);
            _agent.transform.rotation = Quaternion.Slerp(_agent.transform.rotation, _sitTransform.rotation, 10*Time.deltaTime);
        }
    }

    public void Sit(GameObject agent)
    {
        this._agent = agent;
        _agent.GetComponent<NavMeshAgent>().enabled = false;
        _occupied = true;
        OnSit?.Invoke();
    }

    public void Leave()
    {
        _agent.GetComponent<NavMeshAgent>().enabled = true;
        _agent = null;
        _occupied = false;
        selected = false;
        OnLeave?.Invoke();
    }

    public bool IsOccupied()
    {
        return _occupied;
    }
}
