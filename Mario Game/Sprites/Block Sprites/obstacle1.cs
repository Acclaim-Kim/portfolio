using System;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;


public class obstacle1 : IObstacle
{
    private Rectangle sourceRectangle;
    private Rectangle destinationRectangle;
    private int width;
    private int height;
    private Texture2D BlockTexture;

    public obstacle1(Texture2D BlockTexture)
    {
        this.BlockTexture = BlockTexture;
    }

    public void Load(GraphicsDeviceManager graphics)
    {

    }
    public void Update()
    {
        sourceRectangle = new Rectangle(309, 417, 32, 32);
        width = 47;
        height = 47;        
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        spriteBatch.Draw(BlockTexture, destinationRectangle, sourceRectangle, Color.White);
    }
    public Rectangle GetDestination()
    {
        return destinationRectangle;
    }
}
