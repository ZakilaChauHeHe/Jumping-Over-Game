using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float WidthRatio = 9f;
    [SerializeField] private float HeightRatio = 16f;


    private void Start()
    {
        AdjustCamera();
    }
    private void AdjustCamera()
    {
        float targetRatio = WidthRatio/HeightRatio;
        float windowRatio = (float)Screen.width / (float)Screen.height; 
        float scaleHeight = windowRatio / targetRatio;
        Camera camera = GetComponent<Camera>();
        
        if(scaleHeight < 1f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2f;

            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1f / scaleHeight;

            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1f;
            rect.x = (1.0f - scaleWidth) / 2f;
            rect.y = 0;

            camera.rect = rect;

        }
    }
}
