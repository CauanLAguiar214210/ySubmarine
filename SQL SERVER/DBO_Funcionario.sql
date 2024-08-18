USE [ySubmarine]
GO

/****** Object:  Table [dbo].[Funcionario]    Script Date: 18/08/2024 13:43:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Funcionario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Salario] [float] NOT NULL,
	[DepartamentoId] [int] NOT NULL,
	[WorkerLevel] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Funcionario]  WITH CHECK ADD FOREIGN KEY([DepartamentoId])
REFERENCES [dbo].[Departamento] ([Id])
GO


