using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandcuffManager : MonoBehaviour
{
    public List<GameObject> handcuffList = new List<GameObject>();
    public Transform collectPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Handcuff")
        {
            other.gameObject.transform.parent = collectPoint.transform;
            var handcuff = other.gameObject;
            var hcTransform = handcuff.transform.position;
            handcuff.transform.DOMoveY(hcTransform.y + 5f, 0.3f)
                .OnComplete(() => handcuff.transform.DOMoveY( collectPoint.transform.position.y + 1,0.3f));
            

        }

        else if (other.gameObject.tag == "Criminal")
        {
        }
    }


    public void CollectHandcuff()
    {
    }

    public void CatchCriminal()
    {
    }
}