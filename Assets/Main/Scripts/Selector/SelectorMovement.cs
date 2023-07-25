using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Unity.VisualScripting.InputSystem;

public class SelectorInputMovement : MonoBehaviour
{
    [SerializeField] private Tilemap walkableTilemap;
    [SerializeField] private float waitTime;

    private SelectorMovement selectorMovementControls;
    private bool isWithInput;
    private bool isMoving;

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
            selectorMovementControls.Main.Movement.started += ctx =>
            {
                isWithInput = true;
                if(!isMoving) StartCoroutine(Move(ctx));
            };

            selectorMovementControls.Main.Movement.canceled += ctx =>
            {
                isWithInput = false;
            };
        }
    }

    private IEnumerator Move(InputAction.CallbackContext callbackContext)
    {
        while (isWithInput)
        {
            isMoving = true;
            var direction = callbackContext.ReadValue<Vector2>();
            if (CanMove(direction))
            {
                transform.position += (Vector3)direction;
            }

            yield return new WaitForSeconds(waitTime);
            isMoving = false;
        }
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = walkableTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (!walkableTilemap.HasTile(gridPosition)) return false;
        return true;
    }
}
