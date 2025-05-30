using System;
public interface IBlockObject
{
    void drawBlock(int x, int y);
    void destroyedBlock();
    void collectedState();
    void upBlock()
    {

    }



}
public class BlockObject : IBlockObject
{
    public void drawBlock(int x, int y)
    {


    }
    public void collectedState()
    {


    }
    public void destroyedBlock()
    {


    }
    public void upBlock()
    {

    }

}
