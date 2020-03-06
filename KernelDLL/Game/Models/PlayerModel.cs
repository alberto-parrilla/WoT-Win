using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Game.Enums;

namespace KernelDLL.Game.Models
{
    public class PlayerModel
    {
        public PlayerModel(int id, string name, int race, int sex, int nation, bool isChanneler,
            AttributeModel str, AttributeModel dex, AttributeModel con, AttributeModel intell, AttributeModel wis, AttributeModel cha,
            List<PlayerSkillModel> skills, List<PlayerFeatModel> feats, List<PlayerItemModel> items)
        {
            Id = id;
            Name = name;
            Race = (EnumRace)race;
            Sex = (EnumSex) sex;
            Nation = nation;
            IsChanneler = isChanneler;
            Str = str;
            Dex = dex;
            Con = con;
            Int = intell;
            Wis = wis;
            Cha = cha;

            Items = items;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public EnumRace Race { get; private set; }
        public EnumSex Sex { get; private set; }
        public int Nation { get; private set; }
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
    }
}
