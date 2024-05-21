using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PikaMoonMovements : NetworkBehaviour
{

    public Transform followMaster;
     NavMeshAgent navMeshAgent;


    // Logic for AI moving randomly around master
    public bool ai_isEnabled=false;
    private Vector3 lastPosition;
    public float movementThreshold = 0.001f; // Minimum movement distance to consider the player as moving
    public float disableThreshold = 5f; // Time threshold in seconds after which player is disabled
    private float timer = 0f;
    public float resetDelayAI = 5f;
    public int newCirclePositionMinDistance = 0, newCirclePositionMaxDistance = 0;
    /// <summary>
    // Start is called before the first frame update

     Animator animator;

    void Start()
    {

        animator = GetComponent<Animator>();
        if (HasStateAuthority)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (HasStateAuthority == true)
        {
            
            // navMeshAgent.SetDestination(followMaster.position);
       
            if (navMeshAgent.velocity.magnitude > 0.1f)
            {
                // Set blend tree parameter to 1 if moving
                animator.SetFloat("Move", Mathf.MoveTowards(animator.GetFloat("Move"), 1.0f,Time.deltaTime * 3));
            }
            else
            {
                // Set blend tree parameter to 0 if idle
                animator.SetFloat("Move", Mathf.MoveTowards(animator.GetFloat("Move"), 0.0f, Time.deltaTime * 3));
            }

            //Follow master
            if (Vector3.Distance(followMaster.position, lastPosition) > movementThreshold)
            {
                lastPosition = followMaster.position; // Update last position
                navMeshAgent.SetDestination(followMaster.position);
              //  navMeshAgent.stoppingDistance = 1.0f;
                ai_isEnabled = false;
            }
            // follow around master on random points
            else
            {
                ai_isEnabled = true;
             //   navMeshAgent.stoppingDistance = 0.0f;
                timer += Time.deltaTime;
                if (timer >= resetDelayAI)
                {
                    // Reset integer and timer
                   
                    timer = 0f;
                    Debug.Log("Integer reset to 0.");
                   // GetARandomPositionInTorus();
                    navMeshAgent.SetDestination(GetARandomPositionInTorus());
                }
                print("Do AI thing");
            }
        }
    }
   // public GameObject Eggs;
    public Vector3 GetARandomPositionInTorus()
    {

        Vector3 dest = transform.position;

        while (Vector3.Distance(dest, transform.position) < newCirclePositionMinDistance)
        {
            Vector3 randomPosition = Random.insideUnitSphere * newCirclePositionMaxDistance;
            randomPosition = new Vector3(randomPosition.x, transform.position.y, randomPosition.z);
            dest = randomPosition + followMaster.transform.position;
        }
         //       GameObject temp=    Instantiate(Eggs);
         //     temp.transform.position = dest;
        return dest;

    }




}
