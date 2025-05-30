using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pixel_Plumbers_Fall_2024;

public class LuigiControlCenter
{
    private Luigi luigi;
    private PlayerMovementController playerMovementController;

    public LuigiControlCenter(Luigi luigi, PlayerMovementController playerMovementController)
    {
        this.luigi = luigi;
        this.playerMovementController = playerMovementController;

        InitializeCommands();
    }

    private void InitializeCommands()
    {
        IPlayerCommand luigiMoveRight = new LuigiMoveRightCommand(luigi);
        IPlayerCommand luigiMoveLeft = new LuigiMoveLeftCommand(luigi);
        IPlayerCommand luigiJumpCommand = new LuigiJumpCommand(luigi);
        IPlayerCommand luigiCrouchCommand = new LuigiCrouchCommand(luigi);

        IPlayerCommand luigiFireballCommand = new LuigiFireballCommand(luigi);
        IPlayerCommand luigiPowerUpCommand = new LuigiPowerUpCommand(luigi);
        IPlayerCommand luigiTakeDamageCommand = new LuigiTakeDamageCommand(luigi);
        IPlayerCommand luigiStarCommand = new LuigiStarCommand(luigi);

        playerMovementController.addCommand(Keys.Right, luigiMoveRight);
        playerMovementController.addCommand(Keys.Left, luigiMoveLeft);
        playerMovementController.addCommand(Keys.Up, luigiJumpCommand);
        playerMovementController.addCommand(Keys.Down, luigiCrouchCommand);
        playerMovementController.addCommand(Keys.RightShift, luigiFireballCommand);
        playerMovementController.addCommand(Keys.D4, luigiPowerUpCommand);
        playerMovementController.addCommand(Keys.D5, luigiTakeDamageCommand);
        playerMovementController.addCommand(Keys.D6, luigiStarCommand);

    }
}
