using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "DialogInfo", menuName = "Table Info/Dialog")]
public class ObjectData : MonoBehaviour
{
    public int id;
    public bool isNpc;

    private void Start() {
        
    }
    // [SerializeField] string dialog_kr;
    // [SerializeField] string dialog_en;
    
    // public string dialog
    // {
    //     get{
    //         switch(Application.systemLanguage)
    //         {
    //             case SystemLanguage.Korean :
    //                 return dialog_kr;
    //             default :
    //                 return dialog_en;
    //         }
    //     }
    // }
}
