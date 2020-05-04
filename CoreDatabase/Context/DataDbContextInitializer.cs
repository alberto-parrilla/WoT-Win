using System.Collections.Generic;
using System.Data.Entity;
using CoreDatabase.Context.Data;

namespace CoreDatabase.Context
{
    public class DataDbContextInitializer : CreateDatabaseIfNotExists<DataDbContext>
    {
        protected override void Seed(DataDbContext context)
        {
            var area1 = new Area()
            {
                GameId = 1,
                DisplayName = "Campo de Emond",
                Description = "Descripción de Campo de Emond"
            };

            var area2 = new Area()
            {
                GameId = 2,
                DisplayName = "Camino de la Cantera",
                Description = "Descripción de Camino de la Cantera"
            };

            var area3 = new Area()
            {
                GameId = 3,
                DisplayName = "Deven Ride",
                Description = "Descripción de Deven Ride"
            };

            var area4 = new Area()
            {
                GameId = 4,
                DisplayName = "Colina del Vigía",
                Description = "Descripción de Colina del Vigía"
            };

            context.Areas.Add(area1);
            context.Areas.Add(area2);
            context.Areas.Add(area3);
            context.Areas.Add(area4);


            var scene1 = new Scene()
            {
                GameId = 1,
                AreaGameId = 1,
                DisplayName = "Campo de Emond",
                Description = "Descripción de Campo de Emond",
                Type = 0
            };

            var scene2 = new Scene()
            {
                GameId = 2,
                AreaGameId = 1,
                DisplayName = "Posada del Manantial (PB)",
                Description = "Planta baja de la Posada del Manantial",
                Type = 2
            };

            var scene3 = new Scene()
            {
                GameId = 3,
                AreaGameId = 2,
                DisplayName = "Camino de la Cantera",
                Description = "Descripción de Camino de la Cantera",
                Type = 0
            };

            var scene4 = new Scene()
            {
                GameId = 4,
                AreaGameId = 2,
                DisplayName = "Granja Al'Thor",
                Description = "Descripción de granja Al'Thor",
                Type = 1
            };

            var scene5 = new Scene()
            {
                GameId = 5,
                AreaGameId = 2,
                DisplayName = "Cantera",
                Description = "Descripción de Cantera",
                Type = 1
            };

            var scene6 = new Scene()
            {
                GameId = 6,
                AreaGameId = 3,
                DisplayName = "Deven Ride",
                Description = "Descripción de Deven Ride",
                Type = 0
            };

            var scene7 = new Scene()
            {
                GameId = 7,
                AreaGameId = 4,
                DisplayName = "Colina del Vigía",
                Description = "Descripción de Colina del Vigía",
                Type = 0
            };

            var scene8 = new Scene()
            {
                GameId = 8,
                AreaGameId = 4,
                DisplayName = "Embarcadero de Taren",
                Description = "Descripción de Embarcadero de Taren",
                Type = 2
            };

            context.Scenes.Add(scene1);
            context.Scenes.Add(scene2);
            context.Scenes.Add(scene3);
            context.Scenes.Add(scene4);
            context.Scenes.Add(scene5);
            context.Scenes.Add(scene6);
            context.Scenes.Add(scene7);
            context.Scenes.Add(scene8);


            var transition1 = new Transition()
            {
                GameId = 1,
                SourceArea = scene1.AreaGameId,
                SourceScene = scene1.GameId,
                TargetArea = scene2.AreaGameId,
                TargetScene = scene2.GameId,
                Coords =  new List<int>(){ 250, 250, 280, 280 },
                //Coords2 = "250,250,280,280",
                Display = $"Hacia {scene2.DisplayName}"
            };

            var transition2 = new Transition()
            {
                GameId = 2,
                SourceArea = scene2.AreaGameId,
                SourceScene = scene2.GameId,
                TargetArea = scene1.AreaGameId,
                TargetScene = scene1.GameId,
                Display = $"Hacia {scene1.DisplayName}"
            };

            var transition3 = new Transition()
            {
                GameId = 3,
                SourceArea = scene1.AreaGameId,
                SourceScene = scene1.GameId,
                TargetArea = scene3.AreaGameId,
                TargetScene = scene3.GameId,
                Coords = new List<int>() { 0, 250, 30, 280 },
                //Coords2 = "0,250,30,280",
                Display = $"Hacia {scene3.DisplayName}"
            };

            var transition4 = new Transition()
            {
                GameId = 4,
                SourceArea = scene3.AreaGameId,
                SourceScene = scene3.GameId,
                TargetArea = scene1.AreaGameId,
                TargetScene = scene1.GameId,
                Display = $"Hacia {scene1.DisplayName}"
            };

            var transition5 = new Transition()
            {
                GameId = 5,
                SourceArea = scene1.AreaGameId,
                SourceScene = scene1.GameId,
                TargetArea = scene6.AreaGameId,
                TargetScene = scene6.GameId,
                Coords = new List<int>() { 250, 482, 280, 512 },
                //Coords2 = "250,482, 280, 512",
                Display = $"Hacia {scene6.DisplayName}"
            };

            var transition6 = new Transition()
            {
                GameId = 6,
                SourceArea = scene6.AreaGameId,
                SourceScene = scene6.GameId,
                TargetArea = scene1.AreaGameId,
                TargetScene = scene1.GameId,
                Display = $"Hacia {scene1.DisplayName}"
            };

            var transition7 = new Transition()
            {
                GameId = 7,
                SourceArea = scene1.AreaGameId,
                SourceScene = scene1.GameId,
                TargetArea = scene7.AreaGameId,
                TargetScene = scene7.GameId,
                Coords = new List<int>() { 250, 0, 280, 30 },
                //Coords2 = "250,0,280,30",
                Display = $"Hacia {scene7.DisplayName}"
            };

            var transition8 = new Transition()
            {
                GameId = 8,
                SourceArea = scene7.AreaGameId,
                SourceScene = scene7.GameId,
                TargetArea = scene1.AreaGameId,
                TargetScene = scene1.GameId,
                Display = $"Hacia {scene1.DisplayName}"
            };


            var transition9 = new Transition()
            {
                GameId = 9,
                SourceArea = scene3.AreaGameId,
                SourceScene = scene3.GameId,
                TargetArea = scene4.AreaGameId,
                TargetScene = scene4.GameId,
                Display = $"Hacia {scene4.DisplayName}"
            };

            var transition10 = new Transition()
            {
                GameId = 10,
                SourceArea = scene4.AreaGameId,
                SourceScene = scene4.GameId,
                TargetArea = scene3.AreaGameId,
                TargetScene = scene3.GameId,
                Display = $"Hacia {scene1.DisplayName}"
            };

            var transition11 = new Transition()
            {
                GameId = 11,
                SourceArea = scene3.AreaGameId,
                SourceScene = scene3.GameId,
                TargetArea = scene5.AreaGameId,
                TargetScene = scene5.GameId,
                Display = $"Hacia {scene5.DisplayName}"
            };

            var transition12 = new Transition()
            {
                GameId = 12,
                SourceArea = scene5.AreaGameId,
                SourceScene = scene5.GameId,
                TargetArea = scene3.AreaGameId,
                TargetScene = scene3.GameId,
                Display = $"Hacia {scene3.DisplayName}"
            };

            var transition13 = new Transition()
            {
                GameId = 13,
                SourceArea = scene7.AreaGameId,
                SourceScene = scene7.GameId,
                TargetArea = scene8.AreaGameId,
                TargetScene = scene8.GameId,
                Display = $"Hacia {scene8.DisplayName}"
            };

            var transition14 = new Transition()
            {
                GameId = 14,
                SourceArea = scene8.AreaGameId,
                SourceScene = scene8.GameId,
                TargetArea = scene7.AreaGameId,
                TargetScene = scene7.GameId,
                Display = $"Hacia {scene7.DisplayName}"
            };

            context.Transitions.Add(transition1);
            context.Transitions.Add(transition2);
            context.Transitions.Add(transition3);
            context.Transitions.Add(transition4);
            context.Transitions.Add(transition5);
            context.Transitions.Add(transition6);
            context.Transitions.Add(transition7);
            context.Transitions.Add(transition8);
            context.Transitions.Add(transition9);
            context.Transitions.Add(transition10);
            context.Transitions.Add(transition11);
            context.Transitions.Add(transition12);
            context.Transitions.Add(transition13);
            context.Transitions.Add(transition14);

            context.SaveChanges();
        }
    }
}
