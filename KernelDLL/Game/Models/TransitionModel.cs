using System;
using System.Collections.Generic;
using CoreDatabase.Context.Data;
using KernelDLL.Common;

namespace KernelDLL.Game.Models
{
    public class TransitionModel : IMergeable<Transition>
    {
        public TransitionModel(Transition transition)
        {
            Merge(transition);
        }

        public int Id { get; private set; }
        public int SourceArea { get; private set; }
        public int SourceScene { get; private set; }
        public int TargetArea { get; private set; }
        public int TargetScene { get; private set; }
        public List<int> Coords { get; private set; }
        public string CoordsToString { get { return Coords == null ? null : string.Join(",", Coords.ToArray()); } }
        public string Display { get; private set; }

        public void Merge(Transition source)
        {
            if (source == null) throw new NullReferenceException("Transition cannot be null");
            Id = source.Id;
            SourceArea = source.SourceArea;
            SourceScene = source.SourceScene;
            TargetArea = source.TargetArea;
            TargetScene = source.TargetScene;
            Coords = source.Coords;
            Display = source.Display;
        }
    }
}
