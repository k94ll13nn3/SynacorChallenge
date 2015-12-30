// SynacorChallenge plugin

namespace SynacorChallenge.Commands
{
    internal interface ICommand
    {
        ushort Identifier { get; }

        string Name { get; }

        void Execute();
    }
}