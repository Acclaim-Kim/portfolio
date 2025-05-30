using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;


public class KoopaCommand : IController
{
    int count = 0;
    ISpriteEnemy _sprite;

    public KoopaCommand(ISpriteEnemy sprite)
    {
        _sprite = sprite;
    }

    public void Update()
    {
        /*
        count++;
        
        if ((count % 50 == 0) && (count < 175))
        {
            _sprite.changeDirection();
        }
        if (count == 175)
        {
            _sprite.beStomped();
        }
        if ((count % 50 == 0) && (count >= 525) && (count < 725))
        {
            _sprite.changeDirection();
        }
        
        if ((count == 725))
        {
            _sprite.beStomped();
        }
        
        if ((count % 50 == 0) && (count >= 1075))
        {
            _sprite.changeDirection();
        }
        if(count == 1300)
        {
            _sprite.beFlipped();
        }

        if (count == 4000)
        {
            count = 0;
        }
        */

    }
}
