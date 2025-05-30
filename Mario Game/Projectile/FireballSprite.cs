using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FireballSprite
{
    private Texture2D texture;
    private float gameTicks;
    private int animationSpeed;
    private int previousAnimationIndex = 0;
    private int currentAnimationIndex = 0;

    private Rectangle[] FrameRectangles;
    public FireballSprite(Texture2D Texture)
    {
        this.texture = Texture;

        gameTicks = 0;
        animationSpeed = 100;

        FrameRectangles = new Rectangle[4];
        FrameRectangles[0] = new Rectangle(96, 144, 8, 8); // Frame 1
        FrameRectangles[1] = new Rectangle(104, 144, 8, 8); // Frame 2
        FrameRectangles[2] = new Rectangle(96, 152, 8, 8);  // Frame 3
        FrameRectangles[3] = new Rectangle(104, 152, 8, 8); // Frame 4

        previousAnimationIndex = 3;
        currentAnimationIndex = 2;
    }

    public void Load(GraphicsDeviceManager graphics)
    {

    }
    public void Update(GameTime gameTime)
    {
        this.gameTicks += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if (gameTicks <= animationSpeed) return;

        currentAnimationIndex = currentAnimationIndex == 1
            ? (previousAnimationIndex == 0 ? 3 : 0)
            : 1;

        previousAnimationIndex = currentAnimationIndex;
        gameTicks = 0;
    }

    public void Draw(SpriteBatch sb, Vector2 pos)
    {
        sb.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, 16, 16), FrameRectangles[currentAnimationIndex], Color.White);
    }
}
