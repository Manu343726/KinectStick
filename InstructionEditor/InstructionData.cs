using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KinectStick.Commands;
using KinectStick.Instructions;

using System.Collections;

namespace InstructionEditor
{
    public enum InstructionData_CommandType
    {
        JOYSTICKAXIS,
        JOYSTICKBUTTON_PRESS,
        JOYSTICKBUTTON_HOLD,
        JOYSTICKBUTTON_RELEASE,
        KEYBOARD_PRESS,
        KEYBOARD_HOLD,
        KEYBOARD_RELEASE
    }

    public struct InstructionData
    {
        public const char INSTRUCTION_PHRASE_SEPARATOR = ' ';

        private String lCasePhrase;
        public InstructionData_CommandType commandType;
        public int value;
        public int axis;
        public bool control;
        public bool shift;
        public bool isValid;

        public String phrase
        {
            get
            {
                return lCasePhrase;
            }
        }


        public void SetPhrase(String phrase)
        {
            lCasePhrase = phrase.ToLower();
        }

        //Constructor general:
        InstructionData(String phrase, InstructionData_CommandType type, int value, bool control = false, bool shift = false, bool isValid = false)
        {
            this.lCasePhrase = phrase.ToLower();
            this.commandType = type;
            this.value = value;
            this.control = control;
            this.shift = shift;
            this.isValid = isValid;
            this.axis = -1;
        }

        //Constructor para comandos de teclado:
        InstructionData(String phrase, InstructionData_CommandType type, int value, bool control, bool shift)
        {
            this.lCasePhrase = phrase.ToLower();
            this.commandType = type;
            this.control = control;
            this.shift = shift;
            this.value = value;

            this.axis = -1;
            this.isValid = true;
        }

        //Constructor para comandos de ejes de joystick:
        InstructionData(String phrase, InstructionData_CommandType type, int axis, int value)
        {
            this.lCasePhrase = phrase.ToLower();
            this.commandType = type;
            this.value = value;
            this.axis = axis;

            this.control = false;
            this.shift = false;
            this.isValid = true;
        }

        //Constructor para comandos de botones del joystick:
        InstructionData(String phrase, InstructionData_CommandType type, int button)
        {
            this.lCasePhrase = phrase.ToLower();
            this.commandType = type;
            this.value = button;

            this.axis = -1;
            this.control = false;
            this.shift = false;
            this.isValid = true;
        }

        public bool IsSimpleInstruction() { return IsSimpleInstruction(lCasePhrase); }
        public static bool IsSimpleInstruction(String phrase) { return phrase.Split(INSTRUCTION_PHRASE_SEPARATOR).GetLength(0) > 1; }

        public String ToString() { return "'" + lCasePhrase + "'" + "(" + commandType.ToString() + ")"; }
    }

    public class Profile
    {
        private ArrayList instructions;
        private String name;

        public String Name { get { return name; } }

        public Profile(String name)
        {
            this.name = name;
            instructions = new ArrayList();
        }

        public InstructionData Instruction(int index) { return (InstructionData)instructions[index]; }
        public InstructionData this[int index]
        {
            get
            {
                return (InstructionData)instructions[index];
            }
            set
            {
                instructions[index] = value;
            }
        }

        public int InstructionsCount { get { return instructions.Count; } }

        public int AddNewInstruction()
        {
            instructions.Add(new InstructionData());
            return instructions.Count - 1;
        }

        public void DeleteInstruction(int index)
        {
            if(index>=0 && index<instructions.Count)
                instructions.RemoveAt(index);
        }

        public void SetInstruction(int index, InstructionData instruction)
        {
            instructions[index] = instruction;
        }

        public int IsValidPhrase(String phrase, int instructionIndex)
        {
            int i = 0;
            bool isSimpleInstruction = InstructionData.IsSimpleInstruction(phrase);
            int returnIndex = -1;
            InstructionData instruction;

            phrase = phrase.ToUpper();

            while (i < instructions.Count && returnIndex < 0)
            {
                if (i != instructionIndex)
                {
                    instruction = (InstructionData)instructions[i];
                    if (isSimpleInstruction)
                        if (instruction.phrase.Contains(phrase))
                            returnIndex = i;
                        else
                            ++i;
                    else
                        if (phrase.Contains(instruction.phrase))
                            returnIndex = i;
                        else
                            ++i;
                }
                else ++i;
            }

            return returnIndex;
        }
    }
}