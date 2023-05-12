using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSound : MonoBehaviour
{
    public Slider volumeSom;
    public AudioSource soundMaster;

    // Start is called before the first frame update
    void Start()
    {
        volumeSom.value = 0.1F;
    }

    // Update is called once per frame
    void Update()
    {
        soundMaster.GetComponent<AudioSource>().volume = volumeSom.value;
    }
}
