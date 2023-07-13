using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceWallPowerUp : ActivatableArcadeKartPowerup
{
    [SerializeField]
    private Transform _wallPrefab;

    public List<Transform> _walls;

    private void Start()
    {
        _walls = new();
    }

    public override void OnPowerUp(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            base.OnPowerUp(context);
            if (IsCoolingDown) return;

            ApplyPowerUps(kart);


            Transform spawnedWall = Instantiate(_wallPrefab);
            spawnedWall.position = transform.TransformPoint(spawnedWall.localPosition);
            spawnedWall.rotation = transform.rotation;
            _walls.Add(spawnedWall);

        }

    }
}
