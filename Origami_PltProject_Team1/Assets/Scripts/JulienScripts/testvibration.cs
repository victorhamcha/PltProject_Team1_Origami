using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testvibration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testVibration200ms()
    {
        Vibration.Vibrate(200);
    }

    public void testVibration100ms()
    {
        Vibration.Vibrate(100);
    }

    public void testVibration50ms()
    {
        Vibration.Vibrate(50);
    }

    public void testVibrationAlternant()
    {
        long[] pattern = { 0, 1000, 1000, 1000, 1000 };
        Vibration.Vibrate(pattern, 0);
    }

    public void testVibrationAlternantFast50()
    {
        long[] pattern = { 0, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50 };
        Vibration.Vibrate(pattern, 0);
    }

    public void StopVibration()
    {
        Vibration.Cancel();
    }

    public void testVibrationAlternantFast20()
    {
        long[] pattern = { 0, 50, 20, 50, 20, 50, 20, 50, 20, 50, 20, 50, 20, 50, 20, 50, 20, 50, 20, 50, 20, 50, 20, 50, 20 };
        Vibration.Vibrate(pattern, 0);
    }

    public void testVibrationAlternantFast30()
    {
        long[] pattern = { 0, 50, 30, 50, 30, 50, 30, 50, 30, 50, 30, 50, 30, 50, 30, 50, 30, 50, 30, 50, 30, 50, 30, 50, 30 };
        Vibration.Vibrate(pattern, 0);
    }

    public void testVibrationAlternantFast40()
    {
        long[] pattern = { 0, 50, 40, 50, 40, 50, 40, 50, 40, 50, 40, 50, 40, 50, 40, 50, 40, 50, 40, 50, 40, 50, 40, 50, 40 };
        Vibration.Vibrate(pattern, 0);
    }
}
