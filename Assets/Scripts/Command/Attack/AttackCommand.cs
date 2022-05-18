using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackCommand : MonoBehaviour
{

    public abstract void Setup(LivingEntity entity);
    public abstract void Excute();
}
