using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FindCommand : MonoBehaviour
{
    public abstract void Setup(LivingEntity entity);
    public abstract void Excute();
}