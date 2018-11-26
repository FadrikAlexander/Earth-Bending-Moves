using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMover : MonoBehaviour {

    //Earth Mover that will trigger the column attack and move he ground from the player to the column attack position

    //Movement variable
    bool move = false;
    float t = 0;

    Vector3 StartPos;
    Vector3 EndPos;
    Vector3 LookPos;

    float Speed;
    float ColumnRange;

    //Movement
    void Update()
    {
        if (move)
        {
            //trigger the column aatack when near the point
            if (Vector2.Distance(transform.position, EndPos) < ColumnRange*2)
            {
                FindObjectOfType<EarthBendingController>().EarthBend_ColumnAttack(EndPos, LookPos);
            }

            //stop the earth mover when reaching target
            if (Vector2.Distance(transform.position, EndPos) < ColumnRange)
            {
                move = false;
                t = 0;

                Destroy(this.gameObject);
            }
            else
            {
                t += Time.deltaTime * Speed;
                transform.position = Vector3.Lerp(this.gameObject.transform.position, EndPos, Mathf.SmoothStep(0f, 1f, t));
            }
        }
    }

    //start the wave
    //called from the EarthBendingController.cs
    public void StartWave(float Speed, Vector3 StartPos, Vector3 EndPos, float ColumnRange, Vector3 LookPos)
    {
        this.Speed = Speed;
        this.StartPos = StartPos;
        this.EndPos = EndPos;
        this.ColumnRange = ColumnRange;
        this.LookPos = LookPos;

        move = true;
    }

    #region Colliders
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ground")
        {
            //Bend up all rocks that touch the earth mover
            col.gameObject.GetComponent<Earth>().BendUp(StartPos,3,this.transform.position,0);
        }
    }
    #endregion
}
