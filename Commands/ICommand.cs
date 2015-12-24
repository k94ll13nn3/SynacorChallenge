namespace SynacorChallenge.Commands
{
    internal interface ICommand
    {
        int Identifier { get; }

        string Name { get; }

        uint Execute(uint currentPosition);
    }
}