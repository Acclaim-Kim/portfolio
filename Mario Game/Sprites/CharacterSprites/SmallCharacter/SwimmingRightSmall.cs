using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SwimmingRightSmall : ICharacter
{
    private float scale = 2f;
    private Texture2D characterTexture;
    private float AnimationTicks;
    private float AnimationTimer;
    private int AnimationSpeed;
    private int previousAnimationIndex;
    private int currentAnimationIndex;
    private Color tint;

    private Rectangle[] FrameRectangles;
    public SwimmingRightSmall(Texture2D characterTexture)
    {
        this.characterTexture = characterTexture;

        AnimationTimer = 0;
        AnimationTicks = 100;
        AnimationSpeed = 200;

        FrameRectangles = new Rectangle[4];
        FrameRectangles[0] = new Rectangle(210, 30, 15, 15); // Frame 1
        FrameRectangles[1] = new Rectangle(241, 30, 14, 15); // Frame 2
        FrameRectangles[2] = new Rectangle(271, 30, 14, 15);  // Frame 3
        FrameRectangles[3] = new Rectangle(301, 30, 14, 15); // Frame 4

        currentAnimationIndex = 0;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, bool HasStar)
    {
        if (HasStar) { tint = Color.Magenta; }
        else { tint = Color.White; }

        spriteBatch.Draw(characterTexture, position, FrameRectangles[currentAnimationIndex], tint, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }

    public void Update(GameTime gameTime)
    {
        if (AnimationTimer > AnimationSpeed)
        {
            if (currentAnimationIndex == 3)
            {
                currentAnimationIndex = 0;
            }
            else
            {
                currentAnimationIndex++;
            }
            AnimationTimer = 0;
        }
        else
        {
            AnimationTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }

    public Rectangle GetDestination(Vector2 position)
    {
        switch (currentAnimationIndex)
        {
            case 0:
            return new Rectangle((int)position.X, (int)position.Y, 15 * (int)scale, 15 * (int)scale);
            case 1:
            case 2:
            case 3:
            default: return new Rectangle((int)position.X, (int)position.Y, 14 * (int)scale, 15 * (int)scale);
        }
    }
}