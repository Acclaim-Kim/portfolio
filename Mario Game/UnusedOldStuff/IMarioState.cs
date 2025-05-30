// using Pixel_Plumbers_Fall_2024;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using Microsoft.Xna.Framework.Input;
// using System.Text;
// using System.Threading.Tasks;
// using static MarioState;

// //Mario State Machine

// internal interface IMarioState
// {
//     void TakeDamage();
//     void Die();
//     void Jump();
//     void Update();
//     void Stop();
//     void Run();
//     void Swim();
//     void SwapDir();
//     void Turning();
//     void CollectPowerup(int powerupType);
//     void Crouch();
//     MarioStateEnum GetState();
//     MarioDirectionEnum GetDirection();
//     MarioPowerupEnum GetPowerup();
// }

// public class MarioState : IMarioState
// {
//     public enum MarioStateEnum { Still, Run, Jump, Crouch, Swim, Turning, Dead };
//     public enum MarioPowerupEnum { Base, Big, Fire };
//     public enum MarioDirectionEnum { Left, Right };

//     private MarioStateEnum currState = MarioStateEnum.Still;
//     private MarioPowerupEnum currPowerup = MarioPowerupEnum.Base;
//     private MarioDirectionEnum currDirection = MarioDirectionEnum.Right;

//     private Vector2 MarioVelocity;

//     Game1 game;
//     Texture2D marioTexture;

//     public MarioState(Game1 game, Texture2D texture)
//     {
//         this.game = game;
//         this.marioTexture = texture;
//     }

//     public void TakeDamage()
//     {
//         switch (currPowerup)
//         {
//             case MarioPowerupEnum.Fire:
//                 currPowerup = MarioPowerupEnum.Big;
//                 break;
//             case MarioPowerupEnum.Big:
//                 currPowerup = MarioPowerupEnum.Base;
//                 break;
//             case MarioPowerupEnum.Base:
//                 //die
//                 //this.Die();
//                 break;
//         }
//     }

//     public void Die()
//     {
//         //Death not implemented yet
//         //currState = MarioStateEnum.Dead;
//     }
//     public void Jump()
//     {
//         currState = MarioStateEnum.Jump;
//     }

//     public void Stop()
//     {
//         currState = MarioStateEnum.Still;
//     }
//     public void Crouch()
//     {
//         if (currState != MarioStateEnum.Jump)
//         {
//             currState = MarioStateEnum.Crouch;
//         }
//     }
//     public void Run()
//     {
//         if (game.marioVelocity.Y == 0)
//         {
//             currState = MarioStateEnum.Run;
//         }
//     }
//     public void Swim()
//     {
//         currState = MarioStateEnum.Swim;
//     }
//     public void SwapDir()
//     {
//         switch (currDirection)
//         {
//             case MarioDirectionEnum.Left:
//                 currDirection = MarioDirectionEnum.Right;
//                 break;
//             case MarioDirectionEnum.Right:
//                 currDirection = MarioDirectionEnum.Left;
//                 break;
//             default:
//                 break;
//         }
//     }

//     public void Turning()
//     {
//         if (game.marioVelocity.Y == 0)
//         {
//             currState = MarioStateEnum.Turning;
//         }
//     }
//     public void CollectPowerup(int powerupType)
//     {
//         //Probably a better way to get the type of powerup collected. For now, assume
//         //whatever calling this gives an int as input for type.
//         //0 = mushroom, 1 = fire flower
//         switch (powerupType)
//         {
//             case 0:
//                 if (currPowerup == MarioPowerupEnum.Base)
//                 {
//                     currPowerup = MarioPowerupEnum.Big;
//                 }
//                 break;
//             case 1:
//                 currPowerup = MarioPowerupEnum.Fire;
//                 break;
//             default:
//                 break;
//         }
//     }

//     public MarioStateEnum GetState()
//     {
//         return this.currState;
//     }

//     public MarioDirectionEnum GetDirection()
//     {
//         return this.currDirection;
//     }

//     public MarioPowerupEnum GetPowerup()
//     {
//         return this.currPowerup;
//     }

//     void UpdateCheckIfStill()
//     {
//         if (MarioVelocity.X == 0)
//         {
//             switch (currState)
//             {
//                 case MarioStateEnum.Still:
//                 case MarioStateEnum.Run:
//                 case MarioStateEnum.Turning:
//                     this.Stop();
//                     break;
//                 case MarioStateEnum.Jump:
//                 case MarioStateEnum.Crouch:
//                 case MarioStateEnum.Swim:
//                 case MarioStateEnum.Dead:
//                     break;
//             }
//         }
//     }
//     void UpdateCheckIfTurning()
//     {

//         if (MarioVelocity.X > 0)
//         {
//             switch (currDirection)
//             {
//                 case MarioDirectionEnum.Left:
//                     this.Turning();
//                     break;
//                 case MarioDirectionEnum.Right:
//                     this.Run();
//                     break;
//             }
//         }
//         else if (MarioVelocity.X < 0)
//         {
//             switch (currDirection)
//             {
//                 case MarioDirectionEnum.Left:
//                     this.Run();
//                     break;
//                 case MarioDirectionEnum.Right:
//                     this.Turning();
//                     break;
//             }
//         }
//     }

//     void UpdateStopJumping()
//     {
//         if ((currState == MarioStateEnum.Jump) && (MarioVelocity.Y == 0))
//         {
//             switch (MarioVelocity.X)
//             {
//                 case 0:
//                     this.Stop();
//                     break;
//                 default:
//                     this.Run();
//                     break;
//             }
//         }
//     }

//     void UpdateSlowMario()
//     {
//         if (game.marioVelocity.X > 0f)
//         {
//             game.marioVelocity.X -= .1f;
//             if (game.marioVelocity.X < 0f)
//             {
//                 game.marioVelocity.X = 0f;
//             }
//         }
//         if (game.marioVelocity.X < 0f)
//         {
//             game.marioVelocity.X += .1f;
//             if (game.marioVelocity.X > 0f)
//             {
//                 game.marioVelocity.X = 0f;
//             }
//         }

//     }

//     public void Update()
//     {
//         this.MarioVelocity = game.marioVelocity;

//         this.UpdateSlowMario();

//         this.UpdateCheckIfStill();

//         this.UpdateStopJumping();

//         this.UpdateCheckIfTurning();

//         MarioSpriteConstructor.ConstructMarioSprite(this, game, marioTexture);

//     }
// }

