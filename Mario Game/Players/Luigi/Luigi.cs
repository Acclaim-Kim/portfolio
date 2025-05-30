using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pixel_Plumbers_Fall_2024;

public class Luigi : IPlayer
{
    private Texture2D luigiTexture;
    private Texture2D itemTexture;
    private TextureManager textureManager;

    private ICharacter currentLuigiSprite;
    private IMarioSpriteMachine luigiSpriteMachine;
    private PlayerStateMachine playerStateMachine;
    public GameTime gameTime;

    private Vector2 initialPosition;
    public Vector2 luigiPosition;
    private Vector2 luigiVelocity;
    private float groundPosition = 385f;
    private float swimmingMaxHeight = 10f;
    private float gravity = 980f;
    private float jumpSpeed = -570f;

    private bool isSwimmingLevel = false;
    public bool isOnGround = true;
    private bool canPowerUp = true;
    private bool canTakeDamage = true;
    private bool moveKeyPressed = false;
    private bool deathSoundPlaying = false;
    private bool isDead = false;
    private bool waitingForPartnerToDie = false;
    private bool winHitBottom = false;
    private int jumpSoundTimer = 0;

    private float maxSpeed = 3f;
    private float acceleration = 0.03f;

    private int fireballTimer;
    private bool hasShotFireball = false;
    private int starTimer;
    private int visibleCount = 0;
    int luigiDeathBounceIncrement;
    private List<SoundEffect> _sfx;

    private Game1 game;
    private int gameResetTimer = -1;
    private List<IEntity> _entities;

    private GameStateMachine gsm;
    private Mario mario;

    private int scoreMult;
    private const int maxScoreMult = 16;
    private SpriteFont scoreFont;

    private WinCutScene wc;
    public Luigi(Game1 game, List<IEntity> entities, List<SoundEffect> sfx, TextureManager textureManager, GameTime gametime, ref GameStateMachine gsm, ref SpriteFont font)
    {
        this.textureManager = textureManager;
        this.luigiTexture = textureManager.GetTexture("Luigi");
        this.itemTexture = textureManager.GetTexture("Items");

        this.luigiPosition = new Vector2(150, groundPosition);
        this.initialPosition = new Vector2(150, groundPosition);
        this.playerStateMachine = new PlayerStateMachine();
        this.gameTime = gametime;
        this.luigiPosition = initialPosition;
        this.fireballTimer = 0;
        this.starTimer = 0;
        this.luigiDeathBounceIncrement = 15;

        this.currentLuigiSprite = new IdleRightSmall(luigiTexture);
        this.game = game;
        this._entities = entities;

        /*
         * SFX loaded in specific order:
         * 0: Power up
         * 1: Power down
         * 2: Fireball
         * 3: Jump
         * 4: Death
         * 5:FlagPole for winning
         */
        this._sfx = sfx;

        this.gsm = gsm;

        this.scoreMult = 1;
        this.scoreFont = font;
    }

    public PlayerStateMachine GetStateMachine()
    {
        return this.playerStateMachine;
    }
    public void MoveRight()
    {
        if (playerStateMachine.IsDead()) return;
        moveKeyPressed = true;
        if (!playerStateMachine.IsJumping())
        {
            if (luigiVelocity.X < 0f)
            {
                playerStateMachine.SetPlayerTurning();
            }
            else if (!playerStateMachine.IsCrouching())
            {
                playerStateMachine.SetPlayerRight();
                playerStateMachine.SetPlayerMoving();
            }
        }
        if (isSwimmingLevel && (luigiVelocity.X > 0f))
        {
            playerStateMachine.SetPlayerRight();
        }
        if (!playerStateMachine.IsCrouching())
        {
            if (luigiVelocity.X < maxSpeed)
            {
                luigiVelocity.X += acceleration;
            }
        }
    }

    public void MoveLeft()
    {
        if (playerStateMachine.IsDead()) return;
        moveKeyPressed = true;
        if (!playerStateMachine.IsJumping())
        {
            if (luigiVelocity.X > 0f)
            {
                playerStateMachine.SetPlayerTurning();
            }
            else if (!playerStateMachine.IsCrouching())
            {
                playerStateMachine.SetPlayerLeft();
                playerStateMachine.SetPlayerMoving();
            }
        }
        if (isSwimmingLevel && (luigiVelocity.X < 0f))
        {
            playerStateMachine.SetPlayerLeft();
        }
        if (!playerStateMachine.IsCrouching())
        {
            if (luigiVelocity.X > -maxSpeed)
            {
                luigiVelocity.X -= acceleration;
            }
        }
    }

    public void Jump()
    {
        if (playerStateMachine.IsDead()) return;
        if ((isOnGround || isSwimmingLevel) && !playerStateMachine.IsCrouching())
        {
            luigiVelocity.Y = jumpSpeed;
            playerStateMachine.SetPlayerJumping();
            isOnGround = false;
            if (jumpSoundTimer <= 0)
            {
                jumpSoundTimer = 40;
                _sfx[3].Play();
            }
        }
    }

    public void Crouch()
    {
        if (playerStateMachine.IsDead()) return;
        if (!playerStateMachine.IsJumping())
        {
            playerStateMachine.SetPlayerCrouching();
            luigiVelocity.X = 0;
        }
    }

    public void ApplyGravity(GameTime gameTime)
    {
        if (playerStateMachine.IsDead()) return;
        if (!isOnGround)
        {
            luigiVelocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            luigiPosition.Y += luigiVelocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (luigiPosition.Y >= groundPosition)
            {
                luigiPosition.Y = groundPosition;
                luigiVelocity.Y = 0;
                isOnGround = true;
                playerStateMachine.UpdateMoveStateForJumping();
            }
        }
    }
    public void ForceGravity(GameTime gameTime)
    {
        luigiVelocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        luigiPosition.Y += luigiVelocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public void PowerUp()
    {
        if (playerStateMachine.IsDead() || !canPowerUp) return;
        switch (playerStateMachine.CurrentGameState)
        {
            case PlayerStateMachine.PlayerGameState.Small:
                this.SetPositionY(this.luigiPosition.Y - 32);
                playerStateMachine.SetPlayerBig();
                _sfx[0].Play();
                break;

            case PlayerStateMachine.PlayerGameState.Big:
                playerStateMachine.SetPlayerFire();
                _sfx[0].Play();
                break;

            case PlayerStateMachine.PlayerGameState.Fire:

                break;
        }
        canPowerUp = false;
        Task.Delay(1000).ContinueWith(t => canPowerUp = true);
    }

    public void TakeDamage()
    {
        if (playerStateMachine.IsDead() || !canTakeDamage) return;
        switch (playerStateMachine.CurrentGameState)
        {
            case PlayerStateMachine.PlayerGameState.Fire:
                _sfx[1].Play();
                playerStateMachine.SetPlayerBig();
                break;

            case PlayerStateMachine.PlayerGameState.Big:
                this.SetPositionY(this.luigiPosition.Y + 32);
                _sfx[1].Play();
                playerStateMachine.SetPlayerSmall();
                break;

            case PlayerStateMachine.PlayerGameState.Small:
                playerStateMachine.SetPlayerDead();
                break;
        }
        canTakeDamage = false;
        Task.Delay(1000).ContinueWith(t => canTakeDamage = true);
    }
    public void SetWin()
    {
        playerStateMachine.SetPlayerWins();
    }

    public void ResetWin()
    {
        playerStateMachine.ResetWin();
    }

    public void LuigiWins()
    {
        if (playerStateMachine.Wins())
        {
            if (!winHitBottom)
            {
                luigiVelocity.Y = 0;
                luigiVelocity.X = 0;
                luigiPosition.Y++;
                //_sfx[5].Play();
                if (luigiPosition.Y > 326)
                {
                    winHitBottom = true;
                    this.ResetWin();
                    this.WinLevelOne();
                }


            }

        }
        if (winHitBottom)
        {
            playerStateMachine.SetPlayerMoving();
            wc.Update(this.gameTime);
            if (wc.entersDoor())
            {
                this.GetStateMachine().MakeInvisible();
            }

        }


    }
    public void WinLevelOne()
    {
        if (visibleCount == 0)
        {
            this.GetStateMachine().MakeVisible();
            visibleCount++;
        }

        //this.ResetWin();
        //this.GetStateMachine().SetPlayerBig();
        wc = new WinCutScene(this, this.GetDestination());
        wc.play(this.gameTime);
    }

    public void LuigiDeath()
    {
        if (playerStateMachine.IsDead() && !waitingForPartnerToDie)
        {
            if (!deathSoundPlaying)
            {
                _sfx[4].Play();
                deathSoundPlaying = true;
            }
            luigiVelocity.X = 0; luigiVelocity.Y = 0;
            luigiPosition.Y -= (float)luigiDeathBounceIncrement;
            luigiDeathBounceIncrement -= 1;

            if (gameResetTimer > 0)
            {
                gameResetTimer -= 1;
            }
            else if (gameResetTimer < 0)
            {
                game.hudManager.LoseLife();
                gameResetTimer = 250;
            }
            else if (gameResetTimer == 0)
            {
                if (gsm.isMultiplayer() && !mario.getStateMachine().IsDead())
                {
                    waitingForPartnerToDie = true;
                }
                else if (gsm.isSingleplayer() || (gsm.isMultiplayer() && mario.getStateMachine().IsDead()))
                {
                    if (game.hudManager.GetNumLives() <= 0)
                    {
                        game.GameOver();
                    }
                    else
                    {
                        game.ResetGame();
                    }
                }
            }
        }
    }

    public void SwapDirection()
    {
        if (playerStateMachine.IsDead()) return;

        if (playerStateMachine.IsMoving())
        {
            playerStateMachine.SetPlayerTurning();
            luigiVelocity.X = 0f;
        }
    }

    public void Stop()
    {
        if (playerStateMachine.IsDead()) return;
        luigiVelocity.X *= 0.3f;
        moveKeyPressed = false;
        if (!playerStateMachine.IsJumping())
        {
            playerStateMachine.SetPlayerIdle();
        }
    }

    public void JumpStop()
    {
        if (playerStateMachine.IsDead()) return;
        moveKeyPressed = false;
        if (!playerStateMachine.IsMoving())
        {
            playerStateMachine.SetPlayerIdle();
        }
    }

    private void CheckStopTurningUpd()
    {
        if (playerStateMachine.IsTurning())
        {
            if ((playerStateMachine.CurrentFaceState == PlayerStateMachine.PlayerFaceState.Left) && (luigiVelocity.X <= 0f))
            {
                playerStateMachine.SetPlayerLeft();
            }
            else if ((playerStateMachine.CurrentFaceState == PlayerStateMachine.PlayerFaceState.Right) && (luigiVelocity.X >= 0f))
            {
                playerStateMachine.SetPlayerRight();
            }
        }
    }

    private void SlowStopLuigi()
    {
        if (!moveKeyPressed && !playerStateMachine.IsJumping())
        {
            luigiVelocity.X *= 0.5f;
        }

        if (Math.Abs(luigiVelocity.X) < 0.025f)
        {
            luigiVelocity.X = 0f;
        }
    }

    public void ShootFireball()
    {
        if (playerStateMachine.IsDead()) return;

        maxSpeed = 4.5f;
        acceleration = 0.06f;

        if (fireballTimer > 0) return;

        if (playerStateMachine.IsFire() && !hasShotFireball)
        {
            _sfx[2].Play();
            Fireball f = new Fireball(luigiPosition, itemTexture, gameTime, playerStateMachine.CurrentFaceState, game, _entities, this);
            game.fireballs.Add(f);
            fireballTimer = 20;
            hasShotFireball = true;
        }
    }

    public void StopRunning()
    {
        maxSpeed = 3f;
        acceleration = 0.03f;
        hasShotFireball = false;
    }

    public void SetSwimmingLevel(bool isLevelSwimming)
    {
        isSwimmingLevel = isLevelSwimming;
        playerStateMachine.setSwimmingLevel(isLevelSwimming);

        if (isSwimmingLevel)
        {
            luigiSpriteMachine = new LuigiSpriteMachineSwimming();
            gravity = 980f / 4f;
            jumpSpeed = -570f / 4f;
        }
        else
        {
            luigiSpriteMachine = new LuigiSpriteMachine();
            gravity = 980f;
            jumpSpeed = -570f;
        }
    }

    public void Update(GameTime gameTime)
    {
        this.ApplyGravity(gameTime);
        luigiPosition.X += luigiVelocity.X;
        this.SlowStopLuigi();
        this.CheckStopTurningUpd();
        this.CheckSwimmingMaxHeight();
        this.LuigiDeath();
        currentLuigiSprite = luigiSpriteMachine.UpdatePlayerSprite(playerStateMachine, luigiTexture);
        currentLuigiSprite.Update(gameTime);
        fireballTimer += -1;
        starTimer += -1;
        jumpSoundTimer += -1;
        this.RemoveStar();
        this.checkLuigiHeightForDeath();
        this.ResetScoreMult();
        this.LuigiWins();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (this.GetStateMachine().isVisible)
        {
           currentLuigiSprite.Draw(spriteBatch, luigiPosition, this.playerStateMachine.HasStar());
        }
        
    }

    public void Reset()
    {
        luigiPosition = initialPosition;
        luigiVelocity = Vector2.Zero;
        playerStateMachine.Reset();
        isOnGround = true;
        currentLuigiSprite = new IdleRightSmall(luigiTexture);
        luigiDeathBounceIncrement = 20;
        gameResetTimer = -1;
        deathSoundPlaying = false;
        waitingForPartnerToDie = false;
    }

    public Rectangle GetDestination()
    {
        return currentLuigiSprite.GetDestination(luigiPosition);
    }

    public float GroundPosition()
    {
        return this.groundPosition;
    }

    public void checkLuigiHeightForDeath()
    {
        if (this.GetDestination().Y > 1500)
        {
            playerStateMachine.SetPlayerDead();
        }
    }

    private void CheckSwimmingMaxHeight()
    {
        if (isSwimmingLevel && (luigiPosition.Y < swimmingMaxHeight))
        {
            luigiPosition.Y = swimmingMaxHeight;
        }
    }

    public PlayerStateMachine.PlayerGameState GetGameState()
    {
        return playerStateMachine.CurrentGameState;
    }

    public void SetVelocityY(float velocityY)
    {
        luigiVelocity.Y = velocityY;
    }

    public void SetVelocityX(float velocityX)
    {
        luigiVelocity.X = velocityX;
    }

    public void SetPositionY(float positionY)
    {
        luigiPosition.Y = positionY;
    }

    public void SetPositionX(float positionX)
    {
        luigiPosition.X = positionX;
    }

    public void SetIsOnGround(bool isGround)
    {
        isOnGround = isGround;
    }

    public bool GetIsOnGround()
    {
        return isOnGround;
    }

    public void CollectStar()
    {
        starTimer = 450;
        playerStateMachine.SetStar();
    }

    public void RemoveStar()
    {
        if (this.playerStateMachine.HasStar() && starTimer <= 0)
        {
            playerStateMachine.RemoveStar();
        }
    }

    public void updateGroundPosition(float gp)
    {
        this.groundPosition = gp;
    }

    public PlayerStateMachine getStateMachine()
    {
        return this.playerStateMachine;
    }

    public int GetScoreMult()
    {
        return this.scoreMult;
    }

    public void IncreaseScoreMult()
    {
        if ((scoreMult < maxScoreMult) && !isSwimmingLevel)
        {
            this.scoreMult *= 2;
        }
    }

    public void ResetScoreMult()
    {
        if (isOnGround && !this.playerStateMachine.HasStar())
        {
            scoreMult = 1;
        }
    }

    public void AddScore(int scoreAmt)
    {
        game.hudManager.AddScore(scoreAmt * this.scoreMult);
        game.scorePopups.Add(new ScorePopup(luigiPosition, this.game, scoreAmt * this.scoreMult));
    }
    public void AddCoin()
    {
        game.hudManager.CollectCoin();
        game.hudManager.AddScore(100);
    }
    public void playSound(int index)
    {
        _sfx[index].Play();
    }

    public void SetMarioRef(Mario mario)
    {
        this.mario = mario;
    }

    public void Fall()
    {
        this.isOnGround = false;
        this.updateGroundPosition(this.GroundPosition() + 100f);
    }
}