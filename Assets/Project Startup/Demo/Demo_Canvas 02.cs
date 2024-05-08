using TMPro;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using ProjectStartup.ScriptableObjects.Variables;

public class Demo_Canvas02 : MonoBehaviour
{
    [InfoBox("A Float Refrence Works The Same Way A Regular Float Works, Except This Variable Allows For Either A Constant Or Shared Value")]
    [field: SerializeField, BoxGroup("Variable Properties")] private FloatRefrence demoValue01;
    [field: SerializeField, BoxGroup("Variable Properties")] private FloatRefrence demoValue02;
    [field: SerializeField, BoxGroup("Text Properties")] private TMP_Text amountText01;
    [field: SerializeField, BoxGroup("Text Properties")] private TMP_Text amountText02;

    [field: SerializeField] private int amountToUpdateDemoValue01By;
    [field: SerializeField] private int amountToUpdateDemoValue02By;

    [field: SerializeField] private Slider healthBarSlider;


    private void Update()
    {
        healthBarSlider.value = demoValue02.Value;
        amountText01.SetText($"Current Constant Value : {demoValue01.Value}");
        amountText02.SetText($"Current Constant Value : {demoValue02.Value}");

    }


    public void OnIncreaseClicked()
    {
        demoValue01.ConstantValue+= amountToUpdateDemoValue01By;
        demoValue02.Variable.Value+= amountToUpdateDemoValue02By;
    }

    public void OnDecreaseClicked()
    {
        demoValue01.ConstantValue-= amountToUpdateDemoValue01By;
        demoValue02.Variable.Value-= amountToUpdateDemoValue02By;
    }

}
