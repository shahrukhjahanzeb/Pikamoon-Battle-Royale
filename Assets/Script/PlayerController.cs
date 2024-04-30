using Cinemachine;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] Transform playerCameraRoot;
    // Start is called before the first frame update
    public TMP_Text playerName;
    public GameObject PrefabPika;
    public GameObject canvasData;
    public Button AttackButton;
    public GameObject[] characters;
    [Networked]
    public string userName { get; set; } =" ";
    [Networked]
    public int myCharacterindex { get; set; } = 0;

    [Networked, OnChangedRender(nameof(HealthChanged))]
    public int myHealth { get; set; } = 100;

    IEnumerator Start()
    {
        canvasData.GetComponent<LookAtConstraint>().rotationOffset = new Vector3(-180, 0, 180);
        ConstraintSource sc = new ConstraintSource();
        sc.weight = 1.0f;
        sc.sourceTransform = Camera.main.transform;
        canvasData.GetComponent<LookAtConstraint>().SetSource(0, sc);
        HealthChanged();
        AttackButton.onClick.AddListener(DealDamageRpc);

        if (HasStateAuthority == false)
        {
            yield return new WaitForSeconds(2);
            playerName.text = userName;
            GameObject myPlayerAvatar = Instantiate(characters[myCharacterindex], gameObject.transform);
            GetComponent<Animator>().avatar = myPlayerAvatar.GetComponent<Animator>().avatar;
        }
        else
        {
            myCharacterindex = GameManager.instance.myCharacter;
            GameObject myPlayerAvatar = Instantiate(characters[myCharacterindex], gameObject.transform);
            GetComponent<Animator>().avatar = myPlayerAvatar.GetComponent<Animator>().avatar;

            playerName.text = GameManager.instance._playerName;
            GameObject VirtualCamera = GameObject.Find("PlayerFollowCamera");
            VirtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerCameraRoot;
            GetComponent<ThirdPersonController>().enabled = true;
            GetComponent<PlayerInput>().enabled = true;
            userName = GameManager.instance._playerName;
            //  DealDamageRpc(userName);
        }
        canvasData.SetActive(true);
    }

    void HealthChanged()
    {


        AttackButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text= myHealth.ToString();

        //   Debug.Log($"Health changed to: {NetworkedHealth}");
    }


    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc()
    {
        // The code inside here will run on the client which owns this object (has state and input authority).
        Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
        myHealth = myHealth - 1;//  damage;
    }


    IEnumerator SpawnTest()
    {
       yield return new WaitForSeconds(10);
            Runner.Spawn(PrefabPika, new Vector3(0, 0, 0), Quaternion.identity);
        
    }
// Update is called once per frame

}
