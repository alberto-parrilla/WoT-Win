using CoreDatabase.Context.Data;
using KernelDLL.Game.Enums;
using KernelDLL.Game.Models;

namespace KernelDLL.Common
{
    public class CreateFactory : ICreateFactory
    {
        public BaseSkillModel CreateBaseSkillModel(Skill model)
        {

            if (model.Mode == (int)EnumSkillMode.Active)
            {

            }
            else if (model.Mode == (int)EnumSkillMode.Pasive)
            {

            }


            return null;
        }

        public BaseSkillModel CreateBaseFeatModel(Feat model)
        {
            if (model.Mode == (int)EnumSkillMode.Active)
            {

            }
            else if (model.Mode == (int)EnumSkillMode.Pasive)
            {

            }

            return null;
        }


        public BaseWeaveModel CreateBaseWeaveModel(Weave model)
        {
            return null;
        }
    }
}
