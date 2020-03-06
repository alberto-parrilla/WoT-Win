using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using KernelDLL.Game.Models;

namespace KernelDLL.Common
{
    public class RepositoryManager : IRepositoryManager
    {
        public List<TransitionModel> LoadTransitions(string areaName)
        {
           var list = new List<TransitionModel>();

           
               XDocument xmlFile = XDocument.Load(areaName);
               var transitions = xmlFile.Descendants().Where(d => d.Name.LocalName == "objectgroup" && d.Attributes().Any(a => a.Name == "name" && a.Value.StartsWith("Transition"))); //.Where(p => p.Name.LocalName.StartsWith("Transition"));
               foreach (var transition in transitions)
               {
                   var transitionName = transition.Attributes().Where(t => t.Name == "name").Select(t => t.Value).FirstOrDefault();
                   var transitionId = transitionName.ToString().TrimStart("Transition".ToCharArray());
                   var data = transition.Descendants().Where(d => d.Name.LocalName == "object").FirstOrDefault();
                   if (data != null)
                   {

                       var x = Convert.ToInt32(Convert.ToDouble(data.Attributes().Where(a => a.Name == "x").Select(a => a.Value).FirstOrDefault()) * 3 / 4);
                       var y = Convert.ToInt32(Convert.ToDouble(data.Attributes().Where(a => a.Name == "y").Select(a => a.Value).FirstOrDefault()) * 3 / 4);
                       var widthStr = data.Attributes().Where(a => a.Name == "width").Select(a => a.Value).FirstOrDefault();
                       var width = Convert.ToInt32(Convert.ToDouble(widthStr.Replace(".",",")) * 3 / 4);
                       var heightStr = data.Attributes().Where(a => a.Name == "height").Select(a => a.Value).FirstOrDefault();
                       var height = Convert.ToInt32(Convert.ToDouble(heightStr.Replace(".",",")) * 3 / 4);
                       list.Add(new TransitionModel(Convert.ToInt32(transitionId), x, y, width, height));
                   }
               }
           

           return list;
        }
    }
}
