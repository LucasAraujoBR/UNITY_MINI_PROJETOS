using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Skybox))]
public class SkyboxSetter : MonoBehaviour
{
    [SerializeField] List<Material> _skyboxMaterials;

    Skybox _skybox;
    // Update is called once per frame
    void Awake()
    {
        _skybox = GetComponent<Skybox>();
    }

    void OnEnable()
    {
        ChangeSkybox(skyBox: 0);
    }

    void ChangeSkybox(int skyBox)
    {
        if (_skybox != null && skyBox >= 0 && skyBox <= _skyboxMaterials.Count)
        {
            _skybox.material = _skyboxMaterials[skyBox];
        }
    }
}


