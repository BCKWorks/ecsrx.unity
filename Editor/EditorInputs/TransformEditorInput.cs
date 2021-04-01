#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace EcsRx.UnityEditor.Editor.EditorInputs
{
    public class TransformEditorInput : SimpleEditorInput<Transform>
    {
        protected override Transform CreateTypeUI(string label, Transform value)
        { return (Transform)EditorGUILayout.ObjectField(label, value, typeof(Transform), true); }
    }
}
#endif