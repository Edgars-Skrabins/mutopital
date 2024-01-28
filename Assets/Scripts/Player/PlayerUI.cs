using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_scoreText;
    [SerializeField] private Image[] m_strikeImages;
    [SerializeField] private GameObject m_pauseMenu;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        InputManager.I.OnPausePerformed += HandlePause;
        EventManager.I.OnGamePaused += ShowPauseMenu;
        EventManager.I.OnGameUnPaused += HidePauseMenu;
        EventManager.I.OnPlayerStrike += UpdatePlayerStrikeUI;
        EventManager.I.OnScoreUpdate += UpdateScoreUI;
    }

    private void UnSubscribeEvents()
    {
        InputManager.I.OnPausePerformed -= HandlePause;
        EventManager.I.OnGamePaused -= ShowPauseMenu;
        EventManager.I.OnGameUnPaused -= HidePauseMenu;
        EventManager.I.OnPlayerStrike -= UpdatePlayerStrikeUI;
        EventManager.I.OnScoreUpdate -= UpdateScoreUI;
    }

    private void UpdatePlayerStrikeUI(int _playerStrikeAmount)
    {
        for(int i = 0; i < m_strikeImages.Length; i++)
        {
            if(i <= _playerStrikeAmount)
            {
                m_strikeImages[i].enabled = true;
                continue;
            }

            m_strikeImages[i].enabled = false;
        }
    }

    private void UpdateScoreUI(int _newScore)
    {
        m_scoreText.text = _newScore.ToString();
    }

    private void HandlePause()
    {
        if(GameManager.I.IsGamePaused())
        {
            GameManager.I.PauseGame();
            Debug.Log("Game Paused");
            return;
        }

        Debug.Log(GameManager.I + " Game Unpaused");
        GameManager.I.UnPauseGame();
    }

    private void ShowPauseMenu()
    {
        m_pauseMenu.SetActive(true);
    }

    private void HidePauseMenu()
    {
        m_pauseMenu.SetActive(false);
    }
}
