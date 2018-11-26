using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour {

    //ground pieces Code for changing the scale and the rotation

    Vector3 startScale;
    Vector3 endScale;

    bool isBending = false;
    bool isBendDown = false;
    bool StartedBending=false;

    float t = 0;
    float BendingSpeed;

    //save the starting scale
	void Awake () {
        startScale = transform.localScale;
	}
	
    //the bending movement
	void Update () {
        if (isBending)
        {
            //check if size reached the end scale to stop the bending
            if (Mathf.Abs( (endScale - transform.localScale).y ) < 0.01f)
            {
                isBending = false;
                t = 0;

                StartedBending = false;

                //if the rock is allowed to stay up or bend back to its started scale
                if (isBendDown)
                    BendBack();
            }
            else
            {
                t += Time.deltaTime * BendingSpeed;
                gameObject.transform.localScale = Vector3.Lerp(transform.localScale, endScale, Mathf.SmoothStep(0f, 1f, t));
            }
        }
	}

    //bend the rocks up depending on its distance from the player
    public void BendUp(Vector3 Player, float BendingSpeed)
    {
        float newScale = Vector3.Distance(Player, transform.position) / 3f;

        endScale = startScale;
        endScale.y += newScale;
        this.BendingSpeed = BendingSpeed;

        isBendDown = true;
        StartCoroutine(StartBending(newScale / 5, Mathf.Floor(newScale), Player));
    }

    //bend the rock up depending on a power given from the player
    public void BendUp(Vector3 Player, float Power, Vector3 Pos, float BendingSpeed)
    {
        float newScale = Vector3.Distance(Pos, transform.position) * 2;

        endScale = startScale;
        endScale.y += Power - newScale;
        this.BendingSpeed = BendingSpeed;

        isBendDown = false;
        if (!StartedBending)
        {
            StartedBending = true;
            StartCoroutine(StartBending(0, 5f, Player));
        }
    }

    IEnumerator StartBending(float Time, float Speed, Vector3 P)
    {
        yield return new WaitForSeconds(Time);

        BendingSpeed = Speed;

        transform.LookAt(P);

        isBending = true;
    }

    //get the rock back to its started scale
    public void BendBack()
    {
        endScale = startScale;
        isBending = true;
        isBendDown = false ;
    }
}
