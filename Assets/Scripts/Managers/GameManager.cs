using UnityEngine.SceneManagement;
using Time = UnityEngine.Time;

public class GameManager : Singleton<GameManager>
{
    private bool m_isGamePaused;
    private bool m_hasGameStarted;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

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
    }

    private void UnSubscribeEvents()
    {
        InputManager.I.OnPausePerformed -= HandlePause;
    }

    private void Initialize()
    {
        //PauseGame();
    }

    private void HandlePause()
    {
        if (m_isGamePaused)
        {
            UnPauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        m_isGamePaused = true;
        Time.timeScale = 0;
        EventManager.I.OnGamePaused_Invoke();
    }

    public void UnPauseGame()
    {
        m_isGamePaused = false;
        Time.timeScale = 1;
        EventManager.I.OnGameUnPaused_Invoke();
    }

    public bool IsGamePaused()
    {
        return m_isGamePaused;
    }

    public void StartGame()
    {
        SetHasGameStarted(true);
    }

    public bool HasGameStarted()
    {
        return m_hasGameStarted;
    }

    public void SetHasGameStarted(bool _hasGameStarted)
    {
        m_hasGameStarted = _hasGameStarted;
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(0);
        PauseGame();
        //EventManager.I.OnGameLoss_Invoke();
    }
}
