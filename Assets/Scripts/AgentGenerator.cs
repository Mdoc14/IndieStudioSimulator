using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentGenerator : MonoBehaviour
{
    [SerializeField] private float _interval = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateAgents());
    }

    IEnumerator GenerateAgents()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            yield return new WaitForSeconds(_interval);
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
