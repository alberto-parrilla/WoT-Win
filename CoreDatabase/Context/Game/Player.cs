using System.Collections.Generic;
using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Game
{
    public class Player : GameBusinessEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Race { get; set; }
        public int Gender { get; set; }
        public int Location { get; set; }
        public bool IsChanneler { get; set; }
        public string Portrait { get; set; }
        public string Avatar { get; set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public int MaxEnergy { get; set; }
        public int CurrentEnergy { get; set; }
        public int ExpPoints { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<PlayerSkill> Skills { get; set; }
        public List<PlayerFeat> Feats { get; set; }
        public List<PlayerItem> Items { get; set; }
        public List<PlayerWeave> Weaves { get; set; }
    }
}
