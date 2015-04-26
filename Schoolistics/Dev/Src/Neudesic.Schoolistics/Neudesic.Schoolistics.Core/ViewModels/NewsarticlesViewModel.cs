// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NewsarticlesViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Neudesic.Schoolistics.Core.Services;
namespace Neudesic.Schoolistics.Core.ViewModels
{
    /// <summary>
    /// Define the NewsarticlesViewModel type.
    /// </summary>
    public class NewsarticlesViewModel : BaseViewModel
    {
        private INewsArticlesService _newsArticlesService;
        public NewsarticlesViewModel(INewsArticlesService newsArticlesService)
        {
            
        }

    }
}
