using System;
using System.Collections.Generic;
using System.Data.Entity;
using CoreDatabase.Context.Game;
using Attribute = CoreDatabase.Context.Game.Attribute;

namespace CoreDatabase.Context
{
    public class GameDbContextInitializer : CreateDatabaseIfNotExists<GameDbContext>
    {
        protected override void Seed(GameDbContext context)
        {
            var player = new Player()
            {
                GameId = 1,
                UserId = 1,
                Name = "Gwalchaved",
                Race = 0,
                Sex = 0,
                Location = 1,
                IsChanneler = false,
                MaxHp = 120,
                CurrentHp = 85,
                MaxEnergy = 110,
                CurrentEnergy = 90,
                ExpPoints = 5250,
                PosX = 64,
                PosY = 64,
                Attributes = new List<Attribute>()
                {
                    new Attribute()
                    {
                        Type = 0,
                        BaseValue = 12,
                        RaceMod = 0,
                        LocationMod = 0
                    },
                    new Attribute()
                    {
                        Type = 1,
                        BaseValue = 12,
                        RaceMod = 0,
                        LocationMod = 0
                    },
                    new Attribute()
                    {
                        Type = 2,
                        BaseValue = 15,
                        RaceMod = 1,
                        LocationMod = 0
                    },
                    new Attribute()
                    {
                        Type = 3,
                        BaseValue = 14,
                        RaceMod = 0,
                        LocationMod = 1
                    },
                    new Attribute()
                    {
                        Type = 4,
                        BaseValue = 9,
                        RaceMod = 0,
                        LocationMod = 0
                    },
                    new Attribute()
                    {
                        Type = 5,
                        BaseValue = 10,
                        RaceMod = 0,
                        LocationMod = 0
                    },
                }
            };

            context.Players.Add(player);
            context.SaveChanges();

            var gameSession = new GameSession()
            {
                GameId = 1,
                UserId = 1,
                AreaId = 1,
                SceneId = 1,
                PlayerId = 1,
                SavedTime = DateTime.Now
            };

            context.GameSessions.Add(gameSession);
            context.SaveChanges();
        }
    }
}
