//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/PlayerActionInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerActionInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActionInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActionInput"",
    ""maps"": [
        {
            ""name"": ""BaseAbilities"",
            ""id"": ""a4b7f5a0-cbd8-45bd-b8a5-bfca1d7c479e"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""f102fe8d-e912-428d-b5d0-6f3e514a4081"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""249295cc-5b09-407b-b373-9380ca19b786"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""c7877dd5-ec01-402a-9700-bcc45b81ed8e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AltInteract"",
                    ""type"": ""Button"",
                    ""id"": ""82c4cf17-548a-4481-880e-ff53f260b49a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Walk"",
                    ""id"": ""2a82262d-88eb-4f01-affb-a7b5842273af"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""87d55dae-8462-42a3-b1cc-dbfb83775aad"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3036f569-1f1c-48df-96a9-b195aea30b56"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b24d4c38-094e-444a-a8f1-1f7d9b245335"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0a32c9d6-5061-43c9-b3a7-eed41267270c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c6656830-855a-4ef6-b3eb-b646f8b6ada0"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e892d607-c535-49f7-b73a-a4ff38161fab"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3428a7f-61ea-40b7-aacb-f33c094dafc0"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AltInteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // BaseAbilities
        m_BaseAbilities = asset.FindActionMap("BaseAbilities", throwIfNotFound: true);
        m_BaseAbilities_Movement = m_BaseAbilities.FindAction("Movement", throwIfNotFound: true);
        m_BaseAbilities_Attack = m_BaseAbilities.FindAction("Attack", throwIfNotFound: true);
        m_BaseAbilities_Interact = m_BaseAbilities.FindAction("Interact", throwIfNotFound: true);
        m_BaseAbilities_AltInteract = m_BaseAbilities.FindAction("AltInteract", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // BaseAbilities
    private readonly InputActionMap m_BaseAbilities;
    private List<IBaseAbilitiesActions> m_BaseAbilitiesActionsCallbackInterfaces = new List<IBaseAbilitiesActions>();
    private readonly InputAction m_BaseAbilities_Movement;
    private readonly InputAction m_BaseAbilities_Attack;
    private readonly InputAction m_BaseAbilities_Interact;
    private readonly InputAction m_BaseAbilities_AltInteract;
    public struct BaseAbilitiesActions
    {
        private @PlayerActionInput m_Wrapper;
        public BaseAbilitiesActions(@PlayerActionInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_BaseAbilities_Movement;
        public InputAction @Attack => m_Wrapper.m_BaseAbilities_Attack;
        public InputAction @Interact => m_Wrapper.m_BaseAbilities_Interact;
        public InputAction @AltInteract => m_Wrapper.m_BaseAbilities_AltInteract;
        public InputActionMap Get() { return m_Wrapper.m_BaseAbilities; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BaseAbilitiesActions set) { return set.Get(); }
        public void AddCallbacks(IBaseAbilitiesActions instance)
        {
            if (instance == null || m_Wrapper.m_BaseAbilitiesActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BaseAbilitiesActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @AltInteract.started += instance.OnAltInteract;
            @AltInteract.performed += instance.OnAltInteract;
            @AltInteract.canceled += instance.OnAltInteract;
        }

        private void UnregisterCallbacks(IBaseAbilitiesActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @AltInteract.started -= instance.OnAltInteract;
            @AltInteract.performed -= instance.OnAltInteract;
            @AltInteract.canceled -= instance.OnAltInteract;
        }

        public void RemoveCallbacks(IBaseAbilitiesActions instance)
        {
            if (m_Wrapper.m_BaseAbilitiesActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBaseAbilitiesActions instance)
        {
            foreach (var item in m_Wrapper.m_BaseAbilitiesActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BaseAbilitiesActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BaseAbilitiesActions @BaseAbilities => new BaseAbilitiesActions(this);
    public interface IBaseAbilitiesActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAltInteract(InputAction.CallbackContext context);
    }
}