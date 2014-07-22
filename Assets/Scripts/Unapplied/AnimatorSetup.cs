using UnityEngine;
using System.Collections;

public class AnimatorSetup {
    public float speedDampTime = 0.1f;
    public float angularSpeedDampTime = 0.7f;
    public float angleRensponseTime = 0.6f;

    private Animator anim;
    private HashIDs hash;

    public AnimatorSetup(Animator anim, HashIDs hash) {
        this.anim = anim;
        this.hash = hash;
    }

    public void Setup(float speed, float angle) {
        float angleSpeed = angle / angleRensponseTime;

        anim.SetFloat(hash.speedFloat, speed, speedDampTime, Time.deltaTime);
        anim.SetFloat(hash.angularSpeedFloat, angleSpeed, angularSpeedDampTime, Time.deltaTime);
    }
}
