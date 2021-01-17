//Attach this script to a GameObject
//Create an Image GameObject by going to Create>UI>Image. Attach this Image to the Image field in your GameObject’s Inspector window.
//This script creates a toggle that fades an Image in and out.
using UnityEngine;
using UnityEngine.UI;

public class RawImageFader : MonoBehaviour
{
    //Attach an Image you want to fade in the GameObject's Inspector
    public RawImage m_RawImage;
    //Use this to tell if the toggle returns true or false
    bool m_Fading;

    void Update()
    {
        //If the toggle returns true, fade in the Image
        if (m_Fading == true)
        {
            //Fully fade in Image (1) with the duration of 2
            m_RawImage.CrossFadeAlpha(1, 2.0f, false);
        }
        //If the toggle is false, fade out to nothing (0) the Image with a duration of 2
        if (m_Fading == false)
        {
            m_RawImage.CrossFadeAlpha(0, 2.0f, false);
        }
    }

    void OnGUI()
    {
        //Fetch the Toggle's state
        m_Fading = GUI.Toggle(new Rect(0, 0, 100, 30), m_Fading, "Fade In/Out");
    }
}