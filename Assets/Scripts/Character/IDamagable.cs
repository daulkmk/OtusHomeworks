using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public interface IDamagable
    {
        void ApplyDamage(int damage, bool isPlayer);
    }
}