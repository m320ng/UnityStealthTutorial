using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour {
    public bool requireKey;
    public AudioClip doorSwishClip;
    public AudioClip accessDenyClip;

    private Animator anim;
    private HashIDs hash;
    private GameObject player;
    private PlayerInventory playerInventory;
    private int count;

    void Awake() {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject == player) {
            if (requireKey) {
                if (playerInventory.hasKey) {
                    count++;
                } else {
                    audio.clip = accessDenyClip;
                    audio.Play();
                }
            } else {
                count++;
            }
        } else if (coll.gameObject.tag == Tags.enemy) {
            if (coll.collider is CapsuleCollider) {
                count++;
            }
        }
    }

    void OnTriggerExit(Collider coll) {
        if (coll.gameObject == player || (coll.gameObject.tag == Tags.enemy && coll is CapsuleCollider)) {
            count = Mathf.Max(0, count - 1);
        }
    }

    void Update() {
        anim.SetBool(hash.openBool, count > 0);

        if (anim.IsInTransition(0) && !audio.isPlaying) {
            audio.clip = doorSwishClip;
            audio.Play();
        }
    }
}
