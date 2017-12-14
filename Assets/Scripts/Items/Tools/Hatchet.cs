using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatchet : Tool {

    protected override void Start()
    {
        base.Start();
        toolType = ToolType.Axe;
    }
	

}
