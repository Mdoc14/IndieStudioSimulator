using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkManager : MonoBehaviour
{
    [SerializeField] private Bark[] _barks;
    public static BarkManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetBark(string name)
    {
        foreach(Bark b in _barks)
        {
            if (b.name.Equals(name)) return b.sprite;
        }
        return null;
    }
}
