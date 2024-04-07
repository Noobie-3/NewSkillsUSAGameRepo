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
            if(GameStateManager.Instance != null)
            {
                NewSceneInfo.newPoition = NewPos;
                Destroy(GameStateManager.Instance);
                Instantiate(GameStateManager.Instance);
                SceneManager.LoadScene(area);
            }
            else Instantiate(gameStateManager);

        }
    }
}
