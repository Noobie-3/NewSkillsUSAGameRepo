using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingZone : MonoBehaviour
{
    public string area;
    
    private void OnTriggerStay(Collider other)
    {
      SceneManager.LoadScene(area);  
    }
}
