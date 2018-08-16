using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Forum.BLL.DTO;
using Forum.BLL.Infrastructure;
using Forum.BLL.Interfaces;
using Forum.DAL.Entities;
using Forum.DAL.Interfaces;
using Forum.WEB.Controllers;
using Forum.WEB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Forum.Tests
{
    [TestClass]
    public class TestForumANonController
    {
        [TestMethod]
        public void ForumAnonControllerGetMethod_GetAllCategories_ShouldReturnAllCategories()
        {
            //arrange
            Mock<IForumService> mockService = new Mock<IForumService>();
            mockService.Setup(m => m.GetCategories()).Returns(new CategoryDTO[]
            {
                new CategoryDTO {ID=1, Title = "First"},
                new CategoryDTO {ID=2, Title = "Second"},
                new CategoryDTO {ID=3, Title = "Third"},
                new CategoryDTO {ID=4, Title = "Fourth"},
            });

            var controller = new ForumAnonController(mockService.Object);

            //act
            var response = controller.Get() as OkNegotiatedContentResult<List<Categories>>;

            //assert
            Assert.AreEqual(4, response.Content.Count);
            Assert.AreEqual("First", response.Content[0].title);
        }

        [TestMethod]
        public void ForumAnonControllerGetMethod_GetAllNullCategories_ShouldGiveNotFoundResult()
        {
            Mock<IForumService> mockService = new Mock<IForumService>();
            mockService.Setup(m => m.GetCategories()).Returns(new CategoryDTO[]
            {
                null,
                null
            });

            var controller = new ForumAnonController(mockService.Object);

            var response = controller.Get();

            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ForumAnonControllerGetPostsMethod_GetAllPosts_ShouldReturnAllPosts()
        {
            Mock<IForumService> mockService = new Mock<IForumService>();
            mockService.Setup(m => m.GetPosts(1)).Returns(new PostDTO[]
            {
                new PostDTO {ID=1, Title = "First"},
                new PostDTO {ID=2, Title = "Second"},
                new PostDTO {ID=3, Title = "Third"},
            });

            var controller = new ForumAnonController(mockService.Object);

            var response = controller.GetPosts(1) as OkNegotiatedContentResult<List<Posts>>;

            Assert.AreEqual(3, response.Content.Count);
            Assert.AreEqual("Second", response.Content[1].title);
        }

        [TestMethod]
        public void ForumAnonControllerGetPostsMethod_GetAllNullPosts_ShouldGiveNotFoundResult()
        {
            Mock<IForumService> mockService = new Mock<IForumService>();
            mockService.Setup(m => m.GetPosts(1)).Returns(new PostDTO[]
            {
                null,
                null
            });

            var controller = new ForumAnonController(mockService.Object);

            var response = controller.GetPosts(1);

            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }


        [TestMethod]
        public void ForumAnonControllerGetPostWithCommentsMethod_GetPostWithComments_ShouldReturnPost()
        {
            Mock<IForumService> mockService = new Mock<IForumService>();
            mockService.Setup(m => m.GetPostById(1)).Returns(new PostDTO { ID = 1, Title = "First" });
            mockService.Setup(m => m.GetComments(1)).Returns(new CommentDTO[]
            {
                new CommentDTO { ID = 2, Body = "Hello1"},
                new CommentDTO {ID = 3, Body = "Hello2"}
            });

            var controller = new ForumAnonController(mockService.Object);

            var response = controller.GetPostWithComments(1, 1) as OkNegotiatedContentResult<WEB.Models.Post>;

            Assert.AreEqual(1, response.Content.id);
            Assert.AreEqual(2, response.Content.comments.ToList().Count);
        }

        [TestMethod]
        public void ForumAnonControllerGetPostWithCommentsMethod_GetNullPost_ShouldGiveNotFoundResult()
        {
            Mock<IForumService> mockService = new Mock<IForumService>();
            mockService.Setup(m => m.GetPostById(1)).Equals(null);

            var controller = new ForumAnonController(mockService.Object);

            var response = controller.GetPostWithComments(1, 1);

            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }
    }
}

