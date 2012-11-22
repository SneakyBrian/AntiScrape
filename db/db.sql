USE [AntiScrape]
GO
/****** Object:  Table [dbo].[ScrapeRequests]    Script Date: 11/22/2012 20:31:18 ******/
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
	[Headers] [text] NULL,
	[Timestamp] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
