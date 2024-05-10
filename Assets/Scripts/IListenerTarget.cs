using System.Collections.Generic;
using UnityEngine;

public interface IListenerTarget
{

    public void NotifyListeners();
    public void RegisterListener(IListener _listener);
}
