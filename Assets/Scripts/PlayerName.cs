using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;

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
        //Debug.Log(PlayerPrefs.GetString("Username"));
    }

    public void EnterPlayerName()
    {
        string username = input.text.ToString();
        PlayerPrefs.SetString("Username", username);
        LootLockerSDKManager.SetPlayerName(username, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name");
            }
            else
            {
                Debug.Log("Could not set player name" + response.errorData);
            }
        });
        //Debug.Log(username);

        
    }
}
