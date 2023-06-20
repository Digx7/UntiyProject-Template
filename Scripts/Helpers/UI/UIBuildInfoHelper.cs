using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBuildInfoHelper : CustomMonoBehaviorWrapper
{
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Version " + Application.version;
    }
}
