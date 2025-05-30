// using Pixel_Plumbers_Fall_2024;
// using Microsoft.Xna.Framework.Graphics;

// public class MoveRightMarioCommand : ICommand
// {
//     private Game1 game;
//     private Texture2D marioTexture;

//     public MoveRightMarioCommand(Game1 game, Texture2D marioTexture)
//     {
//         this.game = game;
//         this.marioTexture = marioTexture;

//     }
//     public void Execute()
//     {
//         if (game.marioVelocity.X < 5f)
//         {
//             game.marioVelocity.X += .2f;
//             if (game.marioVelocity.X > 5f)
//             {
//                 game.marioVelocity.X = 5f;
//             }
//         }
//         game.marioPosition.X += game.marioVelocity.X;

//         //Only swap direction if Mario is on the ground
//         if ((game.Mario.GetDirection() == MarioState.MarioDirectionEnum.Left) && (game.marioVelocity.Y == 0))
//         {
//             game.Mario.SwapDir();
//         }
//         game.Mario.Run();
//     }
// }