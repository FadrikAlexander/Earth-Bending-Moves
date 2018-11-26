using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBendingController : MonoBehaviour {

    //Player Class to cast the Earth Bending Moves

    //Column attack variables
    [SerializeField]
    float ColumnPower = 500;
    [SerializeField]
    float ColumnRange = 1f;
    [SerializeField]
    float ColumnSpeed = 2f;
    [SerializeField]
    GameObject EarthMover;

    [SerializeField]
    float SmallwaveAttackRange = 10;
    [SerializeField]
    float BigwaveAttackRange = 15;

    //Rock throw variables
    [SerializeField]
    float RockSpeed = 1f;
    [SerializeField]
    GameObject Rock;

    //The controlls
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EarthBend_360Attack(SmallwaveAttackRange);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            EarthBend_360Attack(BigwaveAttackRange);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            EarthBend_StartRockAttack();

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //Get the mouse position on the ground
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            EarthBend_StartColumnAttack(hit.point, transform.position);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //Get the mouse position on the ground
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            Vector3 LookPos = hit.point;
            LookPos.y -= 1.3f;

            EarthBend_CrushAttack(hit.point, LookPos);
        }
	}

    //the wave attack
    void EarthBend_360Attack(float range)
    {
        //Get all the ground pieces around the player in the range
        Collider[] earthColliders = Physics.OverlapSphere(this.transform.position, range);

        foreach (Collider C in earthColliders)
            if (C.gameObject.tag == "Ground")
              C.gameObject.GetComponent<Earth>().BendUp(this.gameObject.transform.position,0);

    }

    //the rock throw attack
    public void EarthBend_StartRockAttack()
    {
        //create the rock
        //sending the postion infront and bellow the player
        GameObject rock = Instantiate(Rock, transform.position + (-transform.up * 2) + (transform.forward * 2), transform.rotation) as GameObject;
        
        //start the rock movement
        rock.GetComponent<RockMovement>().StartRock(RockSpeed, transform.position + (transform.up) + (transform.forward * 2));
    }

    //the column attack
    public void EarthBend_StartColumnAttack(Vector3 Pos , Vector3 StartPos)
    {
        //give the rocks a place to look at and adujest the rotation
        Vector3 LookPos = this.gameObject.transform.position;
        LookPos.y += 4;

        //create the small earth mover
        GameObject earthMover = Instantiate(EarthMover, StartPos, Quaternion.identity) as GameObject;

        //start the earth mover movement
        earthMover.GetComponent<EarthMover>().StartWave(0.2f, StartPos, Pos, ColumnRange, LookPos);
    }

    //Start the column Attack
    //called from the EarthMover.cs and EarthBendingController.cs
    public void EarthBend_ColumnAttack(Vector3 Pos,Vector3 LookPos)
    {
        //Get all the ground pieces around the player in the range
        Collider[] earthColliders = Physics.OverlapSphere(Pos, ColumnRange);
        foreach (Collider C in earthColliders)
            if (C.gameObject.tag == "Ground")
                C.gameObject.GetComponent<Earth>().BendUp(LookPos, ColumnPower, Pos, ColumnSpeed);
    }

    //Start the crush attack
    public void EarthBend_CrushAttack(Vector3 Pos,Vector3 LookPos)
    {
        //get the postions to start the crushing attack and to bring up columns
        EarthBend_ColumnAttack(Pos + (transform.right * 2), LookPos);
        EarthBend_ColumnAttack(Pos + (-transform.right * 2), LookPos);
    }

}
