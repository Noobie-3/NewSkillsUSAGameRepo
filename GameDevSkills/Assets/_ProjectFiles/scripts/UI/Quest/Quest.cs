namespace GameDevSkills
{
    using UnityEngine;
    using System.Collections.Generic;

    [System.Serializable]
    public class Quest
    {
        public int ID;
        public string title;
        public string description;
        public bool isCompleted;

        public Quest(int id, string title, string description)
        {
            this.ID = id;
            this.title = title;
            this.description = description;
            this.isCompleted = false;
        }

        public void CompleteQuest()
        {
            isCompleted = true;
            Debug.Log("Quest '" + title + "' completed!");
            // Additional logic for quest completion
        }
    }
}
