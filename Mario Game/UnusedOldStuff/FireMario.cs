// using Pixel_Plumbers_Fall_2024;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// class FireMario : IMario
// {
//     IMario decoratedMario;
//     Game1 game;
//     public FireMario(IMario decoratedMario, Game1 game)
//     {
//         this.decoratedMario = decoratedMario;
//         this.game = game;
//     }

//     public void TakeDamage()
//     {
//         decoratedMario.TakeDamage();
//         this.RemovePowerup();
//     }
//     public void Jump()
//     {
//         decoratedMario.Jump();
//     }
//     public void Update()
//     {
//         decoratedMario.Update();
//     }

//     public void RemovePowerup()
//     {
//         //Copied directly from carmen, change to better fix our project
//         game.Mario = decoratedMario;
//     }

//     public void ShootFire()
//     {
//         //TODO: this
//     }
//     public void CollectPowerup(int powType)
//     {
//         //Fire Mario is the highest tier of powerup, collecting powerups will do nothing
//     }

//     public void CollectStar()
//     {
//         game.Mario = new StarMario(this, game);
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
