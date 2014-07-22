using UnityEngine;
using System.Collections;

public class LaserSwitchDeactivation : MonoBehaviour {
    public GameObject laser;
    public Material unlockedMat;

    private GameObject player;

    void Awake() {
        player = GameObject.FindGameObjectWithTag(Tags.player);
    }

    void OnTriggerStay(Collider coll) {
        if (coll.gameObject == player) {
            if (Input.GetButton("Switch")) {
                LaserDeactivation();
            }
        }
    }

    void LaserDeactivation() {
        laser.SetActive(false);
        Renderer screen = transform.Find("prop_switchUnit_screen").renderer;
        screen.material = unlockedMat;
        audio.Play();
    }
}
