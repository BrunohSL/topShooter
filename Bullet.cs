using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private EnemyController enemyController;

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "player") {
            if (collision.gameObject.tag == "enemy") {
                collision.gameObject.GetComponent<EnemyController>().getHit(1);
                Destroy(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
    }
}
