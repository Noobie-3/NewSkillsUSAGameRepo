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
    public bool DoSetPos;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Instance Control
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject); // Destroy the old instance
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Make sure this instance persists across scenes
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (GameController.instance != null && DoSetPos == true)
        {
            GameController.instance.Player.transform.position = newSceneInfo.newPoition;
        }
        DoSetPos = true;
    }

    // Method to start the game
    public void StartGame()
    {
        newSceneInfo.newPoition = newPos;
        SceneManager.LoadScene(newScene);
    }

    // Method to exit the game
    public void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        // Update method content if needed
    }
}
