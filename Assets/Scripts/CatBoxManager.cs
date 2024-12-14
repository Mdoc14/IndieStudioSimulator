using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBoxManager : MonoBehaviour
{

    [SerializeField] bool isDirty;
    [SerializeField] List<GameObject> feces = new List<GameObject>();

    void Start()
    {
        if (isDirty) SetDirty();
    }

    public bool IsDirty()
    {
        return isDirty;
    }

    public void SetDirty()
    {
        isDirty = true;
        foreach (GameObject obj in feces)
        {
            obj.SetActive(true);
        }
    }

    public void HideFeces() 
    {
        foreach (GameObject obj in feces)
        {
            obj.SetActive(false);
        }
    }

    public void CleanBox()
    {
        isDirty = false;
    }
}
