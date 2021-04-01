#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EcsRx.UnityEditor.Editor.EditorInputs
{
    public class LayerMaskEditorInput : SimpleEditorInput<LayerMask>
    {
        protected override LayerMask CreateTypeUI(string label, LayerMask value)
        {
            List<string> layerNames = new List<string>();
            for (int i = 0; i < 32; i++)
            {
                string layerName = LayerMask.LayerToName(i);
                if (!string.IsNullOrEmpty(layerName))
                    layerNames.Add(layerName);
            }

            return EditorGUILayout.MaskField(label, value.value, layerNames.ToArray());
        }
    }
}
#endif