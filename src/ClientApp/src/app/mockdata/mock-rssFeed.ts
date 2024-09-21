import { RssFeed } from "../models/rssFeed";
import { RssFeedItem } from "../models/rssFeedItem";

// Mockdaten für RssFeedItems
const rssFeedItems: RssFeedItem[] = [
  {
    id: 1,
    title: "Tech News Today",
    link: "https://example.com/tech-news-today",
    description: "Latest updates in the world of technology.",
    publishDate: new Date("2024-09-01"),
    imageUrl: "pexels-markusspiske-3970330.jpg",
    rssFeedId: 1,
  },
  {
    id: 2,
    title: "Science Breakthroughs",
    link: "https://example.com/science-breakthroughs",
    description: "Recent discoveries in science and space exploration.",
    publishDate: new Date("2024-09-02"),
    imageUrl: "pexels-markusspiske-3970330.jpg",
    rssFeedId: 1,
  },
  {
    id: 3,
    title: "Daily Finance Updates",
    link: "https://example.com/daily-finance-updates",
    description: "Get your daily dose of financial market trends.",
    publishDate: new Date("2024-09-03"),
    imageUrl: "pexels-markusspiske-3970330.jpg",
    rssFeedId: 1,
  },
  {
    id: 1,
    title: "Tech News Today",
    link: "https://example.com/tech-news-today",
    description: "Latest updates in the world of technology.",
    publishDate: new Date("2024-09-01"),
    imageUrl: "pexels-markusspiske-3970330.jpg",
    rssFeedId: 1,
  },
  {
    id: 2,
    title: "Science Breakthroughs",
    link: "https://example.com/science-breakthroughs",
    description: "Recent discoveries in science and space exploration.",
    publishDate: new Date("2024-09-02"),
    imageUrl: "pexels-markusspiske-3970330.jpg",
    rssFeedId: 1,
  },
  {
    id: 3,
    title: "Daily Finance Updates",
    link: "https://example.com/daily-finance-updates",
    description: "Get your daily dose of financial market trends.",
    publishDate: new Date("2024-09-03"),
    imageUrl: "pexels-markusspiske-3970330.jpg",
    rssFeedId: 1,
  },
  {
    id: 1,
    title: "Tech News Today",
    link: "https://example.com/tech-news-today",
    description: "Latest updates in the world of technology.",
    publishDate: new Date("2024-09-01"),
    imageUrl: "pexels-mdsnmdsnmdsn-1577882.jpg",
    rssFeedId: 2,
  },
  {
    id: 2,
    title: "Science Breakthroughs",
    link: "https://example.com/science-breakthroughs",
    description: "Recent discoveries in science and space exploration.",
    publishDate: new Date("2024-09-02"),
    imageUrl: "pexels-mdsnmdsnmdsn-1577882.jpg",
    rssFeedId: 2,
  },
  {
    id: 3,
    title: "Daily Finance Updates",
    link: "https://example.com/daily-finance-updates",
    description: "Get your daily dose of financial market trends.",
    publishDate: new Date("2024-09-03"),
    imageUrl: "pexels-mdsnmdsnmdsn-1577882.jpg",
    rssFeedId: 2,
  },
  {
    id: 1,
    title: "Tech News Today",
    link: "https://example.com/tech-news-today",
    description: "Latest updates in the world of technology.",
    publishDate: new Date("2024-09-01"),
    imageUrl: "pexels-mdsnmdsnmdsn-1577882.jpg",
    rssFeedId: 2,
  },
  {
    id: 2,
    title: "Science Breakthroughs",
    link: "https://example.com/science-breakthroughs",
    description: "Recent discoveries in science and space exploration.",
    publishDate: new Date("2024-09-02"),
    imageUrl: "pexels-mdsnmdsnmdsn-1577882.jpg",
    rssFeedId: 2,
  },
  {
    id: 3,
    title: "Daily Finance Updates",
    link: "https://example.com/daily-finance-updates",
    description: "Get your daily dose of financial market trends.",
    publishDate: new Date("2024-09-03"),
    imageUrl: "pexels-mdsnmdsnmdsn-1577882.jpg",
    rssFeedId: 2,
  }
];

// Mockdaten für RssFeeds
const rssFeeds: RssFeed[] = [
  {
    id: 1,
    url: "https://example.com/tech-rss",
    channelTitle: "Tech & Science News",
    feedItems: rssFeedItems.filter(item => item.rssFeedId === 1),
  },
  {
    id: 2,
    url: "https://example.com/finance-rss",
    channelTitle: "Finance Daily",
    feedItems: rssFeedItems.filter(item => item.rssFeedId === 2),
  }
];

export { rssFeeds, rssFeedItems };
