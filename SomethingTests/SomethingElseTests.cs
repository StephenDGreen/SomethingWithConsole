using Microsoft.EntityFrameworkCore;
using Moq;
using Something.Application;
using Something.Domain;
using Something.Persistence;
using SomethingTests.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;
using Domain = Something.Domain.Models;

namespace SomethingTests
{
    [ExcludeFromCodeCoverage]
    public class SomethingElseTests
    {
        private readonly Domain.SomethingElse somethingElse = Domain.SomethingElse.CreateNamedSomethingElse("Fred Bloggs");
        private readonly Domain.Something something = new Domain.Something() { Name = "Alice Bloggs" };

        public SomethingElseTests()
        {
            somethingElse.Somethings.Add(something);
        }

        [Fact]
        public void SomethingElse_HasAnId()
        {
            var name = "Fred Bloggs";
            var something1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            int expected = 0;

            int actual = something1.Id;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SomethingElse_SetsId()
        {
            var name = "Fred Bloggs";
            var something1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            int expected = 1;

            something1.Id = expected;
            int actual = something1.Id;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void SomethingElse_HasAName()
        {
            var expected = "Fred Bloggs";
            var something1 = Domain.SomethingElse.CreateNamedSomethingElse(expected);

            string actual = something1.Name;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void SomethingElseFactory_Create_CreatesSomethingElseWithName()
        {
            SomethingElseFactory factory = new SomethingElseFactory();
            string expected = "Fred Bloggs";

            Domain.SomethingElse actual = factory.Create(expected);

            Assert.IsType<Domain.SomethingElse>(actual);
            Assert.Equal(expected, actual.Name);
        }
        [Fact]
        public void SomethingElse_CreateNamedSomethingElse_ThrowsArgumentExceptionWithoutName()
        {
            string name = null;

            var exception = Assert.Throws<ArgumentException>(() => Domain.SomethingElse.CreateNamedSomethingElse(name));
            Assert.Equal("name", exception.ParamName);
        }
        [Fact]
        public void SomethingElseFactory_Create_ThrowsArgumentExceptionWithoutName()
        {
            SomethingElseFactory factory = new SomethingElseFactory();
            string name = null;

            var exception = Assert.Throws<ArgumentException>(() => factory.Create(name));
            Assert.Equal("name", exception.ParamName);
        }
        [Fact]
        public void DbContextFactory_CreateAppDbContext_SavesSomethingElseToDatabaseAndRetrievesIt()
        {

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_SavesSomethingElseToDatabaseAndRetrievesIt)))
            {
                ctx.SomethingElses.Add(somethingElse);
                ctx.SaveChanges();
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_SavesSomethingElseToDatabaseAndRetrievesIt)))
            {
                var savedSomethingElse = ctx.SomethingElses.Single();
                Assert.Equal(somethingElse.Name, savedSomethingElse.Name);
            };
        }
        [Fact]
        public async void DbContextFactory_CreateAppDbContext_AsyncSavesSomethingElseToDatabaseAndRetrievesIt()
        {

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_AsyncSavesSomethingElseToDatabaseAndRetrievesIt)))
            {
                ctx.SomethingElses.Add(somethingElse);
                await ctx.SaveChangesAsync();
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_AsyncSavesSomethingElseToDatabaseAndRetrievesIt)))
            {
                var savedSomethingElse = ctx.SomethingElses.Single();
                Assert.Equal(somethingElse.Name, savedSomethingElse.Name);
            };
        }
        [Fact]
        public void DbContextFactory_CreateAppDbContext_SavesSomethingElseToDatabaseAndRetrievesItSettingItsId()
        {
            int expected = 1;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_SavesSomethingElseToDatabaseAndRetrievesItSettingItsId)))
            {
                ctx.SomethingElses.Add(somethingElse);
                ctx.SaveChanges();
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_SavesSomethingElseToDatabaseAndRetrievesItSettingItsId)))
            {
                var savedSomethingElse = ctx.SomethingElses.Single();
                Assert.Equal(expected, savedSomethingElse.Id);
            };
        }
        [Fact]
        public async void DbContextFactory_CreateAppDbContext_AsyncSavesSomethingElseToDatabaseAndRetrievesItSettingItsId()
        {
            int expected = 1;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_AsyncSavesSomethingElseToDatabaseAndRetrievesItSettingItsId)))
            {
                ctx.SomethingElses.Add(somethingElse);
                await ctx.SaveChangesAsync();
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_AsyncSavesSomethingElseToDatabaseAndRetrievesItSettingItsId)))
            {
                var savedSomethingElse = ctx.SomethingElses.Single();
                Assert.Equal(expected, savedSomethingElse.Id);
            };
        }

        [Fact]
        public void SomethingElsePersistence__SaveSomethingElse__SavesSomethingElseToDatabase()
        {
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__SaveSomethingElse__SavesSomethingElseToDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                persistence.SaveSomethingElse(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__SaveSomethingElse__SavesSomethingElseToDatabase)))
            {
                var savedSomethingElse = ctx.SomethingElses.Include(s => s.Somethings).Single();
                Assert.Equal(somethingElse.Somethings[0].Name, savedSomethingElse.Somethings[0].Name);
                Assert.Equal(somethingElse.Name, savedSomethingElse.Name);
            };
        }

        [Fact]
        public async void SomethingElsePersistence__SaveSomethingElseAsync__AsyncSavesSomethingElseToDatabase()
        {
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__SaveSomethingElseAsync__AsyncSavesSomethingElseToDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await persistence.SaveSomethingElseAsync(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__SaveSomethingElseAsync__AsyncSavesSomethingElseToDatabase)))
            {
                var savedSomethingElse = ctx.SomethingElses.Include(s => s.Somethings).Single();
                Assert.Equal(somethingElse.Somethings[0].Name, savedSomethingElse.Somethings[0].Name);
                Assert.Equal(somethingElse.Name, savedSomethingElse.Name);
            };
        }

        [Fact]
        public void SomethingElsePersistence__GetSomethingElseList__RetrievesSomethingElseListFromDatabase()
        {
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__GetSomethingElseList__RetrievesSomethingElseListFromDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                persistence.SaveSomethingElse(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__GetSomethingElseList__RetrievesSomethingElseListFromDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                var savedSomethingElse = persistence.GetSomethingElseList();
                Assert.Equal(somethingElse.Name, savedSomethingElse.Single().Name);
            };
        }

        [Fact]
        public async void SomethingElsePersistence__GetSomethingElseListAsync__AsyncRetrievesSomethingElseListFromDatabase()
        {
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__GetSomethingElseListAsync__AsyncRetrievesSomethingElseListFromDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await persistence.SaveSomethingElseAsync(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__GetSomethingElseListAsync__AsyncRetrievesSomethingElseListFromDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                var savedSomethingElse = await persistence.GetSomethingElseListAsync();
                Assert.Equal(somethingElse.Name, savedSomethingElse.Single().Name);
            };
        }

        [Fact]
        public void SomethingElseCreateInteractor_CreateSomethingElse_PersistsSomethingElseWithName()
        {
            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            Mock<ISomethingElseFactory> mockSomethingElseFactory = new Mock<ISomethingElseFactory>();
            mockSomethingElseFactory.Setup(x => x.Create(somethingElse.Name)).Returns(somethingElse);
            Mock<ISomethingElsePersistence> mockPersistence = new Mock<ISomethingElsePersistence>();
            SomethingElseCreateInteractor somethingElseInteractor = new SomethingElseCreateInteractor(mockSomethingFactory.Object, mockSomethingElseFactory.Object, mockPersistence.Object);

            somethingElseInteractor.CreateSomethingElse(somethingElse.Name);

            mockPersistence.Verify(x => x.SaveSomethingElse(somethingElse));
        }

        [Fact]
        public async void SomethingElseCreateInteractor_CreateSomethingElseAsync_AsyncPersistsSomethingElseWithName()
        {
            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            Mock<ISomethingElseFactory> mockSomethingElseFactory = new Mock<ISomethingElseFactory>();
            mockSomethingElseFactory.Setup(x => x.Create(somethingElse.Name)).Returns(somethingElse);
            Mock<ISomethingElsePersistence> mockPersistence = new Mock<ISomethingElsePersistence>();
            SomethingElseCreateInteractor somethingElseInteractor = new SomethingElseCreateInteractor(mockSomethingFactory.Object, mockSomethingElseFactory.Object, mockPersistence.Object);

            await somethingElseInteractor.CreateSomethingElseAsync(somethingElse.Name);

            mockPersistence.Verify(x => x.SaveSomethingElseAsync(somethingElse));
        }

        [Fact]
        public void SomethingElseReadInteractor_GetSomethingElseList_RetrievesSomethingElseListFromPersistence()
        {
            var somethingElseList = new List<Domain.SomethingElse>();
            somethingElseList.Add(somethingElse);
            var mockPersistence = new Mock<ISomethingElsePersistence>();
            mockPersistence.Setup(x => x.GetSomethingElseList()).Returns(somethingElseList);
            SomethingElseReadInteractor interactor = new SomethingElseReadInteractor(mockPersistence.Object);

            List<Domain.SomethingElse> somethingElseList1 = interactor.GetSomethingElseList();

            Assert.Equal(somethingElseList.Count, somethingElseList1.Count);
            Assert.Equal(somethingElseList[somethingElseList.Count - 1].Name, somethingElseList1[somethingElseList1.Count - 1].Name);
        }

        [Fact]
        public async void SomethingElseReadInteractor_GetSomethingElseListAsync_AsyncRetrievesSomethingElseListFromPersistence()
        {
            var somethingElseList = new List<Domain.SomethingElse>();
            somethingElseList.Add(somethingElse);
            var mockPersistence = new Mock<ISomethingElsePersistence>();
            mockPersistence.Setup(x => x.GetSomethingElseListAsync()).ReturnsAsync(somethingElseList);
            SomethingElseReadInteractor interactor = new SomethingElseReadInteractor(mockPersistence.Object);

            List<Domain.SomethingElse> somethingElseList1 = await interactor.GetSomethingElseListAsync();

            Assert.Equal(somethingElseList.Count, somethingElseList1.Count);
            Assert.Equal(somethingElseList[somethingElseList.Count - 1].Name, somethingElseList1[somethingElseList1.Count - 1].Name);
        }
        [Fact]
        public void SomethingElse_HasAListOfSomethings()
        {
            var name = "Fred Bloggs";
            var somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            int expected = 0;

            int actual = somethingElse1.Somethings.Count;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void SomethingElse_AddSomething_AddsSomethingToSomethings()
        {
            var name = "Fred Bloggs";
            var somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            int expected = 1;

            somethingElse1.Somethings.Add(something);
            int actual = somethingElse1.Somethings.Count;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void DbContextFactory_CreateAppDbContext_SavesSomethingElseWithSomethingToDatabaseAndRetrievesIt()
        {
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_SavesSomethingElseWithSomethingToDatabaseAndRetrievesIt)))
            {
                ctx.SomethingElses.Add(somethingElse);
                ctx.SaveChanges();
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_SavesSomethingElseWithSomethingToDatabaseAndRetrievesIt)))
            {
                var savedSomethingElse = ctx.SomethingElses.Include(s => s.Somethings).Single();
                Assert.Equal(somethingElse.Somethings[0].Name, savedSomethingElse.Somethings[0].Name);
            };
        }
        [Fact]
        public async void DbContextFactory_CreateAppDbContext_AsyncSavesSomethingElseWithSomethingToDatabaseAndRetrievesIt()
        {
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_AsyncSavesSomethingElseWithSomethingToDatabaseAndRetrievesIt)))
            {
                ctx.SomethingElses.Add(somethingElse);
                await ctx.SaveChangesAsync();
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_AsyncSavesSomethingElseWithSomethingToDatabaseAndRetrievesIt)))
            {
                var savedSomethingElse = ctx.SomethingElses.Include(s => s.Somethings).Single();
                Assert.Equal(somethingElse.Somethings[0].Name, savedSomethingElse.Somethings[0].Name);
            };
        }

        [Fact]
        public void SomethingElsePersistence__GetSomethingElseList__RetrievesListOfSomethingElseIncludingSomethingListFromDatabase()
        {
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__GetSomethingElseList__RetrievesListOfSomethingElseIncludingSomethingListFromDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                persistence.SaveSomethingElse(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__GetSomethingElseList__RetrievesListOfSomethingElseIncludingSomethingListFromDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                var savedSomethingElses = persistence.GetSomethingElseIncludingSomethingList();
                foreach (var savedSomethingElse in savedSomethingElses)
                {
                    Assert.Equal(somethingElse.Somethings[0].Name, savedSomethingElse.Somethings[0].Name);
                }
            };
        }

        [Fact]
        public async void SomethingElsePersistence__GetSomethingElseListAsync__AsyncRetrievesListOfSomethingElseIncludingSomethingListFromDatabase()
        {
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__GetSomethingElseListAsync__AsyncRetrievesListOfSomethingElseIncludingSomethingListFromDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await persistence.SaveSomethingElseAsync(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__GetSomethingElseListAsync__AsyncRetrievesListOfSomethingElseIncludingSomethingListFromDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                List<Domain.SomethingElse> savedSomethingElses = await persistence.GetSomethingElseIncludingSomethingListAsync();
                foreach (var savedSomethingElse in savedSomethingElses)
                {
                    Assert.Equal(somethingElse.Somethings[0].Name, savedSomethingElse.Somethings[0].Name);
                }
            };
        }

        [Fact]
        public void SomethingElseReadInteractor_GetSomethingElseIncludingSomethingsList_RetrievesSomethingElseIncludingSomethingsListFromPersistence()
        {
            var somethingElseList = new List<Domain.SomethingElse>();
            somethingElseList.Add(somethingElse);
            var mockPersistence = new Mock<ISomethingElsePersistence>();
            mockPersistence.Setup(x => x.GetSomethingElseIncludingSomethingList()).Returns(somethingElseList);
            SomethingElseReadInteractor interactor = new SomethingElseReadInteractor(mockPersistence.Object);

            List<Domain.SomethingElse> somethingElseList1 = interactor.GetSomethingElseIncludingSomethingsList();

            foreach (var savedSomethingElse in somethingElseList1)
            {
                Assert.Equal(somethingElse.Somethings[0].Name, savedSomethingElse.Somethings[0].Name);
            }
        }

        [Fact]
        public async void SomethingElseReadInteractor_GetSomethingElseIncludingSomethingsListAsync_RetrievesSomethingElseIncludingSomethingsListFromPersistence()
        {
            var somethingElseList = new List<Domain.SomethingElse>();
            somethingElseList.Add(somethingElse);
            var mockPersistence = new Mock<ISomethingElsePersistence>();
            mockPersistence.Setup(x => x.GetSomethingElseIncludingSomethingListAsync()).ReturnsAsync(somethingElseList);
            SomethingElseReadInteractor interactor = new SomethingElseReadInteractor(mockPersistence.Object);

            List<Domain.SomethingElse> somethingElseList1 = await interactor.GetSomethingElseIncludingSomethingsListAsync();

            foreach (var savedSomethingElse in somethingElseList1)
            {
                Assert.Equal(somethingElse.Somethings[0].Name, savedSomethingElse.Somethings[0].Name);
            }
        }

        [Fact]
        public void SomethingElseCreateInteractor_CreateSomethingElse_PersistsSomethingElseWithSomethings()
        {
            var name = "Fred Bloggs";
            var somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns(something);
            Mock<ISomethingElseFactory> mockSomethingElseFactory = new Mock<ISomethingElseFactory>();
            mockSomethingElseFactory.Setup(x => x.Create(somethingElse1.Name)).Returns(somethingElse1);
            Mock<ISomethingElsePersistence> mockPersistence = new Mock<ISomethingElsePersistence>();
            SomethingElseCreateInteractor somethingElseInteractor = new SomethingElseCreateInteractor(mockSomethingFactory.Object, mockSomethingElseFactory.Object, mockPersistence.Object);
            string[] othernames = { "Alice Bloggs" };
            somethingElseInteractor.CreateSomethingElse(name, othernames);

            mockPersistence.Verify(x => x.SaveSomethingElse(somethingElse1));
        }


        [Fact]
        public async void SomethingElseCreateInteractor_CreateSomethingElseAsync_AsyncPersistsSomethingElseWithSomethings()
        {
            var name = "Fred Bloggs";
            var somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns(something);
            Mock<ISomethingElseFactory> mockSomethingElseFactory = new Mock<ISomethingElseFactory>();
            mockSomethingElseFactory.Setup(x => x.Create(somethingElse1.Name)).Returns(somethingElse1);
            Mock<ISomethingElsePersistence> mockPersistence = new Mock<ISomethingElsePersistence>();
            SomethingElseCreateInteractor somethingElseInteractor = new SomethingElseCreateInteractor(mockSomethingFactory.Object, mockSomethingElseFactory.Object, mockPersistence.Object);
            string[] othernames = { "Alice Bloggs" };
            await somethingElseInteractor.CreateSomethingElseAsync(name, othernames);

            mockPersistence.Verify(x => x.SaveSomethingElseAsync(somethingElse1));
        }

        [Fact]
        public void SomethingElsePersistence__UpdateSomethingElseByIdAddSomething__RetrievesSomethingElseByIdFromDatabase()
        {
            int id = 1;
            var something1 = new Domain.Something() { Name = "Bob" };
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdAddSomething__RetrievesSomethingElseByIdFromDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                persistence.SaveSomethingElse(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdAddSomething__RetrievesSomethingElseByIdFromDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                var updatedSomethingElse = persistence.UpdateSomethingElseByIdAddSomething(id, something1);
                Assert.Equal(somethingElse.Name, updatedSomethingElse.Name);
                Assert.Equal(somethingElse.Somethings.Count + 1, updatedSomethingElse.Somethings.Count);
            };
        }

        [Fact]
        public async void SomethingElsePersistence__UpdateSomethingElseByIdAddSomethingAsync__AsyncRetrievesSomethingElseByIdFromDatabase()
        {
            int id = 1;
            var something1 = new Domain.Something() { Name = "Bob" };
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdAddSomethingAsync__AsyncRetrievesSomethingElseByIdFromDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await persistence.SaveSomethingElseAsync(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdAddSomethingAsync__AsyncRetrievesSomethingElseByIdFromDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                var updatedSomethingElse = await persistence.UpdateSomethingElseByIdAddSomethingAsync(id, something1);
                Assert.Equal(somethingElse.Name, updatedSomethingElse.Name);
                Assert.Equal(somethingElse.Somethings.Count + 1, updatedSomethingElse.Somethings.Count);
            };
        }

        [Fact]
        public void SomethingElsePersistence__UpdateSomethingElseByIdAddSomething__ThrowsInvalidOperationExceptionGivenIdOfNonexistentSomethingElse()
        {
            int id = 5;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdAddSomething__ThrowsInvalidOperationExceptionGivenIdOfNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                persistence.SaveSomethingElse(somethingElse);
            };

            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns(something);

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdAddSomething__ThrowsInvalidOperationExceptionGivenIdOfNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                Domain.Something something1 = mockSomethingFactory.Object.Create(something.Name);
                Assert.Throws<InvalidOperationException>(() => persistence.UpdateSomethingElseByIdAddSomething(id, something1));
            };
        }

        [Fact]
        public async void SomethingElsePersistence__UpdateSomethingElseByIdAddSomethingAsync__ThrowsInvalidOperationExceptionGivenIdOfNonexistentSomethingElse()
        {
            int id = 5;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdAddSomethingAsync__ThrowsInvalidOperationExceptionGivenIdOfNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await persistence.SaveSomethingElseAsync(somethingElse);
            };

            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns(something);

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdAddSomethingAsync__ThrowsInvalidOperationExceptionGivenIdOfNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                Domain.Something something1 = mockSomethingFactory.Object.Create(something.Name);
                await Assert.ThrowsAsync<InvalidOperationException>(() => persistence.UpdateSomethingElseByIdAddSomethingAsync(id, something1));
            };
        }
        [Fact]
        public void SomethingElsePersistence__UpdateSomethingElseByIdAddSomething__ThrowsInvalidOperationExceptionGivenNonexistentSomething()
        {
            int id = 5;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdAddSomething__ThrowsInvalidOperationExceptionGivenNonexistentSomething)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                persistence.SaveSomethingElse(somethingElse);
            };

            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns((Domain.Something)null);

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdAddSomething__ThrowsInvalidOperationExceptionGivenNonexistentSomething)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                Domain.Something something1 = mockSomethingFactory.Object.Create(something.Name);
                var exception = Assert.Throws<InvalidOperationException>(() => persistence.UpdateSomethingElseByIdAddSomething(id, something1));
            };
        }

        [Fact]
        public async void SomethingElsePersistence__UpdateSomethingElseByIdAddSomethingAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomething()
        {
            int id = 5;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdAddSomethingAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomething)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await persistence.SaveSomethingElseAsync(somethingElse);
            };

            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns((Domain.Something)null);

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdAddSomethingAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomething)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                Domain.Something something1 = mockSomethingFactory.Object.Create(something.Name);
                await Assert.ThrowsAsync<InvalidOperationException>(() => persistence.UpdateSomethingElseByIdAddSomethingAsync(id, something1));
            };
        }
        [Fact]
        public void SomethingElseUpdateInteractor_UpdateSomethingElseAddSomething_PersistsSomethingElseWithSomethings()
        {
            var name = "Fred Bloggs";
            var somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns(something);
            Mock<ISomethingElseFactory> mockSomethingElseFactory = new Mock<ISomethingElseFactory>();
            mockSomethingElseFactory.Setup(x => x.Create(somethingElse1.Name)).Returns(somethingElse1);
            Mock<ISomethingElsePersistence> mockPersistence = new Mock<ISomethingElsePersistence>();
            SomethingElseUpdateInteractor somethingElseInteractor = new SomethingElseUpdateInteractor(mockSomethingFactory.Object, mockSomethingElseFactory.Object, mockPersistence.Object);
            string othername = "Alice Bloggs";
            int id = 1;
            somethingElseInteractor.UpdateSomethingElseAddSomething(id, othername);

            mockPersistence.Verify(x => x.UpdateSomethingElseByIdAddSomething(id, something));
        }
        [Fact]
        public async void SomethingElseUpdateInteractor_UpdateSomethingElseAddSomethingAsync_AsyncPersistsSomethingElseWithSomethings()
        {
            var name = "Fred Bloggs";
            var somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns(something);
            Mock<ISomethingElseFactory> mockSomethingElseFactory = new Mock<ISomethingElseFactory>();
            mockSomethingElseFactory.Setup(x => x.Create(somethingElse1.Name)).Returns(somethingElse1);
            Mock<ISomethingElsePersistence> mockPersistence = new Mock<ISomethingElsePersistence>();
            SomethingElseUpdateInteractor somethingElseInteractor = new SomethingElseUpdateInteractor(mockSomethingFactory.Object, mockSomethingElseFactory.Object, mockPersistence.Object);
            string othername = "Alice Bloggs";
            int id = 1;
            await somethingElseInteractor.UpdateSomethingElseAddSomethingAsync(id, othername);

            mockPersistence.Verify(x => x.UpdateSomethingElseByIdAddSomethingAsync(id, something));
        }
        [Fact]
        public void SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingById__UpdatesSomethingElseByIdFromDatabaseWithSomethingDeleted()
        {
            int id = 1;
            int something_id = 1;
            var something1 = new Domain.Something() { Name = "Bob" };
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingById__UpdatesSomethingElseByIdFromDatabaseWithSomethingDeleted)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                persistence.SaveSomethingElse(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingById__UpdatesSomethingElseByIdFromDatabaseWithSomethingDeleted)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                var updatedSomethingElse = persistence.UpdateSomethingElseByIdDeleteSomethingById(id, something_id);
                Assert.Equal(somethingElse.Name, updatedSomethingElse.Name);
                Assert.Equal(somethingElse.Somethings.Count - 1, updatedSomethingElse.Somethings.Count);
            };
        }
        [Fact]
        public async void SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingByIdAsync__AsyncUpdatesSomethingElseByIdFromDatabaseWithSomethingDeleted()
        {
            int id = 1;
            int something_id = 1;
            var something1 = new Domain.Something() { Name = "Bob" };
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingByIdAsync__AsyncUpdatesSomethingElseByIdFromDatabaseWithSomethingDeleted)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await persistence.SaveSomethingElseAsync(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingByIdAsync__AsyncUpdatesSomethingElseByIdFromDatabaseWithSomethingDeleted)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                var updatedSomethingElse = await persistence.UpdateSomethingElseByIdDeleteSomethingByIdAsync(id, something_id);
                Assert.Equal(somethingElse.Name, updatedSomethingElse.Name);
                Assert.Equal(somethingElse.Somethings.Count - 1, updatedSomethingElse.Somethings.Count);
            };
        }

        [Fact]
        public void SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingById__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse()
        {
            int id = 5;
            int id2 = 1;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingById__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
            };

            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns((Domain.Something)null);

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingById__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                Domain.Something something1 = mockSomethingFactory.Object.Create(something.Name);
                var exception = Assert.Throws<InvalidOperationException>(() => persistence.UpdateSomethingElseByIdDeleteSomethingById(id, id2));
            };
        }

        [Fact]
        public async void SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingByIdAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse()
        {
            int id = 5;
            int id2 = 1;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingByIdAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
            };

            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns((Domain.Something)null);

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingByIdAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                Domain.Something something1 = mockSomethingFactory.Object.Create(something.Name);
                await Assert.ThrowsAsync<InvalidOperationException>(() => persistence.UpdateSomethingElseByIdDeleteSomethingByIdAsync(id, id2));
            };
        }

        [Fact]
        public void SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingById__ThrowsInvalidOperationExceptionGivenNonexistentSomething()
        {
            int id = 1;
            int id2 = 5;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingById__ThrowsInvalidOperationExceptionGivenNonexistentSomething)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                persistence.SaveSomethingElse(somethingElse);
            };

            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns((Domain.Something)null);

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingById__ThrowsInvalidOperationExceptionGivenNonexistentSomething)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                Domain.Something something1 = mockSomethingFactory.Object.Create(something.Name);
                Assert.Throws<InvalidOperationException>(() => persistence.UpdateSomethingElseByIdDeleteSomethingById(id, id2));
            };
        }

        [Fact]
        public async void SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingByIdAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomething()
        {
            int id = 1;
            int id2 = 5;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingByIdAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomething)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await persistence.SaveSomethingElseAsync(somethingElse);
            };

            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns((Domain.Something)null);

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdDeleteSomethingByIdAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomething)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                Domain.Something something1 = mockSomethingFactory.Object.Create(something.Name);
                await Assert.ThrowsAsync<InvalidOperationException>(() => persistence.UpdateSomethingElseByIdDeleteSomethingByIdAsync(id, id2));
            };
        }
        [Fact]
        public void SomethingElseUpdateInteractor_UpdateSomethingElseDeleteSomething_PersistsSomethingElseWithSomethingDeleted()
        {
            var name = "Fred Bloggs";
            var somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns(something);
            Mock<ISomethingElseFactory> mockSomethingElseFactory = new Mock<ISomethingElseFactory>();
            mockSomethingElseFactory.Setup(x => x.Create(somethingElse1.Name)).Returns(somethingElse1);
            Mock<ISomethingElsePersistence> mockPersistence = new Mock<ISomethingElsePersistence>();
            SomethingElseUpdateInteractor somethingElseInteractor = new SomethingElseUpdateInteractor(mockSomethingFactory.Object, mockSomethingElseFactory.Object, mockPersistence.Object);
            int else_id = 1;
            int something_id = 1;
            somethingElseInteractor.UpdateSomethingElseDeleteSomething(else_id, something_id);

            mockPersistence.Verify(x => x.UpdateSomethingElseByIdDeleteSomethingById(else_id, something_id));
        }
        [Fact]
        public async void SomethingElseUpdateInteractor_UpdateSomethingElseDeleteSomethingAsync_AsyncPersistsSomethingElseWithSomethingDeleted()
        {
            var name = "Fred Bloggs";
            var somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns(something);
            Mock<ISomethingElseFactory> mockSomethingElseFactory = new Mock<ISomethingElseFactory>();
            mockSomethingElseFactory.Setup(x => x.Create(somethingElse1.Name)).Returns(somethingElse1);
            Mock<ISomethingElsePersistence> mockPersistence = new Mock<ISomethingElsePersistence>();
            SomethingElseUpdateInteractor somethingElseInteractor = new SomethingElseUpdateInteractor(mockSomethingFactory.Object, mockSomethingElseFactory.Object, mockPersistence.Object);
            int else_id = 1;
            int something_id = 1;
            await somethingElseInteractor.UpdateSomethingElseDeleteSomethingAsync(else_id, something_id);

            mockPersistence.Verify(x => x.UpdateSomethingElseByIdDeleteSomethingByIdAsync(else_id, something_id));
        }

        [Fact]
        public void SomethingElsePersistence__DeleteSomethingElseById__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse()
        {
            int id = 1;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__DeleteSomethingElseById__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__DeleteSomethingElseById__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                Assert.Throws<InvalidOperationException>(() => persistence.DeleteSomethingElseById(id));
            };
        }

        [Fact]
        public async void SomethingElsePersistence__DeleteSomethingElseByIdAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse()
        {
            int id = 1;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__DeleteSomethingElseByIdAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__DeleteSomethingElseByIdAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await Assert.ThrowsAsync<InvalidOperationException>(() => persistence.DeleteSomethingElseByIdAsync(id));
            };
        }

        [Fact]
        public void SomethingElsePersistence__DeleteSomethingElseById__DeleteSomethingElseFromDatabaseById()
        {
            int id = 1;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__DeleteSomethingElseById__DeleteSomethingElseFromDatabaseById)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                persistence.SaveSomethingElse(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__DeleteSomethingElseById__DeleteSomethingElseFromDatabaseById)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                persistence.DeleteSomethingElseById(id);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__DeleteSomethingElseById__DeleteSomethingElseFromDatabaseById)))
            {
                var persistence = new SomethingElsePersistence(ctx);

                var savedSomethingElses = persistence.GetSomethingElseIncludingSomethingList().Where(f => f.Id == id).ToList();

                int expected = 0;
                int actual = savedSomethingElses.Count;
                Assert.Equal(expected, actual);
            };
        }


        [Fact]
        public async void SomethingElsePersistence__DeleteSomethingElseByIdAsync__AsyncDeletesSomethingElseFromDatabaseById()
        {
            int id = 1;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__DeleteSomethingElseByIdAsync__AsyncDeletesSomethingElseFromDatabaseById)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await persistence.SaveSomethingElseAsync(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__DeleteSomethingElseByIdAsync__AsyncDeletesSomethingElseFromDatabaseById)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await persistence.DeleteSomethingElseByIdAsync(id);
            };

            List<Domain.SomethingElse> savedSomethingElses = null;
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__DeleteSomethingElseByIdAsync__AsyncDeletesSomethingElseFromDatabaseById)))
            {
                var persistence = new SomethingElsePersistence(ctx);

                savedSomethingElses = await persistence.GetSomethingElseIncludingSomethingListAsync();
            };

            var savedSomethingElsesSubset = savedSomethingElses.Where(f => f.Id == id).ToList();
            int expected = 0;
            int actual = savedSomethingElsesSubset.Count;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SomethingElseCreateInteractor_DeleteSomethingElse_DeletesSomethingElseFromPersistence()
        {
            var name = "Fred Bloggs";
            var somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            Mock<ISomethingElseFactory> mockSomethingElseFactory = new Mock<ISomethingElseFactory>();
            mockSomethingElseFactory.Setup(x => x.Create(somethingElse1.Name)).Returns(somethingElse1);
            Mock<ISomethingElsePersistence> mockPersistence = new Mock<ISomethingElsePersistence>();
            SomethingElseDeleteInteractor somethingElseInteractor = new SomethingElseDeleteInteractor(mockSomethingFactory.Object, mockSomethingElseFactory.Object, mockPersistence.Object);

            somethingElseInteractor.DeleteSomethingElse(somethingElse1.Id);

            mockPersistence.Verify(x => x.DeleteSomethingElseById(somethingElse1.Id));
        }
        [Fact]
        public async void SomethingElseCreateInteractor_DeleteSomethingElseAsync_AsyncDeletesSomethingElseFromPersistence()
        {
            var name = "Fred Bloggs";
            var somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            Mock<ISomethingElseFactory> mockSomethingElseFactory = new Mock<ISomethingElseFactory>();
            mockSomethingElseFactory.Setup(x => x.Create(somethingElse1.Name)).Returns(somethingElse1);
            Mock<ISomethingElsePersistence> mockPersistence = new Mock<ISomethingElsePersistence>();
            SomethingElseDeleteInteractor somethingElseInteractor = new SomethingElseDeleteInteractor(mockSomethingFactory.Object, mockSomethingElseFactory.Object, mockPersistence.Object);

            await somethingElseInteractor.DeleteSomethingElseAsync(somethingElse1.Id);

            mockPersistence.Verify(x => x.DeleteSomethingElseByIdAsync(somethingElse1.Id));
        }

        [Fact]
        public void SomethingElsePersistence_DeleteSomethingElseById_DeletionCascades()
        {
            int id = 1;
            List<int> childIds;
            var something1 = new Domain.Something() { Name = "Bob" };
            Domain.SomethingElse somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse("Fred Bloggs");
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence_DeleteSomethingElseById_DeletionCascades)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                persistence.SaveSomethingElse(somethingElse1);
                var updatedSomethingElse = persistence.UpdateSomethingElseByIdAddSomething(somethingElse1.Id, something1);
                var somethingElse = ctx.SomethingElses.Include(s => s.Somethings).Where(r => r.Id == id).FirstOrDefault();
                childIds = somethingElse.Somethings.Select(c => c.Id).ToList();
                ctx.Remove(somethingElse);
                ctx.SaveChanges();
            }

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence_DeleteSomethingElseById_DeletionCascades)))
            {
                Assert.Empty(ctx.Somethings.Where(c => childIds.Contains(c.Id)));
            };
        }

        [Fact]
        public async void SomethingElsePersistence_DeleteSomethingElseByIdAsync_DeletionCascades()
        {
            int id = 1;
            List<int> childIds;
            var something1 = new Domain.Something() { Name = "Bob" };
            Domain.SomethingElse somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse("Fred Bloggs");
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence_DeleteSomethingElseByIdAsync_DeletionCascades)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await persistence.SaveSomethingElseAsync(somethingElse1);
                var updatedSomethingElse = persistence.UpdateSomethingElseByIdAddSomethingAsync(somethingElse1.Id, something1);
                var somethingElse = ctx.SomethingElses.Include(s => s.Somethings).Where(r => r.Id == id).FirstOrDefault();
                childIds = somethingElse.Somethings.Select(c => c.Id).ToList();
                ctx.Remove(somethingElse);
                await ctx.SaveChangesAsync();
            }

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence_DeleteSomethingElseByIdAsync_DeletionCascades)))
            {
                Assert.Empty(ctx.Somethings.Where(c => childIds.Contains(c.Id)));
            };
        }
        [Fact]
        public void SomethingElse_HasATag()
        {
            var name = "Fred Bloggs";
            var expected = "TAG";
            var something1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            something1.Tag = expected;

            string actual = something1.Tag;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public async void DbContextFactory_CreateAppDbContext_AsyncSavesSomethingElseWithTagToDatabaseAndRetrievesItSettingItsTag()
        {
            string expected = "TAG";
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_AsyncSavesSomethingElseWithTagToDatabaseAndRetrievesItSettingItsTag)))
            {
                somethingElse.Tag = expected;
                ctx.SomethingElses.Add(somethingElse);
                await ctx.SaveChangesAsync();
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(DbContextFactory_CreateAppDbContext_AsyncSavesSomethingElseWithTagToDatabaseAndRetrievesItSettingItsTag)))
            {
                var savedSomethingElse = ctx.SomethingElses.Single();
                Assert.Equal(expected, savedSomethingElse.Tag);
            };
        }

        [Fact]
        public void SomethingElsePersistence__UpdateSomethingElseByIdChangeTag__ChangesTagOfSomethingElseInDatabase()
        {
            int id = 1;
            string tag = "TAG";
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdChangeTag__ChangesTagOfSomethingElseInDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                persistence.SaveSomethingElse(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdChangeTag__ChangesTagOfSomethingElseInDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                Domain.SomethingElse updatedSomethingElse = persistence.UpdateSomethingElseByIdChangeTag(id, tag);
                Assert.Equal(somethingElse.Name, updatedSomethingElse.Name);
                Assert.Equal(tag, updatedSomethingElse.Tag);
            };
        }


        [Fact]
        public async void SomethingElsePersistence__UpdateSomethingElseByIdChangeTagAsync__ChangesTagOfSomethingElseInDatabase()
        {
            int id = 1;
            string tag = "TAG";
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdChangeTagAsync__ChangesTagOfSomethingElseInDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await persistence.SaveSomethingElseAsync(somethingElse);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdChangeTagAsync__ChangesTagOfSomethingElseInDatabase)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                Domain.SomethingElse updatedSomethingElse = await persistence.UpdateSomethingElseByIdChangeTagAsync(id, tag);
                Assert.Equal(somethingElse.Name, updatedSomethingElse.Name);
                Assert.Equal(tag, updatedSomethingElse.Tag);
            };
        }

        [Fact]
        public async void SomethingElsePersistence__UpdateSomethingElseByIdChangeTagAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse()
        {
            int id = 1;
            string tag = "TAG";
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdChangeTagAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdChangeTagAsync__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                await Assert.ThrowsAsync<InvalidOperationException>(() => persistence.UpdateSomethingElseByIdChangeTagAsync(id, tag));
            };
        }

        [Fact]
        public void SomethingElsePersistence__UpdateSomethingElseByIdChangeTag__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse()
        {
            int id = 1;
            string tag = "TAG";
            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdChangeTag__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
            };

            using (var ctx = new DbContextFactory().CreateAppDbContext(nameof(SomethingElsePersistence__UpdateSomethingElseByIdChangeTag__ThrowsInvalidOperationExceptionGivenNonexistentSomethingElse)))
            {
                var persistence = new SomethingElsePersistence(ctx);
                Assert.Throws<InvalidOperationException>(() => persistence.UpdateSomethingElseByIdChangeTag(id, tag));
            };
        }

        [Fact]
        public async void SomethingElseUpdateInteractor_UpdateSomethingElseChangeTagAsync_AsyncPersistsSomethingElseWithTagChanged()
        {
            var name = "Fred Bloggs";
            string tag = "TAG";
            var somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns(something);
            Mock<ISomethingElseFactory> mockSomethingElseFactory = new Mock<ISomethingElseFactory>();
            mockSomethingElseFactory.Setup(x => x.Create(somethingElse1.Name)).Returns(somethingElse1);
            Mock<ISomethingElsePersistence> mockPersistence = new Mock<ISomethingElsePersistence>();
            SomethingElseUpdateInteractor somethingElseInteractor = new SomethingElseUpdateInteractor(mockSomethingFactory.Object, mockSomethingElseFactory.Object, mockPersistence.Object);
            int else_id = 1;
            await somethingElseInteractor.UpdateSomethingElseChangeTagAsync(else_id, tag);

            mockPersistence.Verify(x => x.UpdateSomethingElseByIdChangeTagAsync(else_id, tag));
        }

        [Fact]
        public void SomethingElseUpdateInteractor_UpdateSomethingElseChangeTag_PersistsSomethingElseWithTagChanged()
        {
            var name = "Fred Bloggs";
            string tag = "TAG";
            var somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns(something);
            Mock<ISomethingElseFactory> mockSomethingElseFactory = new Mock<ISomethingElseFactory>();
            mockSomethingElseFactory.Setup(x => x.Create(somethingElse1.Name)).Returns(somethingElse1);
            Mock<ISomethingElsePersistence> mockPersistence = new Mock<ISomethingElsePersistence>();
            SomethingElseUpdateInteractor somethingElseInteractor = new SomethingElseUpdateInteractor(mockSomethingFactory.Object, mockSomethingElseFactory.Object, mockPersistence.Object);
            int else_id = 1;
            somethingElseInteractor.UpdateSomethingElseChangeTag(else_id, tag);

            mockPersistence.Verify(x => x.UpdateSomethingElseByIdChangeTag(else_id, tag));
        }

        [Fact]
        public void SomethingElseUpdateInteractor_UpdateSomethingElseChangeTag_ThrowsArgumentExceptionIfIllegalCharactersInTag()
        {
            var name = "Fred Bloggs";
            string tag = "TAG:";
            var somethingElse1 = Domain.SomethingElse.CreateNamedSomethingElse(name);
            Mock<ISomethingFactory> mockSomethingFactory = new Mock<ISomethingFactory>();
            mockSomethingFactory.Setup(x => x.Create(something.Name)).Returns(something);
            Mock<ISomethingElseFactory> mockSomethingElseFactory = new Mock<ISomethingElseFactory>();
            mockSomethingElseFactory.Setup(x => x.Create(somethingElse1.Name)).Returns(somethingElse1);
            Mock<ISomethingElsePersistence> mockPersistence = new Mock<ISomethingElsePersistence>();
            SomethingElseUpdateInteractor somethingElseInteractor = new SomethingElseUpdateInteractor(mockSomethingFactory.Object, mockSomethingElseFactory.Object, mockPersistence.Object);
            int else_id = 1;

            Assert.Throws<ArgumentException>(() => somethingElseInteractor.UpdateSomethingElseChangeTag(else_id, tag));
        }
    }
}
