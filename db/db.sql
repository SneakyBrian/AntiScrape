USE [AntiScrape]
GO
/****** Object:  Table [dbo].[ScrapeRequests]    Script Date: 11/11/2012 02:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScrapeRequests](
	[IP] [nvarchar](max) NULL,
	[HostName] [nvarchar](max) NULL,
	[UserAgent] [nvarchar](max) NULL,
	[Referrer] [nvarchar](max) NULL,
	[Params] [text] NULL,
	[Headers] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
