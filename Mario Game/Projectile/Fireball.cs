using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pixel_Plumbers_Fall_2024;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Fireball : IProjectile
{
    /*
     * When a fireball hits the ground, it bounces up a little bit
     * Use isBouncing and bounceTimer to track this
    */
    private bool isBouncing;
    private bool goingRight;
    private int bounceTimer;
    private Vector2 pos;

    private FireballSprite sprite;
    private GameTime gameTime;
    private Game1 game;
    private List<IEntity> _entities;
    private IPlayer player;
    public Fireball(Vector2 marioPosition, Texture2D texture, GameTime gameTime, PlayerStateMachine.PlayerFaceState direction, Game1 game, List<IEntity> entities, IPlayer player)
    {
        isBouncing = false;
        pos = marioPosition;
        sprite = new FireballSprite(texture);
        this.gameTime = gameTime;
        bounceTimer = 0;
        goingRight = (direction == PlayerStateMachine.PlayerFaceState.Right);
        this.game = game;
        this._entities = entities;
        _entities.Add(this);
        this.player = player;
    }
    private void Move()
    {
        if (goingRight)
        {
            //If mario was facing right when shooting, move to the right
            pos.X += 2;
        }
        else
        {
            //Same as above, but for left
            pos.X += -2;
        }

        if (isBouncing)
        {
            //Move up some
            pos.Y += -2;
            bounceTimer += -1;
            if (bounceTimer <= 0)
            {
                isBouncing = false;
            }
        }
        else
        {
            //Move down some
            pos.Y += 2;
        }
    }
    public void Bounce()
    {
        //Call when top/bottom collision with a block
        isBouncing = true;
        bounceTimer = 40;
    }

    public void Remove()
    {
        game.fireballs.Remove(this);
    }
    public void Update(GameTime gameTime)
    {
        this.Move();
        sprite.Update(gameTime);
    }

    public void Draw(SpriteBatch sb)
    {
        sprite.Draw(sb, pos);
    }

    public Rectangle GetDestination()
    {
        //All fireball sprites are 16*16
        return new Rectangle((int)pos.X, (int)pos.Y, 16, 16);
    }

    public void AddScore(int scoreAmt)
    {
        player.AddScore(scoreAmt);
    }
}
