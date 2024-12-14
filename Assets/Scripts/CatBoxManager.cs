using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBoxManager : MonoBehaviour
{

    [SerializeField] bool isDirty;
    [SerializeField] GameObject feces;

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
        feces.SetActive(true);
    }

    public void HideFeces() 
    {
        feces.SetActive(false);
    }

    public void CleanBox()
    {
        isDirty = false;
    }
}
