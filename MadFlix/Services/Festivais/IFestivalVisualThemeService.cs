using MadFlix.Models;

namespace MadFlix.Services.Festivais;

public interface IFestivalVisualThemeService
{
    string GetImageUrl(Festival festival);
}
