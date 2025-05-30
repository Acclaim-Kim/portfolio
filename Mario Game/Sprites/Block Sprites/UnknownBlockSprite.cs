using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Pixel_Plumbers_Fall_2024;
public class UnknownBlockSprite : IBlock
{
    public Texture2D Texture { get; set; }
    public Vector2 Start { get; set; }
    public Vector2 End { get; set; }
    public int numOfFrames { get; set; }
    public double waitTime { get; set; }
    private double buffer;
    private int width;
    private int height;
    private int currentFrame;
    private Boolean isAnimating;
    public Rectangle destinationRectangle;
    public UnknownBlockSprite(Texture2D texture, int frames, double wait)
    {
        Texture = texture;
        Start = new Vector2(288, 112);
        End = new Vector2(352, 128);
        numOfFrames = frames;
        waitTime = wait;            // the amount of time between frame changes, time per frame
        currentFrame = 0;
        buffer = 0; // total elapsed time
        width = (int)(End.X - Start.X) / frames;
        height = (int)(End.Y - Start.Y);
        this.isAnimating = true;
    }
    public void Update(GameTime gameTime)
    {
        if (isAnimating)
        {
            buffer += gameTime.ElapsedGameTime.TotalSeconds;

            if (buffer >= waitTime)
            {
                currentFrame++;
                buffer -= waitTime;

                // Stop animating at the last frame
                if (currentFrame >= numOfFrames)
                {
                    currentFrame = numOfFrames - 1; // Set to the last frame
                    isAnimating = false; // Stop animation
                }
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 location)
    {
        Rectangle sourceRectangle = new Rectangle(
            (int)Start.X + width * currentFrame,
            (int)Start.Y,
            width,
            height
        );

        destinationRectangle = new Rectangle(
            (int)location.X,
            (int)location.Y,
            31,
            31
        );

        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
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