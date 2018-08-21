using Forum.BLL.DTO;
using Forum.BLL.Interfaces;
using Forum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using AutoMapper;
using Forum.DAL.Entities;
using Forum.BLL.Infrastructure;
using System.Linq;

namespace Forum.BLL.Services
{
    public class ForumService : IForumService
    {
        IUnitOfWork Database { get; set; }

        public ForumService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        /// <summary>
        /// Add new comment into database.
        /// </summary>
        /// <param name="commentDTO">Model of comment.</param>
        /// <returns>Id of created comment, ValidationException.</returns>
        public int AddComment(CommentDTO commentDTO)
        {
            if (commentDTO == null)
                throw new ValidationException("CommentDTO in AddComment() is null", "commentDTO");

            if (Database.Posts.GetById(commentDTO.PostID) == null)
                throw new ValidationException($"Cant add comment to not exist post with id = {commentDTO.PostID}", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>()).CreateMapper();
            Database.Comments.Create(mapper.Map<CommentDTO, Comment>(commentDTO));
            SaveChanges();

            return GetLastCommentId(commentDTO.Name);
        }

        /// <summary>
        /// Add new post into database.
        /// </summary>
        /// <param name="postDTO">Model of post.</param>
        /// <returns>Id of created post, ValidationException.</returns>
        public int AddPost(PostDTO postDTO)
        {
            if (postDTO == null)
                throw new ValidationException("PostDTO in AddPost() is null", "postDTO");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PostDTO, Post>()).CreateMapper();
            Database.Posts.Create(mapper.Map<PostDTO, Post>(postDTO));
            SaveChanges();

            return GetLastPostId(postDTO.UserName);
        }

        /// <summary>
        /// Dispose connection to database.
        /// </summary>
        public void Dispose()
        {
            Database.Dispose();
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>List of categoryDTO model, ValidationException.</returns>
        public IEnumerable<CategoryDTO> GetCategories()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();

            if (Database.Categories.GetAll() == null)
                throw new ValidationException($"Havnt any category", "");

            var categories = mapper.Map<IEnumerable<Category>, List<CategoryDTO>>(Database.Categories.GetAll());

            return categories;
        }

        /// <summary>
        /// Get comments for post by postid.
        /// </summary>
        /// <param name="postid">ID of post.</param>
        /// <returns>List of commentDTO model, ValidationException.</returns>
        public IEnumerable<CommentDTO> GetComments(int postid)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Comment, CommentDTO>()).CreateMapper();

            if (Database.Posts.GetById(postid) == null)
                throw new ValidationException($"Havnt post with id = {postid}", "");

            try
            {
                var comments = mapper.Map<IEnumerable<Comment>, List<CommentDTO>>(Database.Comments.GetAll().Where(c => c.PostID == postid));
                if (comments.Count == 0)
                    comments = null;
                return comments;
            }
            catch (ArgumentNullException ex)
            {
                throw new ValidationException($"Havnt comment for postid = {postid}.", "postid");
            }
        }

        /// <summary>
        /// Get post by postid.
        /// </summary>
        /// <param name="id">Id of post.</param>
        /// <returns>PostDTO model, ValidationException.</returns>
        public PostDTO GetPostById(int id)
        {
            var post = Database.Posts.GetById(id);

            if (post == null)
                throw new ValidationException("Post not found", "");

            return new PostDTO
            {
                Title = post.Title,
                Body = post.Body,
                CreatorName = post.CreatorName,
                DateTime = post.DateTime,
                UserName = post.UserName
            };
        }

        /// <summary>
        /// Get all post by categoryid.
        /// </summary>
        /// <param name="categoryid">Category id.</param>
        /// <returns>List of postDTO model, ValidationException.</returns>
        public IEnumerable<PostDTO> GetPosts(int categoryid)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostDTO>()).CreateMapper();

            try
            {
                var posts = mapper.Map<IEnumerable<Post>, List<PostDTO>>(Database.Posts.GetAll().Where(c => c.CategoryID == categoryid));
                if (posts.Count == 0)
                    throw new ValidationException($"Havnt any posts by categoryid = {categoryid}", "categoryid");
                return posts;
            }
            catch (ArgumentNullException ex)
            {
                throw new ValidationException($"Havnt any posts by categoryid = {categoryid}", "categoryid");
            }


        }

        /// <summary>
        /// Update comment.
        /// </summary>
        /// <param name="commentDTO">CommentDTO model.</param>
        /// <returns>void, ValidationException.</returns>
        public void UpdateComment(CommentDTO commentDTO)
        {
            if (commentDTO == null)
                throw new ValidationException("CommentDTO in UpdateComment() is null", "commentDTO");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>()).CreateMapper();
            Database.Comments.Update(mapper.Map<CommentDTO, Comment>(commentDTO));
            SaveChanges();
        }

        /// <summary>
        /// Update post.
        /// </summary>
        /// <param name="commentDTO">PostDTO model.</param>
        /// <returns>void, ValidationException.</returns>
        public void UpdatePost(PostDTO postDTO)
        {
            if (postDTO == null)
                throw new ValidationException("PostDTO in UpdatePost() is null", "postDTO");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>()).CreateMapper();

            Post post = new Post
            {
                Body = postDTO.Body,
                CategoryID = postDTO.CategoryID,
                CreatorName = postDTO.CreatorName,
                Title = postDTO.Title,
                ID = postDTO.ID,
                DateTime = postDTO.DateTime,
                UserName = postDTO.UserName
            };
            Database.Posts.Update(post);
            SaveChanges();
        }

        /// <summary>
        /// Delete comment by id.
        /// </summary>
        /// <param name="commentid">Comment id.</param>
        public void DeleteComment(int commentid)
        {
            Database.Comments.Delete(commentid);
            SaveChanges();
        }

        /// <summary>
        /// Delete post by id.
        /// </summary>
        /// <param name="postid">Post id.</param>
        public void DeletePost(int postid)
        {
            var comments = GetComments(postid);
            if (comments != null)
            {
                foreach (var comment in comments)
                    DeleteComment(comment.ID);
            }

            Database.Posts.Delete(postid);
            SaveChanges();
        }

        /// <summary>
        /// Get comment by id.
        /// </summary>
        /// <param name="commentid">Comment id.</param>
        /// <returns>CommentDTO model, ValidationException.</returns>
        public CommentDTO GetCommentById(int commentid)
        {
            var comment = Database.Comments.GetById(commentid);

            if (comment == null)
                throw new ValidationException("Comment not found", "");

            return new CommentDTO
            {
                Body = comment.Body,
                Name = comment.Name,
                UserName = comment.UserName,
                DateTime = comment.DateTime,
                PostID = comment.PostID
            };
        }

        /// <summary>
        /// Get last added comment id by username.
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>Id comment.</returns>
        private int GetLastCommentId(string name)
        {
            return Database.Comments.GetAll().Last(x => x.Name == name).ID;
        }

        /// <summary>
        /// Get last added post id by username.
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>Id post.</returns>
        private int GetLastPostId(string name)
        {
            return Database.Posts.GetAll().Last(x => x.UserName == name).ID;
        }

        /// <summary>
        /// Save changes into database.
        /// </summary>
        private void SaveChanges()
        {
            Database.Save();
        }
    }
}
