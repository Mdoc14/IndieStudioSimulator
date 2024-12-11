using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private GameObject _cam;

    void Awake()
    {
        _cam = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        this.transform.forward = _cam.transform.position - this.transform.position;
    }
}
