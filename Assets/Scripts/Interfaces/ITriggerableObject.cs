using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerableObject
{
    public void Triggered(Collider collider);
}
