using UnityEngine;
using System.Collections;

public class BGRepeat_25 : MonoBehaviour
{

    public float scrollSpeed;
    private Vector2 savedOffset;

    private Renderer BG;


   

    private void Awake()
    {
        BG = GetComponent<Renderer>();
    }
    void Start()
    {
        savedOffset = BG.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
      
       
        
            float y = Mathf.Repeat(Time.time * scrollSpeed, 1);
            Vector2 offset = new Vector2(savedOffset.x, y);
            BG.sharedMaterial.SetTextureOffset("_MainTex", offset);
           
        
    }

    void OnDisable()
    {
        BG.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}