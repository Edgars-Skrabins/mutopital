using System;

public class EventManager : Singleton<EventManager>
{
    public event Action OnGamePaused;
    public event Action OnGameUnPaused;
}
