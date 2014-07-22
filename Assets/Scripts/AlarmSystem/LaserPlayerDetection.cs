using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour {
    private GameObject player;
    private LastPlayerSighting lastPlayerSighting;

    void Awake() {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }

    void OnTriggerStay(Collider coll) {
        if (renderer.enabled) {
            if (coll.gameObject == player) {
                lastPlayerSighting.position = coll.transform.position;
            }
        }
    }
}
