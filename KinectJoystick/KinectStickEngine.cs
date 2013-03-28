using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Windows.Forms;
using vJoyWrapper;
using Microsoft.Kinect;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;

using KinectStick.Commands;
using KinectStick.Instructions;

using WindowsInput;

namespace KinectStick
{
    public class KinectStickEngine
    {
        //IN:
        private KinectSensor kinect;
        private SpeechRecognitionEngine speechEngine;

        //MIDLE:
        private CompiledProfile profile;
        private InstructionsTreeNode actualNode;
        private List<CompiledSpeechPhrase> instructionQueue;
        //OUT:
        private VJoyWrapper joy;
        private InputSimulator inputSimulator;
        //-------------------------------------------

        public delegate void InstructionPipelineEvent(String instructionPhrase, CompiledSpeechPhrase lastPhrase, Command command);
        public delegate void InstructionPipelineMiddleEvent(CompiledSpeechPhrase phrase);
        public event InstructionPipelineEvent RejectedInstruction;
        public event InstructionPipelineEvent RecognizedInstruction;
        public event InstructionPipelineMiddleEvent RecognizedPhrase;

        private bool ready;

        public bool Ready { get { return ready; } }

        public KinectStickEngine(CompiledProfile profile)
        {
            actualNode = null;
            instructionQueue = new List<CompiledSpeechPhrase>();
            joy = new VJoyWrapper();
            inputSimulator = new InputSimulator();
            this.profile = profile;

            ready = InitKinect() && LoadSpeechRecognitionEngine();
        }

        public void SetProfile(CompiledProfile profile)
        {
            this.profile = profile;
            ResetInstructionPipeline();
            if (kinect != null)
                ready = LoadSpeechRecognitionEngine();
            else
                ready = false;
        }

        private bool InitKinect()
        {
            kinect = null;

            foreach (KinectSensor sensor in KinectSensor.KinectSensors)
            {
                if (sensor.Status == KinectStatus.Connected)
                {
                    kinect = sensor;
                    break;
                }
            }

            if (kinect != null)
            {
                try
                {
                    kinect.Start();
                }
                catch (System.IO.IOException)
                {
                    kinect = null;
                }
            }

            return kinect != null;
        }

        private bool LoadSpeechRecognitionEngine()
        {
            RecognizerInfo info = null;
            Grammar grammar;
            GrammarBuilder grammarBuilder;
            Choices choices;

            foreach (RecognizerInfo recognizer in SpeechRecognitionEngine.InstalledRecognizers())
            {
                string value;
                recognizer.AdditionalInfo.TryGetValue("Kinect", out value);
                if ("True".Equals(value, StringComparison.OrdinalIgnoreCase) && "en-US".Equals(recognizer.Culture.Name, StringComparison.OrdinalIgnoreCase))
                {
                    info = recognizer;
                    break;
                }
            }

            if (info != null)
            {
                speechEngine = new SpeechRecognitionEngine(info.Id);
                choices = new Choices();

                foreach (CompiledSpeechPhrase phrase in profile.phraseLibrary)
                    choices.Add(new SemanticResultValue(phrase.phrase, phrase.index));

                grammarBuilder = new GrammarBuilder();
                grammarBuilder.Culture = info.Culture;
                grammarBuilder.Append(choices);

                grammar = new Grammar(grammarBuilder);
                speechEngine.LoadGrammar(grammar);

                speechEngine.SpeechRecognized += SpeechFraseRecognized;

                speechEngine.SetInputToAudioStream(kinect.AudioSource.Start(), new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                speechEngine.RecognizeAsync(RecognizeMode.Multiple);

                return true;
            }
            else
                return false;
        }

        public void ResetInstructionPipeline()
        {
            actualNode=null;
            instructionQueue.Clear();
        }

        private void InstructionFetch(int phraseIndex)
        {
            int i = 0;
            bool fetching=true;

            instructionQueue.Add(profile.phraseLibrary[phraseIndex]);

            //Si no hay una instrucción en curso, buscamos una que comience con el comando de voz captado:
            if (actualNode == null)
            {
                while (i < profile.instructionsTreeLibrary.GetLength(0) && fetching)
                {
                    if (profile.instructionsTreeLibrary[i].RootPrhaseIndex == phraseIndex)
                    {
                        actualNode = profile.instructionsTreeLibrary[i].Root;
                        fetching = false;
                    }
                    else
                        ++i;
                }

                if (actualNode != null)
                    InstructionAnalysis();
                else
                    instructionQueue.Clear();
            }
            else
            {
                actualNode = actualNode.SearchNode(phraseIndex);

                if (actualNode == null)
                {
                    LaunchRejectEvent(profile.phraseLibrary[phraseIndex]);
                    ResetInstructionPipeline();
                }
                else
                    InstructionAnalysis();
            }
        }

        private void InstructionAnalysis()
        {
            if (actualNode.IsChild)
            {
                ExecuteCommand(actualNode.Command);
                LaunchAcceptEvent(profile.phraseLibrary[actualNode.PhraseIndex],actualNode.Command);
                ResetInstructionPipeline();
            }
        }

        private void ExecuteCommand(Command command)
        {
            if (command.type == CommandType.JOYSTICK)
            {
                switch (command.joydata.type)
                {
                    case JoystickDataType.XAXIS:
                        joy.SetXAxis(command.joydata.joystickID, command.joydata.value);
                        break;
                    case JoystickDataType.YAXIS:
                        joy.SetYAxis(command.joydata.joystickID, command.joydata.value);
                        break;
                    case JoystickDataType.ZAXIS:
                        joy.SetZAxis(command.joydata.joystickID, command.joydata.value);
                        break;
                    case JoystickDataType.XROTATION:
                        joy.SetXRotation(command.joydata.joystickID, command.joydata.value);
                        break;
                    case JoystickDataType.YROTATION:
                        joy.SetYRotation(command.joydata.joystickID, command.joydata.value);
                        break;
                    case JoystickDataType.ZROTATION:
                        joy.SetZRotation(command.joydata.joystickID, command.joydata.value);
                        break;
                    case JoystickDataType.BUTTON_PRESS:
                        joy.SetButton(command.joydata.joystickID, command.joydata.button, true);
                        joy.SetButton(command.joydata.joystickID, command.joydata.button, false);
                        break;
                    case JoystickDataType.BUTTON_HOLD:
                        joy.SetButton(command.joydata.joystickID, command.joydata.button, true);
                        break;
                    case JoystickDataType.BUTTON_RELEASE:
                        joy.SetButton(command.joydata.joystickID, command.joydata.button, false);
                        break;
                }
            }
            else
            {
                switch (command.keydata.type)
                {
                    case KeyboardCommandType.PRESS:
                        inputSimulator.Keyboard.KeyPress(command.keydata.key);
                        break;
                    case KeyboardCommandType.HOLD:
                        inputSimulator.Keyboard.KeyDown(command.keydata.key);
                        break;
                    case KeyboardCommandType.RELEASE:
                        inputSimulator.Keyboard.KeyUp(command.keydata.key);
                        break;
                }
            }
        }

        private void LaunchAcceptEvent(CompiledSpeechPhrase phrase, Command command)
        {
            if (RecognizedInstruction != null)
                RecognizedInstruction(GenerateInstructionPhrase(),phrase,command);
        }

        private void LaunchRejectEvent(CompiledSpeechPhrase phrase)
        {
            if (RejectedInstruction != null)
                RejectedInstruction(GenerateInstructionPhrase(),phrase,null);
        }

        private void LaunchRecognizedEvent(CompiledSpeechPhrase phrase)
        {
            if (RecognizedPhrase != null)
                RecognizedPhrase(phrase);
        }

        private String GenerateInstructionPhrase()
        {
            String phrase = "";

            foreach (CompiledSpeechPhrase speechPhrase in instructionQueue)
                phrase += speechPhrase.phrase + " ";

            return phrase;
        }

        //Eventos del sistema de reconocimiento de voz:
        private void SpeechFraseRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Add event handler code here.

            // The following code illustrates some of the information available
            // in the recognition result.
            Debug.WriteLine("Grammar({0}), {1}: {2} (Confidence: {3})",
              e.Result.Grammar.Name, e.Result.Audio.Duration, e.Result.Text,e.Result.Confidence);

            // Display the semantic values in the recognition result.
            foreach (KeyValuePair<String, SemanticValue> child in e.Result.Semantics)
            {
                Debug.WriteLine(" {0} key: {1}",
                  child.Key, child.Value.Value ?? "null");
            }
            Console.WriteLine();

            // Display information about the words in the recognition result.
            foreach (RecognizedWordUnit word in e.Result.Words)
            {
                RecognizedAudio audio = e.Result.Audio;
                Debug.WriteLine(" {0,-10} {1,-10} {2,-10} {3} ({4})",
                  word.Text, word.LexicalForm, word.Pronunciation,
                  audio.Duration, word.DisplayAttributes);
            }

            // Display the recognition alternates for the result.
            foreach (RecognizedPhrase phrase in e.Result.Alternates)
            {
                Debug.WriteLine(" alt({0}) {1}", phrase.Confidence, phrase.Text);
            }

            if (e.Result.Confidence > 0.8)
            {
                LaunchRecognizedEvent(profile.phraseLibrary[(int)e.Result.Semantics.Value]);
                InstructionFetch((int)e.Result.Semantics.Value);
            }
            else
                LaunchRejectEvent(new CompiledSpeechPhrase(e.Result.Text, -1));
            }
    }
}
