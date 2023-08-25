using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShellController : MonoBehaviour
{
    private float MaxLifeTime = 1.5f;
    private int Damage = 50;

    private LayerMask EnemyLMask = 7;
    private LayerMask PlayerLMask = 3;

    public enum OriginObject { player, enemy };
    public OriginObject origin;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(MaxLifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Create Interface for the enemies that each different kind of enemy will implement. Change BasicAIScript to search
        // for said interface(Down the line, not needed right now)
        if (other.gameObject.layer == 6 || other.gameObject.layer == 8)
        {
            Destroy(gameObject);
            return;
        }
        else if (other.gameObject.layer == 0)
        {
            Debug.Log("We hit the player's collider");
            return;
        }

        if (origin == OriginObject.player)
        {
            BasicAIController ai = other.GetComponent<BasicAIController>();

            if (!ai)
                Debug.Log("object not found");

            if (ai.gameObject.layer == EnemyLMask)
            {
                ai.Damage(Damage);
            }
        }
        else if (origin == OriginObject.enemy)
        {
            PlayerController pc = other.GetComponent<PlayerController>();

            if (!pc)
                Debug.Log("object not found(player controller)");

            if (pc.gameObject.layer == PlayerLMask)
            {
                pc.Damage(Damage);
            }
        }

        Destroy(gameObject);
    }

    public void setOrigin(int other)
    {
        if (other == 0)
            origin = OriginObject.enemy;
        else if (other == 1)
            origin = OriginObject.player;

        Debug.Log(origin);
    }
}
