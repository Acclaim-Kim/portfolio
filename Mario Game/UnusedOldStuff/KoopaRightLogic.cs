using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;

public class KoopaRightLogic
{
    public void Updates(KoopaFields vars)
    {
        vars.counter2 = -1;
        vars.counter++;
        if (vars.counter == 0)
        {
            vars.sourceRectangle = new Rectangle(vars.rightXOne, vars.rightY, vars.width, vars.height);
        }
        if ((vars.counter >= vars.countStart) && (vars.counter < vars.countEnd))
        {
            vars.position = vars.position + vars.speed;
            if (vars.counter % vars.countMod < (vars.countMod / 2))
            {
                vars.sourceRectangle = new Rectangle(vars.rightXTwo, vars.rightY, vars.width, vars.height);
            }
            else
            {
                vars.sourceRectangle = new Rectangle(vars.rightXOne, vars.rightY, vars.width, vars.height);
            }
        }
        vars.destinationRectangle = new Rectangle(vars.position, vars.posY, vars.width * vars.scaleUp, vars.height * vars.scaleUp);
        if (vars.counter >= vars.countEnd)
        {
            vars.counter = -1;
            vars.position = vars.posX;
        }
    }
}
