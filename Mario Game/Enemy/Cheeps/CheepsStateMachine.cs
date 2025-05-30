using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;

public class CheepsStateMachine
{
	private enum CheepsState { Left, Right, Flipped, Start};
	private CheepsState _currentState = CheepsState.Start;
	private CheepsSprites _sprite;
	private Boolean isDead = false;
    private IPlayer mario;
    private IPlayer luigi;

    public CheepsStateMachine(int color, int posX, int posY, IPlayer mario, IPlayer luigi)
	{
		_sprite = new CheepsSprites(color, posX, posY);
		this.mario = mario;
		this.luigi = luigi;
	}
    public Boolean IsDead()
    {
        return isDead;
    }
    public void changeDirection()
	{
		switch (_currentState)
		{
			case CheepsState.Left:
				_currentState = CheepsState.Right;
				break;
			case CheepsState.Right:
				_currentState = CheepsState.Left;
				break;
		}
	}

	public void beStomped()
	{

    }

	public void beFlipped()
	{
		if (_currentState != CheepsState.Flipped)
		{
			_currentState = CheepsState.Flipped;
		}
	}

    public bool GetIsOnGround()
    {
        return _sprite.GetIsOnGround();
    }
    public void SetIsOnGround(bool val)
    {
        _sprite.SetIsOnGround(val);
    }
    public void SetGroundPosition(float x)
    {
        if (isDead)
        {
            x = 1000;
        }
        _sprite.SetGroundPosition(x);
    }
    public void Update()
	{
        Rectangle mHold = mario.GetDestination();
        Rectangle lHold = luigi.GetDestination();
        Rectangle cheepRec = _sprite.GetDestination();
        if (((cheepRec.X - mHold.X) > 0) && ((cheepRec.X - mHold.X) < 400) && (_currentState == CheepsState.Start))
        {
            _currentState = CheepsState.Left;
        }
        if (((cheepRec.X - lHold.X) > 0) && ((cheepRec.X - lHold.X) < 400) && (_currentState == CheepsState.Start))
        {
            _currentState = CheepsState.Left;
        }
        if (isDead)
        {
            _sprite.ApplyGravity();
        }

        switch (_currentState)
		{
            case CheepsState.Left:
				_sprite.LeftLogic();
				break;
			case CheepsState.Right:
				_sprite.RightLogic();
				break;
			case CheepsState.Flipped:
				_sprite.FlippedLogic();
				break;
            case CheepsState.Start:
                _sprite.StartLogic();
                break;
        }

    }
	public Rectangle GetDestination()
	{
		return _sprite.GetDestination();

    }
	public void Draw(SpriteBatch sb, Texture2D Texture)
	{
		_sprite.Draw(sb, Texture);
	}
}
