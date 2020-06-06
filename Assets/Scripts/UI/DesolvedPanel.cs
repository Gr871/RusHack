using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesolvedPanel : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEngine.UI.Image[] desolvePanels;
    public UnityEngine.UI.Image[] uiPanels;
    public static DesolvedPanel instance { get; private set; } = null;
    private Color[] startCondColor;
    [SerializeField] private float t_desolve = 2.0f;
    [SerializeField] private float minAlpha = 0.2f;

    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        for (int i = 0; i < 3; i++) {
            startCondColor[i] = uiPanels[i].color;
        } 
    }
    public void Dessolve()
    {
        StartCoroutine(CorDessolve(desolvePanels.Select(im => new DesoveStruct(im))));
    }
    private IEnumerator CorDessolve(IEnumerable<DesoveStruct> srtucts)
    {
        float t_start = Time.time, ct;

        while ((ct = (Time.time - t_start) / t_desolve) < 1)
        {
            foreach(var value in srtucts)
                value.image.color = value.FromNewAlpha(Mathf.Lerp(value.maxAlpha, minAlpha, ct));

            yield return null;
        }
        foreach (var value in srtucts)
            value.image.color = value.FromNewAlpha(minAlpha);
    }

    private struct DesoveStruct
    {
        public UnityEngine.UI.Image image;
        public Color color;
        public float maxAlpha;

        public DesoveStruct(UnityEngine.UI.Image image)
        {
            this.image = image;
            color = image.color;
            maxAlpha = color.a;
        }

        public Color FromNewAlpha(float a)
            => new Color(color.r, color.g, color.b, a);
    }


    public void SetUiFigure(int panelNum)
    {
        switch (panelNum)
        {
            case 1:
                uiPanels[0].color = new Color(190 / 255f, 159 / 255f, 202 / 255f, 1f);
                uiPanels[2].color = new Color(1f, 1f, 1f, 161/255f);
                break;
            case 2:
                uiPanels[2].color = new Color(190 / 255f, 159 / 255f, 202 / 255f, 1f);
                uiPanels[0].color = new Color(1f, 1f, 1f, 161 / 255f);
                break;
        }
    }

    public void SetStartColour()
    {
        for (int i = 0; i < 3; i++)
        {
            uiPanels[i].color = startCondColor[i];
        }
    }

}
