using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependOnObject : MonoBehaviour
{
    public GameObject RelieentObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RelieentObject is not null) 
        {
            if(RelieentObject.activeSelf == true)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
                print("Test");
            }
        }

    }
}
