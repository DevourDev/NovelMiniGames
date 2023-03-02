//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/_source/Game/Inputs/PlayerControls.inputactions
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

namespace Game.Inputs
{
    public partial class @PlayerControls : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""UpDown"",
            ""id"": ""f9098e1e-173e-4b3d-923b-362aeae93d05"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f61be00e-b66f-4a4a-b44d-5db38791ce92"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WS"",
                    ""id"": ""b46c477a-8853-4a9c-acb1-e5ae280650b8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1b7d1be6-e0ef-40d0-b33c-cbcac968127e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8eb4a501-3f3d-4380-8875-402a1a74b04e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""527c274a-c4b4-4152-b31d-9841d76ab6a6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""693b121a-dd53-40d2-9d17-096fc041ed3f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4c12cf11-f124-48b2-81c0-1019b87ff340"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftStick"",
                    ""id"": ""d1f013a3-7c44-4a5e-8840-4db75ab63e4b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a76dd8da-c9a4-4a25-bcc2-2e813f84b80c"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c62e776f-96ba-4c5d-aac9-abbaeeab7f2b"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Stealth"",
            ""id"": ""e56be236-7c8b-4ac7-8ab2-0aee2d93b375"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d43f7d1a-6580-47a3-b2b4-571dc3cca402"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""f587965e-0d70-40ee-bb48-a3f9936bff54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AD"",
                    ""id"": ""5f8f4fa7-f9af-4ec0-ab96-c53eaa94b817"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0ff193ee-d63b-411f-a456-8048fad46328"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3f31ca75-b726-4549-98ee-e74979c04b86"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""6dde1ecc-7cf1-4a61-b049-d4a148c24811"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d781690f-678d-455b-86c2-acf1c8fc8538"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""eb77f94f-df6f-4a00-9bb5-e97ea6870578"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left Stick"",
                    ""id"": ""1f309e0e-5789-4ce1-aff1-8f0e2ff36361"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b2cb59c8-af4c-4cc1-88ea-25b10a3aaf29"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3953bd52-644d-43da-98d4-d5d5cc6eddfb"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1cc01356-b951-4e2f-90ae-100a651d82d4"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0648c4a4-32c7-476d-9fdb-bc673f335ca1"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Fighting"",
            ""id"": ""61a65b12-6507-44b7-9df7-1611f023b097"",
            ""actions"": [
                {
                    ""name"": ""ClickSliderLeft"",
                    ""type"": ""Button"",
                    ""id"": ""fe01edd5-86dc-4204-87b0-458bcf136b9f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ClickSliderRight"",
                    ""type"": ""Button"",
                    ""id"": ""0cc91afe-f0f9-4a08-98a4-ee7100456aab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""95024b80-985d-4399-88bc-6e02203f3df6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickSliderLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc4b6062-ab28-4c24-a495-d24f936c7e80"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickSliderLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""93e25e2c-06e0-4f25-81d8-1984ecaa64a1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickSliderRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d162583-5c8f-46d1-aab0-648332373b4c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickSliderRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // UpDown
            m_UpDown = asset.FindActionMap("UpDown", throwIfNotFound: true);
            m_UpDown_Move = m_UpDown.FindAction("Move", throwIfNotFound: true);
            // Stealth
            m_Stealth = asset.FindActionMap("Stealth", throwIfNotFound: true);
            m_Stealth_Move = m_Stealth.FindAction("Move", throwIfNotFound: true);
            m_Stealth_Escape = m_Stealth.FindAction("Escape", throwIfNotFound: true);
            // Fighting
            m_Fighting = asset.FindActionMap("Fighting", throwIfNotFound: true);
            m_Fighting_ClickSliderLeft = m_Fighting.FindAction("ClickSliderLeft", throwIfNotFound: true);
            m_Fighting_ClickSliderRight = m_Fighting.FindAction("ClickSliderRight", throwIfNotFound: true);
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

        // UpDown
        private readonly InputActionMap m_UpDown;
        private IUpDownActions m_UpDownActionsCallbackInterface;
        private readonly InputAction m_UpDown_Move;
        public struct UpDownActions
        {
            private @PlayerControls m_Wrapper;
            public UpDownActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_UpDown_Move;
            public InputActionMap Get() { return m_Wrapper.m_UpDown; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UpDownActions set) { return set.Get(); }
            public void SetCallbacks(IUpDownActions instance)
            {
                if (m_Wrapper.m_UpDownActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_UpDownActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_UpDownActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_UpDownActionsCallbackInterface.OnMove;
                }
                m_Wrapper.m_UpDownActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                }
            }
        }
        public UpDownActions @UpDown => new UpDownActions(this);

        // Stealth
        private readonly InputActionMap m_Stealth;
        private IStealthActions m_StealthActionsCallbackInterface;
        private readonly InputAction m_Stealth_Move;
        private readonly InputAction m_Stealth_Escape;
        public struct StealthActions
        {
            private @PlayerControls m_Wrapper;
            public StealthActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Stealth_Move;
            public InputAction @Escape => m_Wrapper.m_Stealth_Escape;
            public InputActionMap Get() { return m_Wrapper.m_Stealth; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(StealthActions set) { return set.Get(); }
            public void SetCallbacks(IStealthActions instance)
            {
                if (m_Wrapper.m_StealthActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_StealthActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_StealthActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_StealthActionsCallbackInterface.OnMove;
                    @Escape.started -= m_Wrapper.m_StealthActionsCallbackInterface.OnEscape;
                    @Escape.performed -= m_Wrapper.m_StealthActionsCallbackInterface.OnEscape;
                    @Escape.canceled -= m_Wrapper.m_StealthActionsCallbackInterface.OnEscape;
                }
                m_Wrapper.m_StealthActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Escape.started += instance.OnEscape;
                    @Escape.performed += instance.OnEscape;
                    @Escape.canceled += instance.OnEscape;
                }
            }
        }
        public StealthActions @Stealth => new StealthActions(this);

        // Fighting
        private readonly InputActionMap m_Fighting;
        private IFightingActions m_FightingActionsCallbackInterface;
        private readonly InputAction m_Fighting_ClickSliderLeft;
        private readonly InputAction m_Fighting_ClickSliderRight;
        public struct FightingActions
        {
            private @PlayerControls m_Wrapper;
            public FightingActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @ClickSliderLeft => m_Wrapper.m_Fighting_ClickSliderLeft;
            public InputAction @ClickSliderRight => m_Wrapper.m_Fighting_ClickSliderRight;
            public InputActionMap Get() { return m_Wrapper.m_Fighting; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(FightingActions set) { return set.Get(); }
            public void SetCallbacks(IFightingActions instance)
            {
                if (m_Wrapper.m_FightingActionsCallbackInterface != null)
                {
                    @ClickSliderLeft.started -= m_Wrapper.m_FightingActionsCallbackInterface.OnClickSliderLeft;
                    @ClickSliderLeft.performed -= m_Wrapper.m_FightingActionsCallbackInterface.OnClickSliderLeft;
                    @ClickSliderLeft.canceled -= m_Wrapper.m_FightingActionsCallbackInterface.OnClickSliderLeft;
                    @ClickSliderRight.started -= m_Wrapper.m_FightingActionsCallbackInterface.OnClickSliderRight;
                    @ClickSliderRight.performed -= m_Wrapper.m_FightingActionsCallbackInterface.OnClickSliderRight;
                    @ClickSliderRight.canceled -= m_Wrapper.m_FightingActionsCallbackInterface.OnClickSliderRight;
                }
                m_Wrapper.m_FightingActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ClickSliderLeft.started += instance.OnClickSliderLeft;
                    @ClickSliderLeft.performed += instance.OnClickSliderLeft;
                    @ClickSliderLeft.canceled += instance.OnClickSliderLeft;
                    @ClickSliderRight.started += instance.OnClickSliderRight;
                    @ClickSliderRight.performed += instance.OnClickSliderRight;
                    @ClickSliderRight.canceled += instance.OnClickSliderRight;
                }
            }
        }
        public FightingActions @Fighting => new FightingActions(this);
        public interface IUpDownActions
        {
            void OnMove(InputAction.CallbackContext context);
        }
        public interface IStealthActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnEscape(InputAction.CallbackContext context);
        }
        public interface IFightingActions
        {
            void OnClickSliderLeft(InputAction.CallbackContext context);
            void OnClickSliderRight(InputAction.CallbackContext context);
        }
    }
}