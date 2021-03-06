﻿using memiarzeEu.Models;
using Microsoft.Extensions.Configuration;

namespace memiarzeEu.ViewModels.Shared
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; }
        public string UserName { get; }
        public string UserId { get; }
        public int XdPoints { get; }
        public string CreationDate { get; }
        public string AvatarPath { get; }
        public bool IsXdClicked { get; }

        public CommentViewModel(Comment comment, bool isXdClicked, IConfiguration configuration)
        {
            Text = comment.Text;
            UserName = comment.User == null ? "Usuniete konto" : comment.User.UserName;
            XdPoints = comment.XdPoints.Count;
            AvatarPath = comment.User == null ? configuration.GetSection("DefaultAvatarPath").Value : comment.User.AvatarPath;
            CreationDate = comment.CreationDate.ToString("MM/dd/yyyy HH:mm");
            IsXdClicked = isXdClicked;
            UserId = comment.UserId;
            Id = comment.Id;
        }
    }
}
