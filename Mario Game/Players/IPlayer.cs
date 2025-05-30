using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;

public interface IPlayer : IEntity
{
    public void MoveRight();
    public void MoveLeft();
    public void Jump();
    public void Stop();
    public void JumpStop();
    public void Crouch();
    public void TakeDamage();
    public void SetVelocityY(float vY);
    public void SetVelocityX(float vX);
    public void SetPositionY(float pY);
    public void SetPositionX(float pX);
    public void SetIsOnGround(bool maybe);
    public bool GetIsOnGround();
    public void ApplyGravity(GameTime gameTime);
    public void CollectStar();
    public void PowerUp();
    public void SetWin();

    public void ResetWin();
    public PlayerStateMachine getStateMachine();
    public void AddScore(int scoreAmt);
    public void AddCoin();
    public void Fall();
    public void ResetScoreMult();
    public void IncreaseScoreMult();
    public void playSound(int i);

    public void WinLevelOne();
    public PlayerStateMachine GetStateMachine();
}
