using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text RoomName;
    public string roomName;
    void Start()
    {
     DontDestroyOnLoad(this.gameObject);   
    }
    public int Char_Index;
    public void CharacterSelected(int index)
    {
        print(index);
        Char_Index=index;
        SceneManager.LoadScene("Gameplay");
        roomName=RoomName.text;

    }
}
