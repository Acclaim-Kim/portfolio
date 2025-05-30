using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


internal interface IProjectile : IEntity
{
    public void Update(GameTime gameTime);
    public void Draw(SpriteBatch sb);
}

