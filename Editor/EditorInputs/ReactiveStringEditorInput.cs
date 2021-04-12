#if UNITY_EDITOR
using UniRx;
using UnityEditor;

namespace EcsRx.Unity.EditorCode.EditorInputs
{
    public class ReactiveStringEditorInput : SimpleEditorInput<StringReactiveProperty>
    {
        protected override StringReactiveProperty CreateTypeUI(string label, StringReactiveProperty value)
        {
            value.Value = EditorGUILayout.TextField(label, value.Value);
            return null;
        }
    }
}
#endif