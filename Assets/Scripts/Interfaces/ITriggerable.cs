using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerable
{
    public void Triggered(Collider collider);
}
