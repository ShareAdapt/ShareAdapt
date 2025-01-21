using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightToggle : MonoBehaviour
{
    // Start is called before the first frame update
    public MenuButton menuButton;
    public void Init(MenuButton menuButton)
    {
        this.menuButton = menuButton;
        
    }
    public void OnClick_ToggleRightPanel()
    {
        menuButton.ToggleRelation();
    }
}
