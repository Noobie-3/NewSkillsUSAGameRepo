using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMesh_simpleEnemy : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField]private NavMeshAgent nav;
    private void Start()
    {
        target = GameController.instance.Player.transform;

    }
    void Update()
    {
        nav.destination = target.position;
    }
}
