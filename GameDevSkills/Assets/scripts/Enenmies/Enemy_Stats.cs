using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{

    public int EHp;
    public float Attack;
    public GameObject KillPartcle;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EHp <= 0)
        {
            ParticleEffect(gameObject.transform);
            Destroy(gameObject);
        }
    }

    public void ParticleEffect(Transform Enemy)
    {
        Instantiate(KillPartcle, Enemy.position, Enemy.rotation);
    }
}
