using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeCounter : MonoBehaviour
{
    Label m_CubeCounter;

    public static CubeCounter Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        m_CubeCounter = root.Q<Label>("cube-counter");
    }
    
    public void SetCubeCount(int count)
    {
        m_CubeCounter.text = count.ToString();
    }
}
