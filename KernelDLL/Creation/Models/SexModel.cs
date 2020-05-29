using System;
using CoreDatabase.Context.Data;
using KernelDLL.Common;
using KernelDLL.Creation.Interfaces;

namespace KernelDLL.Creation.Models
{
    [Serializable]
    public class GenderModel : IMergeable<Gender>, ISelectable
    {
        public GenderModel(Gender Gender)
        {
            Merge(Gender);
        }

        public int GameId { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }

        public void Merge(Gender source)
        {
            GameId = source.GameId;
            DisplayName = source.DisplayName;
            Description = source.Description;
        }
    }
}
