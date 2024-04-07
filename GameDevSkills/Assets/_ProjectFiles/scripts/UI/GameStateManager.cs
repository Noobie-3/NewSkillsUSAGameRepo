using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public NewSceneInfo newSceneInfo;
    public static GameStateManager Instance;
    public Vector3 newPos;
    public string newScene;
    [SerializeField]
    bool DoSetPos;
    // Start is called before the first frame update
public void StartGame()
    {
        newSceneInfo.newPoition = newPos;
        SceneManager.LoadScene(newScene);
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (GameController.instance != null && DoSetPos == true)
        {
            GameController.instance.Player.transform.position = newSceneInfo.newPoition;
            
        }
        DoSetPos = true;
    }
    public void ExitGame()
    {
        Application.Quit();
    }


}
