using AutoMapper;
using DataServiceLib;
using DataServiceLib.DBObjects;
using DataServiceLib.IDataService;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectPortfolio2_Group11.Controller;
using ProjectPortfolio2_Group11.Model;
using Xunit;

namespace WebServiceTests
{
    public class BookmarkControllerTest
    {
        private readonly Mock<DataServiceFacade> _dataServiceFacadeMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUrlHelper> _urlMock;

        public BookmarkControllerTest()
        {
             _dataServiceFacadeMock = new Mock<DataServiceFacade>();
             _mapperMock = new Mock<IMapper>();
             _urlMock = new Mock<IUrlHelper>();
        }


        [Fact]
        public void GetBookmartkWithValidIdSouldReturnOk()
        {
            _dataServiceFacadeMock.Setup(x => x.BookmarkingDs.GetBookMark(1, "nm0000034" )).Returns(new BookmarkPerson ());


            _mapperMock.Setup(x => x.Map<BookmarkPersonDto>(It.IsAny<BookmarkPerson>())).Returns(new BookmarkPersonDto());
            
            var ctrl = new BookmarkController(_dataServiceFacadeMock.Object, _mapperMock.Object);
            ctrl.Url = _urlMock.Object;
            
            var response = ctrl.GetBookmarks(1);
            response.Should().BeOfType<OkObjectResult>();
        }


        [Fact]
        public void GetBookmartkWithInvalidIdSouldReturnNotFound()
        {
            var ctrl = new BookmarkController(_dataServiceFacadeMock.Object, null);

            var response = ctrl.GetBookmarks(1);

            response.Should().BeOfType<NotFoundResult>();
        }
    }
} 
