public class GameManager : Singleton<GameManager>
{
    private bool m_isGamePaused;

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
        EventManager.I.OnGamePaused += PauseGame;
        EventManager.I.OnGameUnPaused += UnPauseGame;
    }

    private void UnSubscribeEvents()
    {
        EventManager.I.OnGamePaused -= PauseGame;
        EventManager.I.OnGameUnPaused -= UnPauseGame;
    }

    private void Initialize()
    {
    }

    public void PauseGame()
    {
        m_isGamePaused = true;
        EventManager.I.OnGamePaused_Invoke();
    }

    public void UnPauseGame()
    {
        m_isGamePaused = false;
        EventManager.I.OnGameUnPaused_Invoke();
    }

    public bool IsGamePaused()
    {
        return m_isGamePaused;
    }

    public void LoseGame()
    {
        PauseGame();
    }
}
