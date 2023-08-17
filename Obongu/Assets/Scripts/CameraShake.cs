using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    // reference variables
    [SerializeField] private float shakeIntensity = 1f;
    [SerializeField] private float shakeTime = 0.5f;

    // inner params
    private float timer;
    private float initialAmplitude;
    private CinemachineVirtualCamera playerCamera;
    private void Start()
    {
        playerCamera = GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin _cbmcp = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        initialAmplitude = _cbmcp.m_AmplitudeGain;    
    }
    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain += shakeIntensity;
        timer = shakeTime;
    }

    private void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = initialAmplitude;
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = 0;
                StopShake();
            }
        }
    }
}
