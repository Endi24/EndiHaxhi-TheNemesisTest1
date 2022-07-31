using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingletonClass<T> : MonoBehaviour where T : Component
{
    //this is a class I have made so that It automatically builds singletons once you inherit from it.
    private static T instance;

    //This section ensures that at least 1 object in the scene will have the Singleton class. If they are found not to have it, the Class(Component) attaches to it automatically
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    // This makes sure there is only one Object of this type live on the scene, so there aren't more than one instances simultaneously.
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}