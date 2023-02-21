using _Source.Actors.Player.Scripts;
using _Source.Coordinators;
using _Source.Coordinators.GameStates;
using _Source.Scenes.Game.Menus.Scripts;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Game game;
    private Inventory inventory;
    
    public Inventory Inventory
    {
        get => inventory;
        set => inventory = value;
    }

    public bool IsInventoryActive => inventory.IsActive;
    public bool IsPauseMenuActive => game.PauseMenu.IsActive;

    public void ToggleInventory()
    {
        inventory.Toggle();    
    }

    public void TogglePauseMenu()
    {
        game.PauseMenu.Toggle();
    }

}