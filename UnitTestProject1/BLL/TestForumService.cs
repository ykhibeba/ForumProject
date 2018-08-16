using System;
using System.Collections.Generic;
using System.Linq;
using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.BLL.Services;
using Forum.DAL.Entities;
using Forum.DAL.Interfaces;
using Forum.Tests.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace Forum.Tests.BLL
{
    [TestClass]
    public class TestForumService
    {
        private UnitOfWorkTest test;
        public UnitOfWorkTest uowt
        {
            get
            {
                if (test == null)
                    test = new UnitOfWorkTest();
                return test;
            }
        }

        [TestMethod]
        public void ForumService_AddNewComment_CommentShouldEquals()
        {
            CommentDTO comment = new CommentDTO { ID = 4, PostID = 1, Body = "Comment3_Post_1" };

            var service = new ForumService(uowt);
            service.AddComment(comment);

            Assert.AreEqual(comment.ID, uowt.Comments.GetById(comment.ID).ID);
            Assert.AreEqual(comment.Body, uowt.Comments.GetById(comment.ID).Body);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ForumService_AddNullComment_ShouldValidationException()
        {
            CommentDTO comment = null;

            var service = new ForumService(uowt);
            service.AddComment(comment);
        }

        [TestMethod]
        public void ForumService_UpdateExistComment_ShouldEqulas()
        {
            CommentDTO comment = new CommentDTO { ID = 1, PostID = 1, Body = "Comment1_Post_1_Update" };

            var service = new ForumService(uowt);
            service.UpdateComment(comment);

            Assert.AreEqual(comment.ID, uowt.Comments.GetById(comment.ID).ID);
            Assert.AreEqual(comment.Body, uowt.Comments.GetById(comment.ID).Body);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ForumService_UpdateNullComment_ShouldValidationException()
        {
            CommentDTO comment = null;

            var service = new ForumService(uowt);
            service.UpdateComment(comment);
        }

        [TestMethod]
        public void ForumService_DeleteComment_ShouldBeNull()
        {
            var service = new ForumService(uowt);

            service.DeleteComment(1);

            Assert.IsNull(uowt.Comments.GetById(1));
        }

        [TestMethod]
        public void ForumService_GetCommentByID_ShouldReturnCorrectComment()
        {
            var service = new ForumService(uowt);

            var result = service.GetCommentById(1);

            Assert.AreEqual(result.Body, uowt.Comments.GetAll().FirstOrDefault(p => p.ID == 1).Body);
            Assert.AreEqual(result.Name, uowt.Comments.GetAll().FirstOrDefault(p => p.ID == 1).Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ForumService_GetCommentByNotExistID_ShouldValidationException()
        {
            var service = new ForumService(uowt);

            var result = service.GetCommentById(10);
        }

        [TestMethod]
        public void ForumService_GetPostByID_ShouldReturnCorrectPost()
        {
            var service = new ForumService(uowt);

            var result = service.GetPostById(1);

            Assert.AreEqual(result.Body, uowt.Posts.GetAll().FirstOrDefault(p => p.ID == 1).Body);
            Assert.AreEqual(result.CreatorName, uowt.Posts.GetAll().FirstOrDefault(p => p.ID == 1).CreatorName);
            Assert.AreEqual(result.Title, uowt.Posts.GetAll().FirstOrDefault(p => p.ID == 1).Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ForumService_GetPostByNotExistID_ShouldValidationException()
        {
            var service = new ForumService(uowt);

            var result = service.GetPostById(10);
        }

        [TestMethod]
        public void ForumServiceGetCategories_InjectCustomCategories_CategoriesShouldEquals()
        {
            //arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Categories.GetAll()).Returns(uowt.Categories.GetAll());

            var service = new ForumService(mock.Object);

            //act
            var result = service.GetCategories().ToList();

            //assert
            Assert.AreEqual(uowt.Categories.GetAll().ToList().Count, result.Count);
            Assert.AreEqual(uowt.Categories.GetAll().ToList()[0].Title, result[0].Title);
            Assert.AreEqual(uowt.Categories.GetAll().ToList()[1].Title, result[1].Title);
            Assert.AreEqual(uowt.Categories.GetAll().ToList()[2].Title, result[2].Title);
            Assert.AreEqual(uowt.Categories.GetAll().ToList()[3].Title, result[3].Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ForumServiceGetCategories_InjectNullCategories_ShouldValidationException()
        {
            //arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            IEnumerable<Category> category = null;
            mock.Setup(m => m.Categories.GetAll()).Returns(category);
            var service = new ForumService(mock.Object);

            //act
            var result = service.GetCategories().ToList();

            //assert
        }

        [TestMethod]
        public void ForumServiceGetPosts_InjectCustomPosts_PostsShouldEquals()
        {
            //arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Posts.GetAll()).Returns(uowt.Posts.GetAll());

            var service = new ForumService(mock.Object);

            //act
            var result = service.GetPosts(1).ToList();

            //assert
            Assert.AreEqual(uowt.Posts.GetAll().Where(p => p.CategoryID == 1).ToList().Count, result.Count);
            Assert.AreEqual(uowt.Posts.GetAll().ToList()[0].Title, result[0].Title);
            Assert.AreEqual(uowt.Posts.GetAll().ToList()[1].Title, result[1].Title);
        }

        [TestMethod]
        public void ForumServiceAddPost_AddNewPost_PostShouldEquals()
        {
            PostDTO post = new PostDTO { ID = 4, CategoryID = 1, Title = "Post3", Body = "Post3_Category1", CreatorName = "Admin" };

            var service = new ForumService(uowt);
            service.AddPost(post);

            Assert.AreEqual(post.Title, uowt.Posts.GetById(post.ID).Title);
            Assert.AreEqual(post.Body, uowt.Posts.GetById(post.ID).Body);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ForumServiceAddPost_AddNullPost_ShouldValidationException()
        {
            PostDTO post = null;

            var service = new ForumService(uowt);
            service.AddPost(post);
        }

        [TestMethod]
        public void ForumServiceUpdatePost_UpdateExistPost_ShouldEqulas()
        {
            PostDTO post = new PostDTO { CategoryID = 1, Title = "Post1", Body = "Post1_Category1_Update", ID = 1, CreatorName = "Admin" };

            var service = new ForumService(uowt);
            service.UpdatePost(post);

            Assert.AreEqual(post.Title, uowt.Posts.GetById(post.ID).Title);
            Assert.AreEqual(post.Body, uowt.Posts.GetById(post.ID).Body);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ForumServiceUpdatePost_UpdateNullPost_ShouldValidationException()
        {
            PostDTO post = null;

            var service = new ForumService(uowt);
            service.UpdatePost(post);
        }

        [TestMethod]
        public void ForumServiceDeletePost_DeletePost_ShouldBeNull()
        {
            var service = new ForumService(uowt);

            service.DeletePost(1);

            Assert.IsNull(uowt.Posts.GetById(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ForumServiceGetPosts_InjectNullPosts_ShouldValidationException()
        {
            //arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            IEnumerable<Post> posts = null;
            mock.Setup(m => m.Posts.GetAll()).Returns(posts);
            var service = new ForumService(mock.Object);

            //act
            var result = service.GetPosts(1).ToList();

            //assert
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ForumServiceGetPosts_InjectPostsWhereHavntCategoryWithId3_ShouldValidationException()
        {
            //arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Posts.GetAll()).Returns(uowt.Posts.GetAll());
            var service = new ForumService(mock.Object);

            //act
            var result = service.GetPosts(3).ToList();

            //assert
        }

        [TestMethod]
        public void ForumServiceGetComments_InjectCustomComments_CommentsShouldEquals()
        {
            //arrange
            var service = new ForumService(uowt);

            //act
            var result = service.GetComments(1).ToList();

            //assert
            Assert.AreEqual(uowt.Comments.GetAll().Where(p => p.PostID == 1).ToList().Count, result.Count);
            Assert.AreEqual(uowt.Comments.GetAll().ToList()[0].Body, result[0].Body);
            Assert.AreEqual(uowt.Comments.GetAll().ToList()[1].Body, result[1].Body);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ForumServiceGetComments_InjectCommentsWhereHavntPostWithId5_ShouldValidationException()
        {
            //arrange
            var service = new ForumService(uowt);

            //act
            var result = service.GetComments(5).ToList();

            //assert
        }
    }
}
