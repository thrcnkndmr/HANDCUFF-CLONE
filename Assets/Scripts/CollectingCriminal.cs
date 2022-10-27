
using UnityEngine;
using UnityEngine.AI;

public class CollectingCriminal : MonoBehaviour
{

    public bool canFollow;
    public NavMeshAgent navCriminal;
    private bool isCriminalCollected;
    private CollisionManager collisionManager;
    public GameObject handcuff;

    private void Start()
    {
        collisionManager = CollisionManager.Instance;
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
    
    private void Update()
    {
        if (canFollow)
        {
            navCriminal.SetDestination(collisionManager.collectPoint.position);
        }
    }
}