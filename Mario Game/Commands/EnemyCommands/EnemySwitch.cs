using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Transactions;
using System.Xml.Linq;

public class EnemySwitch : ICommand
{
    Game1 game1;
    int PorO;
    List<ISpriteEnemy> enemies;
    public EnemySwitch(Game1 game, int _PorO, List<ISpriteEnemy> _enemies)
    {
        game1 = game;
        PorO = _PorO;
        enemies = _enemies;
    }
    private ISpriteEnemy current;
    private ISpriteEnemy lastSprite;

    public void Execute()
    {
        lastSprite = enemies[0];
        current = enemies[0];
        if (PorO == 0)
        {
            enemies.RemoveAt(0);
            enemies.Add(current);
        }
        else
        {
            for (int i = enemies.Count - 1; i > 0; i--)
            {
                ISpriteEnemy hold1 = enemies[i];
                ISpriteEnemy hold2 = enemies[i - 1];
                enemies[i - 1] = hold1;
                enemies[i] = hold2;
            }
        }
        current = enemies[0];
    }

}