using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDLL.Game.Enums
{
    public enum EnumRace
    {
        Human,
        Ogier
    }

    public enum EnumSex
    {
        Male,
        Female        
    }

    public enum EnumHumanNation
    {
        Aiel,
        Altara,
        Amadicia,
        Andor,
        AradDoman,
        Arafel,
        AthaanMiere,
        Cairhien,
        Ghealdan,
        Illian,
        Kandor,
        Murandy,
        Saldaea,
        Shienar,
        Tarabon,
        Tear
    }

    public enum EnumOgierNation
    {
        Stedding
    }

    public enum EnumAttribute
    {
        Str,
        Dex,
        Con,
        Int,
        Wis,
        Cha
    }

    public enum EnumSkillMode
    {
        Active,
        Pasive
    }

    public enum EnumSkillDistance
    {
        Near,
        Distance
    }

    public enum EnumSkillArea
    {
        Target,
        Zone
    }

    public enum EnumSkillDuration
    {
        None,
        Time
    }

    public enum EnumFeatType
    {
        Attack,
        Defense,
        Weaves
    }

    public enum EnumWeaveType
    {
        Earth,
        Fire,
        Water,
        Wind,
        Energy
    }

    public enum EnumResourceType
    {
        Skin,
        Face,
        Hair1,
        Hair2,
        Eyes,
        BaseEyes,
        Ears,
        Nose,
        Mouth,
        Beard,
        Extras
    }

    public enum EnumObjectType
    {
        Sword,
        None
    }

    public enum EnumSlotType
    {
        Head,
        Neck,
        Cloak,
        Body,
        Belt,
        Glove,
        Ring,
        Item,
        Legs,
        Foot
    }

    public enum EnumMove
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum EnumSceneType
    {
        Outside,
        Inside,
        Service
    }

    public enum EnumServiceType
    {
        Blacksmith,
        Inn,
        Food,
        Herbolary
    }
}
