﻿using System;
using CoreDatabase.Context.Data;
using KernelDLL.Common;
using KernelDLL.Game.Enums;

namespace KernelDLL.Game.Models
{
    [Serializable]
    public class SceneModel : IMergeable<Scene>
    {
        public SceneModel(Scene scene)
        {
            Merge(scene);
        }

        public int Id { get; private set; }
        public int AreaGameId { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public EnumSceneType Type { get; private set; }
        public bool HasMap => Type == EnumSceneType.Outside || Type == EnumSceneType.Inside;
        public string Map => HasMap ? Util.GetMapScene(Id) : null;

        public IServiceModel Service { get; private set; }

        public void Merge(Scene source)
        {
            if (source == null) throw new NullReferenceException("Scene cannot be null");
            Id = source.Id;
            AreaGameId = source.AreaGameId;
            DisplayName = source.DisplayName;
            Description = source.Description;
            Type = (EnumSceneType)source.Type;
        }
    }
}
