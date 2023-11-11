using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public List<PressButtonTest> buttons;
    public GameObject portal;
    [SerializeField] private Material activePortalMaterial; // 포탈 활성화 시 사용할 머티리얼

    private Renderer portalRenderer;
    private Material originalPortalMaterial; // 원래 포탈 머티리얼

    private void Start()
    {
        portalRenderer = portal.GetComponent<Renderer>();
        originalPortalMaterial = portalRenderer.material; // 원래의 포탈 머티리얼 저장
    }

    public void CheckButtons()
    {
        foreach (PressButtonTest button in buttons)
        {
            if (!button.IsPressed)
            {
                ActivatePortal(false);
                return;
            }
        }

        // 모든 버튼이 눌렸을 경우
        ActivatePortal(true);
    }

    private void ActivatePortal(bool activate)
    {
        portal.GetComponent<Collider>().isTrigger = activate;
        portalRenderer.material = activate ? activePortalMaterial : originalPortalMaterial;
    }
}
