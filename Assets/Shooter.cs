using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] ParticleSystem projectiles;


    

    public void StartShooting()
    {
        if (!projectiles.isPlaying)
            projectiles.Play();
    }

    public void StopShooting()
    {
        if (projectiles.isPlaying)
            projectiles.Stop();
    }

    
}
