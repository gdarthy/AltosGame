using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(BasicObject))]
public class Mouse : Entity {

	// Use this for initialization
	new void Start () {
        base.Start();
        /*
        Lives = 10;
        Defense = 2;
        Strength = 2;
        Experiences = 10;*/

	}
	
	// Update is called once per frame
	new void Update () {
        base.Update();
    }
}
