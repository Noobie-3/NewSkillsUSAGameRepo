using UnityEngine;

public class PointInTime
{

    public Quaternion rotation;
    public Vector3 position;
    public Vector3 Velocity;
    public float blendValueH;
    public float blendValueV;


    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _Velocity, float BlendH, float BlendV) {

        position = _position;
        rotation = _rotation;
        Velocity = _Velocity;
        blendValueH = BlendH;
        blendValueV = BlendV;

    }

    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _Velocity, float BlendH, float BlendV, bool set_pos, bool set_vel, bool set_rotation, bool set_anim) {

       if(set_pos == true)
            position = _position;
        if (set_vel == true)
            Velocity = _Velocity;
        if (set_rotation == true)
            rotation = _rotation;
        if (set_anim == true)
            blendValueH = BlendH;
            blendValueV = BlendV;

    }
    public PointInTime(Vector3 _position, Quaternion _rotation, bool set_pos, bool set_rotation) {

       if(set_pos == true)
            position = _position;
        if (set_rotation == true)
            rotation = _rotation;


    }
    public PointInTime(Vector3 _position, Quaternion _rotation, float BlendH, float BlendV, bool set_pos, bool set_rotation, bool set_anim ) {

       if(set_pos == true)
            position = _position;
        if (set_rotation == true)
            rotation = _rotation;
        if (set_anim == true)
            blendValueH = BlendH;
            blendValueV = BlendV;

    }
    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 Vel, bool set_pos, bool set_rotation, bool set_vel) {

       if(set_pos == true)
            position = _position;
        if (set_rotation == true)
            rotation = _rotation;
        if (set_vel == true)
             Velocity = Vel;

    }

}
