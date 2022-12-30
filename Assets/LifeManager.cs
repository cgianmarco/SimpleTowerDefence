using System;
using UnityEngine;

public class LifeManager : MonoBehaviour
{

    [SerializeField] int life = 0;

    public event Action<Unit> OnKilled;



    private void OnParticleCollision(GameObject other)
    {
        life -= 1;

        if (life < 1)
            Kill();

    }


    private void Kill()
    {
        Destroy(gameObject);
        OnKilled?.Invoke(this.GetComponent<Unit>());
    }
}
