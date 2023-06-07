using System;

public interface IStateChangeNotifier
{
    event Action<bool> StateChanged;
}
