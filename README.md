Interactive Angry Birds
=======================
This project is a demonstrartion of the usage of Kinect Camera in controlling game mechanics.

Requirements
------------

- Kinect SDK
- Unity 2018
- kinect with MS-SDK Asset (Downloadable from *Unity Asset Store*)
- Windows Operating System

Kinnect SDK is only availabe for Windows OS.
This project is tested in Unity 2018, conflicts in previous versions may occur

How it works
------------
The asset gives us the a set of enums, each pointing to the corresponding body of the player in front of the Kinect Camera.
'''
public enum NuiSkeletonPositionIndex : int
    {
        HipCenter = 0,
        Spine = 1,
        ShoulderCenter = 2,
        Head = 3,
        ShoulderLeft = 4,
        ElbowLeft = 5,
        WristLeft = 6,
        HandLeft = 7,
        ShoulderRight = 8,
        ElbowRight = 9,
        WristRight = 10,
        HandRight = 11,
        HipLeft = 12,
        KneeLeft = 13,
        AnkleLeft = 14,
        FootLeft = 15,
        HipRight = 16,
        KneeRight = 17,
        AnkleRight = 18,
        FootRight = 19,
        Count = 20
    }
'''
<p align="center">
  <img src="/demonstration/AngryBirds-Kinnect.gif">
</p>

 of the game *Angry Birds Star Wars* with some changes in controllers 
