using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform camTransform;
    
    void Awake()
    {
        camTransform = GameObject.FindWithTag("MainCamera").transform;
    }

    void Update()
    {
        this.transform.forward = this.transform.position - camTransform.position;
    }
}
