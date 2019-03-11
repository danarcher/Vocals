using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using Vocals.InternalClasses;
using System.Xml.Serialization;


//TODO Corriger 9/PGUP
//TODO : Retour mp3
//TODO : Resize
//TODO : Add random phrases
//TODO : Add listen to worda

namespace Vocals
{
    public partial class Form1 : Form
    {

        protected delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("user32.dll")]
        protected static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        [DllImport("user32.dll")]
        protected static extern bool IsWindowVisible(IntPtr hWnd);

        List<string> myWindows;
        List<Profile> profileList;
        IntPtr winPointer;

        SpeechRecognitionEngine speechEngine;

        Options currentOptions;

        private GlobalHotkey ghk;

        public Form1()
        {
            currentOptions = new Options();

            InitializeComponent();

            myWindows = new List<string>();
            RefreshProcessesList();

            FetchProfiles();

            ghk = new GlobalHotkey(0x0004, Keys.None, this);

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Reflection.AssemblyName assemblyName = assembly.GetName();
            Version version = assemblyName.Version;
            this.Text += " version : " + version.ToString();

            RefreshSettings();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0312:
                    OnHotKeyPressed();
                    break;
            }
            base.WndProc(ref m);
        }

        public void OnHotKeyPressed()
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SpeakAsync(currentOptions.answer);
            richTextBox.AppendText("Hotkey recognition is not implemented.\r\n");
            // TODO: handle this again as we deliberately broke it
        }

        public void RefreshProcessesList()
        {
            EnumWindows(new EnumWindowsProc(EnumTheWindows), IntPtr.Zero);
            processComboBox.DataSource = null;
            processComboBox.DataSource = myWindows;
        }

        private void FetchProfiles()
        {
            string dir = @"";
            string serializationFile = Path.Combine(dir, "profiles.vd");
            string xmlSerializationFile = Path.Combine(dir, "profiles_xml.vc");
            try
            {
                Stream xmlStream = File.Open(xmlSerializationFile, FileMode.Open);
                XmlSerializer reader = new XmlSerializer(typeof(List<Profile>));
                profileList = (List<Profile>)reader.Deserialize(xmlStream);
                xmlStream.Close();
            }
            catch
            {
                try
                {
                    Stream stream = File.Open(serializationFile, FileMode.Open);
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    profileList = (List<Profile>)(bformatter.Deserialize(stream));
                    stream.Close();
                }
                catch
                {
                    profileList = new List<Profile>();
                }
            }
            profileComboBox.DataSource = profileList;
        }

        private void StartSpeechEngine()
        {
            if (speechEngine != null)
            {
                richTextBox.AppendText("Speech recognition engine is already started\n");
                return;
            }

            richTextBox.AppendText("Starting speech recognition engine\n");
            RecognizerInfo info = null;

            //Use system locale language if no language option can be retrieved
            if (currentOptions.language == null)
            {
                currentOptions.language = System.Globalization.CultureInfo.CurrentUICulture.DisplayName;
            }

            foreach (RecognizerInfo ri in SpeechRecognitionEngine.InstalledRecognizers())
            {
                if (ri.Culture.DisplayName.Equals(currentOptions.language))
                {
                    info = ri;
                    break;
                }
            }

            if (info == null && SpeechRecognitionEngine.InstalledRecognizers().Count != 0)
            {
                RecognizerInfo ri = SpeechRecognitionEngine.InstalledRecognizers()[0];
                info = ri;
            }

            if (info != null)
            {
                richTextBox.AppendText("Setting VR engine language to " + info.Culture.DisplayName + "\n");
            }
            else
            {
                richTextBox.AppendText("Could not find any installed recognizers\n");
                richTextBox.AppendText("(The developer is trying to find a fix for this specific error.)\n");
                return;
            }

            speechEngine = new SpeechRecognitionEngine(info);
            speechEngine.SpeechRecognized += OnSpeechRecognized;
            speechEngine.AudioLevelUpdated += OnAudioLevelUpdated;

            try
            {
                speechEngine.SetInputToDefaultAudioDevice();
            }
            catch (InvalidOperationException)
            {
                richTextBox.AppendText("No microphones were found\n");
            }

            speechEngine.MaxAlternates = 3;

            var profile = (Profile)profileComboBox.SelectedItem;
            if (profile != null && profile.commandList.Count > 0)
            {
                Choices myWordChoices = new Choices();

                foreach (Command c in profile.commandList)
                {
                    string[] commandList = c.commandString.Split(';');
                    foreach (string s in commandList)
                    {
                        string correctedWord;
                        correctedWord = s.Trim().ToLower();
                        if (correctedWord != null && correctedWord != "")
                        {
                            myWordChoices.Add(correctedWord);
                        }
                    }
                }

                GrammarBuilder builder = new GrammarBuilder();
                builder.Culture = speechEngine.RecognizerInfo.Culture;
                builder.Append(myWordChoices);
                Grammar mygram = new Grammar(builder);

                //speechEngine.UnloadAllGrammars();
                speechEngine.LoadGrammar(mygram);

                ApplyRecognitionSensibility();
                speechEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

        private void StopSpeechEngine()
        {
            if (speechEngine == null)
            {
                return;
            }

            richTextBox.AppendText("Stopping speech recognition engine\r\n");
            speechEngine.SpeechRecognized -= OnSpeechRecognized;
            speechEngine.AudioLevelUpdated -= OnAudioLevelUpdated;
            speechEngine.RecognizeAsyncCancel();
            speechEngine.UnloadAllGrammars();
            richTextBox.AppendText("Disposing of speech recognition\r\n");
            speechEngine.Dispose();
            speechEngine = null;
            richTextBox.AppendText("Speech recognition is stopped\r\n");
        }

        void OnAudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
            if (speechEngine != null)
            {
                int val = (int)(10 * Math.Sqrt(e.AudioLevel));
                progressBar1.Value = val;
            }
        }

        void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            richTextBox.AppendText("Command recognized \"" + e.Result.Text + "\" with confidence of : " + e.Result.Confidence + "\n");

            Profile p = (Profile)profileComboBox.SelectedItem;
            if (p != null)
            {
                foreach (Command c in p.commandList)
                {
                    string[] multiCommands = c.commandString.Split(';');
                    foreach (string s in multiCommands)
                    {
                        string correctedWord = s.Trim().ToLower();
                        if (correctedWord.Equals(e.Result.Text))
                        {
                            c.Perform(winPointer);
                            break;
                        }
                    }
                }
            }
        }

        protected bool EnumTheWindows(IntPtr hWnd, IntPtr lParam)
        {
            int size = GetWindowTextLength(hWnd);
            if (size++ > 0 && IsWindowVisible(hWnd))
            {
                StringBuilder sb = new StringBuilder(size);
                GetWindowText(hWnd, sb, size);
                myWindows.Add(sb.ToString());
            }
            return true;
        }

        private void AddProfile_Click(object sender, EventArgs e)
        {
            var dialog = new FormNewProfile();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.profileName))
                {
                    Profile p = new Profile(dialog.profileName);
                    profileList.Add(p);
                    profileComboBox.DataSource = null;
                    profileComboBox.DataSource = profileList;
                    profileComboBox.SelectedItem = p;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            StopSpeechEngine();

            Profile p = (Profile)profileComboBox.SelectedItem;
            if (p != null)
            {
                commandListBox.DataSource = null;
                commandListBox.DataSource = p.commandList;
            }
        }

        private void AddCommandButton_Click(object sender, EventArgs e)
        {
            StopSpeechEngine();
            FormCommand formCommand = new FormCommand();
            if (formCommand.ShowDialog() == DialogResult.OK)
            {
                Profile p = (Profile)profileComboBox.SelectedItem;
                if (p != null)
                {
                    if (!string.IsNullOrEmpty(formCommand.commandString))
                    {
                        Command c;
                        c = new Command(formCommand.commandString, formCommand.actionList, formCommand.answering, formCommand.answeringString, formCommand.answeringSound, formCommand.answeringSoundPath);
                        p.addCommand(c);
                        commandListBox.DataSource = null;
                        commandListBox.DataSource = p.commandList;
                    }
                }
            }
        }

        private void ProcessComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Process[] pTab = Process.GetProcesses();
            for (int i = 0; i < pTab.Length; i++)
            {
                if (pTab[i] != null && processComboBox.SelectedItem != null)
                {
                    if (pTab[i].MainWindowTitle.Equals(processComboBox.SelectedItem.ToString()))
                    {
                        winPointer = pTab[i].MainWindowHandle;
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DeleteProfileButton_Click(object sender, EventArgs e)
        {
            StopSpeechEngine();
            var profile = (Profile)profileComboBox.SelectedItem;
            profileList.Remove(profile);
            profileComboBox.DataSource = null;
            profileComboBox.DataSource = profileList;
            if (profileList.Count == 0)
            {
                commandListBox.DataSource = null;
            }
            else
            {
                profileComboBox.SelectedItem = profileList[0];
            }
        }

        private void DeleteCommandButton_Click(object sender, EventArgs e)
        {
            StopSpeechEngine();
            var profile = (Profile)profileComboBox.SelectedItem;
            var command = (Command)commandListBox.SelectedItem;
            if (profile != null && command != null)
            {
                profile.commandList.Remove(command);
                commandListBox.DataSource = null;
                commandListBox.DataSource = profile.commandList;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopSpeechEngine();

            string dir = @"";
            string serializationFile = Path.Combine(dir, "profiles.vd");
            string xmlSerializationFile = Path.Combine(dir, "profiles_xml.vc");
            try
            {
                Stream stream = File.Open(serializationFile, FileMode.Create);
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, profileList);
                stream.Close();

                try
                {
                    Stream xmlStream = File.Open(xmlSerializationFile, FileMode.Create);
                    XmlSerializer writer = new XmlSerializer(typeof(List<Profile>));
                    writer.Serialize(xmlStream, profileList);
                    xmlStream.Close();
                }
                catch (Exception)
                {
                    DialogResult res = MessageBox.Show("The profiles_xml.vc file is in use by another process. Do you want to leave without saving?", "Cannot Save Profile", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (res == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }

            }
            catch (Exception)
            {
                DialogResult res = MessageBox.Show("The profiles.vd file is in use by another process. Do you want to leave without saving?", "Cannot Save Profile", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (res == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void DeleteCommand_Click(object sender, EventArgs e)
        {
            StopSpeechEngine();
            var profile = (Profile)(profileComboBox.SelectedItem);
            if (profile != null)
            {
                var command = (Command)commandListBox.SelectedItem;
                if (command != null)
                {
                    profile.commandList.Remove(command);
                    commandListBox.DataSource = null;
                    commandListBox.DataSource = profile.commandList;
                }
            }
        }

        private void EditCommandButton_Click(object sender, EventArgs e)
        {
            StopSpeechEngine();
            var profile = (Profile)profileComboBox.SelectedItem;
            var command = (Command)commandListBox.SelectedItem;
            if (profile != null && command != null)
            {
                using (var dialog = new FormCommand(command))
                {
                    if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(dialog.commandString))
                    {
                        command.commandString = dialog.commandString;
                        command.actionList = dialog.actionList;
                        command.answering = dialog.answering;
                        command.answeringString = dialog.answeringString ?? string.Empty;
                        command.answeringSound = dialog.answeringSound;
                        command.answeringSoundPath = dialog.answeringSoundPath ?? string.Empty;

                        commandListBox.DataSource = null;
                        commandListBox.DataSource = profile.commandList;
                    }
                }
            }
        }

        private void AdvancedSettings_Click(object sender, EventArgs e)
        {
            using (var dialog = new FormOptions())
            {
                dialog.options = new Options(currentOptions);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    currentOptions = dialog.options;
                    RefreshSettings();
                }
            }
        }

        private void RefreshSettings()
        {
            ApplyModificationToGlobalHotKey();
            ApplyToggleListening();
            ApplyRecognitionSensibility();
            currentOptions.save();
        }

        private void ApplyModificationToGlobalHotKey()
        {
            if (currentOptions.key == Keys.Shift ||
                currentOptions.key == Keys.ShiftKey ||
                currentOptions.key == Keys.LShiftKey ||
                currentOptions.key == Keys.RShiftKey)
            {
                ghk.modifyKey(0x0004, Keys.None);
            }
            else if (currentOptions.key == Keys.Control ||
                currentOptions.key == Keys.ControlKey ||
                currentOptions.key == Keys.LControlKey ||
                currentOptions.key == Keys.RControlKey)
            {
                ghk.modifyKey(0x0002, Keys.None);

            }
            else if (currentOptions.key == Keys.Alt)
            {
                ghk.modifyKey(0x0002, Keys.None);
            }
            else
            {
                ghk.modifyKey(0x0000, currentOptions.key);
            }
        }

        private void ApplyToggleListening()
        {
            if (currentOptions.toggleListening)
            {
                try
                {
                    ghk.register();
                }
                catch
                {
                    richTextBox.AppendText("Couldn't register hotkey.\r\n");
                }
            }
            else
            {
                try
                {
                    ghk.unregister();
                }
                catch
                {
                    richTextBox.AppendText("Couldn't unregister hotkey.\r\n");
                }
            }
        }

        private void ApplyRecognitionSensibility()
        {
            if (speechEngine != null)
            {
                speechEngine.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", currentOptions.threshold);
            }
        }

        private void RefreshProcessListButton_Click(object sender, EventArgs e)
        {
            myWindows.Clear();
            RefreshProcessesList();
        }

        private void StartRecognition_Click(object sender, EventArgs e)
        {
            StartSpeechEngine();
        }
    }
}
