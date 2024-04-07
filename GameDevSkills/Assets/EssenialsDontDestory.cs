using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenialsDontDestory : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
