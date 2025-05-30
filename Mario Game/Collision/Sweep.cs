using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Pixel_Plumbers_Fall_2024;

public class Sweep
{
    GameTime gameTime;
    DisablePlayerCommand disablePlayerCommand;

    public Sweep(GameTime gameTime, DisablePlayerCommand disablePlayerCommand)
    {
        this.gameTime = gameTime;
        this.disablePlayerCommand = disablePlayerCommand;
    }
    public enum CollisionType { Top, Bottom, Left, Right, DontCare };

    private Boolean ContainsEnemy(List<IEntity> entities, int index)
    {
        Boolean containsEnemy = false;
        Type type = entities[index].GetType();
        if (type == typeof(Goomba) || type == typeof(Koopa) || type == typeof(Cheeps) || type == typeof(Blooper))
        {
            containsEnemy = true;
            
        }
        return containsEnemy;
    }

    private Boolean ContainsBlock(List<IEntity> entities, int index)
    {
        Boolean containsBlock = false;
        Type type = entities[index].GetType();
        if(type == typeof(BrokenBrickSprite) || type == typeof(LuckyBlockSprite) || type == typeof(StaticBlockSprite))
        {
            containsBlock = true;
        }
        return containsBlock;
    }

    private Boolean ContainsObstacle(List<IEntity> entities, int index)
    {
        Boolean containsObstacle = false;
        Type type = entities[index].GetType();
        if (type == typeof(obstacle1) || type == typeof(obstacle2) || type == typeof(obstacle3))
        {
            containsObstacle = true;
        }
        return containsObstacle;
    }
    private Boolean ContainsItem(List<IEntity> entities, int index)
    {
        Boolean containsItem = false;
        Type type = entities[index].GetType();
        if (type == typeof(Mushroom) || type == typeof(Star) || type == typeof(Fire))
        {
            containsItem  = true;
        }
        return containsItem;
    }

    private Boolean ContainsPlayer(List<IEntity> entities, int index)
    {
        Boolean containsEnemy = false;
        Type type = entities[index].GetType();
        if (type == typeof(Mario) || type == typeof(Luigi))
        {
            containsEnemy = true;
        }
        return containsEnemy;
    }
    private Boolean ContainsGround(List<IEntity> entities, int index)
    {
        Boolean containsGround = false;
        Type type = entities[index].GetType();
        if (type == typeof(Ground) )
        {
            containsGround = true;
        }
        return containsGround;
    }


    EnemyPlayerInteraction EnemyMarioInteraction;
    OtherEnemyInteraction OtherEnemyInteraction;
    EnemyFireballInteraction EnemyFireballInteraction;
    PlayerFirePowerInteraction MarioFirePowerInteraction;
    PlayerMushroomInteraction MarioMushroomInteraction;
    MarioStarInteraction MarioStarInteraction;
    BlockFireballInteraction BlockFireballInteraction;
    PlayerBlockInteraction MarioBlockInteraction;
    PlayerObstacleInteraction MarioObstacleInteraction;
    ItemObstacleInteraction ItemObstacleInteraction;
    ItemBlockInteraction ItemBlockInteraction;
    PlayerItemInteraction MarioItemInteraction;
    PlayerFlagInteraction PlayerFlagInteraction;
    ObstacleFireballInteraction ObstacleFireballInteraction;
   

    public void iterateListInteractions(List<IEntity> entities, List<IEntity> entitiesRemoved)
    {
        int item1 = 0;
        int item2 = 1;
        if(entities.Count > 1)
        {
            while (item1 < entities.Count  &&  item2 < entities.Count )
            {
                handleInteraction(entities, entitiesRemoved, item1, item2);
                item1++;
                item2++;
            }
        }
    }
    private void handleInteraction(List<IEntity> entities, List<IEntity> entitiesRemoved, int index1, int index2)
    {

        /*Should determine the interactionType of two entities and call the appropriate method */
        IEntity item1 = entities[index1];
        IEntity item2 = entities[index2];

          if (ContainsPlayer(entities, index1) && item2.GetType() == typeof(Flag))
        {

            PlayerFlagInteraction = new PlayerFlagInteraction((IPlayer)item1, (Flag)item2, entities, index2, entitiesRemoved, disablePlayerCommand);
            
            PlayerFlagInteraction.update(this.gameTime);
            entities.RemoveAt(index2);

        }
        else if (ContainsPlayer(entities, index2) && item1.GetType() == typeof(Flag))
        {

            PlayerFlagInteraction = new PlayerFlagInteraction((IPlayer)item2, (Flag)item1, entities, index1, entitiesRemoved, disablePlayerCommand);
            
            PlayerFlagInteraction.update(this.gameTime);
            entities.RemoveAt(index1);
        }
        else if (ContainsEnemy(entities, index1) && ContainsPlayer(entities, index2))
        {
            EnemyMarioInteraction = new EnemyPlayerInteraction((ISpriteEnemy)item1, (IPlayer)item2, Rectangle.Intersect(item1.GetDestination(), item2.GetDestination()), entitiesRemoved);
            EnemyMarioInteraction.Update();

        }
        else if (ContainsPlayer(entities, index1) && ContainsEnemy(entities, index2))
        {
            EnemyMarioInteraction = new EnemyPlayerInteraction((ISpriteEnemy)item2, (IPlayer)item1, Rectangle.Intersect(item1.GetDestination(), item2.GetDestination()), entitiesRemoved);
            EnemyMarioInteraction.Update();

        }

        else if (ContainsEnemy(entities, index1) && item2.GetType() == typeof(Fireball))
        {
            EnemyFireballInteraction = new EnemyFireballInteraction((Fireball)item2, (ISpriteEnemy)item1, entities, entitiesRemoved);
            EnemyFireballInteraction.update();
        }
        else if (ContainsEnemy(entities, index2) && item1.GetType() == typeof(Fireball))
        {
            EnemyFireballInteraction = new EnemyFireballInteraction((Fireball)item1, (ISpriteEnemy)item2, entities, entitiesRemoved);
            EnemyFireballInteraction.update();
        }
        else if (ContainsEnemy(entities, index1) && (ContainsEnemy(entities, index2)))
        {
            OtherEnemyInteraction = new OtherEnemyInteraction((ISpriteEnemy)item1, item2);
            OtherEnemyInteraction.Update();
            OtherEnemyInteraction = new OtherEnemyInteraction((ISpriteEnemy)item2, item1);
            OtherEnemyInteraction.Update();
        }
        else if (ContainsEnemy(entities, index1))
        {
            OtherEnemyInteraction = new OtherEnemyInteraction((ISpriteEnemy)item1, item2);
            OtherEnemyInteraction.Update();
        }
        else if (ContainsEnemy(entities, index2))
        {
            OtherEnemyInteraction = new OtherEnemyInteraction((ISpriteEnemy)item2, item1);
            OtherEnemyInteraction.Update();
        }
        //Item Mario Interactions
        else if (item1.GetType() == typeof(Star) && ContainsPlayer(entities, index2))
        {
            MarioStarInteraction = new MarioStarInteraction((IPlayer)item2, (Star)item1, entitiesRemoved);
            MarioStarInteraction.update();
            entities.RemoveAt(index1);
        }
        else if (ContainsPlayer(entities, index1) && item2.GetType() == typeof(Star))
        {
            MarioStarInteraction = new MarioStarInteraction((IPlayer)item1, (Star)item2, entitiesRemoved);
            MarioStarInteraction.update();
            entities.RemoveAt(index2);
        }

        else if (item1.GetType() == typeof(Fire) && ContainsPlayer(entities, index2))
        {
            MarioFirePowerInteraction = new PlayerFirePowerInteraction((IPlayer)item2, (Fire)item1, entitiesRemoved);
            MarioFirePowerInteraction.update();
            entities.RemoveAt(index1);
        }
        else if (item2.GetType() == typeof(Fire) && ContainsPlayer(entities, index1))
        {
            MarioFirePowerInteraction = new PlayerFirePowerInteraction((IPlayer)item1, (Fire)item2, entitiesRemoved);
            MarioFirePowerInteraction.update();
            entities.RemoveAt(index2);
        }
        else if (item1.GetType() == typeof(Mushroom) && ContainsPlayer(entities, index2))
        {
            MarioMushroomInteraction = new PlayerMushroomInteraction((IPlayer)item2, (Mushroom)item1, entitiesRemoved);
            MarioMushroomInteraction.update();

            entities.RemoveAt(index1);
        }
        else if (ContainsPlayer(entities, index1) && item1.GetType() == typeof(Mushroom))
        {
            MarioMushroomInteraction = new PlayerMushroomInteraction((IPlayer)item1, (Mushroom)item2, entitiesRemoved);
            MarioMushroomInteraction.update();


            entities.RemoveAt(index2);
        }


        else if (ContainsItem(entities, index1) && ContainsPlayer(entities, index2))
        {
            MarioItemInteraction = new PlayerItemInteraction((IItem)item1, (IPlayer)item2, entitiesRemoved);
        }
        else if (ContainsPlayer(entities, index1) && ContainsItem(entities, index2))
        {
            MarioItemInteraction = new PlayerItemInteraction((IItem)item2, (IPlayer)item1, entitiesRemoved);
        }
        else if (ContainsObstacle(entities, index1) && ContainsItem(entities, index2))
        {
            ItemObstacleInteraction = new ItemObstacleInteraction((IItem)item2, (IObstacle)item1);
            ItemObstacleInteraction.update();
        }
        else if (ContainsObstacle(entities, index2) && ContainsItem(entities, index1))
        {
            ItemObstacleInteraction = new ItemObstacleInteraction((IItem)item1, (IObstacle)item2);
            ItemObstacleInteraction.update();

        }
        else if (ContainsBlock(entities, index1) && ContainsItem(entities, index2))
        {
            ItemBlockInteraction = new ItemBlockInteraction((IItem)item2, (IBlock)item1, gameTime);
            ItemBlockInteraction.update();
        }
        else if (ContainsBlock(entities, index2) && ContainsItem(entities, index1))
        {
            ItemBlockInteraction = new ItemBlockInteraction((IItem)item1, (IBlock)item2, gameTime);
            ItemBlockInteraction.update();
        }

        else if (item1.GetType() == typeof(Fireball) && ContainsBlock(entities, index2))
        {
            BlockFireballInteraction = new BlockFireballInteraction((Fireball)item1, (IBlock)item2, entitiesRemoved);
            BlockFireballInteraction.update();
        }
        else if (item2.GetType() == typeof(Fireball) && ContainsBlock(entities, index1))
        {
            BlockFireballInteraction = new BlockFireballInteraction((Fireball)item2, (IBlock)item1, entitiesRemoved);
            BlockFireballInteraction.update();
        }

        else if (ContainsPlayer(entities, index1) && ContainsBlock(entities, index2))
        {
            //handle block interaction

            Boolean luckyBlock = true;
            if (item2.GetType() == typeof(LuckyBlockSprite))
            {
                MarioBlockInteraction = new PlayerBlockInteraction((IPlayer)item1, (IBlock)item2, entitiesRemoved, luckyBlock);
            }
            else
            {
                luckyBlock = false;
                MarioBlockInteraction = new PlayerBlockInteraction((IPlayer)item1, (IBlock)item2, entitiesRemoved, luckyBlock);
            }
            MarioBlockInteraction.update();

        }
        else if (ContainsBlock(entities, index1) && ContainsPlayer(entities, index2))
        {
            Boolean luckyBlock = true;
            if (item1.GetType() == typeof(LuckyBlockSprite))
            {
                MarioBlockInteraction = new PlayerBlockInteraction((IPlayer)item2, (IBlock)item1, entitiesRemoved, luckyBlock);
            }
            else
            {
                luckyBlock = false;
                MarioBlockInteraction = new PlayerBlockInteraction((IPlayer)item2, (IBlock)item1, entitiesRemoved, luckyBlock);
            }
            MarioBlockInteraction.update();

        }

        else if (ContainsPlayer(entities, index1) && ContainsObstacle(entities, index2))
        {

            MarioObstacleInteraction = new PlayerObstacleInteraction((IPlayer)item1, (IObstacle)item2, gameTime);
            MarioObstacleInteraction.update();

        }
        else if (ContainsObstacle(entities, index1) && ContainsPlayer(entities, index2))
        {

            MarioObstacleInteraction = new PlayerObstacleInteraction((IPlayer)item2, (IObstacle)item1, gameTime);
            MarioObstacleInteraction.update();

        }
        else if (item1.GetType() == typeof(Fireball) && (ContainsObstacle(entities,index2)))
        {
            ObstacleFireballInteraction = new ObstacleFireballInteraction((Fireball)item1,(IObstacle)item2);
            ObstacleFireballInteraction.Update();
        }
        else if (item2.GetType() == typeof(Fireball) && (ContainsObstacle(entities, index1)))
        {
            ObstacleFireballInteraction = new ObstacleFireballInteraction((Fireball)item2, (IObstacle)item1);
            ObstacleFireballInteraction.Update();
        }
        
    }
    public Rectangle getRectangle(List<IEntity> Entities, int index)
    {
        IEntity entity = Entities[index];
        Rectangle rectangle = new Rectangle();
        rectangle = entity.GetDestination();
        return rectangle;
    }
    public void Compare(List<IEntity> entities, List<IEntity> entitiesRemoved, Vector2 camera)
    {
        if (entities.Count > 1)
        {
            Rectangle firstEntity = entities[0].GetDestination();
            Rectangle secondEntity = entities[1].GetDestination();
            for (int i = 0; (i < entities.Count); i++)
            {
                firstEntity = entities[i].GetDestination();
                for (int j = i+1; (j < entities.Count); j++)
                {
                    secondEntity = entities[j].GetDestination();
                   
                    if (firstEntity.Intersects(secondEntity))
                    {
                        handleInteraction(entities, entitiesRemoved, i, j);
                    }
                    
                }
            }
        }

    }
}
