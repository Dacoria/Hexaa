
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    private Vector3 endposition;
    public GameObject Explosion;
    public PlayerScript Player;
    public Hex HexTarget;

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
        if(collision.gameObject.GetComponent<Hex>() != null)
        {
            var explosion = Instantiate(Explosion, collision.transform.position, Quaternion.identity);
            ActionEvents.PlayerRocketHit?.Invoke(Player, HexTarget);
            Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            var explosion = Instantiate(Explosion, HexTarget.transform.position, Quaternion.identity);

            ActionEvents.PlayerRocketHit?.Invoke(Player, HexTarget);
            Destroy(gameObject);
        }
    }
}
