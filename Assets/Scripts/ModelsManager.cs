using UnityEngine;
using System.Collections;

public class ModelsManager : MonoBehaviour 
{
    public static string TextureLink = "";
    public Transform Spawnpoint;
    private bool createButtonPressed = false;


    public GameObject CreatedPeimitive;

    public bool AppliedTexture;
    private bool showWindow = false;
    public string windowText = "";

    public bool rotateObject;
    public bool mouseOrbiting;



    [Header("Reciuwer info")]
    public string EMail = "";
    public int objectType = 0;

    public void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0,0,200, Screen.height), "", "Window");
        if (!createButtonPressed)
        {
            if (GUILayout.Button("Create model"))
            {
                createButtonPressed = true;
            }
        }
        else if (createButtonPressed)
        {
            if (GUILayout.Button("Create model"))
            {
                createButtonPressed = false;
            }
        }

        if (createButtonPressed)
        {
            if (GUILayout.Button("Sphere"))
            {
                if (CreatedPeimitive == null)
                {
                    CreatedPeimitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                }
                else
                {
                    Destroy(CreatedPeimitive);
                    CreatedPeimitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                }
                mouseOrbiting = false;
                objectType = 0;
                    
            }
            if (GUILayout.Button("Cylinder"))
            {
                if (CreatedPeimitive == null)
                {
                    CreatedPeimitive = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                }
                else
                {
                    Destroy(CreatedPeimitive);
                    CreatedPeimitive = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                }
                mouseOrbiting = false;
                objectType = 1; 
            }
            if (GUILayout.Button("Capsule"))
            {
                if (CreatedPeimitive == null)
                {
                    CreatedPeimitive = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                }
                else
                {
                    Destroy(CreatedPeimitive);
                    CreatedPeimitive = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                }
                mouseOrbiting = false;
                objectType = 2;
            }
        }

        GUILayout.Label("Texture link");
        TextureLink = GUILayout.TextField(TextureLink, GUILayout.Width(170));


        if (GUILayout.Button("Apply texture"))
        {
            if (CreatedPeimitive != null)
            {
                if (TextureLink != "")
                {
                    StartCoroutine(loadTexture());
                }
                else
                {
                    StartCoroutine(Window("Link field is empty!!!!!"));
                }
            }
            else
            {
                StartCoroutine(Window("Befor create a primitive!!!!!"));
            }
        }

        GUILayout.FlexibleSpace();

        if(CreatedPeimitive != null)
        {
            if (!rotateObject)
            {
                if (GUILayout.Button("Rotate object"))
                {
                    rotateObject = true;
                    if (!CreatedPeimitive.GetComponent<RotateObject>())
                        CreatedPeimitive.AddComponent<RotateObject>();
                    CreatedPeimitive.GetComponent<RotateObject>().angle = 150.0f;
                }
            }
            else
            {
                if (GUILayout.Button("Stop rotating"))
                {
                    rotateObject = false;
                    CreatedPeimitive.GetComponent<RotateObject>().enabled = false;
                }
            }

            if (!mouseOrbiting)
            {
                if (GUILayout.Button("On mouse orbit"))
                {
                    mouseOrbiting = true;
                    Camera.main.transform.GetComponent<MouseOrbit>().enabled = true;
                    Camera.main.transform.GetComponent<MouseOrbit>().target = CreatedPeimitive.transform;
                }
            }
            else
            {
                if (GUILayout.Button("Off mouse orbit"))
                {
                    mouseOrbiting = false;
                    Camera.main.transform.GetComponent<MouseOrbit>().enabled = false;
                }
            }
        }



        GUILayout.EndArea();


        if (AppliedTexture)
        {
            if (GUI.Button(new Rect(Screen.width - 210, Screen.height - 60, 200, 50), "Send"))
            {
                WWWForm form = new WWWForm();
                form.AddField("email", EMail);
                form.AddField("texture", TextureLink);
                if (objectType == 0)
                    form.AddField("object_type", "Сфера");
                else if (objectType == 1)
                    form.AddField("object_type", "Цилиндр");
                else if (objectType == 2)
                    form.AddField("object_type", "Капсула");
                WWW w = new WWW("http://abovethesky.ru/sendmail.php", form);
                Debug.Log("Mail sended!");
                StartCoroutine(GetMailing(w));
            }
        }

        if (mouseOrbiting)
        {
            GUILayout.BeginArea(new Rect(Screen.width - 200, 0, 190, 100), "Mouse Controll", "Window");
            GUILayout.Label("RMB - looking around");
            GUILayout.Label("Middle Scroll Wheel - zoom");
            GUILayout.EndArea();
        }


        if (showWindow)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 15, 200, 30), "<color=red>"+windowText+"</color>");
        }

    }

    public IEnumerator loadTexture()
    {
        AppliedTexture = false;
        WWW w = new WWW(TextureLink);
        yield return w;
        CreatedPeimitive.GetComponent<MeshRenderer>().material.mainTexture = w.texture;
        AppliedTexture = true;
    }

    public IEnumerator Window(string text)
    {
        showWindow = true;
        windowText = text;
        yield return new WaitForSeconds(1.5f);
        showWindow = false;
    }

    public IEnumerator GetMailing(WWW w)
    {
        yield return w;
        Debug.Log(w.text);
        if (w.error == null)
        {
            if (w.text == "Succefull")
            {
                StartCoroutine(Window("Mail sended succesfull"));
                Destroy(CreatedPeimitive);
                CreatedPeimitive = null;
                createButtonPressed = false;
                TextureLink = "";
                AppliedTexture = false;
            }
            else
            {
                StartCoroutine(Window("When sending mail something goes wrong!"));
            }
        }
    }
}
