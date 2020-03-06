using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDLL.Creation.Interfaces
{
    public interface IAvatarGenerator
    {
        string Portrait { get; }
        string Avatar { get; }
    }
}
