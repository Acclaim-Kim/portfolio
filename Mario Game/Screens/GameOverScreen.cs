using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameOverScreen : ISprite
{
    private readonly float scale = 2f;
    private SpriteFont MyFont;
    private readonly Rectangle sourceRectangle = new Rectangle(0, 0, 176, 88);
    private readonly Texture2D startScreenTexture;

    private const int ScreenWidth = 800;
    private const int ScreenHeight = 600;

    private Vector2 gameOverPosition;
    private Vector2 restartPosition;
    private Vector2 mainMenuPosition;

    public GameOverScreen(TextureManager textureManager, SpriteFont spriteFont)
    {
        this.startScreenTexture = textureManager.GetTexture("Title");
        this.MyFont = spriteFont;

        string gameOverText = "GAME OVER";
        string restartText = "RESTART";
        string mainMenuText = "MAIN MENU";

        Vector2 gameOverTextSize = MyFont.MeasureString(gameOverText) * scale;
        Vector2 restartTextSize = MyFont.MeasureString(restartText) * scale;
        Vector2 mainMenuTextSize = MyFont.MeasureString(mainMenuText) * scale;

        gameOverPosition = new Vector2((ScreenWidth - gameOverTextSize.X) / 2, 200);
        restartPosition = new Vector2((ScreenWidth - restartTextSize.X) / 2, 300);
        mainMenuPosition = new Vector2((ScreenWidth - mainMenuTextSize.X) / 2, 350);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.DrawString(MyFont, "GAME OVER", gameOverPosition, Color.Red, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        spriteBatch.DrawString(MyFont, "RESTART", restartPosition, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        spriteBatch.DrawString(MyFont, "MAIN MENU", mainMenuPosition, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

        //spriteBatch.Draw(startScreenTexture, GetRestartRectangle(), Color.Red * 0.5f);
        //spriteBatch.Draw(startScreenTexture, GetMainMenuRectangle(), Color.Blue * 0.5f);
    }

    public Rectangle GetRestartRectangle()
    {
        return new Rectangle(
            (int)restartPosition.X,
            (int)restartPosition.Y,
            (int)(MyFont.MeasureString("RESTART").X * scale),
            (int)(MyFont.MeasureString("RESTART").Y * scale)
        );
    }

    public Rectangle GetMainMenuRectangle()
    {
        return new Rectangle(
            (int)mainMenuPosition.X,
            (int)mainMenuPosition.Y,
            (int)(MyFont.MeasureString("MAIN MENU").X * scale),
            (int)(MyFont.MeasureString("MAIN MENU").Y * scale)
        );
    }

    public void Update(GameTime gameTime)
    {

    }
}
