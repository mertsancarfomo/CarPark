using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoTweenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveX(15, 3);
        DOTween.Play(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
