using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractable : Interactable, IExtractable {
    
    protected ToolType requiredTool;

    protected override void Start()
    {
        base.Start();
        interactableType = InteractableType.Extractable;
    }

    public override InteractableType GetInteractableType()
    {
        return interactableType;
    }

    public virtual ToolType GetRequiredTool()
    {
        throw new NotImplementedException();
    }
}
