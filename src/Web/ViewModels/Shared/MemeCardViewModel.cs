using memiarzeEu.Models;
using Microsoft.Extensions.Configuration;

namespace memiarzeEu.ViewModels.Shared
{
    public class MemeCardViewModel
    {
        public int Id { get; }
        public string Title { get; }
        public string ImagePath { get; }
        public string UserName { get; }
        public string UserId { get; }
        public int XdPoints { get; }
        public string CreationDate { get; }
        public bool IsXdClicked { get; }
        public string AvatarPath { get; }

        public MemeCardViewModel(Meme meme, bool isXdClicked, IConfiguration configuration)
        {
            Id = meme.Id;
            Title = meme.Title;
            ImagePath = meme.ImagePath;
            UserName = meme.User == null ? "Usuniete konto" : meme.User.UserName;
            AvatarPath = meme.User == null ? configuration.GetSection("DefaultAvatarPath").Value : meme.User.AvatarPath;
            UserId = meme.UserId;
            XdPoints = meme.XdPoints.Count;
            CreationDate = meme.CreationDate.ToString("MM/dd/yyyy HH:mm");
            IsXdClicked = isXdClicked;
        }
    }
}