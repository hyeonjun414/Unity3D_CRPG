using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillCommand : MonoBehaviour
{
    public abstract void Setup(LivingEntity entity);
    public abstract void Excute();
}
