using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip[] levelMusic; // Array of music clips for each level

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayLevelMusic(0); // Start playing music for the initial level (index 0)
    }

    void PlayLevelMusic(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelMusic.Length)
        {
            audioSource.clip = levelMusic[levelIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Invalid level index or music array length.");
        }
    }

    public void ChangeLevel(int newLevel)
    {
        // Call this method when transitioning to a new level
        PlayLevelMusic(newLevel);
    }
}
