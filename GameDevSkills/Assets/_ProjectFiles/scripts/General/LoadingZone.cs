using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingZone : MonoBehaviour
{
    public string area;
    public Vector3 NewPos;
    public NewSceneInfo NewSceneInfo;
    public GameObject gameStateManager;
    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player_01"))
        {
            if (GameStateManager.Instance != null)
            {
                NewSceneInfo.newPoition = NewPos;
                Instantiate(gameStateManager);
                SceneManager.LoadScene(area);

            }
        }
    }

    private void Update()
    {
        if(GameStateManager.Instance  == null)
        {
            Instantiate(gameStateManager);
        }
    }
}
