using System.Collections.Generic;
using DIKUArcade.Entities;
using Galaga_Exercise_3_1.GalagaEntities.Enemy;
using DIKUArcade.Graphics;

namespace Galaga_Exercise_3_1.Squadrons {
    public interface ISquadron {
        EntityContainer<Enemy> Enemies { get; }
        int MaxEnemies { get; }
        void CreateEnemies(List<Image> enemyStrides);
    }
}

    