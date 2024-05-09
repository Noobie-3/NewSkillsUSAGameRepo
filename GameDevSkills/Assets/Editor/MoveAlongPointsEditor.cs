namespace GameDevSkills
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(MoveAlongwayPoints))]
    public class MoveAlongPointsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MoveAlongwayPoints Object = (MoveAlongwayPoints)target;

            if (GUILayout.Button("Add New WayPoint"))
            {
                Object.AddNewWayPoint();
            }
        }
    }
}
