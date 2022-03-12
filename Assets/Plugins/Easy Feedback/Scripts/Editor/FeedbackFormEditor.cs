using UnityEditor;
using UnityEngine;

namespace AeLa.EasyFeedback.Editor
{
    [CustomEditor(typeof(FeedbackForm))]
    internal class FeedbackFormEditor : UnityEditor.Editor
    {
        private SerializedProperty config;

        void Awake()
        {
            config = serializedObject.FindProperty("Config");
        }

        public override void OnInspectorGUI()
        {
            // show Trello info if setup
            if (config == null || !config.objectReferenceValue)
            {
                EditorGUILayout.LabelField(
                    "Easy Feedback is not yet configured!"
                );
                if (GUILayout.Button("Configure Now"))
                {
                    // TODO
                    // ConfigWindow.Init();
                }
            }
            else
            {
                EFConfig config = this.config.objectReferenceValue as EFConfig;
                if (string.IsNullOrEmpty(config.Token))
                {
                    EditorGUILayout.LabelField(
                        "Not authenticated with Trello!"
                    );
                    if (GUILayout.Button("Authenticate Now"))
                    {
                        // TODO
                        // ConfigWindow.Init();
                    }
                }
            }

            base.OnInspectorGUI();
        }
    }
}