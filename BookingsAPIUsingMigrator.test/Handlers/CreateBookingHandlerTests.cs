using AutoMapper;
using BookingsAPIUsingMigrator.core.Handlers;
using BookingsAPIUsingMigrator.core.Mappers;
using BookingsAPIUsingMigrator.core.Requests;
using BookingsAPIUsingMigrator.dataaccess;
using BookingsAPIUsingMigrator.dataaccess.Entities;
using BookingsAPIUsingMigrator.dataaccess.Repositories.Interfaces;
using BookingsAPIUsingMigrator.shared.Common;
using BookingsAPIUsingMigrator.test.Common;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPIUsingMigrator.test.Handlers
{
    public class CreateBookingHandlerTests
    {
        private List<Booking> _bookingStore;
        private IdGenerator _idGenerator;
        private readonly MapperConfiguration _autoMapperConfiguration;

        public CreateBookingHandlerTests()
        {
            _idGenerator = new IdGenerator(20);

            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BookingDtoToDomainMapper>();
            });

            _bookingStore = StoreFactory.BookingStore;
        }

        [Fact]
        public async Task WhenClientTriggeringBookingCreate_ThenNewBookingAdded_ReturnTheCreatedBooking()
        {
            // Mock
            var bookingRepositoryMock = new Mock<IBookingRepository>();


            // Setup
            bookingRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_bookingStore.Find(x => x.Id == p))).Verifiable();



            bookingRepositoryMock.Setup(m => m.Create(It.IsAny<Booking>()))
                .Returns((Booking p) =>
                {
                    p.Id = _idGenerator.Next();
                    _bookingStore.Add(p);
                    return Task.FromResult(p);

                }).Verifiable();

            bookingRepositoryMock.Setup(m => m.FindByCondition(It.IsAny<Expression<Func<Booking, bool>>>(), true))
                .Returns((Expression<Func<Booking, bool>> expression, bool notrace) =>
                {
                    return Task.FromResult(_bookingStore.Where(expression.Compile()).AsQueryable());

                }).Verifiable();

            var unitOfWorkMock = new Mock<IBookingRepositoryManager>();

            unitOfWorkMock.Setup(m => m.BookingRepository).Returns(bookingRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var createHandler = new CreateBookingHandler(unitOfWorkMock.Object, autoMapper);

            var request = new CreateBookingRequest
            {
                name = "New Booking",
                bookingTime = "10:00"
            };

            var result = await createHandler.Handle(request, CancellationToken.None);

            // Assert
            var verifiedObject = _bookingStore.Find(x => x.Id == result.id);
            Assert.NotNull(verifiedObject);
            verifiedObject.Id.Should().Be(_idGenerator.Last());
            verifiedObject.name.Should().Be(request.name);
            verifiedObject.bookingTime.Should().Be(request.bookingTime);

            bookingRepositoryMock.Verify(m => m.Create(It.IsAny<Booking>()), Times.Once);
        }

        [Fact]
        public async Task WhenClientTriggeringBookingCreate_ThenNameNotFound_ThrowException()
        {
            // Mock
            var bookingRepositoryMock = new Mock<IBookingRepository>();

            // Setup
            bookingRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_bookingStore.Find(x => x.Id == p)));

            bookingRepositoryMock.Setup(m => m.Create(It.IsAny<Booking>()))
                .Returns((Booking p) =>
                {
                    p.Id = _idGenerator.Next();
                    _bookingStore.Add(p);
                    return Task.FromResult(p);

                }).Verifiable();

            bookingRepositoryMock.Setup(m => m.FindByCondition(It.IsAny<Expression<Func<Booking, bool>>>(), true))
                .Returns((Expression<Func<Booking, bool>> expression, bool notrace) =>
                {
                    return Task.FromResult(_bookingStore.Where(expression.Compile()).AsQueryable());

                }).Verifiable();

            var unitOfWorkMock = new Mock<IBookingRepositoryManager>();

            unitOfWorkMock.Setup(m => m.BookingRepository).Returns(bookingRepositoryMock.Object);
            
            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var createHandler = new CreateBookingHandler(unitOfWorkMock.Object, autoMapper);

            var request = new CreateBookingRequest
            {
                name = "",
                bookingTime = "10:00"
            };

            // Sut
            Func<Task> act = () => createHandler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                        .Where(e => e.Message.StartsWith(ConstantStrings.NAME_INVALID));

            bookingRepositoryMock.Verify(m => m.Create(It.IsAny<Booking>()), Times.Never);
        }

        [Fact]
        public async Task WhenClientTriggeringBookingCreate_ThenBOOKINGTIMENOTINBUSSINESSHOURS_ThrowException()
        {
            // Mock
            var bookingRepositoryMock = new Mock<IBookingRepository>();

            // Setup
            bookingRepositoryMock.Setup(m => m.Find(It.IsAny<int>()))
                .Returns((int p) => Task.FromResult(_bookingStore.Find(x => x.Id == p)));

            bookingRepositoryMock.Setup(m => m.Create(It.IsAny<Booking>()))
                .Returns((Booking p) =>
                {
                    p.Id = _idGenerator.Next();
                    _bookingStore.Add(p);
                    return Task.FromResult(p);

                }).Verifiable();

            bookingRepositoryMock.Setup(m => m.FindByCondition(It.IsAny<Expression<Func<Booking, bool>>>(), true))
                .Returns((Expression<Func<Booking, bool>> expression, bool notrace) =>
                {
                    return Task.FromResult(_bookingStore.Where(expression.Compile()).AsQueryable());

                }).Verifiable();

            var unitOfWorkMock = new Mock<IBookingRepositoryManager>();

            unitOfWorkMock.Setup(m => m.BookingRepository).Returns(bookingRepositoryMock.Object);

            var autoMapper = _autoMapperConfiguration.CreateMapper();

            var createHandler = new CreateBookingHandler(unitOfWorkMock.Object, autoMapper);

            var request = new CreateBookingRequest
            {
                name = "New Booking",
                bookingTime = "17:00"
            };

            // Sut
            Func<Task> act = () => createHandler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                        .Where(e => e.Message.StartsWith(ConstantStrings.BOOKING_TIME_NOT_IN_BUSSINESS_HOURS));

            bookingRepositoryMock.Verify(m => m.Create(It.IsAny<Booking>()), Times.Never);
        }
    }
}
