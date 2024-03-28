using Den.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Basic : MonoBehaviour
{

    [SerializeField]
    private GameObject spawnableItem;
    [SerializeField] private float Interval;
    [SerializeField] private float DefaultInterval;
    [SerializeField] private Vector3 Direction;
    [SerializeField] private Vector3 StartDir;
    [SerializeField] private TimeRewinderV2 tr;
    // Start is called before the first frame update
    void Start()
    {
        StartDir = transform.position;
        if (GameObject.FindGameObjectWithTag("Player_01").GetComponent<TimeRewinderV2>() != null)
        {
            tr = GameObject.FindGameObjectWithTag("Player_01").GetComponent<TimeRewinderV2>();

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.IsPaused != true)
        {


            if (GameObject.FindGameObjectWithTag("Player_01").GetComponent<TimeRewinderV2>() != null)
            {
                tr = GameObject.FindGameObjectWithTag("Player_01").GetComponent<TimeRewinderV2>();

            }
            if (tr != null)
            {


                if (tr.Isrewinding == false)
                {
                    if (Interval > 0)
                    {
                        Interval -= Time.deltaTime;
                    }
                    else
                    {
                        Interval = DefaultInterval;
                        Spawn_Object();
                    }
                }
            }
        }
    }



    private void Spawn_Object()
    {
        Instantiate(spawnableItem, StartDir + Direction, Quaternion.identity);
    }



}