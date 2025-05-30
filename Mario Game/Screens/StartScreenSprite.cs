using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class StartScreenSprite : ISprite
{
    private readonly float scale = 2f;
    private SpriteFont MyFont;
    private readonly Texture2D startScreenTexture;
    private readonly Rectangle sourceRectangle = new Rectangle(0, 0, 176, 88);

    private const int ScreenWidth = 800;
    private const int ScreenHeight = 600;

    private Vector2 player1Position;
    private Vector2 player2Position;
    private Vector2 helpPosition;

    public StartScreenSprite(TextureManager textureManager, SpriteFont spriteFont)
    {
        this.startScreenTexture = textureManager.GetTexture("Title");
        this.MyFont = spriteFont;

        string player1Text = "SINGLEPLAYER";
        string player2Text = "MULTIPLAYER";
        string helpText = "HELP";

        Vector2 player1TextSize = MyFont.MeasureString(player1Text) * scale;
        Vector2 player2TextSize = MyFont.MeasureString(player2Text) * scale;
        Vector2 helpTextSize = MyFont.MeasureString(helpText) * scale;

        player1Position = new Vector2(240, 250);
        player2Position = new Vector2(260, 300);
        helpPosition = new Vector2(360, 350);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(startScreenTexture, new Vector2(220, 60), sourceRectangle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

        spriteBatch.DrawString(MyFont, "SINGLEPLAYER", new Vector2(240, 250), Color.White);
        spriteBatch.DrawString(MyFont, "MULTIPLAYER", new Vector2(260, 300), Color.White);
        spriteBatch.DrawString(MyFont, "HELP", new Vector2(360, 350), Color.White);

        // spriteBatch.Draw(startScreenTexture, GetOnePlayerRectangle(), Color.Red * 0.5f);
        // spriteBatch.Draw(startScreenTexture, GetTwoPlayerRectangle(), Color.Blue * 0.5f);
        // spriteBatch.Draw(startScreenTexture, GetHelpRectangle(), Color.Green * 0.5f);
    }

    public Rectangle GetOnePlayerRectangle()
    {
        return new Rectangle((int)player1Position.X, (int)player1Position.Y, (int)MyFont.MeasureString("SINGLEPLAYER").X, (int)MyFont.MeasureString("SINGLEPLAYER").Y);
    }

    public Rectangle GetTwoPlayerRectangle()
    {
        return new Rectangle((int)player2Position.X, (int)player2Position.Y, (int)MyFont.MeasureString("MULTIPLAYER").X, (int)MyFont.MeasureString("MULTIPLAYER").Y);
    }

    public Rectangle GetHelpRectangle()
    {
        return new Rectangle((int)helpPosition.X, (int)helpPosition.Y, (int)MyFont.MeasureString("HELP").X, (int)MyFont.MeasureString("HELP").Y);
    }

    public void Update(GameTime gameTime)
    {

    }
}
