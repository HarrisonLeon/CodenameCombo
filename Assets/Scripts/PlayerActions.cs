using UnityEngine;
using System.Collections;

using InControl;

public class PlayerActions : PlayerActionSet
{
	public PlayerAction MoveLeft;
	public PlayerAction MoveRight;
	public PlayerAction MoveUp;
	public PlayerAction MoveDown;
	public PlayerTwoAxisAction Move;

	public PlayerAction AttackLeft;
	public PlayerAction AttackRight;
	public PlayerAction AttackUp;
	public PlayerAction AttackDown;

	public PlayerActions()
	{
		MoveLeft = CreatePlayerAction("Move Left");
		MoveRight = CreatePlayerAction("Move Right");
		MoveUp = CreatePlayerAction("Move Up");
		MoveDown = CreatePlayerAction("Move Down");
		Move = CreateTwoAxisPlayerAction(MoveLeft, MoveRight, MoveDown, MoveUp);

		AttackLeft = CreatePlayerAction("Punch Left");
		AttackRight = CreatePlayerAction("Punch Right");
		AttackUp = CreatePlayerAction("Punch Up");
		AttackDown = CreatePlayerAction("Kick");
	}
}
