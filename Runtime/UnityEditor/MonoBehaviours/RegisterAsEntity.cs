using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EcsRx.Collections;
using EcsRx.Collections.Database;
using EcsRx.Collections.Entity;
using EcsRx.Components;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Plugins.Views.Components;
using EcsRx.Unity.MonoBehaviours;
using EcsRx.UnityEditor.Extensions;
using EcsRx.Zenject;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace EcsRx.UnityEditor.MonoBehaviours
{
    public abstract class RegisterAsEntity : MonoBehaviour, IConvertToEntity
    {
        public IEntityDatabase EntityDatabase { get; private set; }

        [FormerlySerializedAs("CollectionName")] 
        [SerializeField]
        public int CollectionId;

        public void RegisterEntity()
        {
            if (!gameObject.activeInHierarchy || !gameObject.activeSelf) { return; }

            IEntityCollection collectionToUse;

            if (CollectionId == 0)
            { collectionToUse = EntityDatabase.GetCollection(); }
            else if (EntityDatabase.Collections.All(x => x.Id != CollectionId))
            { collectionToUse = EntityDatabase.CreateCollection(CollectionId); }
            else
            { collectionToUse = EntityDatabase.GetCollection(CollectionId); }

            var entityView = gameObject.GetComponent<EntityView>();
            if (entityView != null)
            {
                SetupEntityComponents(entityView.Entity);
            }
            else
            {
                var createdEntity = collectionToUse.CreateEntity();
                createdEntity.AddComponents(new ViewComponent { View = gameObject });
                SetupEntityBinding(createdEntity, collectionToUse);
                SetupEntityComponents(createdEntity);
            }

            Destroy(this);
        }

        IEnumerator Start()
        {
            while (EcsRxApplicationBehaviour.Instance == null)
                yield return null;

            while (!EcsRxApplicationBehaviour.Instance.Started)
                yield return null;

            while (EcsRxApplicationBehaviour.Instance.EntityDatabase == null)
                yield return null;

            EntityDatabase = EcsRxApplicationBehaviour.Instance.EntityDatabase;

            RegisterEntity();
        }

        private void SetupEntityBinding(IEntity entity, IEntityCollection entityCollection)
        {
            var entityBinding = gameObject.AddComponent<EntityView>();
            entityBinding.Entity = entity;
            entityBinding.EntityCollection = entityCollection;
        }

        private void SetupEntityComponents(IEntity entity)
        {
            var components = GetComponents<IConvertToEntity>();
            foreach (var component in components)
            {
                component.Convert(entity);
            }
        }

        public virtual void Convert(IEntity entity)
        {
            Destroy(this);
        }
    }
}