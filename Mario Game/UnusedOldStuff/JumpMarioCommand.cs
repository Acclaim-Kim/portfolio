// using Pixel_Plumbers_Fall_2024;
// using Microsoft.Xna.Framework.Graphics;

// public class JumpMarioCommand : ICommand
// {
//     private Game1 game;
//     private Texture2D marioTexture;

//     public JumpMarioCommand(Game1 game, Texture2D marioTexture)
//     {
//         this.game = game;
//         this.marioTexture = marioTexture;
//     }

//     public void Execute()
//     {
//         if (!game.isJumping)
//         {
//             game.isJumping = true;
//             game.marioVelocity.Y = game.jumpSpeed;

//             game.Mario.Jump();
//         }
//     }
// }
