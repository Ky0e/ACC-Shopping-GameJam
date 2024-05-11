using UnityEngine;

public interface IListener
{
    public void Notify(GameObject _messenger);
    public void OnDestroy();


}
