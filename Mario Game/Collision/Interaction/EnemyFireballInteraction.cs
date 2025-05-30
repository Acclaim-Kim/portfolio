using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

    public class EnemyFireballInteraction
{
    private Fireball fireball;
    private ISpriteEnemy enemy;
    private List<IEntity> _entities;
    private List<IEntity> _entitiesRemoved;


    public EnemyFireballInteraction(Fireball _fireball, ISpriteEnemy _enemy, List<IEntity> entities, List<IEntity> entitiesRemoved)
    {
        this.enemy = _enemy;
        this.fireball = _fireball;
        this._entities = entities;
        this._entitiesRemoved = entitiesRemoved;
    }
    public void update()
    {
        enemy.beFlipped();
        fireball.Remove();
        removeFromList();
        fireball.AddScore(100);
    }
    private void removeFromList()
    {
        _entitiesRemoved.Add(enemy);
        _entitiesRemoved.Add(fireball);
        fireball.Remove();
    }
}

