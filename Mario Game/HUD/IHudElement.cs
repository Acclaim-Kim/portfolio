using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IHudElement
{
    public void Update(GameTime gameTime, FollowCamera camera);

    public void Draw(SpriteBatch sb);
}

