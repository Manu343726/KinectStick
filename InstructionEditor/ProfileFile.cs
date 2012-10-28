using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace InstructionEditor
{
    class ProfileFile
    {
        public const char FILE_DATASEPARATOR = ',';

        public static bool Save(String filePath, Profile profile)
        {
            StreamWriter writer;
            FileInfo info=new FileInfo(filePath);

            try
            {
                if (info.Exists)
                    writer = new StreamWriter(filePath);
                else
                    writer = info.CreateText();

                writer.AutoFlush = false;

                writer.WriteLine(profile.Name);
                writer.WriteLine(profile.InstructionsCount);

                for (int i = 0; i < profile.InstructionsCount; ++i)
                    writer.WriteLine(profile[i].phrase + FILE_DATASEPARATOR + (int)profile[i].commandType + FILE_DATASEPARATOR + profile[i].axis + FILE_DATASEPARATOR + profile[i].value + FILE_DATASEPARATOR + profile[i].control + FILE_DATASEPARATOR + profile[i].shift);

                writer.Flush();
                writer.Close();
            }
            catch (Exception) { return false; }

            return true;
        }

        public static bool Load(String filePath,out Profile profile)
        {
            StreamReader reader;
            int instructionsCount;
            InstructionData instruction=new InstructionData();
            String[] splittedLine;

            if (File.Exists(filePath))
            {
                reader = new StreamReader(filePath);

                profile = new Profile(reader.ReadLine());
                instructionsCount = Convert.ToInt32(reader.ReadLine());

                for (int i = 0; i < instructionsCount; ++i)
                {
                    profile.AddNewInstruction();
                    splittedLine = reader.ReadLine().Split(FILE_DATASEPARATOR);

                    instruction.SetPhrase(splittedLine[0]);
                    instruction.commandType = (InstructionData_CommandType)Convert.ToInt32(splittedLine[1]);
                    instruction.axis = Convert.ToInt32(splittedLine[2]);
                    instruction.value = Convert.ToInt32(splittedLine[3]);
                    instruction.control = Convert.ToBoolean(splittedLine[4]);
                    instruction.shift = Convert.ToBoolean(splittedLine[5]);

                    profile[i] = instruction;
                }

                return true;
            }
            else
            {
                profile = null;
                return false;
            }
        }
    }
}
