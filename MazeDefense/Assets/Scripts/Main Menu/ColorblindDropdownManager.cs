using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class ColorblindDropdownManager : MonoBehaviour
{
    List<string> names = new List<string>() {
        "Normal",
        "Protanopia",
        "Protanomaly",
        "Deuteranopia",
        "Deuteranomaly",
        "Tritanopia",
        "Tritanomaly",
        "Achromatopsia",
        "Achromatomaly",
    };
    public TMPro.TMP_Dropdown dropdown;
    public TMP_Text selectedName;

    public void Dropdown_IndexChanged(int index) 
    {
        selectedName.text = names[index];

        // GameObject obj = GameObject.Find("Main Camera");
        // obj.GetComponent<ColorBlindFilter>().handleMode(index);
    }

    void Start() 
    {
        PopulateList();
    }

    void PopulateList() 
    {
        dropdown.AddOptions(names);
    }
}


