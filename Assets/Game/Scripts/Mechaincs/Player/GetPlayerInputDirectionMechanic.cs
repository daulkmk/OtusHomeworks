using System.Collections;
using System.Collections.Generic;
using Atomic.Elements;
using UnityEngine;

public class GetPlayerInputDirectionMechanic
{
    public IAtomicValueObservable<Vector2> Direction => _direction;

    private AtomicVariable<Vector2> _direction = new();

    public void Update()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.D))
            direction.x += 1;
        if (Input.GetKey(KeyCode.A))
            direction.x -= 1;
        
        if (Input.GetKey(KeyCode.W))
            direction.y += 1;
        if (Input.GetKey(KeyCode.S))
            direction.y -= 1;

        _direction.Value = direction;
    }
}
