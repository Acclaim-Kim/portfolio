using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BrokenBrickSprite : IBlock
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
    private bool broken;
    private bool bumping;
    public bool animationDone;
    public Rectangle destinationRectangle;
    int originalY;

    public BrokenBrickSprite(Texture2D texture, int frames, double wait)
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
    }

    public void StartAnimation()
    {
        // Method to trigger animation
        broken = true;
    }

    public void Update(GameTime gameTime)
    {
        if (bumping)
        {
            const float bumpHeight = 10f; // Total height the block moves up
            const float bumpSpeed = 100f; // Speed of the bump animation

            // Calculate total animation duration
            double bumpDuration = 2 * (bumpHeight / bumpSpeed);

            buffer += (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (buffer <= bumpDuration)
            {
                // Determine how far along the animation is
                double progress = buffer / bumpDuration;

                // Move block up for the first half, down for the second
                if (progress <= 0.5f) // Moving up
                {
                    destinationRectangle.Y = (int)(originalY - (bumpHeight * (progress / 0.5f)));
                }
                else // Moving down
                {
                    destinationRectangle.Y = (int)(originalY - bumpHeight + (bumpHeight * ((progress - 0.5f) / 0.5f)));
                }
            }
            else
            {
                // End the bump animation and reset
                destinationRectangle.Y = (int)originalY; // Return to original position
                buffer = 0;
                bumping = false;
            }
        }


        // Handle other animation frames if needed
        if (broken)
        {
            buffer += gameTime.ElapsedGameTime.TotalSeconds;

            if (buffer >= waitTime)
            {
                currentFrame++;
                buffer -= waitTime;

                // Stop animating at the last frame
                if (currentFrame >= numOfFrames-1)
                {
                    currentFrame = numOfFrames - 2; // Set to the last frame
                    broken = false; // Stop animation
                    animationDone = true;
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

        if (!animationDone)
        {
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
        
    }

    public Rectangle GetDestination()
    {
        return destinationRectangle;
    }

    public void Load(GraphicsDeviceManager graphics)
    {
        // Loading logic if needed
    }
    public void bump()
    {
        bumping = true;
    }
    public void broke()
    {
        broken = true;
    }
}