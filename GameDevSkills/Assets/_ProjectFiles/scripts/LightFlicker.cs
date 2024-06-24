using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public GameObject light;
    public float RandomNunm;
    public float MinRandomTime = 0;
    public float MaxRandomTime = 10;
    public float DefaultRandomTime;
    public float defaultLighInten = 1;
    void Start()    {
        light = this.gameObject;
    }
       void Update()
    {
        if(!(RandomNunm <= 0))        {
            RandomNunm -= Time.deltaTime;
        }
        else        {
            if(light.GetComponent<Light>().intensity == 0)            {
                light.GetComponent<Light>().intensity = defaultLighInten;
            }
            else            {
                light.GetComponent<Light>().intensity = 0;
            }
                RandomNunm = Random.Range(MinRandomTime, MaxRandomTime);
                print(RandomNunm);                       
        }
    }
}
