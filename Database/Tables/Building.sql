CREATE TABLE [dbo].[Building] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK__Building__3214EC0751C793FD] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Building]
    ON [dbo].[Building]([Name] ASC);

