using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool collected = false;
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (collected == true) {
            animator.SetBool("open", true);
        }
    }

    public bool getCollected() {
        return this.collected;
    }

    public void setCollected(bool collected) {
        this.collected = collected;
    }
}
