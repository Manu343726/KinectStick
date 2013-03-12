using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KinectStick.Commands;
using KinectStick;
using WindowsInput.Native;

namespace InstructionEditor
{
    public partial class Form1 : Form
    {

        #region GUI string constants
        public const String COMMANDTYPESTRING_JOYSTICKAXIS = "Joystick (Eje)";
        public const String COMMANDTYPESTRING_JOYSTICKBUTTON_PRESS = "Joystick (Pulsar)";
        public const String COMMANDTYPESTRING_JOYSTICKBUTTON_RELEASE = "Joystick (Soltar)";
        public const String COMMANDTYPESTRING_JOYSTICKBUTTON_HOLD = "Joystick (Mantener)";
        public const String COMMANDTYPESTRING_KEYBOARD_PRESS = "Teclado (Pulsar)";
        public const String COMMANDTYPESTRING_KEYBOARD_RELEASE = "Teclado (Soltar)";
        public const String COMMANDTYPESTRING_KEYBOARD_HOLD = "Teclado (Mantener)";

        public const String JOYSTICKAXISSTRING_XAXIS = "Eje X";
        public const String JOYSTICKAXISSTRING_YAXIS = "Eje Y";
        public const String JOYSTICKAXISSTRING_ZAXIS = "Eje Z";
        public const String JOYSTICKAXISSTRING_XROTATION = "Rotación X";
        public const String JOYSTICKAXISSTRING_YROTATION = "Rotación Y";
        public const String JOYSTICKAXISSTRING_ZROTATION = "Rotación Z";
        public const String JOYSTICKAXISSTRING_SLIDER = "Slider";
        public const String JOYSTICKAXISSTRING_DIAL = "Dial";

        public static String GetString(InstructionData_CommandType type)
        {
            switch (type)
            {
                case InstructionData_CommandType.JOYSTICKAXIS:
                    return COMMANDTYPESTRING_JOYSTICKAXIS;
                case InstructionData_CommandType.JOYSTICKBUTTON_PRESS:
                    return COMMANDTYPESTRING_JOYSTICKBUTTON_PRESS;
                case InstructionData_CommandType.JOYSTICKBUTTON_HOLD:
                    return COMMANDTYPESTRING_JOYSTICKBUTTON_HOLD;
                case InstructionData_CommandType.JOYSTICKBUTTON_RELEASE:
                    return COMMANDTYPESTRING_JOYSTICKBUTTON_RELEASE;
                case InstructionData_CommandType.KEYBOARD_PRESS:
                    return COMMANDTYPESTRING_KEYBOARD_PRESS;
                case InstructionData_CommandType.KEYBOARD_HOLD:
                    return COMMANDTYPESTRING_KEYBOARD_HOLD;
                case InstructionData_CommandType.KEYBOARD_RELEASE:
                    return COMMANDTYPESTRING_KEYBOARD_RELEASE;
                default:
                    return "";
            }
        }

        public static String GetString(JoystickDataType type)
        {
            switch (type)
            {
                case JoystickDataType.XAXIS:
                    return JOYSTICKAXISSTRING_XAXIS;
                case JoystickDataType.YAXIS:
                    return JOYSTICKAXISSTRING_YAXIS;
                case JoystickDataType.ZAXIS:
                    return JOYSTICKAXISSTRING_ZAXIS;
                case JoystickDataType.XROTATION:
                    return JOYSTICKAXISSTRING_XROTATION;
                case JoystickDataType.YROTATION:
                    return JOYSTICKAXISSTRING_YROTATION;
                case JoystickDataType.ZROTATION:
                    return JOYSTICKAXISSTRING_ZROTATION;
                case JoystickDataType.SLIDER:
                    return JOYSTICKAXISSTRING_SLIDER;
                case JoystickDataType.DIAL:
                    return JOYSTICKAXISSTRING_DIAL;
                default:
                    return "";
            }
        }
        #endregion

        #region private fields
        public Profile profile;
        private int selectedIndex;
        private InstructionData selectedInstruction;
        private bool saved;
        private String profileFilePath;
        private String profileCompilationFilePath;

        private KinectStickEngine kinectStick;
        private CompiledProfile compiledProfile;
        #endregion

        public Form1()
        {
            InitializeComponent();

            LoadProfile("Profile1");
            InitGUI();

            kinectStick = null;
            compiledProfile = null;
        }

        private void LoadProfile(String name)
        {
            profile = new Profile(name);
            selectedIndex = -1;
            saved = true;
            profileFilePath = "";
            profileCompilationFilePath = "";
        }

        private void InitGUI()
        {
            FillCommandTypeCombo();
            groupBox1.Visible = false;
            notify.Visible = true;
        }

        private void FillCommandTypeCombo()
        {
            commandTypeCombo.Items.Clear();

            commandTypeCombo.Items.Add(COMMANDTYPESTRING_JOYSTICKAXIS);
            commandTypeCombo.Items.Add(COMMANDTYPESTRING_JOYSTICKBUTTON_PRESS);
            commandTypeCombo.Items.Add(COMMANDTYPESTRING_JOYSTICKBUTTON_HOLD);
            commandTypeCombo.Items.Add(COMMANDTYPESTRING_JOYSTICKBUTTON_RELEASE);
            commandTypeCombo.Items.Add(COMMANDTYPESTRING_KEYBOARD_PRESS);
            commandTypeCombo.Items.Add(COMMANDTYPESTRING_KEYBOARD_HOLD);
            commandTypeCombo.Items.Add(COMMANDTYPESTRING_KEYBOARD_RELEASE);
        }

        private void FillGUIData(int instructionIndex)
        {
            instructionText.Text = profile[instructionIndex].phrase;
            commandTypeCombo.SelectedIndex = (int)profile[instructionIndex].commandType;

            if (profile[instructionIndex].commandType >= InstructionData_CommandType.KEYBOARD_PRESS)
            {
                SetGUIMode_KEYBOARD();

                controlCheckBox.Checked = profile[instructionIndex].control;
                shiftCheckBox.Checked = profile[instructionIndex].shift;
                valueTypeCombo.SelectedIndex = profile[instructionIndex].value;
            }
            else if (profile[instructionIndex].commandType == InstructionData_CommandType.JOYSTICKAXIS)
            {
                SetGUIMode_JOYSTICK_AXIS();

                valueTypeCombo.SelectedIndex = profile[instructionIndex].axis;
                joystickValue.Value = profile[instructionIndex].value;
            }
            else
            {
                SetGUIMode_JOYSTICK_BUTTON();

                valueTypeCombo.SelectedIndex = profile[instructionIndex].value;
            }
        }

        private void FillValueTypeCombo_KEYBOARD()
        {
            valueTypeCombo.Items.Clear();

            foreach (String key in Enum.GetNames(typeof(VirtualKeyCode)))
                valueTypeCombo.Items.Add(key);
        }

        private void FillValueTypeCombo_JOYSTICK_AXIS()
        {
            valueTypeCombo.Items.Clear();

            valueTypeCombo.Items.Add(JOYSTICKAXISSTRING_XAXIS);
            valueTypeCombo.Items.Add(JOYSTICKAXISSTRING_YAXIS);
            valueTypeCombo.Items.Add(JOYSTICKAXISSTRING_ZAXIS);
            valueTypeCombo.Items.Add(JOYSTICKAXISSTRING_XROTATION);
            valueTypeCombo.Items.Add(JOYSTICKAXISSTRING_YROTATION);
            valueTypeCombo.Items.Add(JOYSTICKAXISSTRING_ZROTATION);
            valueTypeCombo.Items.Add(JOYSTICKAXISSTRING_SLIDER);
            valueTypeCombo.Items.Add(JOYSTICKAXISSTRING_DIAL);
        }

        private void FillValueTypeCombo_JOYSTICK_BUTTON()
        {
            valueTypeCombo.Items.Clear();

            for (int i = JoystickCommandData.JOYSTICKCOMMANDDATA_MINBUTTONINDEX; i <= JoystickCommandData.JOYSTICKCOMMANDDATA_MAXBUTTONINDEX; ++i)
                valueTypeCombo.Items.Add("Botón_" + i.ToString());
        }

        private void FillProfileList()
        {
            profileList.Items.Clear();

            for (int i = 0; i < profile.InstructionsCount; ++i)
            {
                profileList.Items.Add("");
                UpdateProfileListText(i);
            }
        }

        private void SetGUIMode_JOYSTICK_AXIS()
        {
            DisableControlShiftControls();
            EnableJoystickAxisControls();
            FillValueTypeCombo_JOYSTICK_AXIS();
        }

        private void SetGUIMode_JOYSTICK_BUTTON()
        {
            DisableControlShiftControls();
            DisableJoystickAxisControls();
            FillValueTypeCombo_JOYSTICK_BUTTON();
        }

        private void SetGUIMode_KEYBOARD()
        {
            EnableControlShiftControls();
            DisableJoystickAxisControls();
            EnableControlShiftControls();
            FillValueTypeCombo_KEYBOARD();
        }

        private void EnableJoystickAxisControls()
        {
            joystickValue.Enabled = true;
            joystickValueLabel.Enabled = true;
            joystickValuePercentLabel.Enabled = true;
        }

        private void DisableJoystickAxisControls()
        {
            joystickValue.Enabled = false;
            joystickValueLabel.Enabled = false;
            joystickValuePercentLabel.Enabled = false;
        }

        private void EnableControlShiftControls()
        {
            controlCheckBox.Enabled=true;
            shiftCheckBox.Enabled=true;
        }

        private void DisableControlShiftControls()
        {
            controlCheckBox.Enabled = false;
            shiftCheckBox.Enabled = false;
        }

        private void UpdateProfileListText(int index)
        {
            InstructionData instruction;

            if (index == selectedIndex)
                instruction = selectedInstruction;
            else
                instruction = profile[index];

            profileList.Items[index].SubItems.Clear();
            profileList.Items[index].SubItems[0].Text = instruction.phrase;
            profileList.Items[index].SubItems.Add(GetString(instruction.commandType));

            if (instruction.commandType == InstructionData_CommandType.JOYSTICKAXIS)
                profileList.Items[index].SubItems.Add(GetString((JoystickDataType)instruction.axis));
            else if (instruction.commandType <= InstructionData_CommandType.JOYSTICKBUTTON_RELEASE)
                profileList.Items[index].SubItems.Add("Botón_" + instruction.value.ToString());
            else
                profileList.Items[index].SubItems.Add(Enum.GetNames(typeof(VirtualKeyCode))[instruction.value]);

            if (instruction.commandType == InstructionData_CommandType.JOYSTICKAXIS)
                profileList.Items[index].SubItems.Add(instruction.value + "%");
            else if (instruction.commandType <= InstructionData_CommandType.JOYSTICKBUTTON_RELEASE)
                profileList.Items[index].SubItems.Add("");
            else
                if (instruction.control && instruction.shift)
                    profileList.Items[index].SubItems.Add("CONTROL + SHIFT");
                else if (instruction.control)
                    profileList.Items[index].SubItems.Add("CONTROL");
                else if (instruction.shift)
                    profileList.Items[index].SubItems.Add("SHIFT");
                else
                    profileList.Items[index].SubItems.Add("");
        }

        private void UpdateProfileListText_PHRASE()
        {
            profileList.Items[selectedIndex].Text = selectedInstruction.phrase;
        }

        private void UpdateProfileListText_COMMANDTYPE()
        {
            profileList.Items[selectedIndex].SubItems[1].Text = GetString(selectedInstruction.commandType);
        }

        private void UpdateProfileListText_VALUETYPE()
        {
            if (selectedInstruction.commandType == InstructionData_CommandType.JOYSTICKAXIS)
                profileList.Items[selectedIndex].SubItems[2].Text = GetString((JoystickDataType)selectedInstruction.axis);
            else if (selectedInstruction.commandType <= InstructionData_CommandType.JOYSTICKBUTTON_RELEASE)
                profileList.Items[selectedIndex].SubItems[2].Text = "Botón_" + selectedInstruction.value.ToString();
            else
                profileList.Items[selectedIndex].SubItems[2].Text = Enum.GetNames(typeof(Keys))[selectedInstruction.value];
        }

        private void UpdateProfileListText_VALUE()
        {
            if (selectedInstruction.commandType == InstructionData_CommandType.JOYSTICKAXIS)
                profileList.Items[selectedIndex].SubItems[3].Text = selectedInstruction.value + "%";
            else if (selectedInstruction.commandType <= InstructionData_CommandType.JOYSTICKBUTTON_RELEASE)
                profileList.Items[selectedIndex].SubItems[3].Text = "";
            else
                if (selectedInstruction.control && selectedInstruction.shift)
                    profileList.Items[selectedIndex].SubItems[3].Text = "CONTROL + SHIFT";
                else if (selectedInstruction.control)
                    profileList.Items[selectedIndex].SubItems[3].Text = "CONTROL";
                else if (selectedInstruction.shift)
                    profileList.Items[selectedIndex].SubItems[3].Text = "SHIFT";
                else
                    profileList.Items[selectedIndex].SubItems[3].Text = "";
        }

        private void profileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (profileList.SelectedIndices.Count > 0)
            {
                selectedIndex = profileList.SelectedIndices[0];
                selectedInstruction = profile[selectedIndex];
                FillGUIData(selectedIndex);
            }
        }

        private void instructionText_TextChanged(object sender, EventArgs e)
        {
            int conflictIndex;

            if (selectedIndex >= 0)
            {
                conflictIndex=profile.IsValidPhrase(instructionText.Text, selectedIndex);

                if (conflictIndex >= 0)
                    MessageBox.Show("La frase introducida invalida o es invalidada por la frase de la instrucción " + conflictIndex + ": " + profile[conflictIndex].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    selectedInstruction.SetPhrase(instructionText.Text);
                    UpdateProfileListText(selectedIndex);
                    UpdateProfile();
                }
            }
        }

        private void newInstructionButton_Click(object sender, EventArgs e)
        {
            selectedIndex = profile.AddNewInstruction();
            profileList.Items.Add("");
            UpdateProfileListText(selectedIndex);
            saved = false;

            if (profileList.SelectedIndices.Count > 0)
                profileList.SelectedIndices.Clear();

            //profileList.SelectedIndices.Add(index);
        }

        private void commandTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstructionData_CommandType selectedtype = (InstructionData_CommandType)commandTypeCombo.SelectedIndex;

            if (selectedIndex >= 0 && selectedInstruction.commandType!=selectedtype)
            {
                selectedInstruction.commandType = selectedtype;
                UpdateProfileListText(selectedIndex);
                UpdateProfile();

                if (selectedtype == InstructionData_CommandType.JOYSTICKAXIS)
                {
                    SetGUIMode_JOYSTICK_AXIS();

                    valueTypeCombo.SelectedIndex = 0;
                }
                else if (selectedtype <= InstructionData_CommandType.JOYSTICKBUTTON_RELEASE)
                {
                    SetGUIMode_JOYSTICK_BUTTON();

                    valueTypeCombo.SelectedIndex = 0;
                }
                else
                {
                    SetGUIMode_KEYBOARD();

                    valueTypeCombo.SelectedIndex = 0;
                    controlCheckBox.Checked = false;
                    shiftCheckBox.Checked = false;
                }

                
            }
        }

        private void valueTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                if (selectedInstruction.commandType == InstructionData_CommandType.JOYSTICKAXIS)
                    selectedInstruction.axis = valueTypeCombo.SelectedIndex;
                else 
                    selectedInstruction.value = valueTypeCombo.SelectedIndex;

                UpdateProfileListText(selectedIndex);
                UpdateProfile();
            }
        }

        private void joystickValue_Scroll(object sender, EventArgs e)
        {
            if (selectedIndex >= 0 && selectedInstruction.commandType == InstructionData_CommandType.JOYSTICKAXIS)
            {
                selectedInstruction.value = joystickValue.Value;
                UpdateProfileListText(selectedIndex);
                UpdateProfile();
            }
        }

        private void controlCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedIndex >= 0 && selectedInstruction.commandType >= InstructionData_CommandType.KEYBOARD_PRESS)
            {
                selectedInstruction.control = controlCheckBox.Checked;
                UpdateProfileListText(selectedIndex);
                UpdateProfile();
            }
        }

        private void shiftCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedIndex >= 0 && selectedInstruction.commandType >= InstructionData_CommandType.KEYBOARD_PRESS)
            {
                selectedInstruction.shift = shiftCheckBox.Checked;
                UpdateProfileListText(selectedIndex);
                UpdateProfile();
            }
        }

        private void cargarPerfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadProfile();
            if (!groupBox1.Visible) groupBox1.Visible = true;
            groupBox1.Text = profile.Name + " (Edición)";
        }

        private void LoadProfile()
        {
            bool canceled = false;
            if (!saved)
            {
                switch (MessageBox.Show("El perfil '" + profile.Name + "' contiene cambios que no han sido guardados \n ¿Desea guardar antes de continuar?",
                   "Prefil no guardado", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        SaveProfile();
                        break;
                    case DialogResult.Cancel:
                        canceled = true;
                        break;
                }
            }

            if (!canceled && open.ShowDialog() == DialogResult.OK)
                if (ProfileFile.Load(open.FileName, out profile))
                {
                    statusLabel.Text = "Perfil cargado correctamente";
                    FillProfileList();
                    selectedIndex = -1;
                    profileList.SelectedIndices.Clear();
                    profileFilePath = open.FileName;
                    saved = true;
                    compilarToolStripMenuItem.Enabled = saved;
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al intentar cargar el perfil", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    statusLabel.Text = "Error al cargar";
                }
        }

        private void SaveProfile()
        {
            if (profileFilePath == "")
                if (save.ShowDialog() == DialogResult.OK)
                    profileFilePath = save.FileName;

            if (profileFilePath != "")
            {
                if (ProfileFile.Save(profileFilePath, profile))
                {
                    statusLabel.Text = "Elementos guardados";
                    saved = true;
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al intentar guardar el perfil", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    statusLabel.Text = "Error al guardar";
                }
            }

            compilarToolStripMenuItem.Enabled = saved;
        }

        private void guardarPrefilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProfile();
        }

        private void GuardarComoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            profileFilePath = "";
            SaveProfile();
        }

        private void UpdateProfile()
        {
            if (selectedIndex >= 0)
            {
                profile.SetInstruction(selectedIndex, selectedInstruction);
                saved = false;
                compilarToolStripMenuItem.Enabled = saved;
                statusLabel.Text = "Listo";
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private bool Salir()
        {
            if (!saved)
            {
                switch (MessageBox.Show("El perfil '" + profile.Name + "' contiene cambios que no han sido guardados \n ¿Desea guardar antes de continuar?",
                   "Prefil no guardado", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        SaveProfile();
                        return true;
                    case DialogResult.Cancel:
                        return false;
                    case System.Windows.Forms.DialogResult.No:
                        return true;
                    default:
                        return true;
                }
            }
            else
                return true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !Salir();
        }

        private void deleteInstructionButton_Click(object sender, EventArgs e)
        {
            int index;

            if (profileList.SelectedIndices.Count > 0)
                index = profileList.SelectedIndices[0];
            else
                if (selectedIndex >= 0)
                    index = selectedIndex;
                else
                    index = -1;

            if (index >= 0)
            {
                profile.DeleteInstruction(index);
                FillProfileList();
            }
        }

        private void nuevoPerfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!saved)
            {
                switch (MessageBox.Show("El perfil '" + profile.Name + "' contiene cambios que no han sido guardados \n ¿Desea guardar antes de continuar?",
                   "Prefil no guardado", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        SaveProfile();
                        profile = new Profile(InputBox.Show("Introduce el nombre del perfil:", "KinectStick - Nuevo perfil", "Perfil " + DateTime.Now.ToShortDateString()));
                        groupBox1.Visible = true;
                        groupBox1.Text = profile.Name + " (Edición)";
                        FillProfileList();
                        break;
                    case DialogResult.Cancel:
                        break;
                    case System.Windows.Forms.DialogResult.No:
                        profile = new Profile(InputBox.Show("Introduce el nombre del perfil:", "KinectStick - Nuevo perfil", "Perfil " + DateTime.Now.ToShortDateString()));
                        groupBox1.Visible = true;
                        groupBox1.Text = profile.Name + " (Edición)";
                        FillProfileList();
                        break;
                }
            }
        }

        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved)
            {
                if (compiledProfile == null)
                {
                    compiledProfile = ProfileCompiler.Compile(profile);

                    if (compiledProfile != null)
                    {
                        kinectStick = new KinectStickEngine(compiledProfile);

                        kinectStick.RecognizedInstruction += OnRecognizedInstruction;
                        kinectStick.RejectedInstruction += OnRejectedInstruction;
                        kinectStick.RecognizedPhrase += OnRecognizedPhrase;
                    }
                    else
                        MessageBox.Show("Error al compilar el perfil", "KinectStick - Compilación", MessageBoxButtons.OK, MessageBoxIcon.Error);        
                }
                else
                {
                    compiledProfile = ProfileCompiler.Compile(profile);

                    if (compiledProfile != null)
                        kinectStick.SetProfile(compiledProfile);
                    else
                        MessageBox.Show("Error al compilar el perfil", "KinectStick - Compilación", MessageBoxButtons.OK, MessageBoxIcon.Error);         
                }

                if(!kinectStick.Ready)
                    MessageBox.Show("Error al configurar el engine: Error de conexión con Kinect o fallo en la carga del motor de reconocimiento de voz", "KinectStick - Compilación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Perfil compilado correctamente", "KinectStick - Compilación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                compilarToolStripMenuItem.Enabled = false;
        }

        private void OnRecognizedPhrase(CompiledSpeechPhrase lastPhrase)
        {
            notify.ShowBalloonTip(3000, "KinectStick - Frase reconocida", "[" + lastPhrase.phrase + "]", ToolTipIcon.Warning);
        }

        private void OnRecognizedInstruction(String instructionPhrase, CompiledSpeechPhrase lastPhrase, Command command)
        {
            notify.ShowBalloonTip(3000, "KinectStick - Instrucción reconocida", "'" + instructionPhrase + "'", ToolTipIcon.Info);
            System.Media.SystemSounds.Beep.Play();
        }

        private void OnRejectedInstruction(String instructionPhrase, CompiledSpeechPhrase lastPhrase, Command command)
        {
            notify.ShowBalloonTip(3000, "KinectStick - Instrucción descartada", "'" + instructionPhrase + "'", ToolTipIcon.Error);
            System.Media.SystemSounds.Exclamation.Play();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (kinectStick != null) kinectStick.ResetInstructionPipeline();
        }
    }
}
