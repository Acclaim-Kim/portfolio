using System;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;

public class KoopaFlippedLogic
{
    public void Updates(KoopaFields vars)
	{
        vars.rotation = 3.1415926535f;
        vars.counter++;
        if (vars.counter == 0)
        {
            vars.sourceRectangle = new Rectangle(180, 0, vars.width, vars.height);
        }
        if ((vars.counter >= vars.countStart) && (vars.counter < vars.countEnd))
        {
            if (vars.counter % vars.countMod < (vars.countMod / 2))
            {
                vars.sourceRectangle = new Rectangle(150, 0, vars.width, vars.height);
            }
            else
            {
                vars.sourceRectangle = new Rectangle(180, 4, vars.width, vars.height);
            }
        }
        if (vars.counter >= vars.countEnd)
        {
            vars.counter = -1;
        }
    }
}
