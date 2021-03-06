﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KernelDLL.Game.Models;
using KernelDLL.Init;

namespace KernelDLL.Database
{
    public interface IDatabase
    {
        IList<SavedGameModel> LoadSavedGames();
        PlayerModel LoadPlayer(int id);       
        AreaModel LoadArea(int id);      
        SceneModel LoadScene(int areaId, int id);
    }
}
