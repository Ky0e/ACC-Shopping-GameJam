using UnityEngine;
using NaughtyAttributes;
using ProjectStartup.ScriptableObjects.Helpers;

namespace ProjectStartup.Helpers
{
    public class ShowCanvas : MonoBehaviour
    {
        [InfoBox("Add Extra Canvases To The Dropdown List As Needed To Show Other Canvases. Please Ensure Naming Conventions Are Correct.", EInfoBoxType.Warning)]
        [field: SerializeField, Dropdown("GetCanvasValues"), BoxGroup("Canvas Properties")] private CanvasObject canvasToShow;
        [field: SerializeField, InfoBox("Shows The Specified Canvas And Destroys The Current.", EInfoBoxType.Normal), BoxGroup("Information"), Label("Extra Information"), ResizableTextArea] private string info;

        private DropdownList<CanvasObject> GetCanvasValues()
        {
            return new DropdownList<CanvasObject>()
            {
                { "Canvas Test 01",  Resources.Load("Scriptable Objects/Helpers/UI/Demo_Canvas 01") as CanvasObject },
                { "Canvas Test 02",  Resources.Load("Scriptable Objects/Helpers/UI/Demo_Canvas 02") as CanvasObject },
                // ADD MORE CANVASES TO THIS LIST AS NEEDED & MAKE SURE THEY ARE NAMED CORRECTLY
            };
        }


        public void OnShowCanvas()
        {
            canvasToShow.InstantiateCanvas();
            Destroy(transform.root.gameObject);
        }
    }
}