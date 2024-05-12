using System.Collections.Generic;
using UnityEngine;

public class TriggerVolume : MonoBehaviour
{

    [SerializeField] private List<GameObject> triggerTarget;

    private ITriggerableObject trigger;

    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject triggerTarget in triggerTarget)
        {
            trigger = triggerTarget.GetComponent<ITriggerableObject>();
            if (trigger != null)
            {
                trigger.Triggered(other);
            }
        }
    }
}
