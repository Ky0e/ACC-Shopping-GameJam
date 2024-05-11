using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkStick : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.ToString());
    }
}
