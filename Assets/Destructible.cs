using System;
using UnityEngine;



public class Unit : MonoBehaviour 
{
    public Action<Unit> onDestroyed;


    private void OnDestroy()
    {
        onDestroyed?.Invoke(this);
    }

    public virtual void OnReachedDestination() { }
}


