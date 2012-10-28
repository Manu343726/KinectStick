using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using WindowsInput;

namespace KinectStick.Commands
{
    public enum CommandType
    {
        JOYSTICK,
        KEYBOARD
    }

    public enum JoystickDataType
    {
        XAXIS,
        YAXIS,
        ZAXIS,
        XROTATION,
        YROTATION,
        ZROTATION,
        SLIDER,
        DIAL,
        BUTTON_PRESS,
        BUTTON_HOLD,
        BUTTON_RELEASE,
        UNKNOWN
    }

    public enum KeyboardCommandType
    {
        PRESS,
        HOLD,
        RELEASE
    }

    public struct JoystickCommandData
    {
        public const int JOYSTICKCOMMANDDATA_MAXBUTTONINDEX = vJoyWrapper.VJoyWrapper.VJOY_MAXBUTTONS - 1;
        public const int JOYSTICKCOMMANDDATA_MINBUTTONINDEX = 0;

        public int joystickID;
        public JoystickDataType type;
        public short value;
        public int button;

        public JoystickCommandData(int JoyID, JoystickDataType type, short value,int button=-1)
        {
            this.joystickID = JoyID;
            this.type = type;
            this.value = value;
            this.button = button;
        }
    }

    public struct KeyboardCommandData
    {
        public VirtualKeyCode key;
        public KeyboardCommandType type;
        public bool control;
        public bool shift;

        public KeyboardCommandData(VirtualKeyCode key, KeyboardCommandType type, bool control = false, bool shift = false)
        {
            this.key = key;
            this.type = type;
            this.control = control;
            this.shift = shift;
        }
    }

    public class Command
    {
        public CommandType type;
        public JoystickCommandData joydata;
        public KeyboardCommandData keydata;

        public Command(JoystickCommandData joydata)
        {
            type = CommandType.JOYSTICK;
            this.joydata = joydata;
            this.keydata = new KeyboardCommandData();
        }

        public Command(KeyboardCommandData keydata)
        {
            type = CommandType.KEYBOARD;
            this.keydata = keydata;
            this.joydata = new JoystickCommandData();
        }
    }
}
