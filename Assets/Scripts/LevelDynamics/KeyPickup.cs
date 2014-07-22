using UnityEngine;
using System.Collections;

public class KeyPickup : MonoBehaviour {
    public AudioClip keyGrab;

    private GameObject player;
    private PlayerInventory playerInventory;

    void Awake() {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerInventory = player.GetComponent<PlayerInventory>();

    }

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject == player) {
            AudioSource.PlayClipAtPoint(keyGrab, transform.position); 
            playerInventory.hasKey = true;
            Destroy(gameObject);
        }
    }
}
