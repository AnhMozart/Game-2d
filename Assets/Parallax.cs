using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Material Material;
    [SerializeField]
    private float parallaxFactor = 0.01f;
    private float offset;


    private void Start()
    {
        Material = GetComponent<Renderer>().material;
    }


    private void Update()
    {
        ParallaxSroll();
    }


    private void ParallaxSroll()
    {
        float speed = parallaxFactor * GameManager.instance.GetGameSpeed();
        offset += speed * Time.deltaTime;
        Material.SetTextureOffset("_MainTex", Vector2.right * offset);
    }

}
