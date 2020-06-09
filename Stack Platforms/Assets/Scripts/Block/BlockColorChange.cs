using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockColorChange : MonoBehaviour
{
    public static BlockColorChange Instance { get; set; }

    public static Color BlockColor;
    
    [SerializeField] private Renderer blockRenderer;
    [SerializeField] private float hueAmountToAdd = 0.01f;
    [SerializeField] private float saturationAmountToAdd = 0.05f;
    [SerializeField] private float brightnessAmountToAdd = 0.05f;

    private static float _hueValue;
    private static float _saturation;
    private static float _brightness;

    private static bool _isSaturationMax;
    private static bool _isSaturationMin;
    
    private static bool _isBrightnessMax;
    private static bool _isBrightnessMin;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        SetBlockColor();
    }

    private void SetBlockColor()
    {
        if (!BlockController.LastBlock)
        {
            _hueValue = Random.value;
            _saturation = Random.Range(0.3f, 0.9f);
            _brightness = Random.Range(0.3f, 0.9f);
        }

        _hueValue += hueAmountToAdd;

        if (_hueValue >= 1f)
        {
            _hueValue = 0;
        }

        _brightness = ChangeParamValue(ref _brightness, brightnessAmountToAdd, ref _isBrightnessMax,
            ref _isBrightnessMin);
        _saturation = ChangeParamValue(ref _saturation, saturationAmountToAdd, ref _isSaturationMax,
            ref _isSaturationMin);

        BlockColor = Color.HSVToRGB(_hueValue, _brightness, _saturation);
        blockRenderer.material.color = BlockColor;
    }

    private float ChangeParamValue(ref float value, float amountToAdd, ref bool isMax, ref bool isMin)
    {
        if (!isMax && !isMin)
        {
            value += amountToAdd;
        }
        
        if (value >= 1 || isMax)
        {
            isMin = false;
            isMax = true;
            value -= amountToAdd;
        }
       
        if (value <= 0.3f || isMin)
        {
            isMin = true;
            isMax = false;
            value += amountToAdd;
        }

        return value;
    }
    
    
}
