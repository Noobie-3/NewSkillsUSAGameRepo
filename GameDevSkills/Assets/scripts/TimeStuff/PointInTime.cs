using UnityEngine;

public class PointInTime
{

    public Quaternion rotation;
    public Vector3 position;
    public Vector3 Velocity;
    public AnimatorStateInfo AnimStates;


    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _Velocity, AnimatorStateInfo _AnimStates) {

        position = _position;
        rotation = _rotation;
        Velocity = _Velocity;
        AnimStates = _AnimStates;

    }

}
