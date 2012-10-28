using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KinectStick.Commands;
using KinectStick.Instructions;

using System.Collections;

public class PreCompiledInstruction
{
    public int[] phraseIndices;
    public Command command;

    public int Length { get { return phraseIndices.GetLength(0); } }

    public PreCompiledInstruction(int[] indices, Command command)
    {
        this.phraseIndices = indices;
        this.command = command;
    }
}

public class PreCompiledProfile
{
    public PreCompiledInstruction[] instructions;
    public CompiledSpeechPhrase[] speechLibrary;

    public PreCompiledProfile(PreCompiledInstruction[] instructions, CompiledSpeechPhrase[] speechLibrary)
    {
        this.instructions = instructions;
        this.speechLibrary = speechLibrary;
    }
}

public struct CompiledSpeechPhrase
{
    public String phrase;
    public int index;

    public CompiledSpeechPhrase(String phrase, int phraseIndex)
    {
        this.phrase = phrase;
        this.index = phraseIndex;
    }
}

public class CompiledProfile
{
    public CompiledSpeechPhrase[] phraseLibrary;
    public InstructionsTree[] instructionsTreeLibrary;

    public CompiledProfile(CompiledSpeechPhrase[] phraseLibrary, InstructionsTree[] instructionsTreeLibrary)
    {
        this.phraseLibrary = phraseLibrary;
        this.instructionsTreeLibrary = instructionsTreeLibrary;
    }
}
