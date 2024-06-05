namespace GameDevSkills
{
    using UnityEngine;
    using UnityEditor;
    using System.Collections;

    [CustomEditor(typeof(MoveAlongwayPoints))]
    public class MoveAlongPointsEditor : Editor
    {
        private IEnumerator previewCoroutine;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MoveAlongwayPoints Object = (MoveAlongwayPoints)target;

            if (GUILayout.Button("Add New WayPoint"))
            {
                Object.AddNewWayPoint();
            }

            if (GUILayout.Button("Preview Movement"))
            {
                // If a preview coroutine is already running, stop it first
                if (previewCoroutine != null)
                {
                    EditorApplication.update -= UpdatePreview;
                    previewCoroutine = null;
                }

                // Initialize and start the movement preview
                Object.PreviewMovementInEditor();
                previewCoroutine = PreviewMovement(Object);
                EditorApplication.update += UpdatePreview;
            }

            if (GUILayout.Button("Stop Preview"))
            {
                // If a preview coroutine is running, stop it
                if (previewCoroutine != null)
                {
                    Object.transform.position = Object.WayPoints[0];
                    EditorApplication.update -= UpdatePreview;
                    previewCoroutine = null;
                }
            }
        }
        // Method called on each editor update cycle
        private void UpdatePreview()
        {
            // Advance the preview coroutine if it exists
            if (previewCoroutine != null)
            {
                previewCoroutine.MoveNext();
            }
        }

        // Coroutine to handle the movement preview
        private IEnumerator PreviewMovement(MoveAlongwayPoints Object)
        {
            while (true)
            {
                // Execute the movement logic
                Object.MoveAlongWaypoints();
                yield return null; // Continue on the next editor update cycle
            }
        }

    }

}

