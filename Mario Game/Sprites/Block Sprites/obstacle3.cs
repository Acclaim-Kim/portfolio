using System;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class obstacle3 : IObstacle
{
    private Rectangle sourceRectangle;
    private Rectangle destinationRectangle;
    private int width;
    private int height;
    private Texture2D BlockTexture;

    public obstacle3(Texture2D BlockTexture)
    {
        this.BlockTexture = BlockTexture;
    }
    public void Update()
    {
        sourceRectangle = new Rectangle(230, 385, 32, 64);
        width = 47;
        height = 84;
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
    public void Load(GraphicsDeviceManager graphics)
    {
        throw new NotImplementedException();
    }
}
