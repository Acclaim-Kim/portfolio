using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class MovingLeftSmall : ICharacter
{
    private float scale = 2f;
    private Texture2D characterTexture;
    private float AnimationTicks;
    private float AnimationTimer;
    private int AnimationSpeed;
    private int previousAnimationIndex = 0;
    private int currentAnimationIndex = 0;
    private Color tint;

    private Rectangle[] FrameRectangles;

    public MovingLeftSmall(Texture2D characterTexture)
    {
        this.characterTexture = characterTexture;

        AnimationTimer = 0;
        AnimationTicks = 100;
        AnimationSpeed = 200;

        FrameRectangles = new Rectangle[3];
        FrameRectangles[0] = new Rectangle(150, 0, 14, 15); // Frame 1
        FrameRectangles[1] = new Rectangle(121, 0, 12, 16); // Frame 2
        FrameRectangles[2] = new Rectangle(89, 0, 16, 16);  // Frame 3

        previousAnimationIndex = 2;
        currentAnimationIndex = 1;
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
            if (currentAnimationIndex == 1)
            {
                if (previousAnimationIndex == 0)
                {
                    currentAnimationIndex = 2;
                }
                else
                {
                    currentAnimationIndex = 0;
                }
                previousAnimationIndex = currentAnimationIndex;
            }
            else
            {
                currentAnimationIndex = 1;
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
            case 0: return new Rectangle((int)position.X, (int)position.Y, 14 * (int)scale, 15 * (int)scale);
            case 1: return new Rectangle((int)position.X, (int)position.Y, 12 * (int)scale, 16 * (int)scale);
            case 2:
            default: return new Rectangle((int)position.X, (int)position.Y, 16 * (int)scale, 16 * (int)scale);
        }

    }
}