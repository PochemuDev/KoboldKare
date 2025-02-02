using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements.Experimental;
using UnityEngine;

public class OrbitCameraFPSHeadPivot : OrbitCameraLerpTrackPivot {
    private Vector3 eyeOffset;
    public override void Initialize(Animator targetAnimator, HumanBodyBones bone, float lerpTrackSpeed) {
        base.Initialize(targetAnimator, bone, lerpTrackSpeed);
        Vector3 eyeCenter = (targetAnimator.GetBoneTransform(HumanBodyBones.LeftEye).position + targetAnimator.GetBoneTransform(HumanBodyBones.RightEye).position) * 0.5f;
        eyeOffset = targetTransform.InverseTransformPoint(eyeCenter);
    }

    protected override void LateUpdate() {
        Vector3 a = transform.localPosition;
        Vector3 b = transform.parent.InverseTransformPoint(targetTransform.TransformPoint(eyeOffset));
        Vector3 correction = b - a;
        float diff = correction.magnitude;
        transform.localPosition = Vector3.MoveTowards(a, a+correction, (0.1f+diff)*Time.deltaTime*lerpTrackSpeed);
    }

    public override float GetDistanceFromPivot(Quaternion camRotation) {
        //if (ragdoller.ragdolled) {
            return -0.1f;
        //}
        //Vector3 forward = camRotation * Vector3.forward;
        //float t = Mathf.Clamp01(forward.y + 1f);
        //return Mathf.Lerp(-0.1f, -0.2f, Easing.OutCubic(t));
    }
}
