using UnityEngine;

public class ENEMY1 : MonoBehaviour
{
    [Header ("Unity Setup")]
    public ParticleSystem deathParticles;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Shot")) //Buraya tag ekleyeceðiz
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
