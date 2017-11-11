using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(BasicObject))]
public class Imp : Entity {

	// Use this for initialization
    new	void Start () {
        base.Start();

    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
    }
}
