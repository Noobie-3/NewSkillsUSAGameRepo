using System;
using UnityEngine;

public class PointInTime
{

    public Quaternion rotation;
    public Vector3 position;
    public Vector3 Velocity;
    public float blendValueH;
    public float blendValueV;
    public float XLook;
    public float YLook;
    public int Points;


    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _Velocity, float BlendH, float BlendV) {

        position = _position;
        rotation = _rotation;
        Velocity = _Velocity;
        blendValueH = BlendH;
        blendValueV = BlendV;

    }

    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _Velocity, float BlendH, float BlendV, float xcam, float yCam, bool set_pos, bool set_vel, bool set_rotation, bool set_anim, bool Set_Cam) {

       if(set_pos == true)
            position = _position;
        if (set_vel == true)
            Velocity = _Velocity;
        if (set_rotation == true)
            rotation = _rotation;
        if (set_anim == true)
            blendValueH = BlendH;
            blendValueV = BlendV;


        if (Set_Cam == true )
        {
            XLook = xcam;
            YLook = yCam;

        }

    }
    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _Velocity, float BlendH, float BlendV, float xcam, float yCam, bool set_pos, bool set_vel, bool set_rotation, bool set_anim, bool Set_Cam, int Point) {

       if(set_pos == true)
            position = _position;
        if (set_vel == true)
            Velocity = _Velocity;
        if (set_rotation == true)
            rotation = _rotation;
        if (set_anim == true)
            blendValueH = BlendH;
            blendValueV = BlendV;

        try
        {
            Points = Point;
        }
        catch
        {
            print("Points Not Recorded");
        }

        
        if (Set_Cam == true )
        {
            XLook = xcam;
            YLook = yCam;

        }

    }

    private void print(string v)
    {
        throw new NotImplementedException();
    }

    public PointInTime(Vector3 _position, Quaternion _rotation, float xcam, float yCam, bool set_pos, bool set_rotation, bool Set_Cam) {

       if(set_pos == true)
            position = _position;
        if (set_rotation == true)
            rotation = _rotation;


        if (Set_Cam)
        {
            XLook = xcam;
            YLook = yCam;

        }
    }
    public PointInTime(Vector3 _position, Quaternion _rotation, float BlendH, float BlendV, float xcam, float yCam, bool set_pos, bool set_rotation, bool set_anim, bool Set_Cam) {

       if(set_pos == true)
            position = _position;
        if (set_rotation == true)
            rotation = _rotation;
        if (set_anim == true)
            blendValueH = BlendH;
            blendValueV = BlendV;

        if (Set_Cam)
        {
            XLook = xcam;
            YLook = yCam;

        }
    }
    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 Vel, bool set_pos, float xcam, float yCam, bool set_rotation, bool set_vel, bool Set_Cam) {

       if(set_pos == true)
            position = _position;
        if (set_rotation == true)
            rotation = _rotation;
        if (set_vel == true)
             Velocity = Vel;

        if (Set_Cam)
        {
            XLook = xcam;
            YLook = yCam;

        }
    }

}
