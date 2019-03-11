using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using WindowsInput;
using WindowsInput.Native;

namespace Vocals
{
    [Serializable]
    public class Actions
    {
        private static InputSimulator inputSimulator = new InputSimulator();

        public string type;
        public Keys keys;
        public float timer;
        public Keys keyModifier;
        public Keys pre;

        public Actions() {

        }
        public Actions(string type, float timer, Keys modifier, Keys pre, Keys keys) {
            // TODO: Complete member initialization
            this.type = type;
            this.timer = timer;
            this.keyModifier = modifier;
            this.pre = pre;
            this.keys = keys;
        }

        public Actions Clone()
        {
            return new Actions(type, timer, keyModifier, pre, keys);
        }

        public bool IsValid
        {
            get { return type == "Key press" && keys != Keys.None || type == "Timer" && timer > float.Epsilon; }
        }

        public override string ToString() {
            string text;
            switch (type) {
                case "Key press":
                    text = "Key press : ";
                    if (keyModifier != Keys.None) text += keyModifier.ToString() + " ";
                    if (pre != Keys.None) text += pre.ToString() + " ";
                    text += keys.ToString();
                    return text;
                case "Timer":
                    return "Timer : " + timer.ToString() + " secs";
                default:
                    return "Error : Unknown event";
            }
        }


        public void perform() {
            switch (type) {
                case "Key press":
                    if (keyModifier != Keys.None)
                    {
                        if (keyModifier.HasFlag(Keys.Control)) inputSimulator.Keyboard.KeyDown((VirtualKeyCode)Keys.ControlKey);
                        if (keyModifier.HasFlag(Keys.Shift)) inputSimulator.Keyboard.KeyDown((VirtualKeyCode)Keys.ShiftKey);
                        if (keyModifier.HasFlag(Keys.Alt)) inputSimulator.Keyboard.KeyDown((VirtualKeyCode)Keys.LMenu);
                        Thread.Sleep(10);
                    }
                    if (pre != Keys.None)
                    {
                        inputSimulator.Keyboard.KeyDown((VirtualKeyCode)pre);
                        Thread.Sleep(10);
                    }
                    inputSimulator.Keyboard.KeyDown((VirtualKeyCode)keys);
                    Thread.Sleep(50);
                    inputSimulator.Keyboard.KeyUp((VirtualKeyCode)keys);
                    if (pre != Keys.None)
                    {
                        Thread.Sleep(10);
                        inputSimulator.Keyboard.KeyUp((VirtualKeyCode)pre);
                    }
                    if (keyModifier != Keys.None)
                    {
                        Thread.Sleep(10);
                        if (keyModifier.HasFlag(Keys.Control)) inputSimulator.Keyboard.KeyUp((VirtualKeyCode)Keys.ControlKey);
                        if (keyModifier.HasFlag(Keys.Shift)) inputSimulator.Keyboard.KeyUp((VirtualKeyCode)Keys.ShiftKey);
                        if (keyModifier.HasFlag(Keys.Alt)) inputSimulator.Keyboard.KeyUp((VirtualKeyCode)Keys.LMenu);
                    }
                    Thread.Sleep(50);
                    break;
                case "Timer":
                    Thread.Sleep((int)(timer*1000));
                    break;
            }
        }
    }
}
