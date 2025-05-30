// using Microsoft.Xna.Framework.Graphics;
// using Pixel_Plumbers_Fall_2024;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
    
// class Mario : IMario
// {
//     IMarioState state;
//     //IMarioObject is reference to IMario in Game1. Kept here as variable so that the
//     //decorators can change it
//     Game1 game;
//     Texture2D marioTexture;

//     public Mario(Game1 game, Texture2D texture)
//     {
//         this.game = game;
//         this.marioTexture = texture;
//         state = new MarioState(game, marioTexture);
//     }

//     public void TakeDamage()
//     {
//         state.TakeDamage();
//     }
//     public void Jump()
//     {
//         state.Jump();
//     }
//     public void Update()
//     {
//         state.Update();
//     }

//     public void Stop()
//     {
//         state.Stop();
//     }
//     public void Run()
//     {
//         state.Run();
//     }
//     public void Swim()
//     {
//         state.Swim();
//     }

//     public void SwapDir()
//     {
//         state.SwapDir();
//     }
//     public void Turning()
//     {
//         state.Turning();
//     }
//     public void Crouch()
//     {
//         state.Crouch();
//     }
//     public void CollectPowerup(int powType)
//     {
//         //Probably a better way to get the type of powerup collected. For now, assume
//         //whatever calling this gives an int as input for type.
//         //0 = mushroom, 1 = fire flower
//         state.CollectPowerup(powType);
//         if (powType == 1)
//         {
//             game.Mario = new FireMario(this, game);
//         }
//     }
//     public void CollectStar()
//     {
//         game.Mario = new StarMario(this, this.game);
//     }

//     public MarioState.MarioStateEnum GetState()
//     {
//         return state.GetState();
//     }

//     public MarioState.MarioDirectionEnum GetDirection()
//     {
//         return state.GetDirection();
//     }

//     public MarioState.MarioPowerupEnum GetPowerup()
//     {
//         return state.GetPowerup();
//     }
// }

