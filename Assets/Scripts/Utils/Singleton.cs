using UnityEngine;

public abstract class Singleton : MonoBehaviour
{
    public static Singleton Init(Singleton singleton = null, string managerType = "")
    {
        var go = new GameObject();
        go.AddComponent(singleton.GetType());

        go.name = managerType == "" ? singleton.GetType().Name: managerType;

        DontDestroyOnLoad(go);

        return go.GetComponent<Singleton>();
    }

}