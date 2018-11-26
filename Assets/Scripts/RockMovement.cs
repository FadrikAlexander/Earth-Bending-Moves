using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMovement : MonoBehaviour {

    //this Class is for the Rock movement for the Rock Throw Move

    bool move = false;
    bool moveForward = false;
    float t = 0;

    float Speed;
    Vector3 EndPos;

	void Update () {

        //Start moving forward
        if (moveForward)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }

        //Start Moving upward to prepare to be throwen
        if (move)
        {
            //When the move is finished
            if (Vector2.Distance(transform.position, EndPos) < 0.1f)
            {
                //start forward moving
                moveForward = true;

                //stop upward movement
                move = false;
                t = 0;
            }
            else
            {
                //move the rock upword
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(this.gameObject.transform.position, EndPos, Mathf.SmoothStep(0f, 1f, t));
            }
        }
	}

    //Start the Rock movement
    //called from the EarthBendingController.cs
    public void StartRock(float Speed, Vector3 EndPos)
    {
        this.Speed = Speed;
        this.EndPos = EndPos;

        move = true;

        //Kill the Rock after 10 seconds
        Destroy(this.gameObject, 10f);
    }
}
