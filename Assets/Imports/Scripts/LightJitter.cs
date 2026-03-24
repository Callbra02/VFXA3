using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightJitter : MonoBehaviour
{
    private Vector3 _origin;
    public float maxJitter = 1.0f;
    public float jitterSpeed = 0.5f;
    public float lightAttenuationSpeed = 1.0f;
    private Vector3 _jitterPosition;
    private int positionIndex = 0;

    private Light _light;
    private float _lightDefaultIntensity;
    
    void Start()
    {
        _light = GetComponent<Light>();
        _origin = this.transform.position;
        _jitterPosition = _origin;
        _lightDefaultIntensity = _light.intensity;
    }

    void Update()
    {
        HandleJitterPositioning();
        HandleLightAttenuation();
    }

    private void HandleLightAttenuation()
    {
    
		// Get random intensity value
        float currentLightIntensity = _lightDefaultIntensity + Random.Range(0.2f, _lightDefaultIntensity);

        // Lerp to intensity value, repeat
        if (_light.intensity != currentLightIntensity)
        {
            _light.intensity = Mathf.Lerp(_light.intensity, currentLightIntensity,
                lightAttenuationSpeed * Time.deltaTime);
        }
    }
    
    
    // Lerp between two positions, only get a new position after lerping back from origin point
    private void HandleJitterPositioning()
    {
        if (positionIndex == -1)
        {
            Vector3 newPosition = _origin + new Vector3(Random.Range(-maxJitter, maxJitter), Random.Range(-maxJitter, maxJitter), Random.Range(-maxJitter, maxJitter));
            _jitterPosition = newPosition;
            positionIndex = 0;
        }
        
        if (positionIndex == 0)
        {
            MoveToPosition(0);
        }

        if (positionIndex == 1)
        {
            MoveToPosition(1);
        }
    }

    private void MoveToPosition(int index)
    {
        Vector3 newPosition = index == 0 ? _jitterPosition : _origin;
        
        if (this.transform.position != newPosition)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, newPosition, jitterSpeed * Time.deltaTime);
        }
        else
        {
            positionIndex = index == 0 ? 1 : -1;
        }
    }
}
