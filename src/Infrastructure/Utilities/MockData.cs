using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Utilities
{
    public static class MockData
    {
        public static void GetMockData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RssFeed>()
               .HasData(new RssFeed
                {
                    Id = 1,
                    ChannelTitle = "Test Feed 1",
                    Url = "https://testurl1.com/"
                },
                new RssFeed
                {
                    Id = 2,
                    ChannelTitle = "Test Feed 2",
                    Url = "https://testurl2.com/"
                });

            modelBuilder.Entity<RssFeedItem>()
            .HasData(new RssFeedItem
            {
                Id = 1,
                Title = "Test Feed Item 1",
                Description = "Test Feed Item 1 Description Apple has released a new iPad Pro with a 12.9-inch display, 10th generation processor, and a 5G chip. The iPad Pro is priced at $1299 and comes with a 12MP camera and 4K video recording",
                Link = "https://www.testlink1.com/",
                PublishDate = new System.DateTime(2021, 4, 21, 10, 30, 0),
                ImageUrl = "../../../ClientApp/public/pexels-mdsnmdsnmdsn-1577882.jpg",
                RssFeedId = 1
            },
            new RssFeedItem
            {
                Id = 2,
                Title = "Test Feed Item 2",
                Description = "Test Feed Item 2 Description Apple has released a new iPad Pro with a 12.9-inch display, 10th generation processor, and a 5G chip. The iPad Pro is priced at $1299 and comes with a 12MP camera and 4K video recording.",
                Link = "https://testlink2.com/",
                PublishDate = new System.DateTime(2021, 4, 21, 10, 30, 0),
                ImageUrl = "../../ClientApp/public/pexels-mdsnmdsnmdsn-1577882.jpg",
                RssFeedId = 1
            },
            new RssFeedItem
            {
                Id = 3, 
                Title = "Test Feed Item 3",
                Description = "Test Feed Item 3 Description Apple has released a new iPad Pro with a 12.9-inch display, 10th generation processor, and a 5G chip. The iPad Pro is priced at $1299 and comes with a 12MP camera and 4K video recording.",
                Link = "https://testlink3.com/",
                PublishDate = new System.DateTime(2021, 4, 21, 10, 30, 0),
                ImageUrl = "../../ClientApp/public/pexels-mdsnmdsnmdsn-1577882.jpg",
                RssFeedId = 1
            },
            new RssFeedItem
            {
                Id = 4,
                Title = "Test Feed Item 4",
                Description = "Test Feed Item 4 Description Apple has released a new iPad Pro with a 12.9-inch display, 10th generation processor, and a 5G chip. The iPad Pro is priced at $1299 and comes with a 12MP camera and 4K video recording.",
                Link = "https://testlink4.com/",
                PublishDate = new System.DateTime(2021, 4, 21, 10, 30, 0),
                ImageUrl = "/images/pexels-markusspiske-3970330.jpg",
                RssFeedId = 2     
            },
                        new RssFeedItem
            {
                Id = 5,
                Title = "Test Feed Item 5",
                Description = "Test Feed Item 5 Apple has released a new iPad Pro with a 12.9-inch display, 10th generation processor, and a 5G chip. The iPad Pro is priced at $1299 and comes with a 12MP camera and 4K video recording.",
                Link = "https://testlink5.com/",
                PublishDate = new System.DateTime(2021, 4, 21, 10, 30, 0),
                ImageUrl = "../../ClientApp/public/pexels-markusspiske-3970330.jpg",
                RssFeedId = 2     
            },
                        new RssFeedItem
            {
                Id = 6,
                Title = "Test Feed Item 6",
                Description = "Test Feed Item 6 Apple has released a new iPad Pro with a 12.9-inch display, 10th generation processor, and a 5G chip. The iPad Pro is priced at $1299 and comes with a 12MP camera and 4K video recording.",
                Link = "https://testlink6.com/",
                PublishDate = new System.DateTime(2021, 4, 21, 10, 30, 0),
                ImageUrl = "../../ClientApp/public/pexels-markusspiske-3970330.jpg",
                RssFeedId = 2     
            });
            }
        }
    }
