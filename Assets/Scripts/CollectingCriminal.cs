using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingCriminal : MonoBehaviour
{
    private bool isCriminalCollected;

    private void Start()
    {
        isCriminalCollected = false;
    }

    public bool IsCriminalCollected()
    {
        return isCriminalCollected;
    }

    public void CriminalCollected()
    {
        isCriminalCollected = true;
    }
}