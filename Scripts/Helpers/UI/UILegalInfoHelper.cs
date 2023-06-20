using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILegalInfoHelper : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Developed by " + Application.companyName;
    }
}
