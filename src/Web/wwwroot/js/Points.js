$(document).ready(function () {
    $(document).on("click", ".awardCommentPoint", function () {
        var self = $(this);
        var id = this.id.split("_")[1];

        $.ajax({
            url: siteRoot + `api/CommentXdPoint/${id}/award`,
            method: "POST",
            success: function () {
                self.text("xD!");
                self.removeClass();
                self.addClass("removeCommentPoint");
                self.css("font-weight", "bold");
                
                var newPoints = parseInt($(`#commentCardPoints_${id}`).text()) + 1;
                $(`#commentCardPoints_${id}`).text(newPoints.toString());

            }

        });
    });

    $(document).on("click", ".removeCommentPoint", function () {
        var self = $(this);
        var id = this.id.split("_")[1];

        $.ajax({
            url: siteRoot + `api/CommentXdPoint/${id}/remove`,
            method: "DELETE",
            success: function () {
                self.text("xD");
                self.removeClass();
                self.addClass("awardCommentPoint");
                self.css("font-weight", "normal");
                
                var newPoints = parseInt($(`#commentCardPoints_${id}`).text()) - 1;
                if (newPoints < 1) {
                    newPoints = 0;
                }
                $(`#commentCardPoints_${id}`).text(newPoints.toString());
            }
        });
    });
    
    $(document).on("click", ".awardMemePoint", function () {
        var self = $(this);
        var id = this.id.split("_")[1];

        $.ajax({
            url: siteRoot + `api/MemeXdPoint/${id}/award`,
            method: "POST",
            success: function () {
                self.text("xD!");
                self.removeClass();
                self.addClass("removeMemePoint");
                self.css("font-weight", "bold");

                var newPoints = parseInt($(`#memeCardPoints_${id}`).text()) + 1;
                $(`#memeCardPoints_${id}`).text(newPoints.toString());
            }
        });
    });

    $(document).on("click", ".removeMemePoint", function () {
        var self = $(this);
        var id = this.id.split("_")[1];

        $.ajax({
            url: siteRoot + `api/MemeXdPoint/${id}/remove`,
            method: "DELETE",
            success: function () {
                self.text("xD");
                self.removeClass();
                self.addClass("awardMemePoint");
                self.css("font-weight", "normal");

                var newPoints = parseInt($(`#memeCardPoints_${id}`).text()) - 1;
                if (newPoints < 1) {
                    newPoints = 0;
                }
                $(`#memeCardPoints_${id}`).text(newPoints.toString());
            }
        });
    });
});