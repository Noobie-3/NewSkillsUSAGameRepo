namespace GameDevSkills
{ 
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<Quest> quests = new List<Quest>();
    public TextMeshProUGUI questDisplay;
        public AudioSource questAudioSource;

    private Quest currentQuest;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
        private void Update()
        {
            SetCurrentQuest();
        }

        private void Start()
    {
        UpdateQuestDisplay();
    }

    public bool IsQuestCompleted(int questID)
    {
        foreach (Quest quest in quests)
        {
            if (quest.ID == questID)
            {
                return quest.isCompleted;
            }
        }
        return false;
    }

    public void CompleteQuest(int questID)
    {
        foreach (Quest quest in quests)
        {
            if (quest.ID == questID)
            {
                quest.CompleteQuest();
                    questAudioSource.Play();
                break; // Exit loop after completing the quest
            }
        }
        UpdateQuestDisplay(); // Update quest display after completing a quest
    }

    public void AddNewQuest(int id, string title, string description)
    {
        Quest newQuest = new Quest(id, title, description);
        quests.Add(newQuest);
        UpdateQuestDisplay();
    }

    public void SetCurrentQuest(int questID = -1)
    {
        if (questID == -1 && quests.Count > 0)
        {
            int lowestID = int.MaxValue;
            foreach (Quest quest in quests)
            {
                if (!quest.isCompleted && quest.ID < lowestID)
                {
                    lowestID = quest.ID;
                }
            }

            foreach (Quest quest in quests)
            {
                if (quest.ID == lowestID)
                {
                    currentQuest = quest;
                    break; // Exit loop after setting the current quest
                }
            }
        }
        else
        {
            foreach (Quest quest in quests)
            {
                if (quest.ID == questID && !quest.isCompleted)
                {
                    currentQuest = quest;
                    break; // Exit loop after setting the current quest
                }
            }
        }
        UpdateQuestDisplay(); // Update quest display after setting the current quest
    }

    private void UpdateQuestDisplay()
    {
        if (currentQuest != null)
        {
            questDisplay.text = currentQuest.title;
        }
        else
        {
            questDisplay.text = "No Active Quest";
        }
    }
}
}
