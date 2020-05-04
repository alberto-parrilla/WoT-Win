﻿using System;
using CoreDatabase.Context.Data;
using KernelDLL.Common;

namespace KernelDLL.Game.Models
{
    public class AreaModel : IMergeable<Area>
    {
        public AreaModel(Area area)
        {
            Merge(area);
        }

        public int Id { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }

        public void Merge(Area source)
        {
            if (source == null) throw new NullReferenceException("Area cannot be null");
            Id = source.Id;
            DisplayName = source.DisplayName;
            Description = source.Description;
        }
    }
}
