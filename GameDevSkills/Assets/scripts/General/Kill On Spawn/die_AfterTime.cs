using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class die_AfterTime : MonoBehaviour
{
    public int TimeOFDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    Destroy(gameObject, TimeOFDeath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
