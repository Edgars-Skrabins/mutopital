using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    private const string PUBLIC_KEY = "b484cac428e571619ec90ca33aa4a5c97ef39e4c7f38a85d6f0cb0fc7cc8611b";

    [SerializeField] private List<TMP_Text> names;
    [SerializeField] private List<TMP_Text> scores;

    [SerializeField] private TMP_InputField m_playeNameInputField;
    [SerializeField] private Transform m_leaderboardUITF;

    private void Start()
    {
        GetLeaderBoard();
    }

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(PUBLIC_KEY, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }

            Debug.Log(msg);
        }));
    }

    public void SetEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(PUBLIC_KEY, username, score, ((msg) =>
        {
            GetLeaderBoard();
        }));
    }
}
