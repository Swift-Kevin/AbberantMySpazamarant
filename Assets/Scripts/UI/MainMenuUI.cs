using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : BaseUI
{

    public override void TurnOffObject()
    {
        associatedGameObj.SetActive(false);
    }

    public override void TurnOnObject()
    {
        associatedGameObj.SetActive(true);
    }
}
