using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IItem: IEntity
{
    public void swapDirection();
    public void update(GameTime gameTime);
    public void setGroundPosition(int groundPosition);
    public void draw();
    public Vector2 currentPosition();
    public void destroy();
    public void roams();
    public void collect();
    public void idling();
    public void MakeFalling();
    public void NotFalling();

    public void SetVelocityY(float velocityY);
    public void SetVelocityX(float velocityX);
    public void SetPositionY(float positionY);
    public void SetPositionX(float positionX);
    public void SetIsOnGround(bool isOnGround);
    public void ApplyGravity(GameTime gameTime);
    public bool GetIsOnGround();
}

