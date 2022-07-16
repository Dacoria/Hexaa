
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    private Vector3 endposition;
    public GameObject Explosion;

    private void Start()
    {
        endposition = transform.position + new Vector3(0, -100, 0);
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endposition, 4 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var explosion = Instantiate(Explosion, collision.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
