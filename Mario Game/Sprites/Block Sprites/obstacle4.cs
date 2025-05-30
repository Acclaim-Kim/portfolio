using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public class obstacle4 : IObstacle
{

    private Rectangle sourceRectangle;
    private Rectangle destinationRectangle;
    private int width;
    private int height;
    private Texture2D BlockTexture;

    public obstacle4(Texture2D BlockTexture)
    {
        this.BlockTexture = BlockTexture;
    }

    public void Update()
    {
        sourceRectangle = new Rectangle(172, 236, 80, 81);
        width = 80;
        height = 81;
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
        throw new System.NotImplementedException();
    }
}
