using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenialsDontDestory : MonoBehaviour
{

    public static EssenialsDontDestory instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

    }
}
