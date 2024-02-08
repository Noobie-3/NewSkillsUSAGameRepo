using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Win_Flag : MonoBehaviour
{
    public interface IQuestCompleter
    {
        void CompleteQuest();
    }
}
