
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
            PlayerRocketHitTile(Player, HexTarget); // of een player wordt geraakt wordt later bepaald
            Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            var explosion = Instantiate(Explosion, HexTarget.transform.position, Quaternion.identity);

            PlayerRocketHitTile(Player, HexTarget);
            Destroy(gameObject);
        }
    }

    private void PlayerRocketHitTile(PlayerScript playerWhoShotRocket, Hex hexTileHit)
    {
        if(!Netw.IsMyTurn())
        {
            // logica bij curr player
            return;
        }

        var allPlayers = NetworkHelper.instance.GetPlayers();
        PlayerScript playerHit = null;

        foreach (var player in allPlayers)
        {
            if (player.CurrentHexTile == hexTileHit)
            {
                // voor nu -> altijd dood met 1 raket-hit --> KILL
                player.gameObject.SetActive(false);
                playerHit = player;
            }
        }

        NetworkActionEvents.instance.PlayerRocketHitTile(playerWhoShotRocket, hexTileHit, playerHit, playerHit != null);       
    }
}
