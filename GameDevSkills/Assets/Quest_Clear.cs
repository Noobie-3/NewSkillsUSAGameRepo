using GameDevSkills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Clear : MonoBehaviour
{
    private void Start()
    {
        if (!QuestManager.Instance.quests[5].isCompleted)
        {
            QuestManager.Instance.quests[5].CompleteQuest();
        }
    }


}
