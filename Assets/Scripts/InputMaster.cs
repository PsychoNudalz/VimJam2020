// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""64f27d8d-7808-4ebf-ba19-19e9dbed5d48"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""4bf0fee0-715f-4511-9374-718bf9312568"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""599d3f2a-b59a-4c13-aa3f-128fd8bc3d9c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraMovement"",
                    ""type"": ""Value"",
                    ""id"": ""9055f527-5e64-46e2-be70-0298be67b576"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""80e8366e-3a10-4edf-b09b-6bb22c98fc94"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""M&S"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5beccc3c-84e6-4608-8acb-a28ff0140f2a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""M&S"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Button With Two Modifiers"",
                    ""id"": ""6c097c4f-b965-432c-8a42-1d25165aff7d"",
                    ""path"": ""ButtonWithTwoModifiers"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""fb408cc6-f5b6-4ea2-b451-de96f470a33b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c140733d-2924-46b5-93a8-8b4c3434659e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&S"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c67d9991-933d-407c-bd5b-1cc2473fe582"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&S"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c311fb78-9ba8-43fe-af76-704b27a31683"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&S"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""04a52460-d17a-43e1-8085-11b374f20f3e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&S"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""8c7b7d8f-3185-4a7c-8f11-66593698248e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""48de9864-9d90-4b25-9e72-4c96d9742fc3"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&S"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""00eaeac9-51ba-4cdc-b3b0-875813b2c236"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&S"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ae4c829f-1334-44c5-89b3-9ffc4e4a2efa"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&S"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""75351836-b1e9-44cd-9add-fa59ecb1e7cc"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&S"",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""M&S"",
            ""bindingGroup"": ""M&S"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Select = m_Player.FindAction("Select", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_CameraMovement = m_Player.FindAction("CameraMovement", throwIfNotFound: true);
    }

    public void Dispose()
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Select;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_CameraMovement;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_Player_Select;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @CameraMovement => m_Wrapper.m_Player_CameraMovement;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelect;
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @CameraMovement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMovement;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @CameraMovement.started += instance.OnCameraMovement;
                @CameraMovement.performed += instance.OnCameraMovement;
                @CameraMovement.canceled += instance.OnCameraMovement;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_MSSchemeIndex = -1;
    public InputControlScheme MSScheme
    {
        get
        {
            if (m_MSSchemeIndex == -1) m_MSSchemeIndex = asset.FindControlSchemeIndex("M&S");
            return asset.controlSchemes[m_MSSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnCameraMovement(InputAction.CallbackContext context);
    }
}
