using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.BLL.Interfaces;
using Forum.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Forum.WEB.Controllers
{
    /// <summary>
    /// Forum controller for annonymous user
    /// </summary>
    [AllowAnonymous]
    public class ForumAnonController : ApiController
    {
        IForumService forumService;

        /// <summary>
        /// Ninjection IForumService to specific service
        /// </summary>
        /// <param name="service">Specific service which realize IForumService</param>
        public ForumAnonController(IForumService service)
        {
            forumService = service;
        }

        /// <summary>
        /// Return titles of categoies and it posts.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/forum/titles")]
        public IHttpActionResult GetTitles()
        {
            try
            {
                List<Title> titles = new List<Title>();

                IEnumerable<CategoryDTO> categoriesDto = forumService.GetCategories();
                foreach (var category in categoriesDto)
                {
                    var posts = forumService.GetPosts(category.ID);
                    List<PostTitles> postTitle = new List<PostTitles>();
                    foreach (var post in posts)
                    {
                        postTitle.Add(new PostTitles
                        {
                            id = post.ID,
                            title = post.Title
                        });
                    }

                    titles.Add(new Title
                    {
                        id = category.ID,
                        title = category.Title,
                        posts = postTitle
                    });
                }
                return Ok(titles);

            }
            catch (ValidationException ex)
            {
                return NotFound();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Get all forum category topic
        /// </summary>
        /// <returns>JSON with information about category (Categories model) and status code</returns>
        [HttpGet]
        [Route("api/forum")]
        public IHttpActionResult Get()
        {
            IEnumerable<CategoryDTO> categoriesDto = null;

            try
            {
                categoriesDto = forumService.GetCategories();
                List<Categories> categories = new List<Categories>();

                foreach (var category in categoriesDto)
                    categories.Add(new Categories
                    {
                        id = category.ID,
                        description = category.Description,
                        title = category.Title
                    });

                return Ok(categories);
            }
            catch (ValidationException ex)
            {
                return NotFound();
            }
            catch (NullReferenceException ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get posts by categoryid
        /// </summary>
        /// <param name="categoryid">Used for take all posts of specific categoryid</param>
        /// <returns>JSON with information about posts (Posts model) and status code</returns>
        // GET api/topic/5(categoryid)
        [HttpGet]
        [Route("api/forum/{categoryid:int}")]
        public IHttpActionResult GetPosts(int categoryid)
        {
            IEnumerable<PostDTO> postsDto = null;

            try
            {
                postsDto = forumService.GetPosts(categoryid);

                List<Posts> posts = new List<Posts>();

                foreach (var post in postsDto)
                    posts.Add(new Posts
                    {
                        id = post.ID,
                        title = post.Title,
                        datetime = post.DateTime,
                        name = post.CreatorName,
                        username = post.UserName
                    });

                return Ok(posts);
            }
            catch (ValidationException ex)
            {
                return NotFound();
            }
            catch (NullReferenceException ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a specific post by categoryid and postid
        /// </summary>
        /// <param name="categoryid">ID of chose category</param>
        /// <param name="postid">Used for take post and List of comments</param>
        /// <returns>JSON with information about post (Post model) and status code</returns>
        //GET api/topic/categoryid/1(postid)
        [HttpGet]
        [Route("api/forum/{categoryid:int}/{postid:int}")]
        public IHttpActionResult GetPostWithComments(int categoryid, int postid)
        {
            PostDTO postDto = null;
            try
            {
                postDto = forumService.GetPostById(postid);

                var commentsDto = forumService.GetComments(postid);
                Post post;

                if (commentsDto != null)
                {
                    List<Comment> comments = new List<Comment>();

                    foreach (var comment in commentsDto)
                        comments.Add(new Comment
                        {
                            id = comment.ID,
                            name = comment.Name,
                            username = comment.UserName,
                            body = comment.Body,
                            datetime = comment.DateTime
                        });

                    post = new Post
                    {
                        id = postDto.ID,
                        name = postDto.CreatorName,
                        username = postDto.UserName,
                        body = postDto.Body,
                        title = postDto.Title,
                        datetime = postDto.DateTime,
                        comments = comments
                    };
                }
                else
                    post = new Post
                    {
                        id = postDto.ID,
                        name = postDto.CreatorName,
                        username = postDto.UserName,
                        body = postDto.Body,
                        datetime = postDto.DateTime,
                        title = postDto.Title
                    };

                return Ok(post);
            }
            catch (ValidationException ex)
            {
                return NotFound();
            }
            catch (NullReferenceException ex)
            {
                return NotFound();
            }
        }
    }
}
