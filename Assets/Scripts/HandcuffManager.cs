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
        if (other.gameObject.CompareTag("Handcuff") )
        {
            CollectableHandcuff handcuff = other.GetComponent<CollectableHandcuff>();
            
            if (!handcuff.IsHcCollected())
            {
                handcuff.Collected();
                handcuff.transform.parent = collectPoint.transform;
                float handcuffCount = handcuffList.Count;

                handcuff.transform.DOLocalJump(
                    new Vector3(collectPoint.localPosition.x, handcuffCount * 0.3f, collectPoint.localPosition.z), 5f, 1,
                    0.6f).OnComplete(() => handcuff.transform.localEulerAngles = Vector3.zero);
                handcuffList.Add(handcuff.gameObject);
            }
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