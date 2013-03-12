using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using KinectStick.Instructions;
using KinectStick.Commands;

using System.Collections;
using System.Windows.Forms;
using WindowsInput.Native;

namespace InstructionEditor
{
    public class ProfileCompiler
    {
        public const char PHRASESEPARATOR = ' ';
        public static Array Values = Enum.GetValues(typeof(VirtualKeyCode));

        public static Command GenerateCommand(InstructionData instruction)
        {
            VirtualKeyCode[] keys=new VirtualKeyCode[Values.GetLength(0)];
            Values.CopyTo(keys, 0);

            switch (instruction.commandType)
            {
                case InstructionData_CommandType.JOYSTICKAXIS:
                    return new Command(new JoystickCommandData(0,(JoystickDataType)instruction.axis,(short)instruction.value));
                case InstructionData_CommandType.JOYSTICKBUTTON_PRESS:
                    return new Command(new JoystickCommandData(0, JoystickDataType.BUTTON_PRESS, 0, instruction.value));
                case InstructionData_CommandType.JOYSTICKBUTTON_HOLD:
                    return new Command(new JoystickCommandData(0, JoystickDataType.BUTTON_HOLD, 0, instruction.value));
                case InstructionData_CommandType.JOYSTICKBUTTON_RELEASE:
                    return new Command(new JoystickCommandData(0, JoystickDataType.BUTTON_RELEASE, 0, instruction.value));
                case InstructionData_CommandType.KEYBOARD_PRESS:
                    return new Command(new KeyboardCommandData(keys[instruction.value], KeyboardCommandType.PRESS, instruction.control, instruction.shift));
                case InstructionData_CommandType.KEYBOARD_HOLD:
                    return new Command(new KeyboardCommandData(keys[instruction.value], KeyboardCommandType.HOLD, instruction.control, instruction.shift));
                case InstructionData_CommandType.KEYBOARD_RELEASE:
                    return new Command(new KeyboardCommandData(keys[instruction.value], KeyboardCommandType.RELEASE, instruction.control, instruction.shift));
            }

            return null;
        }

        public static PreCompiledProfile PreCompile(Profile profile)
        {
            List<String> phraseLibrary = new List<String>();
            List<CompiledSpeechPhrase> generatedPhraseLibrary = new List<CompiledSpeechPhrase>();
            List<PreCompiledInstruction> preInstructions = new List<PreCompiledInstruction>();
            List<int> indices = new List<int>();
            String[] splittedPhrase;
            int phraseIndex;

            for (int i = 0; i < profile.InstructionsCount; ++i)
            {
                splittedPhrase = profile[i].phrase.Split(PHRASESEPARATOR);
                indices.Clear();

                foreach (String phrase in splittedPhrase)
                {
                    phraseIndex = phraseLibrary.IndexOf(phrase);

                    if (phraseIndex >= 0)
                        indices.Add(phraseIndex);
                    else
                    {
                        indices.Add(phraseLibrary.Count);
                        phraseLibrary.Add(phrase);
                    }
                }

                preInstructions.Add(new PreCompiledInstruction(indices.ToArray(), GenerateCommand(profile[i])));
            }

            for (int i = 0; i < phraseLibrary.Count; ++i)
                generatedPhraseLibrary.Add(new CompiledSpeechPhrase(phraseLibrary[i], i));

            return new PreCompiledProfile(preInstructions.ToArray(), generatedPhraseLibrary.ToArray());
        }

        public static CompiledProfile GenerateTree(PreCompiledProfile preProfile)
        {
            List<InstructionsTree> treeList = new List<InstructionsTree>();
            InstructionsTree tree;
            int i = 0;
            bool searching;

            foreach (PreCompiledInstruction instruction in preProfile.instructions)
            {
                i = 0;
                searching = true;

                if (treeList.Count > 0)
                {
                    while (i < treeList.Count && searching)
                    {
                        tree = (InstructionsTree)treeList[i];

                        if (tree.RootPrhaseIndex == instruction.phraseIndices[0])
                        {
                            tree.GenerateBranch(instruction);
                            searching = false;
                        }
                        else
                            ++i;
                    }

                    if (searching)
                    {
                        treeList.Add(new InstructionsTree());
                        ((InstructionsTree)treeList[treeList.Count - 1]).GenerateBranch(instruction);
                    }
                }
                else
                {
                    treeList.Add(new InstructionsTree());
                    ((InstructionsTree)treeList[treeList.Count - 1]).GenerateBranch(instruction);
                }
            }

            return new CompiledProfile(preProfile.speechLibrary, treeList.ToArray());
        }

        public static CompiledProfile Compile(Profile profile)
        {
            return GenerateTree(PreCompile(profile));
        }

    }
}
