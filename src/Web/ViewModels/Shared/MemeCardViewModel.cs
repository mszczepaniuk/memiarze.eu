using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.ViewModels.Shared
{
    public class MemeCardViewModel
    {
        public int Id { get; }
        public string Title { get; }
        public string ImagePath { get; }
        public string UserName{ get; }
        public string UserId { get; }
        public int XdPoints { get; }
        public string CreationDate { get; }
        public bool IsXdClicked { get; }
        public string AvatarPath { get; }

        public MemeCardViewModel(Meme meme, bool isXdClicked)
        {
            Id = meme.Id;
            Title = meme.Title;
            ImagePath = meme.ImagePath;
            UserName = meme.User == null ? "Usuniete konto" : meme.User.UserName;
            if (meme.User == null || meme.User.AvatarPath == null)
            {
                AvatarPath = "/img/avatars/default.png";
            }
            else
            {
                AvatarPath = meme.User.AvatarPath;
            }
            UserId = meme.UserId;
            XdPoints = meme.XdPoints.Count;
            CreationDate = meme.CreationDate.ToString("MM/dd/yyyy HH:mm");
            IsXdClicked = isXdClicked;
        }
    }
}