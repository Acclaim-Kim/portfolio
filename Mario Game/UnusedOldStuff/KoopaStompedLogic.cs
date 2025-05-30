using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;

public class KoopaStompedLogic
{
    public void Updates(KoopaFields vars)
    {
        vars.counter2++;
        if (vars.counter2 == 0)
        {
            vars.sourceRectangle = new Rectangle(360, 5, 16, 15);
            vars.destinationRectangle = new Rectangle(vars.position, vars.posY + 20, 16 * vars.scaleUp, 15 * vars.scaleUp);
        }
        if (vars.counter2 == 200)
        {
            vars.sourceRectangle = new Rectangle(330, 4, 16, 15);
            vars.destinationRectangle = new Rectangle(vars.position, vars.posY + 20, 16 * vars.scaleUp, 15 * vars.scaleUp);
        }
        if ((vars.counter2 == 350) || (vars.counter >= vars.countEnd))
        {
            vars.counter = -1;
        }
    }
}
