using MadFlix.Models;

namespace MadFlix.Services.Festivais;

public interface IFestivalHighlightScoreStrategy
{
    int Score(Festival festival, DateTime today);
}
