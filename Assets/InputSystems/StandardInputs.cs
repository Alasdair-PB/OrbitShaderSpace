// GENERATED AUTOMATICALLY FROM 'Assets/StandardInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @StandardInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @StandardInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""StandardInputs"",
    ""maps"": [
        {
            ""name"": ""OverworldActions"",
            ""id"": ""2fa0dbe4-dd31-47dd-9122-49e437b00051"",
            ""actions"": [
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""Value"",
                    ""id"": ""a76b104e-ae9f-4cc2-93e6-48dfcccb1274"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical"",
                    ""type"": ""Value"",
                    ""id"": ""af64cb0a-eb8f-4ca1-b484-ec62fed45465"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Left Right"",
                    ""id"": ""4c80443d-2204-4a6d-b9ad-07b1b346fd41"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""661debfd-4221-44a8-9c75-8ed71dbd1308"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4ba5f9ed-397a-4c46-b9ca-87beae036e4f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""A D"",
                    ""id"": ""380a2dee-d4ad-4dee-a51c-c1f91198ecf6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0d21551c-7ff3-4703-9e78-d78b1fa1cc34"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0c41050b-45ce-422d-82d0-100a0f3aba33"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Up down"",
                    ""id"": ""b481b928-eb45-430a-83bb-bbc483ef8f23"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f2abad2a-027c-4b80-a98f-9f6e95610676"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""768b4f14-de44-422b-bec4-4812a0c8c3ac"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""W S"",
                    ""id"": ""42f0dbb3-9551-45a8-a4a1-4f07429c5281"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e8be7406-3dd7-4ef5-b991-846cb2b32a43"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""66ae3f4f-097a-4d06-9170-44315c809c67"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""CombatActions"",
            ""id"": ""146bd327-0ac8-43ce-8b30-b404763a2670"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""dd79c94e-e269-4d0a-ad75-5ec3b8193f2c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""874956ed-b896-439d-9492-159d099eeaae"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // OverworldActions
        m_OverworldActions = asset.FindActionMap("OverworldActions", throwIfNotFound: true);
        m_OverworldActions_Horizontal = m_OverworldActions.FindAction("Horizontal", throwIfNotFound: true);
        m_OverworldActions_Vertical = m_OverworldActions.FindAction("Vertical", throwIfNotFound: true);
        // CombatActions
        m_CombatActions = asset.FindActionMap("CombatActions", throwIfNotFound: true);
        m_CombatActions_Newaction = m_CombatActions.FindAction("New action", throwIfNotFound: true);
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

    // OverworldActions
    private readonly InputActionMap m_OverworldActions;
    private IOverworldActionsActions m_OverworldActionsActionsCallbackInterface;
    private readonly InputAction m_OverworldActions_Horizontal;
    private readonly InputAction m_OverworldActions_Vertical;
    public struct OverworldActionsActions
    {
        private @StandardInputs m_Wrapper;
        public OverworldActionsActions(@StandardInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Horizontal => m_Wrapper.m_OverworldActions_Horizontal;
        public InputAction @Vertical => m_Wrapper.m_OverworldActions_Vertical;
        public InputActionMap Get() { return m_Wrapper.m_OverworldActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OverworldActionsActions set) { return set.Get(); }
        public void SetCallbacks(IOverworldActionsActions instance)
        {
            if (m_Wrapper.m_OverworldActionsActionsCallbackInterface != null)
            {
                @Horizontal.started -= m_Wrapper.m_OverworldActionsActionsCallbackInterface.OnHorizontal;
                @Horizontal.performed -= m_Wrapper.m_OverworldActionsActionsCallbackInterface.OnHorizontal;
                @Horizontal.canceled -= m_Wrapper.m_OverworldActionsActionsCallbackInterface.OnHorizontal;
                @Vertical.started -= m_Wrapper.m_OverworldActionsActionsCallbackInterface.OnVertical;
                @Vertical.performed -= m_Wrapper.m_OverworldActionsActionsCallbackInterface.OnVertical;
                @Vertical.canceled -= m_Wrapper.m_OverworldActionsActionsCallbackInterface.OnVertical;
            }
            m_Wrapper.m_OverworldActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Horizontal.started += instance.OnHorizontal;
                @Horizontal.performed += instance.OnHorizontal;
                @Horizontal.canceled += instance.OnHorizontal;
                @Vertical.started += instance.OnVertical;
                @Vertical.performed += instance.OnVertical;
                @Vertical.canceled += instance.OnVertical;
            }
        }
    }
    public OverworldActionsActions @OverworldActions => new OverworldActionsActions(this);

    // CombatActions
    private readonly InputActionMap m_CombatActions;
    private ICombatActionsActions m_CombatActionsActionsCallbackInterface;
    private readonly InputAction m_CombatActions_Newaction;
    public struct CombatActionsActions
    {
        private @StandardInputs m_Wrapper;
        public CombatActionsActions(@StandardInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_CombatActions_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_CombatActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActionsActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActionsActions instance)
        {
            if (m_Wrapper.m_CombatActionsActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_CombatActionsActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_CombatActionsActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_CombatActionsActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_CombatActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public CombatActionsActions @CombatActions => new CombatActionsActions(this);
    public interface IOverworldActionsActions
    {
        void OnHorizontal(InputAction.CallbackContext context);
        void OnVertical(InputAction.CallbackContext context);
    }
    public interface ICombatActionsActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
