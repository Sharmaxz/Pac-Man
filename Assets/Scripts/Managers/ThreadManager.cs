using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ThreadManager : Singleton
{
    public static ThreadManager instance = new ThreadManager();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        instance = (ThreadManager)Init(instance);
    }
    
    
    
    public void GameModeWaitFor(float seconds, GameManager gameManager, string mode)
    {
        StartCoroutine(GameModeWaitForRoutine(seconds, gameManager, mode));
    }
    
    IEnumerator GameModeWaitForRoutine(float seconds, GameManager gameManager, string mode)
    {
        yield return new WaitForSeconds(seconds);

        gameManager.setGhostMode(mode);
    }
    

    public void WaitFor(float seconds, UnityAction callback)
    {
        StartCoroutine(WaitForRoutine(seconds, callback));
    }
    
    IEnumerator WaitForRoutine(float seconds, UnityAction callback)
    {
        yield return new WaitForSeconds(seconds);

        callback.Invoke();
    }
}
