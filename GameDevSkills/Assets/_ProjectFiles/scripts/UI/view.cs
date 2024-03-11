using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class view : MonoBehaviour
{
    public ViewSO viewData;

    public GameObject containerTop;
    public GameObject containerCenter;
    public GameObject containerBottom;

    private Image imageTop;
    private Image imageCenter;
    private Image imageBottom;

    private VerticalLayoutGroup verticalLayoutGroup;

    private void Awake()
    {
        init();
    }

    private void init()
    {
        Setup();
        Configure();
    }

    private void Configure()
    {
        verticalLayoutGroup.padding = viewData.padding;
        verticalLayoutGroup.spacing = viewData.spacing;
    }

    private void Setup()
    {

        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        imageTop = GetComponent<Image>();
        imageCenter = GetComponent<Image>();
        imageBottom = GetComponent<Image>();

    }

    private void OnValidate()
    {
        init();
        
    }
}
