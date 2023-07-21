using UnityEngine;
using UnityEngine.UI;

namespace GUIScripts
{
    [RequireComponent(typeof(Image))]
    public class StatBarComponent : MonoBehaviour
    {
        private Image _healthbarImage;

        // Use this for initialization
        protected virtual void Start()
        {
            _healthbarImage = CreateBackgroundImage(GetComponent<Image>());
        }

        protected void ChangeAmount(float amount)
        {
            _healthbarImage.fillAmount = amount;
        }
        
        private Image CreateBackgroundImage(Image parent)
        {
            var cooldownImageObject = new GameObject("BackgroundImage");
            cooldownImageObject.transform.parent = parent.transform;

            var image = cooldownImageObject.AddComponent<Image>();
            image.type = Image.Type.Filled;
            image.fillMethod = Image.FillMethod.Horizontal;
            image.sprite = parent.sprite;
            image.fillClockwise = false;
            image.color = Color.white;

            CopyRectTransform(parent.transform, image.transform);

            return image;
        }

        private void CopyRectTransform(Transform current, Transform target)
        {
            target.position = current.position;
            var rectTransform = target as RectTransform;
            var currentRectTransform = current as RectTransform;
            rectTransform.sizeDelta = new Vector2(currentRectTransform.sizeDelta.x, currentRectTransform.sizeDelta.y);
            rectTransform.localScale = currentRectTransform.localScale;
        }
    }
}