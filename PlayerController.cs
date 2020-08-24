using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D player;
    public Camera cam;
    public Text text;

    Vector2 movement;
    Vector2 mousePosition;
    Chest chest;

    public Transform firePoint;
    public GameObject bulletPrefab;
    private GameObject chestObj;

    private bool canShoot = false;
    private bool canCollect = false;
    public float bulletForce = 20f;
    public float moveSpeed = 5f;

    void Start() {
        chestObj = GameObject.FindGameObjectWithTag("chest");
        chest = chestObj.GetComponent<Chest>();
        text.enabled = false;
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        if (canCollect) {
            text.enabled = true;
        } else {
            text.enabled = false;
        }

        if (Input.GetButtonDown("Fire1") && canShoot) {
            Shoot();
        }

        if (canCollect && Input.GetKeyDown(KeyCode.Space)) {
            getFirstWeapon();
            // Show get weapon message + How to shoot tutorial
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "chest") {
            canCollect = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        canCollect = false;
    }

    void getFirstWeapon() {
        chest.setCollected(true);
        this.canShoot = true;
    }

    void FixedUpdate() {
        player.MovePosition(player.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePosition - player.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        player.rotation = angle;
    }

    void Shoot() {
        Vector2 shootingDirection = mousePosition - player.position;
        shootingDirection.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * bulletForce;
        bullet.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
    }
}
