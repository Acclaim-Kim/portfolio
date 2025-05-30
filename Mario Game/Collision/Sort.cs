using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;

public class Sort
{
    public void Remove(List<IEntity> objects, IEntity objToRemove)
    {
        if (!objects.Remove(objToRemove))
        {
            //Error: unable to remove object from collision detection list.
            string objName = objToRemove.ToString();
            Environment.Exit(50);
        }
    }
    public List<IEntity> RemovePast(List<IEntity> objects, Rectangle camera)
    {
        if (objects.Count != 0)
        {
            while ((objects.Count > 0) && (objects[0].GetDestination().X < camera.X))
            {
                objects.RemoveAt(0);
            }
        }
        return objects;
    }


    private void Sorting(List<IEntity> objectsSorting, int size, IEntity insert)
    {
        objectsSorting[size-1] = insert;
        int i = size-1;
        while ((i > 0) && (objectsSorting[i-1].GetDestination().X > objectsSorting[i].GetDestination().X))
        {
            IEntity store = objectsSorting[i-1];
            objectsSorting[i-1] = objectsSorting[i];
            objectsSorting[i] = store;
            i--;
        }
    }
    public List<IEntity> SortList(List<IEntity> objects, int size, List<IEntity> objectsSorting)
    {
        if (size == 1)
        {
            objectsSorting[0] = objects[0];
        }
        if (size > 1)
        {
            SortList(objects, size - 1, objectsSorting);
            Sorting(objectsSorting, size, objects[size-1]);
        }
        return objectsSorting;
    }

    public void clearList(List<Object> objects)
    {
        objects.Clear();

    }
}
