using System;
using System.Configuration.Assemblies;

public class PlayerStateMachine
{
    public enum PlayerGameState { Small, Big, Fire}
    public enum PlayerFaceState { Left, Right }
    public enum PlayerMoveState { Idle, Moving, Jumping, Crouching, Turning, Dead }

    public PlayerGameState CurrentGameState { get; private set; }
    public PlayerFaceState CurrentFaceState { get; private set; }
    public PlayerMoveState CurrentMoveState { get; private set; }

    public bool _HasStar;
    private bool PlayerWins;
    public bool isVisible;
    public bool isSwimmingLevel;

    public PlayerStateMachine()
    {
        CurrentGameState = PlayerGameState.Small;
        CurrentFaceState = PlayerFaceState.Right;
        CurrentMoveState = PlayerMoveState.Idle;
        _HasStar = false;
        isVisible = true;
        PlayerWins = false;
    }

    public void SetPlayerSmall()
    {
        CurrentGameState = PlayerGameState.Small;
    }

    public void SetPlayerBig()
    {
        CurrentGameState = PlayerGameState.Big;
    }

    public void SetPlayerFire()
    {
        CurrentGameState = PlayerGameState.Fire;
    }

    public void SetPlayerIdle()
    {
        CurrentMoveState = PlayerMoveState.Idle;
    }

    public void SetPlayerMoving()
    {
        CurrentMoveState = PlayerMoveState.Moving;
    }

    public void SetPlayerJumping()
    {
        CurrentMoveState = PlayerMoveState.Jumping;
    }

    public void SetPlayerCrouching()
    {
        CurrentMoveState = PlayerMoveState.Crouching;
    }

    public void SetPlayerTurning()
    {
        CurrentMoveState = PlayerMoveState.Turning;
    }

    public void SetPlayerDead()
    {
        CurrentMoveState = PlayerMoveState.Dead;
    }
    public void SetPlayerWins()
    {
        PlayerWins = true;
    }
    public void ResetWin()
    {
        PlayerWins = false;
    }
    public void SetPlayerLeft()
    {
        CurrentFaceState = PlayerFaceState.Left;
    }

    public void SetPlayerRight()
    {
        CurrentFaceState = PlayerFaceState.Right;
    }
    public Boolean HasStar()
    {
        return _HasStar;
    }

    public void SetStar()
    {
        _HasStar = true;
    }

    public void RemoveStar()
    {
        _HasStar = false;
    }

    public void UpdateMoveStateForJumping()
    {
        if (CurrentMoveState == PlayerMoveState.Jumping)
        {
            CurrentMoveState = PlayerMoveState.Idle;
        }
    }

    public bool IsCrouching()
    {
        return CurrentMoveState == PlayerMoveState.Crouching;
    }

    public bool IsJumping()
    {
        return CurrentMoveState == PlayerMoveState.Jumping;
    }

    public bool IsTurning()
    {
        return CurrentMoveState == PlayerMoveState.Turning;
    }

    public bool IsRight()
    {
        return CurrentFaceState == PlayerFaceState.Right;
    }

    public bool IsMoving()
    {
        return CurrentMoveState == PlayerMoveState.Moving;
    }

    public bool IsDead()
    {
        return CurrentMoveState == PlayerMoveState.Dead;
    }

    public bool IsFire()
    {
        return CurrentGameState == PlayerGameState.Fire;
    }
    public void Reset()
    {
        CurrentGameState = PlayerGameState.Small;
        CurrentFaceState = PlayerFaceState.Right;
        CurrentMoveState = PlayerMoveState.Idle;
        PlayerWins = false;
        this.isVisible = true;
        
        
    }
    public bool Wins()
    {
        return PlayerWins;
    }
    public bool IsVisible()
    {
        return isVisible;
    }
    public void MakeVisible()
    {
        isVisible = true;
    }
    public void MakeInvisible()
    {
        isVisible = false;
    }

    public bool IsSmall()
    {
        return CurrentGameState == PlayerGameState.Small;
    }
    public bool IsBig()
    {
        return CurrentGameState == PlayerGameState.Big;
    }
    public void setSwimmingLevel(bool isLevelSwimming)
    {
        this.isSwimmingLevel = isLevelSwimming;
    }
}
