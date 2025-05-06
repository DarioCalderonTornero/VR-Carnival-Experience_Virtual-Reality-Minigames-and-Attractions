using UnityEngine;

public class PartyLights : MonoBehaviour
{
    public Material sharedMaterial; 
    public float flickerInterval = 0.5f; 

    private float timer = 0f;
    private int currentColorIndex = 0;

   
    private Color[] partyColors = new Color[]
    {
        Color.red,
        Color.yellow,
        Color.green,
        Color.blue
    };

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= flickerInterval)
        {
            timer = 0f;
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        if (sharedMaterial != null)
        {
            Color color = partyColors[currentColorIndex];

            
            sharedMaterial.color = color;
            sharedMaterial.SetColor("_EmissionColor", color * 2f);

            // Avanza al siguiente color
            currentColorIndex = (currentColorIndex + 1) % partyColors.Length;
        }
    }
}
