using Pixel_Plumbers_Fall_2024;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework;
using System.Security.Cryptography.X509Certificates;
public interface IitemManager
{
   
    void updateCurrentItem(ref int curr, int num);
}
public class ItemManager : IitemManager
{
    public int lastButton;
    private KeyboardState state;
    private KeyboardState previousState;
    public void itemManager()
    {
        this.lastButton = 0;
    }

        
    public void updateCurrentItem(ref int currentItem, int numItems)
    {
        
        this.state = Keyboard.GetState();
        
        if (state.IsKeyDown(Keys.U) && !(this.previousState.IsKeyDown(Keys.U)))
        {
            if (currentItem == 0)
            {
                currentItem = numItems - 1;
            }
            else
            {
                currentItem--;
            }
            

        }
        if (state.IsKeyDown(Keys.I)&& !(this.previousState.IsKeyDown(Keys.I)))
        {
            if (currentItem == (numItems - 1))
            {
                currentItem = 0;
            }
            else
            {
                currentItem++;
            }
            
        }
        this.previousState = state;

    }
    public void draw(int currentItem, Texture2D itemsText, SpriteBatch sB,Vector2 position)
    {
        if (currentItem == 0)
        {
            //StarPower sp = new StarPower(itemsText);
            //sp.Draw(sB, position);
        }
        else if (currentItem == 1)
        {
            FirePower fp = new FirePower(sB, itemsText, position);
            //fp.draw(position);

        }
        else if (currentItem == 2)
        {
            MushroomPower mp = new MushroomPower(itemsText);
            mp.Draw(sB, position);
        }
    }
}

