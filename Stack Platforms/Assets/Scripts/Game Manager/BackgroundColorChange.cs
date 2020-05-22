using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundColorChange : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    private float _timeLeft;
    private Color _targetColor;
    
    private void Start()
    {
        _targetColor = new Color(Random.value, Random.value, Random.value);
    }

    private void Update()
    {

        if (_timeLeft <= Time.deltaTime)
        {
            camera.backgroundColor = _targetColor;

            _targetColor = new Color(Random.value, Random.value, Random.value);
            _timeLeft = 3f;
        }
        else
        {
            camera.backgroundColor =
                Color.Lerp(camera.backgroundColor, _targetColor, Time.deltaTime / _timeLeft);

            _timeLeft -= Time.deltaTime;
        }
    }
}
