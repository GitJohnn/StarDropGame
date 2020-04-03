// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerScripts/PlayerControls.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerControls : IInputActionCollection
{
    private InputActionAsset asset;
    public PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""6660de6d-6010-41e7-a660-cae51a2fa52a"",
            ""actions"": [
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""cf5aa3a0-babb-4336-883c-3d6651601bcf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""f733b408-efb3-4a0d-9732-5598ff53f41a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move_LS"",
                    ""type"": ""Button"",
                    ""id"": ""d9ed7173-27df-4c8c-9978-165a18171e07"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""b9168076-2042-402c-bd9c-2205030b1f58"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Bash"",
                    ""type"": ""Button"",
                    ""id"": ""419f5ac3-2f6e-4c30-8b92-69d9f440ade6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim_RS"",
                    ""type"": ""Button"",
                    ""id"": ""9039e1ee-d9e8-4074-84f0-2ea145868122"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""997163a6-5708-4273-9fd4-a7bc5ba8925e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""221e796b-2d26-499c-9557-b1461672d961"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""a5e0216a-8d96-429b-b06d-b923c940c229"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f9b6fe91-8faa-4d9b-ac62-9037d61874b4"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17a31c30-cb94-409c-992e-d6085ac7c9c9"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9867c563-8634-41cc-8fb6-64da5e86c8ee"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69823137-689c-4eca-ab0e-56bfe3422c9b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a63bd35-2495-4d9c-8a62-1bbc09671720"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move_LS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""07c6069f-efc2-4391-9ffa-51fca82463b7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move_LS"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""442fd55b-468c-4cb4-a408-2ed5bd3dd852"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move_LS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c7f6a315-c245-4a5a-9688-2f9b3d16f13b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move_LS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""628dbe33-a077-4678-93dc-bedc9c877afd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move_LS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5d6f7d51-f043-498f-9944-84d024f4bf1f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move_LS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""13b59625-2784-4c81-8319-fe4d092ff830"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard and Mouse"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9ef8f18-d27d-42da-9df2-c2cb3c0974f9"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f22f6201-9a5d-4ac1-8415-d85bb9d2b0f9"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Bash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3524bcc6-4767-4eea-9cea-59b491bc8b16"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Aim_RS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7f879aa-118c-4d55-bf49-78e7096cd668"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbc949ce-71f3-4d61-9727-3d3879f87ce9"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fdebe3c-dc15-45a1-9022-c92939e330f0"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5fbaaa99-1a80-424b-9308-a2977eee523b"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2bb3f5ff-2411-45df-a6f4-0de3e3602e8f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.GetActionMap("Gameplay");
        m_Gameplay_Throw = m_Gameplay.GetAction("Throw");
        m_Gameplay_Dash = m_Gameplay.GetAction("Dash");
        m_Gameplay_Move_LS = m_Gameplay.GetAction("Move_LS");
        m_Gameplay_Grab = m_Gameplay.GetAction("Grab");
        m_Gameplay_Bash = m_Gameplay.GetAction("Bash");
        m_Gameplay_Aim_RS = m_Gameplay.GetAction("Aim_RS");
        m_Gameplay_Shoot = m_Gameplay.GetAction("Shoot");
        m_Gameplay_Jump = m_Gameplay.GetAction("Jump");
        m_Gameplay_Interact = m_Gameplay.GetAction("Interact");
    }

    ~PlayerControls()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Throw;
    private readonly InputAction m_Gameplay_Dash;
    private readonly InputAction m_Gameplay_Move_LS;
    private readonly InputAction m_Gameplay_Grab;
    private readonly InputAction m_Gameplay_Bash;
    private readonly InputAction m_Gameplay_Aim_RS;
    private readonly InputAction m_Gameplay_Shoot;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Interact;
    public struct GameplayActions
    {
        private PlayerControls m_Wrapper;
        public GameplayActions(PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Throw => m_Wrapper.m_Gameplay_Throw;
        public InputAction @Dash => m_Wrapper.m_Gameplay_Dash;
        public InputAction @Move_LS => m_Wrapper.m_Gameplay_Move_LS;
        public InputAction @Grab => m_Wrapper.m_Gameplay_Grab;
        public InputAction @Bash => m_Wrapper.m_Gameplay_Bash;
        public InputAction @Aim_RS => m_Wrapper.m_Gameplay_Aim_RS;
        public InputAction @Shoot => m_Wrapper.m_Gameplay_Shoot;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Interact => m_Wrapper.m_Gameplay_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                Throw.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnThrow;
                Throw.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnThrow;
                Throw.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnThrow;
                Dash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                Dash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                Dash.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                Move_LS.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove_LS;
                Move_LS.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove_LS;
                Move_LS.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove_LS;
                Grab.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrab;
                Grab.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrab;
                Grab.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrab;
                Bash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBash;
                Bash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBash;
                Bash.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBash;
                Aim_RS.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim_RS;
                Aim_RS.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim_RS;
                Aim_RS.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim_RS;
                Shoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                Shoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                Shoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                Interact.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                Interact.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                Interact.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                Throw.started += instance.OnThrow;
                Throw.performed += instance.OnThrow;
                Throw.canceled += instance.OnThrow;
                Dash.started += instance.OnDash;
                Dash.performed += instance.OnDash;
                Dash.canceled += instance.OnDash;
                Move_LS.started += instance.OnMove_LS;
                Move_LS.performed += instance.OnMove_LS;
                Move_LS.canceled += instance.OnMove_LS;
                Grab.started += instance.OnGrab;
                Grab.performed += instance.OnGrab;
                Grab.canceled += instance.OnGrab;
                Bash.started += instance.OnBash;
                Bash.performed += instance.OnBash;
                Bash.canceled += instance.OnBash;
                Aim_RS.started += instance.OnAim_RS;
                Aim_RS.performed += instance.OnAim_RS;
                Aim_RS.canceled += instance.OnAim_RS;
                Shoot.started += instance.OnShoot;
                Shoot.performed += instance.OnShoot;
                Shoot.canceled += instance.OnShoot;
                Jump.started += instance.OnJump;
                Jump.performed += instance.OnJump;
                Jump.canceled += instance.OnJump;
                Interact.started += instance.OnInteract;
                Interact.performed += instance.OnInteract;
                Interact.canceled += instance.OnInteract;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.GetControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.GetControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnThrow(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnMove_LS(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnBash(InputAction.CallbackContext context);
        void OnAim_RS(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
