using System.Runtime.InteropServices;
using UnityEngine;

public class SoundListener : MonoBehaviour
{
    public static SoundListener Instance;

    public PlayerController controller;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float sensitivity = 50; // 감도 조절
    public float loudness = 0;
    AudioClip _clipRecord;
    string _device;

    [DllImport("__Internal")]
    public static extern void InitMicrophoneJS();
    [DllImport("__Internal")]
    public static extern float GetMicrophoneVolumeJS();
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

#if     UNITY_WEBGL && !UNITY_EDITOR
        InitMicrophoneJS();
#else
        // 1. 마이크 장치 확인 및 녹음 시작
        if (Microphone.devices.Length > 0)
        {
            _device = Microphone.devices[0];
            // 999초 동안, 44100Hz로 녹음 시작 (계속 덮어씀)
            _clipRecord = Microphone.Start(_device, true, 999, 44100);
        }
#endif
    }

    void Update()
    {
        bool isReady = false;

#if UNITY_WEBGL && !UNITY_EDITOR
        isReady = true; // WebGL은 무조건 준비 완료로 처리
#else
        isReady = (_clipRecord != null); // 에디터는 마이크 켜졌는지 확인
#endif

        if (isReady)
        {
            // [참고] 감도(sensitivity)를 여기서 곱합니다.
            loudness = GetVolume() * sensitivity;

            // 디버그용 (너무 시끄러우면 주석 처리)
            // Debug.Log(loudness);
            if (loudness > 1.0f)
            {
                // Debug.Log("소리 감지됨! 크기: " + loudness);
            }
        }
    }

    float GetVolume()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return GetMicrophoneVolumeJS();
#else
        return GetAveragedVolumeEditor(); 
#endif
    }

    // 현재 마이크 볼륨 평균 계산 함수
#if UNITY_EDITOR 
    float GetAveragedVolumeEditor()
    {
        float[] data = new float[256];
        int a = Microphone.GetPosition(_device) - 256 + 1;
        if (a < 0) return 0;

        _clipRecord.GetData(data, a);
        float aSum = 0;
        foreach (float s in data)
        {
            aSum += Mathf.Abs(s);
        }
        return aSum / 256;
    }
#endif
}
