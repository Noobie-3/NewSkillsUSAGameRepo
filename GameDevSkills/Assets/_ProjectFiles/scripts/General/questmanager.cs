using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class questmanager : MonoBehaviour
{ 
    public Text questText;
    public Button completeButton;

    public GameObject Text_Display;

    public  Queue<Quest> questQueue = new Queue<Quest>();

    private void Start()
    {
        // Initialize quests
        InitializeQuests();

        // Display the current quest
        DisplayCurrentQuest();
    }

    private void InitializeQuests()
    {
        questQueue.Enqueue(new Quest("Find a key To Get In Bolt'ys Office and find what he has been hiding from the workers."));
        questQueue.Enqueue(new Quest("Bolty's office key should open his door Find his office Fast."));
        questQueue.Enqueue(new Quest("find what he has been hiding."));
        questQueue.Enqueue(new Quest("This ''Time'' Upgrade Feels powerful Use it to escape through the top Floor door."));
    }

    private void DisplayCurrentQuest()
    {
        if (questQueue.Count == 0)
        {
            Debug.Log("No more quests.");
            return;
        }

        Quest currentQuest = questQueue.Peek();
        Text_Display.GetComponent<TextMeshProUGUI>().text = currentQuest.description;
    }

    public void OnComplete()
    {
        // Remove the completed quest from the queue
        questQueue.Dequeue();

        // Display the next quest
        DisplayCurrentQuest();
    }
    private void Update()
    {
        DisplayCurrentQuest() ;
    }
}

public class Quest
{
    public string description;

    public Quest(string desc)
    {
        description = desc;
    }
}


