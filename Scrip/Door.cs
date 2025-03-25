using TMPro;
using UnityEngine;
public enum BonusType { Addition, Differrence, Multiply, Division }

public class Door : MonoBehaviour
{

    [Header("Elemment")]
    [SerializeField] private SpriteRenderer rightDoorRenderer;
    [SerializeField] private SpriteRenderer leftDoorRenderer;
    [SerializeField] private TextMeshPro rightText;
    [SerializeField] private TextMeshPro leftText;
    private BoxCollider bx;

    [Header("Setting")]
    [SerializeField] BonusType rightDoorBonusType;
    [SerializeField] private int rightDoorBonusAmount; // giá trị bên phải

    [SerializeField]BonusType leftDoorBonusType;
    [SerializeField] private int leftDoorBonusAmount; //Gia trị bên trái

    [SerializeField] private Color bonusColor; //Mùa thưởng
    [SerializeField] private Color penaltyColor; // Màu phạt

    private void Start()
    {
        Configuredoor();
        bx = GetComponent<BoxCollider>();
    }


    private void Configuredoor()
    {
        //Right Door
        switch(rightDoorBonusType)
        {
            case BonusType.Addition:
                rightDoorRenderer.color = bonusColor;
                rightText.text = "+" + rightDoorBonusAmount.ToString();
                break;

            case BonusType.Differrence:
                rightDoorRenderer.color = penaltyColor;
                rightText.text = "-" + rightDoorBonusAmount.ToString();
                break;

            case BonusType.Multiply:
                rightDoorRenderer.color = bonusColor;
                rightText.text = "x" + rightDoorBonusAmount.ToString();
                break;

            case BonusType.Division:
                rightDoorRenderer.color = penaltyColor;
                rightText.text = "-" + rightDoorBonusAmount.ToString();
                break;
        }




        /*
        if(rightDoorBonusType == BonusType.Addition)
        {
            rightDoorRenderer.color = bonusColor;
            rightText.text = "+" + rightDoorBonusAmount.ToString();
        }
        else if(rightDoorBonusType== BonusType.Differrence)
        {
            rightDoorRenderer.color = penaltyColor;
            rightText.text = "-" + rightDoorBonusAmount.ToString();
        }
        */

        //Left Door
        /*
        if (leftDoorBonusType == BonusType.Addition)
        {
            leftDoorRenderer.color = bonusColor;
            leftText.text = "+" + leftDoorBonusAmount.ToString();
        }
        else if (leftDoorBonusType == BonusType.Differrence)
        {
            leftDoorRenderer.color = penaltyColor;
            leftText.text = "-" + leftDoorBonusAmount.ToString();
        }
        */

        switch (leftDoorBonusType)
        {
            case BonusType.Addition:
                leftDoorRenderer.color = bonusColor;
                leftText.text = "+" + leftDoorBonusAmount.ToString();
                break;

            case BonusType.Differrence:
                leftDoorRenderer.color = penaltyColor;
                leftText.text = "-" + leftDoorBonusAmount.ToString();
                break;

            case BonusType.Multiply:
                leftDoorRenderer.color = bonusColor;
                leftText.text = "*" + leftDoorBonusAmount.ToString();
                break;

            case BonusType.Division:
                leftDoorRenderer.color = penaltyColor;
                leftText.text = "/" + leftDoorBonusAmount.ToString();
                break;
        }

    }    



    public int GetBonesAmount(float xposition)
    {
        if(xposition >0)
        {
            return rightDoorBonusAmount;
        }    
        else
        {
            return leftDoorBonusAmount;
        }    
    }


    public BonusType GetBonusType(float xposition)
    {
        if(xposition > 0)
        {
            return rightDoorBonusType;
        }
        else
        {
            return leftDoorBonusType;
        }
    }

    public void Disable()
    {
        bx.enabled = false;
    }
}
