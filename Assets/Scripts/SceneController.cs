using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public ParticleSystem fireParticleSystem;
    public ParticleSystem smokeParticleSystem;
    public ParticleSystem explosiveParticleSystem;

    private float _timerMax = 5.0f;
    private float _timer = 0.0f;

    public Light explosionLight;
    private bool _lerpLightToZero = false;
    
    // Start is called before the first frame update
    void Start()
    {
        fireParticleSystem.Pause();
        fireParticleSystem.gameObject.SetActive(false);
        explosiveParticleSystem.Pause();
        explosionLight.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer > _timerMax)
        {

            if (explosiveParticleSystem.isPaused)
            {
                explosiveParticleSystem.Play();
                fireParticleSystem.Play();
                fireParticleSystem.gameObject.SetActive(true);
            }

            if (explosionLight.intensity > 9.95)
            {
                _lerpLightToZero = true;
            }

            if (explosionLight.intensity < 10 && !_lerpLightToZero)
            {
                explosionLight.intensity = Mathf.Lerp(explosionLight.intensity, 10, 15.0f * Time.deltaTime);
            }
            else if (_lerpLightToZero)
            {
                explosionLight.intensity = Mathf.Lerp(explosionLight.intensity, 0, 7.0f * Time.deltaTime);
            }
        }

        if (_timer > _timerMax + 1.5f)
        {
            explosiveParticleSystem.Pause();
            explosiveParticleSystem.gameObject.SetActive(false);
        }
    }
}
