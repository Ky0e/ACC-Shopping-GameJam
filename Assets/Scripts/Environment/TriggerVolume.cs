using UnityEngine;

public class TriggerVolume : MonoBehaviour
{

    [SerializeField] private GameObject triggerTarget;

    private ITriggerable trigger;

    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerTarget.GetComponent<ITriggerable>().Triggered(other);
    }

}
