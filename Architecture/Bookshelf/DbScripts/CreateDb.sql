/****** Object:  Table [dbo].[Author]    Script Date: 11/23/2011 23:50:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[id] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_Author_FirstName_LastName] UNIQUE NONCLUSTERED 
(
	[FirstName] ASC,
	[LastName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Author] ([id], [FirstName], [LastName]) VALUES (2, N'Chip', N'Heath')
INSERT [dbo].[Author] ([id], [FirstName], [LastName]) VALUES (4, N'Harrison ', N'Owen')
INSERT [dbo].[Author] ([id], [FirstName], [LastName]) VALUES (6, N'Holly', N'Arrow')
INSERT [dbo].[Author] ([id], [FirstName], [LastName]) VALUES (5, N'Jeff', N'Conklin')
INSERT [dbo].[Author] ([id], [FirstName], [LastName]) VALUES (3, N'Sam', N'Kaner')
INSERT [dbo].[Author] ([id], [FirstName], [LastName]) VALUES (1, N'Stuart', N'Kauffman')
/****** Object:  Table [dbo].[RegisteredUser]    Script Date: 11/23/2011 23:50:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisteredUser](
	[id] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RegisteredUser] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_RegisteredUser_FistName_LastName] UNIQUE NONCLUSTERED 
(
	[FirstName] ASC,
	[LastName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[RegisteredUser] ([id], [FirstName], [LastName]) VALUES (6, N'Felipe', N'Massa')
INSERT [dbo].[RegisteredUser] ([id], [FirstName], [LastName]) VALUES (5, N'Ferdinando', N'Alonso')
INSERT [dbo].[RegisteredUser] ([id], [FirstName], [LastName]) VALUES (4, N'Jenson', N'Button')
INSERT [dbo].[RegisteredUser] ([id], [FirstName], [LastName]) VALUES (3, N'Lewis', N'Hammilton')
INSERT [dbo].[RegisteredUser] ([id], [FirstName], [LastName]) VALUES (2, N'Mark', N'Webber')
INSERT [dbo].[RegisteredUser] ([id], [FirstName], [LastName]) VALUES (7, N'Michael', N'Schumacher')
INSERT [dbo].[RegisteredUser] ([id], [FirstName], [LastName]) VALUES (8, N'Nico', N'Rosberg')
INSERT [dbo].[RegisteredUser] ([id], [FirstName], [LastName]) VALUES (1, N'Sebastian', N'Vettel')
/****** Object:  Table [dbo].[Book]    Script Date: 11/23/2011 23:50:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[ISBN] [decimal](13, 0) NOT NULL,
	[Title] [nvarchar](128) NOT NULL,
	[Author_Id] [int] NOT NULL,
	[Loaned_to_RegisteredUser_Id] [int] NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Book_Author_Id] ON [dbo].[Book] 
(
	[Author_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Book_Loaned_to] ON [dbo].[Book] 
(
	[ISBN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Book_Title] ON [dbo].[Book] 
(
	[Title] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[Book] ([ISBN], [Title], [Author_Id], [Loaned_to_RegisteredUser_Id]) VALUES (CAST(9780195111309 AS Decimal(13, 0)), N'At home in the Universe', 1, 5)
INSERT [dbo].[Book] ([ISBN], [Title], [Author_Id], [Loaned_to_RegisteredUser_Id]) VALUES (CAST(9780385528757 AS Decimal(13, 0)), N'Switch: how to change things when change is hard', 2, 7)
INSERT [dbo].[Book] ([ISBN], [Title], [Author_Id], [Loaned_to_RegisteredUser_Id]) VALUES (CAST(9780470017685 AS Decimal(13, 0)), N'Dialogue Mapping:  Building Shared Understanding of Wicked Problems', 5, NULL)
INSERT [dbo].[Book] ([ISBN], [Title], [Author_Id], [Loaned_to_RegisteredUser_Id]) VALUES (CAST(9780787982669 AS Decimal(13, 0)), N'Facilitator''s Guide to Participatory Decision-Making, 2nd edition', 3, NULL)
INSERT [dbo].[Book] ([ISBN], [Title], [Author_Id], [Loaned_to_RegisteredUser_Id]) VALUES (CAST(9780803972308 AS Decimal(13, 0)), N'Small Groups as Complex Systems', 6, NULL)
INSERT [dbo].[Book] ([ISBN], [Title], [Author_Id], [Loaned_to_RegisteredUser_Id]) VALUES (CAST(9781576754764 AS Decimal(13, 0)), N'Open Space Technology: A User''s Guide, 3rd edition', 4, NULL)
/****** Object:  ForeignKey [FK_Book_Author]    Script Date: 11/23/2011 23:50:10 ******/
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Author] FOREIGN KEY([Author_Id])
REFERENCES [dbo].[Author] ([id])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Author]
GO
/****** Object:  ForeignKey [FK_Book_RegisteredUser]    Script Date: 11/23/2011 23:50:10 ******/
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_RegisteredUser] FOREIGN KEY([Loaned_to_RegisteredUser_Id])
REFERENCES [dbo].[RegisteredUser] ([id])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_RegisteredUser]
GO
