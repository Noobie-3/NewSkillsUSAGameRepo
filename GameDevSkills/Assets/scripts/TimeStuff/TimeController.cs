using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {
    public struct RecordedData {
        public Vector3 pos;
        public Vector3 vel;
        public Quaternion rot;
        public float animationTime;
        public List<List<AnimatorStateInfo>> animStates;

    }

    RecordedData[,] recordedData;//Record vars
    int recordMax = 100000;
    public int recordCount;
    public int recordIndex;
    [SerializeField]
    public bool wasSteppingBack;


    public TimeControlled[] TimeObjects;
    public Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();


    private void Awake() {
        TimeObjects = GameObject.FindObjectsOfType<TimeControlled>();
        recordedData = new RecordedData[TimeObjects.Length, recordMax];

        
    }

    void Update() {

        bool pause = Input.GetKey(KeyCode.UpArrow);
        bool StepBack = Input.GetKey(KeyCode.LeftArrow);
        bool StepForward = Input.GetKey(KeyCode.RightArrow);

        if(TimeObjects.Length < GameObject.FindObjectsOfType<TimeControlled>().Length)
        {
            TimeObjects = FindObjectsOfType<TimeControlled>();
            print("Test");
        }

        if (pause & !StepBack) {
            for(int ObjectIndex = 0; ObjectIndex < TimeObjects.Length; ObjectIndex++) {
                TimeControlled TimeObject = TimeObjects[ObjectIndex];
                TimeObject.rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                TimeObject.isRewinding = true;
                TimeObject.animator.speed = 0;
            }
        }
        else if(!pause) {
            for(int ObjectIndex = 0; ObjectIndex < TimeObjects.Length; ObjectIndex++) {
                TimeControlled TimeObject = TimeObjects[ObjectIndex];
                TimeObject.rb.constraints &= ~(RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY);

                TimeObject.animator.speed = 1;

            }
        }

        if(StepBack && recordIndex + 1 > 0) {
            wasSteppingBack = true;
            if(recordIndex > 0) {
                recordIndex--;
            }
            for(int ObjectIndex = 0; ObjectIndex < TimeObjects.Length; ObjectIndex++) {
                TimeControlled TimeObject = TimeObjects[ObjectIndex];
                RecordedData Data = recordedData[ObjectIndex, recordIndex];
                if(TimeObject.RecordTransform) {
                    TimeObject.transform.position = Data.pos;
                }
                if(TimeObject.RecordVelocity) {
                    TimeObject.rb.velocity = Data.vel;
                }

                if(TimeObject.RecordRotation) {
                    TimeObject.transform.rotation = Data.rot;
                }
                if(TimeObject.RecordAnimation) {
                    // Set animation states using the captured data
                    TimeObject.SetAnimationStates(Data.animStates);
                }








                TimeObject.isRewinding = false;

                // Set animation states using the captured data
                TimeObject.SetAnimationStates(Data.animStates);
            }
        }
    

        else if (pause && StepForward)
        {
            wasSteppingBack = true;
            if (recordIndex < recordCount - 1)
            {
                recordIndex++;

                for (int objectIndex = 0; objectIndex < TimeObjects.Length; objectIndex++)
                {
                    TimeControlled timeObject = TimeObjects[objectIndex];
                    RecordedData data = recordedData[objectIndex, recordIndex];
                    timeObject.transform.position = data.pos;
                    timeObject.rb.velocity = data.vel;
                    timeObject.transform.rotation = data.rot;
                    timeObject.animationTime = data.animationTime;

                    // Set animation states using the captured data
                    timeObject.SetAnimationStates(data.animStates);
                }
            }
        }
        else if (!pause && !StepBack)
        {
            if (wasSteppingBack)
            {
                recordCount = recordIndex;
                wasSteppingBack = false;
            }

            for (int ObjectIndex = 0; ObjectIndex < TimeObjects.Length; ObjectIndex++)
            {
                TimeControlled TimeObject = TimeObjects[ObjectIndex];
                RecordedData Data = new RecordedData();
                Data.pos = TimeObject.transform.position;
                Data.vel = TimeObject.rb.velocity;
                Data.rot = TimeObject.transform.rotation;
                Data.animationTime = TimeObject.animationTime;

                // Capture animation states and store them in the RecordedData
                Data.animStates = TimeObject.CaptureAnimationStates();

                recordedData[ObjectIndex, recordCount] = Data;
                TimeObject.isRewinding = false;
            }
            recordCount++;
            recordIndex = recordCount;
            foreach (TimeControlled TimeObject in TimeObjects)
            {
                TimeObject.TimeUpdate();
            }
        }
    }

}
