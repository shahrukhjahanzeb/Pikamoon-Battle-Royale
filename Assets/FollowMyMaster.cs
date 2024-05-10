using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowMyMaster : NetworkBehaviour
{

    public Transform followTarget;
    public NavMeshAgent navMeshAgent;
//    public NetworkObject
    // Start is called before the first frame update
    void Start()
    {
        if (HasStateAuthority)
        {
            navMeshAgent.enabled = true;

        }


    }
    // Update is called once per frame
    void Update()
    {
        if (HasStateAuthority == true)
        {
            navMeshAgent.SetDestination(followTarget.position);
        }
    }
   
}
