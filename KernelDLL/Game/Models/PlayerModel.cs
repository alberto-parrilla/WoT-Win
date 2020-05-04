using System;
using System.Collections.Generic;
using System.Linq;
using CoreDatabase.Context.Game;
using KernelDLL.Common;
using KernelDLL.Game.Enums;

namespace KernelDLL.Game.Models
{
    [Serializable]
    public class PlayerModel : IMergeable<Player>
    {
        public PlayerModel(int id, string name, int race, int sex, int nation, bool isChanneler,
            AttributeModel str, AttributeModel dex, AttributeModel con, AttributeModel intell, AttributeModel wis, AttributeModel cha,
            List<PlayerSkillModel> skills, List<PlayerFeatModel> feats, List<PlayerItemModel> items)
        {
            Id = id;
            Name = name;
            Race = (EnumRace)race;
            Sex = (EnumSex) sex;
            Location = nation;
            IsChanneler = isChanneler;
            Str = str;
            Dex = dex;
            Con = con;
            Int = intell;
            Wis = wis;
            Cha = cha;

            Items = items;
        }

        public PlayerModel(Player model)
        { 
            Merge(model);
        }

        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public EnumRace Race { get; private set; }
        public EnumSex Sex { get; private set; }
        public int Location { get; private set; }
        public bool IsChanneler { get; private set; }
        public AttributeModel Str { get; private set; }
        public AttributeModel Dex { get; private set; }
        public AttributeModel Con { get; private set; }
        public AttributeModel Int { get; private set; }
        public AttributeModel Wis { get; private set; }
        public AttributeModel Cha { get; private set; }
        public List<PlayerSkillModel> Skills { get; private set; }
        public List<PlayerFeatModel> Feats { get; private set; }
        public List<PlayerItemModel> Items { get; private set; }

        public void Merge(Player source)
        {
            Id = source.GameId;
            UserId = source.UserId;
            Name = source.Name;
            Race = (EnumRace)source.Race;
            Sex = (EnumSex)source.Sex;
            Location = source.Location;
            IsChanneler = source.IsChanneler;

            Str = new AttributeModel(source.Attributes.FirstOrDefault(a => a.Type == 0));
            Dex = new AttributeModel(source.Attributes.FirstOrDefault(a => a.Type == 1));
            Con = new AttributeModel(source.Attributes.FirstOrDefault(a => a.Type == 2));
            Int = new AttributeModel(source.Attributes.FirstOrDefault(a => a.Type == 3));
            Wis = new AttributeModel(source.Attributes.FirstOrDefault(a => a.Type == 4));
            Cha = new AttributeModel(source.Attributes.FirstOrDefault(a => a.Type == 5));
        }
    }
}
