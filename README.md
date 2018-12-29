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
The asset gives us the a set of enums, each pointing to the corresponding body part of the player in front of the Kinect Camera. The code below is part of the script found in */Assets/Kinect/KinectScripts/KinectWrapper.cs*.
```C# 
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
```
In this project, we needed two anchor points for implementing the action of throwing a bird. One for the head of the slingshot, and another for simulating the hand stretching the rubber band to it.
As for these anchors, we used the head and the left hand of the player; *Head* being the head of the slingshot, and *HandLeft* as the controller used for aiming the birds, and setting the throw power.
We also used *HandRight* as the trigger for shooting the birds; when it enters a specific area in the right half of the game screen, the shoot command is sent.

Below is a demonstration of the result.
<p align="center">
  <img src="/demonstration/AngryBirds-Kinnect.gif">
</p>

**Important Note:** This game portrays characters and environments of the game *Angry Birds Star Wars* by *Rovio*, with some changes in controllers. This project is only used for educational purposes, it is not, and will not be used in any other way. Sprites of the game were used only to facilitate the developmnent process.
