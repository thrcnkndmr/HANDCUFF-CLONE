using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class HandcuffManager : MonoBehaviour
{
    //Handcuff Managing
    public List<GameObject> handcuffList = new List<GameObject>();
    public Transform collectPoint;

    //Criminal Managing
    private NavMeshAgent navCriminal;
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

                handcuff.transform.DOLocalJump(
                    new Vector3(collectPoint.localPosition.x, handcuffCount * 0.3f, collectPoint.localPosition.z), 5f,
                    1,
                    0.6f).OnComplete(() => handcuff.transform.localEulerAngles = Vector3.zero);
                handcuffList.Add(handcuff.gameObject);
            }
        }

        else if (other.gameObject.tag == "Criminal")

        {
            CollectingCriminal criminal = other.GetComponent<CollectingCriminal>();

            if (!criminal.IsCriminalCollected())
            {
                criminal.CriminalCollected();
                navCriminal = criminal.GetComponent<NavMeshAgent>();
                navCriminal.SetDestination(transform.position);

            }
        }
    }
}