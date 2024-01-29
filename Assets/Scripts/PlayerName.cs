using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{

    [SerializeField] private GameObject EnterPlayerUsernamePanel;
    [SerializeField] private Text input;

    void Start()
    {
        if (PlayerPrefs.GetString("Username") == "")
        {
            EnterPlayerUsernamePanel.SetActive(true);
            
        }
        Debug.Log(PlayerPrefs.GetString("Username"));
    }

    public void EnterPlayerName()
    {
        string username = input.text.ToString();
        PlayerPrefs.SetString("Username", username);
        Debug.Log(username);
    }
}
