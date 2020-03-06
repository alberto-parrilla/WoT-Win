using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Game.Models;

namespace KernelDLL.Common
{
    public interface IRepositoryManager
    {
        List<TransitionModel> LoadTransitions(string areaName);
    }
}
