using Plugins.VKSDK;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InviteButton : MonoBehaviour
{
    private void Start() => GetComponent<Button>().onClick.AddListener(Invite);

    private void Invite()
    {
#if VK_SDK
        VKSDK.instance.ShowInvite();
#endif
    }
}