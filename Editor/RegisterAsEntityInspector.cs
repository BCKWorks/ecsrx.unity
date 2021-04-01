#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using EcsRx.Components;
using EcsRx.Plugins.Views.Components;
using EcsRx.UnityEditor.Editor.Extensions;
using EcsRx.UnityEditor.Editor.UIAspects;
using EcsRx.UnityEditor.Extensions;
using EcsRx.UnityEditor.MonoBehaviours;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UEditor = UnityEditor.Editor;

namespace EcsRx.UnityEditor.Editor
{
    [Serializable]
    [CustomEditor(typeof(RegisterAsEntity))]
    public partial class RegisterAsEntityInspector : UEditor
    {
        private RegisterAsEntity _registerAsEntity;

        private void PoolSection()
        {
            this.UseVerticalBoxLayout(() =>
            {
                _registerAsEntity.CollectionId = this.WithNumberField("EntityCollection: ", _registerAsEntity.CollectionId);
            });
        }

        private void PersistChanges()
        {
            if (GUI.changed)
            { this.SaveActiveSceneChanges(); }
        }

        public override void OnInspectorGUI()
        {
            _registerAsEntity = (RegisterAsEntity)target;
            
            PoolSection();
            PersistChanges();
        }

        public override VisualElement CreateInspectorGUI()
        {
            _registerAsEntity = (RegisterAsEntity)target;

            return base.CreateInspectorGUI();
        }
    }
}
#endif