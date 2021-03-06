/*
   12 October 201709:47:58
   User: 
   Server: DESKTOP-O3OQ8T3\SQLEXPRESS
   Database: LotterySyndicate
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Prizes
	(
	ID int NOT NULL IDENTITY (1, 1),
	Value decimal(18, 0) NULL,
	WinningNumber int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Prizes ADD CONSTRAINT
	PK_Prizes PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Prizes SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
