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
        public  System.Windows.Forms.Keys keys;
        public float timer;
        public System.Windows.Forms.Keys keyModifier;

        public Actions() {

        }
        public Actions(string type, System.Windows.Forms.Keys keys, System.Windows.Forms.Keys modifier, float timer) {
            // TODO: Complete member initialization
            this.type = type;
            this.keys = keys;
            this.timer = timer;
            this.keyModifier = modifier;
        }

        public Actions Clone()
        {
            return new Actions(type, keys, keyModifier, timer);
        }

        public bool IsValid
        {
            get { return type == "Key press" && keys != Keys.None || type == "Timer" && timer > float.Epsilon; }
        }

        public override string ToString() {
            switch (type) {
                case "Key press":
                    return "Key press : " + (keyModifier != Keys.None ? (keyModifier.ToString() + " + "): string.Empty) + keys.ToString();
                case "Timer":
                    return "Timer : " + timer.ToString() + " secs";
                default:
                    return "Error : Unknown event";
            }
        }


        public void perform() {
            switch (type) {
                case "Key press":
                    inputSimulator.Keyboard.KeyDown((VirtualKeyCode)keys);
                    Thread.Sleep(100);
                    inputSimulator.Keyboard.KeyUp((VirtualKeyCode)keys);
                    break;
                case "Timer":
                    Thread.Sleep((int)(timer*1000));
                    break;
            }
        }
    }
}
