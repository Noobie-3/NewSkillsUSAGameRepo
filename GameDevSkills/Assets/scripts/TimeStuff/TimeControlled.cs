using UnityEngine;
using System.Collections.Generic;

public class TimeControlled : MonoBehaviour {
    public float animationTime;
    public Rigidbody rb;
    public Animator animator;
    public bool isRewinding;
    public bool wasSteppingBack = false;//Controlls what is being recorded per object
    public bool RecordAnimation = false;
    public bool RecordTransform = false;
    public bool RecordVelocity = false;
    public bool RecordRotation = false;
    public bool record_all = false;
    TimeController TCr;
    public enum Type {
        Default,
        Player,
        Enenmy,
        NOAnimObject,

    };
    public Type type;
    private List<List<AnimatorStateInfo>> capturedAnimStates;

    // Capture the current animation states
    public List<List<AnimatorStateInfo>> CaptureAnimationStates()
    {
        capturedAnimStates = new List<List<AnimatorStateInfo>>();

        if (animator != null)
        {
            // Iterate through animator layers (0 is the default layer)
            for (int layerIndex = 0; layerIndex < animator.layerCount; layerIndex++)
            {
                List<AnimatorStateInfo> layerStates = new List<AnimatorStateInfo>();

                AnimatorControllerParameter[] parameters = animator.parameters;
                foreach (AnimatorControllerParameter parameter in parameters)
                {
                    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
                    layerStates.Add(stateInfo);
                }

                capturedAnimStates.Add(layerStates);
            }
        }

        return capturedAnimStates;
    }

    // Set the captured animation states
    public void SetAnimationStates(List<List<AnimatorStateInfo>> animStates)
    {
        if (animator != null && animStates != null && animStates.Count > 0)
        {
            // Rewind and set the animation states based on the captured states
            for (int layerIndex = 0; layerIndex < animStates.Count; layerIndex++)
            {
                List<AnimatorStateInfo> layerStates = animStates[layerIndex];

                for (int stateIndex = 0; stateIndex < layerStates.Count; stateIndex++)
                {
                    AnimatorStateInfo stateInfo = layerStates[stateIndex];
                    animator.Play(stateInfo.fullPathHash, layerIndex, stateInfo.normalizedTime);
                }
            }
        }
    }

    public virtual void TimeUpdate() {

    }
    private void Update()
    {
        if (record_all)
        {
            if (!(RecordAnimation | RecordTransform | RecordVelocity | RecordRotation))
            {
                RecordAnimation = true;
                RecordTransform = true;
                RecordVelocity = true;
                RecordRotation = true;
                print("HEllo");
            }
        }
    }
    private void Awake()
    {
        if (type == 0)
            {//Default timerewindable object
                if (record_all == false)
                {
                    record_all = true;
                }
            }
            if ((int)type == 1)
            {//Player that Will have everything rewinding
                if (record_all == false)
                {
                    record_all = true;
                }
            }
            if ((int)type == 2)
            { //Enenemies that have animations and should record everything
                if (record_all == false)
                {
                    record_all = true;
                }
            }
            if ((int)type == 3)
            {//NormalObjects without stuff like animations
                if (RecordAnimation == true)
                {
                    RecordAnimation = false;
                }

                if (RecordRotation == false)
                {
                    RecordRotation = true;
                }

                if (RecordVelocity == false)
                {
                    RecordVelocity = true;

                }

                if (RecordTransform == false)
                {

                    RecordTransform = true;
                }
            }
        
    }

}
