using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InputManager
{
    static InputManager _instance;
    Dictionary<Controls, InputBase> inputs = new Dictionary<Controls, InputBase>();
    Context currentContext = Context.General;

    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InputManager();
            }

            return _instance;
        }
    }

    public enum Context
    {
        General,
        OnFoot
    };

    public enum Controls
    {
        Menu,
        Forward,
        Backward,
        Left,
        Right,
        Jump,
        Fire,
        FireAlt,
        Reload,
        NextWeapon,
        PreviousWeapon
    }

    [System.Serializable]
    public class InputBase
    {
        public Context context;
        public bool rebindable;

        public InputBase()
        {

        }

        public InputBase(Context keyContext, bool canRebind = true)
        {
            context = keyContext;
            rebindable = canRebind;
        }
    }

    [System.Serializable]
    public class Key : InputBase
    {
        public KeyCode keyCode;

        public Key(KeyCode key, Context keyContext, bool canRebind = true)
        {
            keyCode = key;
            context = keyContext;
            rebindable = canRebind;
        }
    }

    [System.Serializable]
    public class Axis : InputBase
    {
        public string axis;

        public Axis(string axisName, Context keyContext, bool canRebind = true)
        {
            axis = axisName;
            context = keyContext;
            rebindable = canRebind;
        }
    }

    InputManager()
    {
        inputs.Add(Controls.Menu, new Key(KeyCode.Escape, Context.General, false));
        inputs.Add(Controls.Forward, new Key(KeyCode.W, Context.OnFoot));
        inputs.Add(Controls.Backward, new Key(KeyCode.S, Context.OnFoot));
        inputs.Add(Controls.Left, new Key(KeyCode.A, Context.OnFoot));
        inputs.Add(Controls.Right, new Key(KeyCode.D, Context.OnFoot));
        inputs.Add(Controls.Jump, new Key(KeyCode.Space, Context.OnFoot));
        inputs.Add(Controls.Fire, new Key(KeyCode.Mouse0, Context.OnFoot));
        inputs.Add(Controls.FireAlt, new Key(KeyCode.Mouse1, Context.OnFoot));
        inputs.Add(Controls.Reload, new Key(KeyCode.R, Context.OnFoot));
        inputs.Add(Controls.NextWeapon, new Axis("Mouse ScrollWheel", Context.OnFoot));
        //inputs.Add(Controls.PreviousWeapon, new Key(KeyCode.Mouse1, Context.OnFoot));
    }
    
    //Return the key bound the the control given
    public Key GetInput(Controls control)
    {
        return (Key)inputs[control];
    }

    //Return all keys bound. Position in array matches enum value (e.g. Menu is 0)
    public InputBase[] GetInputs()
    {
        InputBase[] keys = new InputBase[inputs.Count];
        inputs.Values.CopyTo(keys, 0);

        return keys;
    }

    //Bind a key to a control with a given context
    public void SetInput(Controls control, KeyCode key, Context keyContext)
    {
        inputs[control] = new Key(key, keyContext);
    }

    //Get all the controls in a given context
    public Controls[] GetControls(Context context)
    {
        Dictionary<Controls, InputBase> contextInputs = inputs.Where(key => key.Value.context == context).ToDictionary(x => x.Key, x => x.Value);
        Controls[] controls = new Controls[contextInputs.Count];
        contextInputs.Keys.CopyTo(controls, 0);

        return controls;
    }

    //Set all the controls using an array of keys. Position in array matches enum value (e.g. Menu is 0)
    public void SetControls(InputBase[] keys)
    {
        Dictionary<Controls, InputBase> newInputs = new Dictionary<Controls, InputBase>();

        for (int i = 0; i < keys.Length; i++)
        {
            newInputs.Add((Controls)i, keys[i]);
        }

        inputs = newInputs;
    }

    public bool GetKey(Controls control)
    {
        return Input.GetKey(GetInput(control).keyCode);
    }

    public bool GetKeyDown(Controls control)
    {
        return Input.GetKeyDown(GetInput(control).keyCode);
    }

    public bool GetKeyUp(Controls control)
    {
        return Input.GetKeyUp(GetInput(control).keyCode);
    }

    public Vector2 GetMouseAxes()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    public float GetMouseWheel()
    {
        return Input.GetAxis("Mouse ScrollWheel");
    }
}
