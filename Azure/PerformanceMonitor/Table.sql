/****** Object:  Table [dbo].[spironelli]    Script Date: 13/12/2022 13:11:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[spironelli](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DeviceName] [nvarchar](50) NOT NULL,
	[AcquisitionDate] [datetime] NULL,
	[CpuUsage] [float] NULL,
	[RamUsage] [float] NULL,
 CONSTRAINT [PK_spironelli] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

