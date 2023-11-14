using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public List<PressButtonTest> buttons;
    public List<GameObject> portals; // 여러 포탈을 관리하기 위한 리스트
    [SerializeField] private Material activePortalMaterial; // 포탈 활성화 시 사용할 머티리얼

    private Dictionary<GameObject, Material> originalPortalMaterials = new Dictionary<GameObject, Material>(); // 각 포탈의 원래 머티리얼을 저장

    private void Start()
    {
        // 각 포탈의 렌더러와 원래 머티리얼을 저장합니다.
        foreach (GameObject portal in portals)
        {
            Renderer portalRenderer = portal.GetComponent<Renderer>();
            originalPortalMaterials[portal] = portalRenderer.material;
        }
    }

    public void CheckButtons()
    {
        foreach (PressButtonTest button in buttons)
        {
            if (!button.IsPressed)
            {
                ActivatePortals(false);
                return;
            }
        }

        // 모든 버튼이 눌렸을 경우
        ActivatePortals(true);
    }

    private void ActivatePortals(bool activate)
    {
        foreach (GameObject portal in portals)
        {
            portal.GetComponent<Collider>().isTrigger = activate;
            Renderer portalRenderer = portal.GetComponent<Renderer>();
            portalRenderer.material = activate ? activePortalMaterial : originalPortalMaterials[portal];
        }
    }
}