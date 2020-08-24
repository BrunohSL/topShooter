using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHp = 1;
    private int actualHp;

    private GameObject tutorialControllerObj;
    // private TutorialController tutorialController;

    void Start()
    {
        actualHp = maxHp;
    }

    void Update()
    {
        // if (getHit) {
            // run enemy death animation
            // instantiate particle system
        // }
    }

    public void getHit(int damage) {
        this.actualHp -= damage;

        if (actualHp <= 0) {
            Destroy(gameObject);
        }
    }
}
