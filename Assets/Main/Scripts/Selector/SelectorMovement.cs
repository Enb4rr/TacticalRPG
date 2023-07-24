using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class SelectorInputMovement : MonoBehaviour
{
    [SerializeField] private Tilemap walkableTilemap;

    private SelectorMovement selectorMovementControls;

    private void Awake()
    {
        selectorMovementControls = new SelectorMovement();
    }

    private void OnEnable()
    {
        selectorMovementControls?.Enable();
    }

    private void OnDisable()
    {
        selectorMovementControls?.Disable();
    }

    private void Start()
    {
        if (selectorMovementControls != null)
        {
            selectorMovementControls.Main.Movement.performed += ctx =>
            {
                if (ctx.interaction is HoldInteraction) MoveWhenHold(ctx.ReadValue<Vector2>());
                else if (ctx.interaction is PressInteraction) Move(ctx.ReadValue<Vector2>());
            };

            selectorMovementControls.Main.Movement.canceled += ctx =>
            {
                if (ctx.interaction is HoldInteraction) Move(ctx.ReadValue<Vector2>());
            };
        }
    }

    private void Move(Vector2 direction)
    {
        Debug.Log("moving while free");
        Debug.Log("This is my direction" + direction);
        if (CanMove(direction))
        {
            transform.position += (Vector3)direction;
        }
    }

    private void MoveWhenHold(Vector2 direction)
    {
        Debug.Log("moving while holding");
        Debug.Log("This is my direction" + direction);
        if (CanMove(direction))
        {
            transform.position += (Vector3)direction;
        }
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = walkableTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (!walkableTilemap.HasTile(gridPosition)) return false;
        return true;
    }
}
