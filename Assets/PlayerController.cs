using Cinemachine;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] Transform playerCameraRoot;
    // Start is called before the first frame update
    public TMP_Text playerName;
    public GameObject PrefabPika;

    [Networked]//(OnChanged = nameof(NetworkedHealthChanged))]
    public int NetworkedHealth { get; set; } = 100;
    [Networked]
    public string userName { get; set; } =" ";
    void Start()
    {
        if (HasStateAuthority == false)
        {
            return;
        }
        playerName.text = GameManager.instance._playerName;
        GameObject VirtualCamera = GameObject.Find("PlayerFollowCamera");
        VirtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerCameraRoot;
        GetComponent<ThirdPersonController>().enabled = true;
        GetComponent<PlayerInput>().enabled = true;
      //  StartCoroutine(SpawnTest());
        userName = GameManager.instance._playerName;
        NetworkedHealth = 45;
        DealDamageRpc(userName);

    }

    IEnumerator SpawnTest()
    {
       yield return new WaitForSeconds(10);
            Runner.Spawn(PrefabPika, new Vector3(0, 0, 0), Quaternion.identity);
        
    }
// Update is called once per frame
void Update()
    {
        if (HasStateAuthority == false)
        {
            return;
        }
        else
            if (Input.GetKeyDown(KeyCode.Space))
        {
            print("R pressed");
            NetworkedHealth = NetworkedHealth - 1;
            DealDamageRpc(userName);

        }
    }
    
    [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    public void DealDamageRpc(string playername)
    {
    
    // The code inside here will run on the client which owns this object (has state and input authority).
    Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
        userName = playername;
        playerName.text = playername;
    }
    
}
