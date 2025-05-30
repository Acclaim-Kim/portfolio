using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
public class StaticBlockSprite: IBlock
{
    public Texture2D texture { get; set; }
    public Rectangle rectangle { get; set; }
    private Rectangle destinationRectangle;
    public StaticBlockSprite(Texture2D texture, Rectangle sourceRectangle)
    {
        this.texture = texture;
        this.rectangle = sourceRectangle;
    }
    public void Update(GameTime gametime)
    {

    }
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 31, 31);
        spriteBatch.Draw(texture, destinationRectangle, rectangle, Color.White);

    }
    public Rectangle GetDestination()
    {
        return destinationRectangle;
    }
    public void Load(GraphicsDeviceManager graphics)
    {

    }
    public void bump()
    {

    }
    public void broke()
    {

    }
}

