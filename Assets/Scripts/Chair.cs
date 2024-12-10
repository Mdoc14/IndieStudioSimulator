using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chair : MonoBehaviour
{
    [SerializeField] private Transform _sitTransform;
    private GameObject _agent;

    private void Update()
    {
        if(_agent != null)
        {
            _agent.transform.position = Vector3.Lerp(_agent.transform.position, _sitTransform.position, 10*Time.deltaTime);
            _agent.transform.rotation = Quaternion.Slerp(_agent.transform.rotation, _sitTransform.rotation, 10*Time.deltaTime);
        }
    }

    public void Sit(GameObject agent)
    {
        this._agent = agent;
        _agent.GetComponent<NavMeshAgent>().enabled = false;
    }

    public void Leave()
    {
        _agent.GetComponent<NavMeshAgent>().enabled = true;
        _agent = null;
    }
}
