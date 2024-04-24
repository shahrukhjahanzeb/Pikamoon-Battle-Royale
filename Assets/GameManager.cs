using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion.Sockets;
using System;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public static GameManager instance;
   // public bool connectOnAwake = false;
    public NetworkRunner runner;
    public GameObject PlayerPrefab;
    public string _playerName = null;

    public TMP_Text userInputField;
    [Header("Session list")]
   
    public Button refreshButton;
    public Transform sessionListContent;
    public GameObject sessionEntryPrefab;
    public List<SessionInfo> _session = new List<SessionInfo>();
    public GameObject _roomList;

    private void Awake()
    {
        if(instance==null) { instance = this; }
    
    }
    void Start()
    {
      
      //  ConnectToLobby("Usman");
    }

    public void SetPlayerName()
    {
     StartCoroutine ( ConnectToLobby(userInputField.text));
        
    }

    public IEnumerator ConnectToLobby(string playerName)
    {
        print(playerName);
        yield return new WaitForSeconds(1f);
        _playerName = playerName;
        if (runner==null)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }
        runner.JoinSessionLobby(SessionLobby.Shared);
    }

  
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        print("session list updated");
        _session.Clear();
        _session = sessionList;

      



    }
    public void RefreshSessionListUI()
    {

        //Create Session list UI so we dont create duplicates
        foreach(Transform child in sessionListContent)
        {
            Destroy(child.gameObject);
        }

        foreach(SessionInfo session in _session)
        {
            if(session.IsVisible)
            {
                GameObject entry = GameObject.Instantiate(sessionEntryPrefab, sessionListContent);
                SessionEntryPrefab script = entry.GetComponent<SessionEntryPrefab>();
                script.sessionName.text = session.Name;
                script.playerCount.text = session.PlayerCount + "/" + session.MaxPlayers;

                if(session.IsOpen==false || session.PlayerCount >=session.MaxPlayers)
                {

                    script.joinButton.interactable = false;
                }
                else
                {
                    script.joinButton.interactable = true;
                }
            }
        }
    }
    public async void ConnectToSession(string sessionName)
    {
        _roomList.SetActive(false);
        if (runner==null)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }
        runner.name = sessionName;

        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
           
            SessionName = sessionName,
  
        }) ;
    }
    public async void CreateSession()
    {
        _roomList.SetActive(false);
       int randomint = UnityEngine.Random.Range(1000, 9999);
        string randomSessionName = "Room-" + randomint.ToString();

        if (runner == null)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }

        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = randomSessionName,
            PlayerCount = 4,
        });
    }
    public void OnConnectedToServer(NetworkRunner runner)
    {
    //    throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
       // throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
      //  throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    //    throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
      //  throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
     //   throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
     //   throw new NotImplementedException();
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
      //  throw new NotImplementedException();
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
       // throw new NotImplementedException();
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
      //  throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        print("Onplayer Joinned");
        if (player == runner.LocalPlayer)
        {
           
          runner.Spawn(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity, player);
         }
      //  throw new NotImplementedException();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
     //   throw new NotImplementedException();
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
     //   throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
       // throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
      //  throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
     //   throw new NotImplementedException();
    }

   

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    //    throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
      //  throw new NotImplementedException();
    }

    // Start is called before the first frame update
    

}
