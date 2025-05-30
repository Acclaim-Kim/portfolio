using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class JumpingRightSmall : ICharacter
{
    private float scale = 2f;
    private Texture2D characterTexture;
    private Color tint;
    
    public JumpingRightSmall(Texture2D characterTexture)
    {
        this.characterTexture = characterTexture;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, bool HasStar)
    {
        if (HasStar) { tint = Color.Magenta; }
        else { tint = Color.White; }

        Rectangle sourceRectangle = new Rectangle(359, 0, 17, 16);
        spriteBatch.Draw(characterTexture, position, sourceRectangle, tint, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }

    public void Update(GameTime gametime)
    {

    }

    public Rectangle GetDestination(Vector2 position)
    {
        return new Rectangle((int)position.X, (int)position.Y, 17 * (int)scale, 16 * (int)scale);
    }
}