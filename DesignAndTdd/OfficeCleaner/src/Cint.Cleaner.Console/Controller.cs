using System;
using Cint.Cleaner.Console.InputOutput;
using Cint.Cleaner.Core;

namespace Cint.Cleaner.Console
{
    public class Controller
    {
        IStandardInputLineReader _inputLineReader;
        IStandardOutputLineWriter _outputLineWriter;


        public Controller(IStandardInputLineReader inputView, IStandardOutputLineWriter outputView)
        {
            _inputLineReader = inputView;
            _outputLineWriter = outputView;
        }

        public void CleanUpTheSpacelySpaceSprocketsOffice()
        {
            try
            {

                IOfficeSpace spacelySpaceSprocketsOffice = new OfficeSpace();
                CommandsReader georgeJetsonCommandsReader = new CommandsReader(_inputLineReader);
                georgeJetsonCommandsReader.ReadCommandFromStandardInput();

                Robot rosieTheRobotMaid = new Robot(georgeJetsonCommandsReader.StartingPositionX,
                                                    georgeJetsonCommandsReader.StartingPositionY,
                                                    spacelySpaceSprocketsOffice);


                while (georgeJetsonCommandsReader.MoveForwardCommands.Count > 0)
                {
                    MoveForwardCommand georgeJetsonCommand = georgeJetsonCommandsReader.MoveForwardCommands.Dequeue();

                    rosieTheRobotMaid.MoveForward(georgeJetsonCommand);
                }

                _outputLineWriter.WriteLine(string.Format("=> Cleaned: {0}", rosieTheRobotMaid.CleanedPlacesCount));
            }
            catch (Exception e)
            {
                _outputLineWriter.WriteLine("Rosie the Robot Maid malfunctions, call George Jetson to fix her!");
                _outputLineWriter.WriteLine("Here are the diagnostic messages:");
                _outputLineWriter.WriteLine(e.ToString());
            }
        }
    }
}
