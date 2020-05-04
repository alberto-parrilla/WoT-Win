using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Common;
using KernelDLL.Game.Interfaces;

namespace KernelDLL.Game.Models
{
    public class SceneModelLegacy : IScene
    {
        public SceneModelLegacy(int id, string descriptor, string header, int areaId, int width, int height, IList<TransitionModelLegacy> transitions,
            IList<ObjectModel> objects)
        {
            Id = id;
            Descriptor = descriptor;
            Header = header;
            AreaId = areaId;
            Width = width;
            Height = height;
            Transitions = transitions;
            Objects = objects;
        }

        public int Id { get; private set; }
        public string Descriptor { get; private set; }
        public string Header { get; private set; }
        public int AreaId { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public string Map
        {
            get { return string.Format(@"{0}\{1}\{2}_{3}_{4}_48px.png", Util.AreasPath, AreaId, AreaId, Id, Descriptor); }
        }

        public string BackgroundMap
        {
            get { return string.Format(@"{0}\{1}\{2}_BackgroundMap.png", Util.AreasPath, AreaId, Id); }
        }

        public IList<TransitionModelLegacy> Transitions { get; private set; }
        public IList<ObjectModel> Objects { get; private set; }

        public void LoadTransitions(IRepositoryManager repositoryManager)
        {
            Transitions = repositoryManager.LoadTransitions(string.Format(@"{0}\{1}\{2}_{3}_{4}_48px.tmx", Util.AreasPath, AreaId, AreaId, Id, Descriptor));
        }
    }
}
