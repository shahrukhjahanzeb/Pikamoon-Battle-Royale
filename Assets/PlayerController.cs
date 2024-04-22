using Cinemachine;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StarterAssets;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] Transform playerCameraRoot;
    // Start is called before the first frame update
    void Start()
    {
        if (HasStateAuthority == false)
        {
            return;
        }

        GameObject VirtualCamera = GameObject.Find("PlayerFollowCamera");
        VirtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerCameraRoot;
        GetComponent<ThirdPersonController>().enabled = true;
        GetComponent<PlayerInput>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
