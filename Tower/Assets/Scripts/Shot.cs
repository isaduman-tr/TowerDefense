// 9.02.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class Shot : MonoBehaviour
{
    public float speed = 10f;
    private GameObject target;

    public void Initialize(GameObject target)
    {
        this.target = target;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                target.GetComponent<Enemy>().TakeDamage();
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}