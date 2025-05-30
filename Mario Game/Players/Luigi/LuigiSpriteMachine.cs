using Microsoft.Xna.Framework.Graphics;
using Pixel_Plumbers_Fall_2024;

namespace Pixel_Plumbers_Fall_2024
{
    public class LuigiSpriteMachine : IMarioSpriteMachine
    {
        private static ICharacter lastValidSprite;
        public ICharacter UpdatePlayerSprite(PlayerStateMachine luigiStateMachine, Texture2D texture)
        {
            ICharacter newSprite = null;
            if (luigiStateMachine.IsDead())
            {
                newSprite = new DeadCharacter(texture);
            }
            else if (luigiStateMachine.Wins())
            {
                newSprite = GetWinLuigiState(luigiStateMachine, texture);
            }
            else
            {
                newSprite = GetSpriteForFaceState(luigiStateMachine, texture);
            }

            if (lastValidSprite != null && newSprite != null && newSprite.GetType() == lastValidSprite.GetType())
            {
                return lastValidSprite;
            }

            if (newSprite != null)
            {
                lastValidSprite = newSprite;
                return newSprite;
            }

            return lastValidSprite ?? new IdleLeftBig(texture);
        }

        private static ICharacter GetSpriteForFaceState(PlayerStateMachine luigiStateMachine, Texture2D texture)
        {
            switch (luigiStateMachine.CurrentFaceState)
            {
                case PlayerStateMachine.PlayerFaceState.Right:
                    return GetSpriteForGameStateRight(luigiStateMachine, texture);
                case PlayerStateMachine.PlayerFaceState.Left:
                    return GetSpriteForGameStateLeft(luigiStateMachine, texture);
                default:
                    return null;
            }
        }

        private static ICharacter GetSpriteForGameStateRight(PlayerStateMachine luigiStateMachine, Texture2D texture)
        {
            switch (luigiStateMachine.CurrentGameState)
            {
                case PlayerStateMachine.PlayerGameState.Small:
                    return GetSmallLuigiSpriteRight(luigiStateMachine, texture);
                case PlayerStateMachine.PlayerGameState.Big:
                    return GetBigLuigiSpriteRight(luigiStateMachine, texture);
                case PlayerStateMachine.PlayerGameState.Fire:
                    return GetFireLuigiSpriteRight(luigiStateMachine, texture);
                default:
                    return null;
            }
        }

        private static ICharacter GetSpriteForGameStateLeft(PlayerStateMachine luigiStateMachine, Texture2D texture)
        {
            switch (luigiStateMachine.CurrentGameState)
            {
                case PlayerStateMachine.PlayerGameState.Small:
                    return GetSmallLuigiSpriteLeft(luigiStateMachine, texture);
                case PlayerStateMachine.PlayerGameState.Big:
                    return GetBigLuigiSpriteLeft(luigiStateMachine, texture);
                case PlayerStateMachine.PlayerGameState.Fire:
                    return GetFireLuigiSpriteLeft(luigiStateMachine, texture);
                default:
                    return null;
            }
        }

        private static ICharacter GetSmallLuigiSpriteRight(PlayerStateMachine luigiStateMachine, Texture2D texture)
        {
            switch (luigiStateMachine.CurrentMoveState)
            {
                case PlayerStateMachine.PlayerMoveState.Idle:
                    return new IdleRightSmall(texture);
                case PlayerStateMachine.PlayerMoveState.Moving:
                    return new MovingRightSmall(texture);
                case PlayerStateMachine.PlayerMoveState.Jumping:
                    return new JumpingRightSmall(texture);
                case PlayerStateMachine.PlayerMoveState.Turning:
                    return new TurningLeftSmall(texture);
                default:
                    return null;
            }
        }

        private static ICharacter GetBigLuigiSpriteRight(PlayerStateMachine luigiStateMachine, Texture2D texture)
        {
            switch (luigiStateMachine.CurrentMoveState)
            {
                case PlayerStateMachine.PlayerMoveState.Idle:
                    return new IdleRightBig(texture);
                case PlayerStateMachine.PlayerMoveState.Moving:
                    return new MovingRightBig(texture);
                case PlayerStateMachine.PlayerMoveState.Jumping:
                    return new JumpingRightBig(texture);
                case PlayerStateMachine.PlayerMoveState.Crouching:
                    return new CrouchRightBig(texture);
                case PlayerStateMachine.PlayerMoveState.Turning:
                    return new TurningLeftBig(texture);
                default:
                    return null;
            }
        }

        private static ICharacter GetFireLuigiSpriteRight(PlayerStateMachine luigiStateMachine, Texture2D texture)
        {
            switch (luigiStateMachine.CurrentMoveState)
            {
                case PlayerStateMachine.PlayerMoveState.Idle:
                    return new IdleRightFire(texture);
                case PlayerStateMachine.PlayerMoveState.Moving:
                    return new MovingRightFire(texture);
                case PlayerStateMachine.PlayerMoveState.Jumping:
                    return new JumpingRightFire(texture);
                case PlayerStateMachine.PlayerMoveState.Crouching:
                    return new CrouchRightFire(texture);
                case PlayerStateMachine.PlayerMoveState.Turning:
                    return new TurningLeftFire(texture);
                default:
                    return null;
            }
        }

        private static ICharacter GetSmallLuigiSpriteLeft(PlayerStateMachine luigiStateMachine, Texture2D texture)
        {
            switch (luigiStateMachine.CurrentMoveState)
            {
                case PlayerStateMachine.PlayerMoveState.Idle:
                    return new IdleLeftSmall(texture);
                case PlayerStateMachine.PlayerMoveState.Moving:
                    return new MovingLeftSmall(texture);
                case PlayerStateMachine.PlayerMoveState.Jumping:
                    return new JumpingLeftSmall(texture);
                case PlayerStateMachine.PlayerMoveState.Turning:
                    return new TurningRightSmall(texture);
                default:
                    return null;
            }
        }

        private static ICharacter GetBigLuigiSpriteLeft(PlayerStateMachine luigiStateMachine, Texture2D texture)
        {
            switch (luigiStateMachine.CurrentMoveState)
            {
                case PlayerStateMachine.PlayerMoveState.Idle:
                    return new IdleLeftBig(texture);
                case PlayerStateMachine.PlayerMoveState.Moving:
                    return new MovingLeftBig(texture);
                case PlayerStateMachine.PlayerMoveState.Jumping:
                    return new JumpingLeftBig(texture);
                case PlayerStateMachine.PlayerMoveState.Crouching:
                    return new CrouchLeftBig(texture);
                case PlayerStateMachine.PlayerMoveState.Turning:
                    return new TurningRightBig(texture);
                default:
                    return null;
            }
        }

        private static ICharacter GetFireLuigiSpriteLeft(PlayerStateMachine luigiStateMachine, Texture2D texture)
        {
            switch (luigiStateMachine.CurrentMoveState)
            {
                case PlayerStateMachine.PlayerMoveState.Idle:
                    return new IdleLeftFire(texture);
                case PlayerStateMachine.PlayerMoveState.Moving:
                    return new MovingLeftFire(texture);
                case PlayerStateMachine.PlayerMoveState.Jumping:
                    return new JumpingLeftFire(texture);
                case PlayerStateMachine.PlayerMoveState.Crouching:
                    return new CrouchLeftFire(texture);
                case PlayerStateMachine.PlayerMoveState.Turning:
                    return new TurningRightFire(texture);
                default:
                    return null;
            }
        }

        private static ICharacter GetWinLuigiState(PlayerStateMachine marioStateMachine, Texture2D texture)
        {
            switch (marioStateMachine.CurrentGameState)
            {
                case PlayerStateMachine.PlayerGameState.Small:
                    return new WinSmall(texture);
                case PlayerStateMachine.PlayerGameState.Big:
                    return new WinBig(texture);
                case PlayerStateMachine.PlayerGameState.Fire:
                    return new WinFire(texture);
                default: return null;
            }

        }
    }
}
