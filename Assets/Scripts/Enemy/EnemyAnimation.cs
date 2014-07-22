using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour {
    public float deadZone = 5f;

    private Transform player;
    private EnemySight enemySight;
    private NavMeshAgent nav;
    private Animator anim;
    private HashIDs hash;
    private AnimatorSetup animSetup;

    void Awake() {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        anim = GetComponent<Animator>();

        nav.updateRotation = false;
        animSetup = new AnimatorSetup(anim, hash);

        anim.SetLayerWeight(1, 1f);
        anim.SetLayerWeight(2, 1f);

        deadZone *= Mathf.Deg2Rad;
    }

    void NavAnimSetup() {
        float speed;
        float angle;
        if (enemySight.playerInSight) {
            speed = 0;
            angle = FindAngle(transform.forward, player.position - transform.position, transform.up);
        } else {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
            if (Mathf.Abs(angle) < deadZone) {
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0f;
            }
        }

        animSetup.Setup(speed, angle);
    }

    void Update() {
        NavAnimSetup();
    }

    void OnAnimatorMove() {
        nav.velocity = anim.deltaPosition / Time.deltaTime;
        transform.rotation = anim.rootRotation;
    }

    float FindAngle(Vector3 from, Vector3 to, Vector3 up) {
        if (to == Vector3.zero) {
            return 0f;
        }

        float angle = Vector3.Angle(from, to);
        Vector3 normal = Vector3.Cross(from, to);
        angle *= Mathf.Sign(Vector3.Dot(normal, up));
        angle *= Mathf.Deg2Rad;

        return angle;
    }
}
