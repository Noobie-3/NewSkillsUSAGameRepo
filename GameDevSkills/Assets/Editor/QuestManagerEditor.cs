namespace  GameDevSkills
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(QuestManager))]
    public class QuestManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            QuestManager questManager = (QuestManager)target;

            if (GUILayout.Button("Add New Quest"))
            {
                questManager.AddNewQuest(questManager.quests.Count + 1, "New Quest", "Description");
            }
        }
    }
}
