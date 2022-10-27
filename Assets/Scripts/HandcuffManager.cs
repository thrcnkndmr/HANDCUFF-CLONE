using System.Collections.Generic;
using Blended;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;


public class HandcuffManager : MonoSingleton<HandcuffManager>
{
    //Handcuff Managing
    public List<GameObject> handcuffList = new List<GameObject>();
    public Transform collectPoint;

    //Criminal Managing
    
    public List<GameObject> criminalList = new List<GameObject>();
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Handcuff"))
        {
            CollectingHandcuff handcuff = other.GetComponent<CollectingHandcuff>();

            if (!handcuff.IsHcCollected())
            {
                handcuff.HcCollected();
                handcuff.transform.parent = collectPoint.transform;
                float handcuffCount = handcuffList.Count;

                var localPosition = collectPoint.localPosition;
                handcuff.transform.DOLocalJump(
                    new Vector3(localPosition.x, handcuffCount * 0.3f, localPosition.z), 5f,
                    1,
                    0.6f).OnComplete(() => handcuff.transform.localEulerAngles = Vector3.zero);
                handcuffList.Add(handcuff.gameObject);
            }
        }

        else if (other.gameObject.CompareTag("Criminal"))

        {
            CollectingCriminal criminal = other.GetComponent<CollectingCriminal>();
            if (!criminal.IsCriminalCollected())
            {
                
                    var handcuff = handcuffList[0].gameObject;
                    handcuff.transform.parent = criminal.transform;
                    handcuff.transform.DOLocalJump(new Vector3(0,0.5f,-0.2f), 5f,
                        1,
                        0.6f).OnComplete(() =>
                    {
                        handcuff.transform.localEulerAngles = Vector3.zero;
                        
                    });
                    handcuffList.Remove(handcuffList[0].gameObject);

                    
                criminal.CriminalCollected();
                criminal.targetTransform = collectPoint;
                criminal.canFollow = true;
                criminalList.Add(criminal.gameObject);
                
                for (int i = 1; i < criminalList.Count; i++)
                {
                    criminal.navCriminal.stoppingDistance = i;
                }


            }
        }


    }

    
    
}