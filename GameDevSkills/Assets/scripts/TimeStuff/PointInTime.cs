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

    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _Velocity, AnimatorStateInfo _AnimStates, bool set_pos, bool set_vel, bool set_rotation, bool set_anim) {

       if(set_pos == true)
            position = _position;
        if (set_vel == true)
            Velocity = _Velocity;
        if (set_rotation == true)
            rotation = _rotation;
        if (set_anim == true)
            AnimStates = _AnimStates;

    }
    public PointInTime(Vector3 _position, Quaternion _rotation, bool set_pos, bool set_rotation) {

       if(set_pos == true)
            position = _position;
        if (set_rotation == true)
            rotation = _rotation;


    }
}
