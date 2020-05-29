using CoreDatabase.Context.Data;
using KernelDLL.Game.Models;

namespace KernelDLL.Common
{
    public interface ICreateFactory
    {
        BaseSkillModel CreateBaseSkillModel(Skill model);
        BaseSkillModel CreateBaseFeatModel(Feat model);
        BaseWeaveModel CreateBaseWeaveModel(Weave model);
    }
}
