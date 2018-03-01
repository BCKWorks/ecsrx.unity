﻿using System;
using EcsRx.Entities;
using EcsRx.Groups;
using EcsRx.Groups.Accessors;
using EcsRx.Systems;
using EcsRx.Unity.Examples.SimpleMovement.Components;
using EcsRx.Views.Components;
using UniRx;
using UnityEngine;

namespace EcsRx.Unity.Examples.SimpleMovement.Systems
{
    public class PlayerControlSystem : IReactToGroupSystem
    {
        public readonly float MovementSpeed = 2.0f;

        public IGroup TargetGroup => new GroupBuilder()
            .WithComponent<ViewComponent>()
            .WithComponent<PlayerControlledComponent>()
            .Build();

        public IObservable<IObservableGroup> ReactToGroup(IObservableGroup group)
        { return Observable.EveryUpdate().Select(x => group); }

        public void Execute(IEntity entity)
        {
            var strafeMovement = 0f;
            var forardMovement = 0f;

            if (Input.GetKey(KeyCode.A)) { strafeMovement = -1.0f; }
            if (Input.GetKey(KeyCode.D)) { strafeMovement = 1.0f; }
            if (Input.GetKey(KeyCode.W)) { forardMovement = 1.0f; }
            if (Input.GetKey(KeyCode.S)) { forardMovement = -1.0f; }

            var viewComponent = entity.GetComponent<ViewComponent>();
            var view = viewComponent.View as GameObject;
            var transform = view.transform;

            var newPosition = view.transform.position;
            newPosition.x += strafeMovement * MovementSpeed * Time.deltaTime;
            newPosition.z += forardMovement * MovementSpeed * Time.deltaTime;

            transform.position = newPosition;
        }
    }
}