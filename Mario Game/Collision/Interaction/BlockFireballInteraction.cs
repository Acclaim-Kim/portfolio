using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

    public class BlockFireballInteraction
{
    private Fireball _fireball;
    private IBlock _block;
    private List<IEntity> _entities;
    private List<IEntity> _entitiesRemoved;

    public BlockFireballInteraction(Fireball fireball, IBlock block, List<IEntity> entitiesRemoved)
    {
        this._block = block;
        this._fireball = fireball;
        this._entitiesRemoved = entitiesRemoved;
    }
    
    public void update()
    {
        Rectangle temp = Rectangle.Intersect(_block.GetDestination(), _fireball.GetDestination());
        if (temp.Width > temp.Height)
        {
            _fireball.Bounce();
        }
        else
        {
            _fireball.Remove();
            removeFromList();
        }
    }
    private void removeFromList()
    {
        _entitiesRemoved.Add(_fireball);
        _fireball.Remove();
    }
}

