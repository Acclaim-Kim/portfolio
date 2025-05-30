using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework;



public class Ground
{
    private List<Rectangle> collisionRectangles;

    public Ground(List<Rectangle> collisionRects)
    {
        this.collisionRectangles = collisionRects;
    }

    public void addCollisionRect(Rectangle emptyFloorRectangle)
    {
        collisionRectangles.Add(emptyFloorRectangle);

    }
    public void removeCollisionRect(Rectangle emptyFloorRectangle)
    {
        collisionRectangles.Remove(emptyFloorRectangle);
    }

    public List<Rectangle> allCollisionRectangles()
    {
        return this.collisionRectangles;
    }
    public void clearCollisionRectangleList()
    {
        collisionRectangles.Clear();
    }

}

