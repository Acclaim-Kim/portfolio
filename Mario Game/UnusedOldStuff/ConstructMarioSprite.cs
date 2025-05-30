// using Microsoft.Xna.Framework.Graphics;
// using Pixel_Plumbers_Fall_2024;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Security.Cryptography.X509Certificates;
// using System.Text;
// using System.Threading.Tasks;

// namespace Pixel_Plumbers_Fall_2024;

// public class MarioSpriteConstructor
// {
//     public static void ConstructMarioSprite(MarioState marioState, Game1 game, Texture2D texture)
//     {
//         switch (marioState.GetDirection())
//         {
//             case MarioState.MarioDirectionEnum.Left:
//                 switch (marioState.GetState())
//                 {
//                     case MarioState.MarioStateEnum.Still:
//                         switch (marioState.GetPowerup())
//                         {
//                             case MarioState.MarioPowerupEnum.Base:
//                                 game.currentMarioSprite = new IdleLeftSmallMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Big:
//                                 game.currentMarioSprite = new IdleLeftBigMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Fire:
//                                 game.currentMarioSprite = new IdleLeftFireMario(texture);
//                                 break;
//                         }
//                         break;
//                     case MarioState.MarioStateEnum.Run:
//                         switch (marioState.GetPowerup())
//                         {
//                             case MarioState.MarioPowerupEnum.Base:
//                                 if (!(game.currentMarioSprite.GetType() == new MovingLeftSmallMario(texture).GetType()))
//                                 {
//                                     game.currentMarioSprite = new MovingLeftSmallMario(texture);
//                                 }
//                                 break;
//                             case MarioState.MarioPowerupEnum.Big:
//                                 if (!(game.currentMarioSprite.GetType() == new MovingLeftBigMario(texture).GetType()))
//                                 {
//                                     game.currentMarioSprite = new MovingLeftBigMario(texture);
//                                 }
//                                 break;
//                             case MarioState.MarioPowerupEnum.Fire:
//                                 if (!(game.currentMarioSprite.GetType() == new MovingLeftFireMario(texture).GetType()))
//                                 {
//                                     game.currentMarioSprite = new MovingLeftFireMario(texture);
//                                 }
//                                 break;
//                         }
//                         break;
//                     /* Swimming not implemented yet
//                     case MarioState.MarioStateEnum.Swim:
//                         switch (marioState.GetPowerup())
//                         {
//                             case MarioState.MarioPowerupEnum.Base:
//                                 game.currentMarioSprite = (IMarioSprite)new SmallSwimLeftMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Big:
//                                 game.currentMarioSprite = (IMarioSprite)new BigSwimLeftMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Fire:
//                                 game.currentMarioSprite = (IMarioSprite)new FireSwimLeftMario(texture);
//                                 break;
//                         }
//                         break;
//                     */
//                     case MarioState.MarioStateEnum.Turning:
//                         switch (marioState.GetPowerup())
//                         {
//                             case MarioState.MarioPowerupEnum.Base:
//                                 game.currentMarioSprite = new TurningLeftSmallMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Big:
//                                 game.currentMarioSprite = new TurningLeftBigMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Fire:
//                                 game.currentMarioSprite = new TurningLeftFireMario(texture);
//                                 break;
//                         }
//                         break;
//                     case MarioState.MarioStateEnum.Crouch:
//                         switch (marioState.GetPowerup())
//                         {
//                             case MarioState.MarioPowerupEnum.Base:
//                                 game.currentMarioSprite = new IdleLeftSmallMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Big:
//                                 game.currentMarioSprite = new CrouchLeftBigMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Fire:
//                                 game.currentMarioSprite = new CrouchLeftFireMario(texture);
//                                 break;
//                         }
//                         break;
//                     case MarioState.MarioStateEnum.Jump:
//                         switch (marioState.GetPowerup())
//                         {
//                             case MarioState.MarioPowerupEnum.Base:
//                                 game.currentMarioSprite = new JumpingLeftSmallMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Big:
//                                 game.currentMarioSprite = new JumpingLeftBigMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Fire:
//                                 game.currentMarioSprite = new JumpingLeftFireMario(texture);
//                                 break;
//                         }
//                         break;
//                     case MarioState.MarioStateEnum.Dead:
//                         game.currentMarioSprite = new DeadMario(texture);
//                         break;
//                 }
//                 break;
//             case MarioState.MarioDirectionEnum.Right:
//                 switch (marioState.GetState())
//                 {
//                     case MarioState.MarioStateEnum.Still:
//                         switch (marioState.GetPowerup())
//                         {
//                             case MarioState.MarioPowerupEnum.Base:
//                                 game.currentMarioSprite = new IdleRightSmallMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Big:
//                                 game.currentMarioSprite = new IdleRightBigMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Fire:
//                                 game.currentMarioSprite = new IdleRightFireMario(texture);
//                                 break;
//                         }
//                         break;
//                     case MarioState.MarioStateEnum.Run:
//                         switch (marioState.GetPowerup())
//                         {
//                             case MarioState.MarioPowerupEnum.Base:
//                                 if (!(game.currentMarioSprite.GetType() == new MovingRightSmallMario(texture).GetType()))
//                                 {
//                                     game.currentMarioSprite = new MovingRightSmallMario(texture);
//                                 }
//                                 break;
//                             case MarioState.MarioPowerupEnum.Big:
//                                 if (!(game.currentMarioSprite.GetType() == new MovingRightBigMario(texture).GetType()))
//                                 {
//                                     game.currentMarioSprite = new MovingRightBigMario(texture);
//                                 }
//                                 break;
//                             case MarioState.MarioPowerupEnum.Fire:
//                                 if (!(game.currentMarioSprite.GetType() == new MovingRightFireMario(texture).GetType()))
//                                 {
//                                     game.currentMarioSprite = new MovingRightFireMario(texture);
//                                 }
//                                 break;
//                         }
//                         break;
//                     /* Swimming not implemented yet
//                     case MarioState.MarioStateEnum.Swim:
//                         switch (marioState.GetPowerup())
//                         {
//                             case MarioState.MarioPowerupEnum.Base:
//                                 game.currentMarioSprite = new SmallSwimRightMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Big:
//                                 game.currentMarioSprite = (IMarioSprite)new BigSwimRightMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Fire:
//                                 game.currentMarioSprite = (IMarioSprite)new FireSwimRightMario(texture);
//                                 break;
//                         }
//                         break;
//                     */
//                     case MarioState.MarioStateEnum.Turning:
//                         switch (marioState.GetPowerup())
//                         {
//                             case MarioState.MarioPowerupEnum.Base:
//                                 game.currentMarioSprite = new TurningRightSmallMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Big:
//                                 game.currentMarioSprite = new TurningRightBigMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Fire:
//                                 game.currentMarioSprite = new TurningRightFireMario(texture);
//                                 break;
//                         }
//                         break;
//                     case MarioState.MarioStateEnum.Crouch:
//                         switch (marioState.GetPowerup())
//                         {
//                             case MarioState.MarioPowerupEnum.Base:
//                                 game.currentMarioSprite = new IdleRightSmallMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Big:
//                                 game.currentMarioSprite = new CrouchRightBigMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Fire:
//                                 game.currentMarioSprite = new CrouchRightFireMario(texture);
//                                 break;
//                         }
//                         break;
//                     case MarioState.MarioStateEnum.Jump:
//                         switch (marioState.GetPowerup())
//                         {
//                             case MarioState.MarioPowerupEnum.Base:
//                                 game.currentMarioSprite = new JumpingRightSmallMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Big:
//                                 game.currentMarioSprite = new JumpingRightBigMario(texture);
//                                 break;
//                             case MarioState.MarioPowerupEnum.Fire:
//                                 game.currentMarioSprite = new JumpingRightFireMario(texture);
//                                 break;
//                         }
//                         break;
//                     case MarioState.MarioStateEnum.Dead:
//                         game.currentMarioSprite = new DeadMario(texture);
//                         break;
//                 }
//                 break;
//         }
//     }
// }
