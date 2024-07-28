using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    [SerializeField] private Image _image;

    private void Start()
    {
        print("Start animation");

        #region Variant_1

        StartCoroutine(UiAnimations.AnimateFadeOut(_image, 5f, EndAnimation));

        #endregion

        #region Variant_2

        StartCoroutine(UiAnimations.AnimateFadeOut(_image, 5f, delegate { print("End animation"); }));

        #endregion
    }

    private void EndAnimation()
    {
        print("End animation");
    }
}
