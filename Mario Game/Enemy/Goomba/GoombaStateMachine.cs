using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;

public class GoombaStateMachine
{
	public enum GoombaState {Left, Right, Stomped, Flipped, Start};
	private GoombaState _currentState = GoombaState.Start;
	private GoombaSprites _sprite;
	private Boolean isDead = false;
	private IPlayer mario;
    private IPlayer luigi;

    public GoombaStateMachine(int posX, int posY, IPlayer mario, IPlayer luigi)
	{
		_sprite = new GoombaSprites(posX, posY);
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
			case GoombaState.Left:
				_currentState = GoombaState.Right;
				break;
			case GoombaState.Right:
				_currentState = GoombaState.Left;
				break;
		}
	}

	public void beStomped()
	{
        if (_currentState != GoombaState.Stomped)
        {
            _currentState = GoombaState.Stomped;
        }
		isDead = true;
    }

	public void beFlipped()
	{
		if (_currentState != GoombaState.Flipped)
		{
			_currentState = GoombaState.Flipped;
		}
		isDead = true;
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
        Rectangle goombaRec = _sprite.GetDestination();
        if ((((goombaRec.X - mHold.X) > 0) && ((goombaRec.X - mHold.X) < 400) && (_currentState == GoombaState.Start))){
            _currentState = GoombaState.Left;
        }
        if (((goombaRec.X - lHold.X) > 0) && ((goombaRec.X - lHold.X) < 400) && (_currentState == GoombaState.Start))
        {
            _currentState = GoombaState.Left;
        }

        _sprite.ApplyGravity();
        if (_currentState != GoombaState.Start)
		{
			SetIsOnGround(false);
        }

        switch (_currentState)
		{
            case GoombaState.Left:
				_sprite.LeftLogic();
				break;
			case GoombaState.Right:
				_sprite.RightLogic();
				break;
			case GoombaState.Stomped:
				_sprite.StompedLogic();
				break;
			case GoombaState.Flipped:
				_sprite.FlippedLogic(0);
				break;
			case GoombaState.Start:
				_sprite.FlippedLogic(1);
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
