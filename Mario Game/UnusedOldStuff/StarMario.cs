// using Pixel_Plumbers_Fall_2024;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// class StarMario : IMario
// {
//     IMario decoratedMario;
//     Game1 game;
//     //Replace timer with however long the star lasts
//     int timer = 100;

//     public StarMario(IMario decoratedMario, Game1 game)
//     {
//         this.decoratedMario = decoratedMario;
//         this.game = game;
//     }

//     public void TakeDamage()
//     {
//         //Left empty; Mario is invincible
//     }
//     public void Jump()
//     {
//         decoratedMario.Jump();
//     }
//     public void Update()
//     {
//         timer--;
//         if (timer == 0)
//         {
//             RemoveStar();
//         }

//         decoratedMario.Update();
//     }
//     public void CollectPowerup(int powType)
//     {
//         decoratedMario.CollectPowerup(powType);
//         if (powType == 1)
//         {
//             game.Mario = new FireMario(this, game);
//         }
//     }

//     public void CollectStar()
//     {
//         //Do nothing since already has star
//         //Maybe reset timer?
//     }

//     public void RemoveStar()
//     {
//         //Copied directly from carmen, change to better fix our project
//         game.Mario = decoratedMario;
//     }

//     public void Stop()
//     {
//         decoratedMario.Stop();
//     }

//     public void Run()
//     {
//         decoratedMario.Run();
//     }

//     public void Swim()
//     {
//         decoratedMario.Swim();
//     }

//     public void SwapDir()
//     {
//         decoratedMario.SwapDir();
//     }

//     public void Turning()
//     {
//         decoratedMario.Turning();
//     }

//     public void Crouch()
//     {
//         decoratedMario.Crouch();
//     }

//     public MarioState.MarioStateEnum GetState()
//     {
//         return decoratedMario.GetState();
//     }

//     public MarioState.MarioDirectionEnum GetDirection()
//     {
//         return decoratedMario.GetDirection();
//     }

//     public MarioState.MarioPowerupEnum GetPowerup()
//     {
//         return decoratedMario.GetPowerup();
//     }
// }
