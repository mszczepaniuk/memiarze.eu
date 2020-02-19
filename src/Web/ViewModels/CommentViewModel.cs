using memiarzeEu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.ViewModels
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

        public CommentViewModel(Comment comment, bool isXdClicked)
        {
            Text = comment.Text;
            UserName = comment.User == null ? "Usuniete konto" : comment.User.UserName;
            XdPoints = comment.XdPoints.Count;
            AvatarPath = comment.User.AvatarPath;
            CreationDate = comment.CreationDate.ToString("MM/dd/yyyy HH:mm");
            IsXdClicked = isXdClicked;
            UserId = comment.UserId;
            Id = comment.Id;
        }
    }
}
