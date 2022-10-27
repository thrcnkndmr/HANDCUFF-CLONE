using System;
using System.Collections;
using System.Collections.Generic;
using Blended;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;


public class CollisionManager : MonoSingleton<CollisionManager>
{
    private CollectingCriminal criminal;
    private int tempCount;

    //Handcuff Managing
    public List<GameObject> handcuffList = new List<GameObject>();
    public Transform collectPoint;

    //Criminal Managing
    public GameObject jail;
    public List<GameObject> criminalList = new List<GameObject>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jail"))
        {
            StartCoroutine(Jailing(1.5f));
        }

        else if (other.gameObject.CompareTag("Handcuff"))
        {
            CollectingHandcuff handcuff = other.GetComponent<CollectingHandcuff>();

            if (!handcuff.IsHcCollected())
            {
                handcuff.HcCollected();
                handcuff.transform.parent = collectPoint.transform;
                handcuff.transform.DOLocalJump(
                    new Vector3(collectPoint.localPosition.x, handcuffList.Count * 0.3f, collectPoint.localPosition.z), 3f,
                    1,
                    0.6f).OnComplete(() => handcuff.transform.localEulerAngles = Vector3.zero);
                handcuffList.Add(handcuff.gameObject);
            }
        }

        else if (other.gameObject.CompareTag("Criminal"))

        {
            criminal = other.GetComponent<CollectingCriminal>();
            if (!criminal.IsCriminalCollected() && handcuffList.Count > 0)
            {
                var handcuff = handcuffList[handcuffList.Count - 1].gameObject;
                handcuff.transform.parent = criminal.transform;
                handcuff.transform.DOLocalJump(new Vector3(0, 0.5f, -0.2f), 3f,
                    1,
                    0.6f).OnComplete(() => { handcuff.transform.localEulerAngles = Vector3.zero; });
                criminal.handcuff = handcuffList[handcuffList.Count - 1].gameObject;
                handcuffList.RemoveAt(handcuffList.Count - 1);


                criminal.CriminalCollected();
                criminal.canFollow = true;
                criminalList.Add(criminal.gameObject);

                for (int i = 1; i < criminalList.Count; i++)
                {
                    criminal.navCriminal.stoppingDistance = i;
                }
            }
        }
    }


    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Jailing(float duration)
    {
        tempCount = criminalList.Count;
        
        for (int i = 0; i < tempCount; i++)
        {
            var criminal = criminalList[0].gameObject.GetComponent<CollectingCriminal>();
            criminal.navCriminal.stoppingDistance = 0;
            criminal.navCriminal.SetDestination(jail.transform.position);
            criminal.canFollow = false;
            criminal.handcuff.transform.parent = null;
            criminal.handcuff.transform.parent = collectPoint.transform;
            criminal.handcuff.transform.DOLocalJump(
                new Vector3(collectPoint.localPosition.x, handcuffList.Count * 0.3f, collectPoint.localPosition.z), 3f,
                1,
                0.6f).OnComplete(() => criminal.handcuff.transform.localEulerAngles = Vector3.zero);
            criminal.handcuff.transform.localRotation = new Quaternion(0,0,0,1);
            handcuffList.Add(criminal.handcuff);
            criminal.handcuff = null;
            criminalList.Remove(criminalList[0]);
            yield return new WaitForSeconds(duration);
        }
    }
}