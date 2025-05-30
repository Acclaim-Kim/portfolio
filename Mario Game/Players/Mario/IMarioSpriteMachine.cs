using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pixel_Plumbers_Fall_2024;

public interface IMarioSpriteMachine
{
    public ICharacter UpdatePlayerSprite(PlayerStateMachine marioStateMachine, Texture2D texture);
}
