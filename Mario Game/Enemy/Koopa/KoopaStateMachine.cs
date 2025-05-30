using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;


public class KoopaStateMachine
{
	private enum KoopaState {Left, Right, StompedLeft, StompedRight, StompedTwiceLeft, StompedTwiceRight, Flipped, Start};
	private KoopaState _currentState = KoopaState.Start;
    private int done;
    private KoopaSprites _sprite;
	private Boolean _isMovingShell = false;
	private Boolean isDead = false;
    private IPlayer mario;
	private IPlayer luigi;
    public KoopaStateMachine(int posX, int posY, IPlayer mario, IPlayer luigi)
	{
		_sprite = new KoopaSprites(posX, posY);
        this.mario = mario;
		this.luigi = luigi;
    }

	public Boolean IsDead()
	{
		return isDead;
	}
	public Boolean IsMovingShell()
	{
		return _isMovingShell;
	}

    public void changeDirection()
	{
		switch (_currentState)
		{
			case KoopaState.Left:
				_currentState = KoopaState.Right;
				break;
			case KoopaState.Right:
				_currentState = KoopaState.Left;
				break;
			case KoopaState.StompedTwiceLeft:
				_currentState = KoopaState.StompedTwiceRight;
				break;
			case KoopaState.StompedTwiceRight:
				_currentState = KoopaState.StompedTwiceLeft;
				break;
        }
	}

	public void beStomped()
	{
		switch (_currentState)
		{
			case KoopaState.Left:
				_currentState = KoopaState.StompedLeft;
                _isMovingShell = false;
                break;
			case KoopaState.Right:
				_currentState = KoopaState.StompedRight;
                _isMovingShell = false;
                break;
			case KoopaState.StompedRight:
				_currentState = KoopaState.StompedTwiceRight;
				_isMovingShell = true;
				break;
			case KoopaState.StompedLeft:
				_currentState = KoopaState.StompedTwiceLeft;
                _isMovingShell = true;
                break;
			case KoopaState.StompedTwiceRight:
				_currentState = KoopaState.StompedRight;
                _isMovingShell = false;
                break;
			case KoopaState.StompedTwiceLeft:
				_currentState = KoopaState.StompedLeft;
                _isMovingShell = false;
                break;
		}

	}

	public void beFlipped()
	{
		if (_currentState != KoopaState.Flipped)
		{
			_currentState = KoopaState.Flipped;
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
	public void setGroundPosition(int x)
	{
        if (isDead)
        {
            x = 1000;
        }
        this._sprite.setGroundPosition(x);
	}
    public void Update()
	{
        Rectangle mHold = mario.GetDestination();
        Rectangle lHold = luigi.GetDestination();
        Rectangle koopaRec = _sprite.GetDestination();
        if (((koopaRec.X - mHold.X) > 0) && ((koopaRec.X - mHold.X) < 400) && (_currentState == KoopaState.Start))
        {
            _currentState = KoopaState.Left;
        }
        if (((koopaRec.X - lHold.X) > 0) && ((koopaRec.X - lHold.X) < 400) && (_currentState == KoopaState.Start))
        {
            _currentState = KoopaState.Left;
        }
        if (_currentState != KoopaState.Start)
        {
            SetIsOnGround(false);
        }
        _sprite.ApplyGravity();

        switch (_currentState)
		{
            case KoopaState.Left:
				_sprite.LeftLogic();
				break;
			case KoopaState.Right:
				_sprite.RightLogic();
				break;
			case KoopaState.StompedLeft:
				done = _sprite.StompedLogic();
				if (done == 1)
				{
					_currentState = KoopaState.Left;
				}
				break;
            case KoopaState.StompedRight:
                done = _sprite.StompedLogic();
                if (done == 1)
                {
                    _currentState = KoopaState.Right;
                }
                break;
			case KoopaState.StompedTwiceLeft:
				_sprite.StompedTwiceLogicLeft();
				break;
			case KoopaState.StompedTwiceRight:
				_sprite.StompedTwiceLogicRight();
				break;
            case KoopaState.Flipped:
				_sprite.FlippedLogic();
				break;
            case KoopaState.Start:
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
