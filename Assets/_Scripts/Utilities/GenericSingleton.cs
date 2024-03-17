using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T: GenericSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get { return _instance; }
        private set { }
    }
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            Debug.Log(typeof(T).ToString() + " is NULL.");
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log(typeof(T).ToString() + " has tried to instantiate again!");
        }

    }

    protected virtual void OnApplicationQuit()
    {
        _instance = null;
        Destroy(gameObject);
    }
}
