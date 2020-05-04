using System;
using KernelDLL.Common;

namespace KernelDLL.Game.Models
{
    [Serializable]
    public class PlayerInfoModel : IMergeable<PlayerModel>
    {
        public PlayerInfoModel(PlayerModel model)
        {
            Merge(model);
        }

        public int UserId { get; private set; }
        public int PlayerId { get; private set; }
        public string Name { get; private set; }
        public string Avatar { get; private set; }

        public void Merge(PlayerModel source)
        {
            //UserId = source.UserId;
            PlayerId = source.Id;
            Name = source.Name;
            //Avatar = source.Avatar;
        }
    }
}
