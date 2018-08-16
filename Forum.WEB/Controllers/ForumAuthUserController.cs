using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.BLL.Interfaces;
using Forum.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Description;

namespace Forum.WEB.Controllers
{
    /// <summary>
    /// Forum contoller for auth users
    /// </summary>
    /// 
    [Authorize]
    public class ForumAuthUserController : ApiController
    {
        IForumService forumService;

        /// <summary>
        /// Ninjection IForumService to specific service.
        /// </summary>
        /// <param name="service">Specific service which realize IForumService.</param>
        public ForumAuthUserController(IForumService service)
        {
            forumService = service;
        }

        /// <summary>
        /// Create post.
        /// </summary>
        /// <param name="categoryid">Id of chose category.</param>
        /// <param name="post">Post model.</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Post))]
        [Route("api/forum/{categoryid}/post")]
        public IHttpActionResult Post(int categoryid, [FromBody]Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TakeClaims(User.Identity, out string firstNameClaims, out string lastNameClaims, out string userName);

            var postDto = new PostDTO
            {
                CreatorName = String.Format($"{firstNameClaims} {lastNameClaims}"),
                UserName = userName,
                Body = post.body,
                DateTime = DateTime.Now,
                Title = post.title,
                CategoryID = categoryid
            };

            post.id = forumService.AddPost(postDto);
            post.datetime = postDto.DateTime;
            post.name = String.Format($"{firstNameClaims} {lastNameClaims}");
            post.username = userName;

            return Content(HttpStatusCode.Created, post);
        }

        //Take user claims
        private void TakeClaims(IIdentity identity, out string firstNameClaims, out string lastNameClaims, out string userName)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            firstNameClaims = claimsIdentity.FindFirst("FirstName").Value;
            lastNameClaims = claimsIdentity.FindFirst("LastName").Value;
            userName = claimsIdentity.FindFirst("Username").Value;
        }

        /// <summary>
        /// Add new comment
        /// </summary>
        /// <param name="postid">Number of post</param>
        /// <param name="comment">Comment</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Comment))]
        [Route("api/forum/{categoryid}/{postid}/comments")]
        public IHttpActionResult PostComment(int postid, [FromBody]Comment comment)
        {
            if (comment.body == null)
                return BadRequest(ModelState);

            TakeClaims(User.Identity, out string firstNameClaims, out string lastNameClaims, out string userName);

            var comentDto = new CommentDTO
            {
                PostID = postid,
                Name = String.Format($"{firstNameClaims} {lastNameClaims}"),
                UserName = userName,
                Body = comment.body,
                DateTime = DateTime.Now
            };

            comment.id = forumService.AddComment(comentDto);
            comment.datetime = comentDto.DateTime;
            comment.name = comentDto.Name;
            return Content(HttpStatusCode.Created, comment);
        }

        /// <summary>
        /// Update post.
        /// </summary>
        /// <param name="categoryid">Id of category.</param>
        /// <param name="postid">Id of post.</param>
        /// <param name="post">Post model.</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(PutPost))]
        [Route("api/forum/{categoryid}/{postid}/update")]
        public IHttpActionResult UpdatePost(int categoryid, int postid, [FromBody]PutPost post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var find = forumService.GetPostById(postid);

            TakeClaims(User.Identity, out string firstNameClaims, out string lastNameClaims, out string userName);

            var postDto = new PostDTO
            {
                ID = postid,
                CategoryID = categoryid,
                CreatorName = String.Format($"{firstNameClaims} {lastNameClaims}"),
                UserName = userName,
                Body = post.body ?? find.Body,
                Title = post.title ?? find.Title,
                DateTime = DateTime.Now
            };

            forumService.UpdatePost(postDto);

            return Content(HttpStatusCode.OK, post);
        }

        /// <summary>
        /// Delete post.
        /// </summary>
        /// <param name="postid">Id of post.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/forum/{categoryid}/{postid}/delete")]
        public IHttpActionResult DeletePost(int postid)
        {
            PostDTO postDTO = forumService.GetPostById(postid);

            if (postDTO == null)
                return NotFound();

            forumService.DeletePost(postid);
            return Ok();
        }

        /// <summary>
        /// Update comment
        /// </summary>
        /// <param name="postid">Id of post</param>
        /// <param name="commentid">Id of comment</param>
        /// <param name="comment">Comment model</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(Comment))]
        [Route("api/forum/{categoryid}/{postid}/{commentid}")]
        public IHttpActionResult UpdateComment(int postid, int commentid, [FromBody]Comment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var find = forumService.GetCommentById(commentid);

            TakeClaims(User.Identity, out string firstNameClaims, out string lastNameClaims, out string userName);

            var comentDto = new CommentDTO
            {
                ID = commentid,
                PostID = postid,
                Name = String.Format($"{firstNameClaims} {lastNameClaims}"),
                UserName = userName,
                Body = comment.body ?? find.Body,
                DateTime = DateTime.Now
            };

            forumService.UpdateComment(comentDto);

            return Content(HttpStatusCode.OK, comment);

        }

        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="commentid">Id of comment.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/forum/{categoryid}/{postid}/{commentid}/delete")]
        public IHttpActionResult DeleteComment(int commentid)
        {
            CommentDTO commentDTO = forumService.GetCommentById(commentid);

            if (commentDTO == null)
                return NotFound();

            forumService.DeleteComment(commentid);
            return Ok();
        }
    }
}
