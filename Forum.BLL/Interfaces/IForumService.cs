using Forum.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.BLL.Interfaces
{
    public interface IForumService
    {
        int AddPost(PostDTO postDTO);
        int AddComment(CommentDTO commentDTO);

        IEnumerable<CategoryDTO> GetCategories();
        IEnumerable<PostDTO> GetPosts(int categoryid);
        IEnumerable<CommentDTO> GetComments(int postid);

        void UpdateComment(CommentDTO commentDTO);
        void UpdatePost(PostDTO postDTO);

        void DeleteComment(int id);
        void DeletePost(int id);

        CommentDTO GetCommentById(int id);
        PostDTO GetPostById(int id);

        void Dispose();
    }
}
