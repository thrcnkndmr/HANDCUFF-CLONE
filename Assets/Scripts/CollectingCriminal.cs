
using UnityEngine;
using UnityEngine.AI;

public class CollectingCriminal : MonoBehaviour
{

    public bool canFollow;
    public Transform targetTransform;
    public NavMeshAgent navCriminal;
    private bool isCriminalCollected;
    private HandcuffManager handcuffManager;

    private void Start()
    {
        handcuffManager = HandcuffManager.Instance;
        navCriminal = GetComponent<NavMeshAgent>();
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
    
    private void LateUpdate()
    {
        if (canFollow)
        {
            navCriminal.SetDestination(handcuffManager.collectPoint.position);
        }
    }
}